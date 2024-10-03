document.addEventListener("DOMContentLoaded", function () {
  async function userInfo() {
    try {
      const response = await fetch(
        `https://localhost:44364/api/Users/ShowUserByID/${localStorage.getItem(
          "userId"
        )}`
      );
      const data = await response.json();
      document.getElementById("username").innerText = data.username;
      document.getElementById("firstname").innerText = data.firstName;
      document.getElementById("lastname").innerText = data.lastName;
      document.getElementById("username").innerText = data.username;
      document.getElementById("email").innerText = data.email;
      document.getElementById("phone").innerText = data.phoneNumber;
      document.getElementById("address").innerText = data.address;
      document.getElementById("Image").src = data.userImage
        ? `https://localhost:44364/${data.userImage}`
        : "https://via.placeholder.com/150";
    } catch (error) {
      console.log("Error fetching user or doctor information:", error);
    }
  }
  userInfo();
});

//////////////////////////////////
document.addEventListener("DOMContentLoaded", function () {
  async function userInfo() {
    try {
      const response = await fetch(
        `https://localhost:44364/api/Users/ShowUserByID/${localStorage.getItem(
          "userId"
        )}`
      );
      const data = await response.json();
      document.getElementById("firstName").value = data.firstName;
      document.getElementById("midName").value = data.midName; // Assuming midName is part of your API response
      document.getElementById("lastName").value = data.lastName;
      document.getElementById("username").innerText = data.username;
      document.getElementById("usernameInput").value = data.username; // Set username input
      document.getElementById("email").value = data.email;
      document.getElementById("phone").value = data.phoneNumber;
      document.getElementById("address").value = data.address;
      document.getElementById("userImage").src = data.userImage
        ? `https://localhost:44364/${data.userImage}`
        : "https://via.placeholder.com/150";
    } catch (error) {
      console.log("Error fetching user information:", error);
    }
  }

  async function updateUser() {
    const id = localStorage.getItem("userId");
    const user = {
      firstName: document.getElementById("firstName").value,
      midName: document.getElementById("midName").value,
      lastName: document.getElementById("lastName").value,
      userName: document.getElementById("usernameInput").value,
      email: document.getElementById("email").value,
      phoneNumber: document.getElementById("phone").value,
      address: document.getElementById("address").value,
      userImage: document.getElementById("imageInput").files[0],
    };

    const formData = new FormData();
    for (const key in user) {
      formData.append(key, user[key]);
    }

    try {
      const response = await fetch(
        `https://localhost:44364/api/Users/UpdateUser/${id}`,
        {
          method: "PUT",
          body: formData,
        }
      );

      const modalMessage = document.getElementById("modalMessage");
      const modal = document.getElementById("successModal");

      if (response.ok) {
        modalMessage.textContent = "User updated successfully!";
      } else {
        modalMessage.textContent = "Failed to update user.";
      }
      modal.style.display = "block"; // Show the modal
    } catch (error) {
      const modalMessage = document.getElementById("modalMessage");
      const modal = document.getElementById("successModal");
      modalMessage.textContent = "Error updating user: " + error;
      modal.style.display = "block"; // Show the modal
    }
  }

  // Call userInfo to fetch user data
  userInfo();

  // Add event listener to the Save Changes button
  document.querySelector(".btn-info").addEventListener("click", updateUser);

  // Close the modal when the user clicks on <span> (x)
  document.getElementById("closeModal").onclick = function () {
    document.getElementById("successModal").style.display = "none";
  };

  // Close the modal when the user clicks anywhere outside of the modal
  window.onclick = function (event) {
    if (event.target == document.getElementById("successModal")) {
      document.getElementById("successModal").style.display = "none";
    }
  };
});
//////////////////////////////
document.addEventListener("DOMContentLoaded", function () {
  const modalMessage = document.getElementById("modalMessage");
  const modal = document.getElementById("successModal");
  /////////////////////////////
  document
    .getElementById("ChangePasswordForm")
    .addEventListener("submit", async function (event) {
      event.preventDefault(); // Prevent the default form submission

      const newPassword = document.getElementById("newPassword").value;
      const confirmPassword = document.getElementById("confirmPassword").value;

      // Basic validation
      if (!newPassword || !confirmPassword) {
        modalMessage.textContent = "Both fields are required.";
        modal.style.display = "block";
        return;
      }

      if (newPassword !== confirmPassword) {
        modalMessage.textContent = "Passwords do not match.";
        modal.style.display = "block";
        return;
      }

      const id = localStorage.getItem("userId");
      const user = {
        password: newPassword,
      };

      try {
        const response = await fetch(
          `https://localhost:44364/api/Users/ChangePassword/${id}`,
          {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(user),
          }
        );

        if (response.ok) {
          modalMessage.textContent = "Password changed successfully!";
          modal.style.display = "block";
          document.getElementById("ChangePasswordForm").reset();
        } else {
          const errorData = await response.json();
          modalMessage.textContent =
            "Failed to change password: " +
            (errorData.message || "Unknown error");
          modal.style.display = "block";
        }
      } catch (error) {
        console.error("Error changing password:", error);
        modalMessage.textContent =
          "An error occurred while changing the password.";
        modal.style.display = "block";
      }
    });

  // Close the modal when the user clicks on <span> (x)
  document.getElementById("closeModal").onclick = function () {
    modal.style.display = "none";
  };

  // Close the modal when the user clicks anywhere outside of the modal
  window.onclick = function (event) {
    if (event.target == modal) {
      modal.style.display = "none";
    }
  };
});
