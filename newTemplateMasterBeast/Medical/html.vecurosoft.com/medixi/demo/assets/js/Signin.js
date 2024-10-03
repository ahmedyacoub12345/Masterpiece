let loginForm = document.getElementById("LoginForm");
let url = "https://localhost:44364/api/Users/LoginUsers";
loginForm.addEventListener("submit", async (event) => {
  event.preventDefault();
  var formdata = new FormData(loginForm);
  const response = await fetch(url, {
    method: "POST",
    body: formdata,
  });

  const result = await response.json();
  console.log(result);

  if (response.ok) {
    localStorage.setItem("Token", result.token);
    localStorage.setItem("userId", result.user.userID);

    alert("Login Successful");
    window.location.href = "index-3.html";
  } else {
    console.error("Login failed:", result);
    iziToast.error({
      title: "Bad credintial",
      message:
        "please make sure that you have entered a valid email and password",
      position: "topCenter",
      timeout: 3000,
    });
  }
});
