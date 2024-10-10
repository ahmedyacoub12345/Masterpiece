function changeNav() {
  const token = localStorage.getItem("Token");
  const userId = localStorage.getItem("userId");

  const signInButton = document.getElementById("signInssd");
  const signInButton1 = document.getElementById("signInss");
  const profileMenuItem = document.getElementById("profile");
  const logoutMenuItem = document.getElementById("Logout");
  const logoutMenuItem1 = document.getElementById("Logout1");

  if (userId && token) {
    signInButton.style.display = "none";
    signInButton1.style.display = "none";

    profileMenuItem.style.display = "block";
    logoutMenuItem.style.display = "block";
    logoutMenuItem1.style.display = "block";
  } else {
    signInButton.style.display = "block";
    profileMenuItem.style.display = "none";
    signInButton1.style.display = "none";
    logoutMenuItem.style.display = "none";
    logoutMenuItem1.style.display = "none";
  }
}

function ClearLocalStorage(event) {
  event.preventDefault();
  localStorage.removeItem("Token");
  localStorage.removeItem("userId");
  localStorage.removeItem("messeges");
  localStorage.removeItem("user");
  window.location.href = "SignIn.html";
}

document.addEventListener("DOMContentLoaded", changeNav);
