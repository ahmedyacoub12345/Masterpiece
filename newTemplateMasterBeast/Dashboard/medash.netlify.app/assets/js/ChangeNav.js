function changeNav() {
  const token = localStorage.getItem("Admintoken");
  const adminId = localStorage.getItem("adminId");
  const doctorId = localStorage.getItem("doctorID");
  const token1 = localStorage.getItem("Doctortoken");

  const DoctorLi = document.getElementById("DoctorLi");
  const Appointment = document.getElementById("Appointment");
  const Blog = document.getElementById("Blog");

  if (doctorId && token1) {
    DoctorLi.style.display = "none";
    Appointment.style.display = "block";
    Blog.style.display = "none";
  } else {
    DoctorLi.style.display = "block";
    Appointment.style.display = "none";
    Blog.style.display = "block";
  }
}

function ClearLocalStorage(event) {
  event.preventDefault();
  localStorage.removeItem("Doctortoken");
  localStorage.removeItem("Admintoken");
  localStorage.removeItem("adminId");
  localStorage.removeItem("doctorID");
  localStorage.removeItem("adminName");
  localStorage.removeItem("UserId");
  localStorage.removeItem("doctorName");
  localStorage.removeItem("doctorId");
  window.location.href = "ui-login.html";
}
changeNav();
