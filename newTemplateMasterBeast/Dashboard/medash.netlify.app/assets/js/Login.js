async function handleLogin(event) {
  event.preventDefault();
  // Get form values
  const email = document.querySelector('input[name="formfield2"]').value;
  const password = document.querySelector('input[name="formfield1"]').value;

  // Prepare payload
  const payload = {
    Email: email,
    Password: password,
  };

  // Attempt admin login first
  try {
    const adminResponse = await fetch(
      "https://localhost:44364/api/Admin/LoginAdmin",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
        body: new URLSearchParams(payload),
      }
    );

    if (adminResponse.ok) {
      const adminData = await adminResponse.json();
      const adminInfo = adminData.usersWithAdmins[0]; // Access the first object in the array

      // Save the admin details to local storage
      localStorage.setItem("adminId", adminInfo.adminId);
      localStorage.setItem("adminName", adminInfo.username);
      localStorage.setItem("Admintoken", adminData.token);

      window.location.href = "index-hos-dashboard.html"; // Redirect to admin dashboard
      return;
    }
  } catch (error) {
    console.error("Admin login error:", error);
  }

  // If admin login fails, attempt doctor login
  //   try {
  const doctorResponse = await fetch(
    "https://localhost:44364/api/Doctors/LoginDoctors",
    {
      method: "POST",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
      body: new URLSearchParams(payload),
    }
  );

  if (!doctorResponse.ok) {
    throw new Error("Doctor login failed");
  }
  const doctorData = await doctorResponse.json();
  localStorage.setItem("doctorID", doctorData.doctor.doctorId);
  localStorage.setItem("doctorName", doctorData.doctor.name);
  localStorage.setItem("Doctortoken", doctorData.token);
  window.location.href = "index-hos-dashboard.html"; // Redirect to doctor dashboard
  //   } catch (error) {
  //     console.error("Doctor login error:", error);
  //     alert("Login failed. Please check your credentials and try again.");
  //   }
}

// Attach the function to the form's submit event
const form = document.getElementById("msg_validate");
form.addEventListener("submit", handleLogin);

// handleLogin();
