async function Register() {
  event.preventDefault();
  let url = "https://localhost:44364/api/Users/RegisterUsers";

  const formData = new FormData(document.getElementById("Register"));

  var password = formData.get("Password");
  var repeatPassword = formData.get("repeatpassword");

  // Validate passwords
  if (password !== repeatPassword) {
    alert(
      "Passwords do not match. Please make sure both passwords are the same."
    );
    return; // Stop the function execution
  }
  const response = await fetch(url, {
    method: "POST",
    body: formData,
  });

  const result = await response.json();

  if (response.ok) {
    alert("Registration Successful");
    window.location.href = "SignIn.html";
  } else {
    console.error("Registration failed:", result);
    alert("Please Enter a Valid Data");
  }
}
