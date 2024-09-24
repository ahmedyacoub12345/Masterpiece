document.addEventListener("DOMContentLoaded", function () {
  const currentDoctor = {
    id: 1, // Replace with actual doctor ID from your context
    name: "Ahmed Yacoub",
    email: "ahmedyacoub718@gmail.com",
    phone: "00962797182579",
  };
  debugger;
  // Populate doctor information
  document.getElementById("doctorName").value = currentDoctor.name;
  document.getElementById("doctorEmail").value = currentDoctor.email;
  document.getElementById("doctorPhone").value = currentDoctor.phone;

  document
    .getElementById("submitBooking")
    .addEventListener("click", function (event) {
      event.preventDefault();

      const userId = localStorage.getItem("userId"); // Get from local storage or your auth mechanism
      const doctorId = localStorage.getItem("DoctorId");
      const time = document.getElementById("bookingTime").value;
      const bookingDate = document.getElementById("bookingDateTime").value; // Ensure correct date format
      const userEmail = document.getElementById("userEmail").value;
      const userName = document.getElementById("userName").value;
      const userPhone = document.getElementById("userPhone").value;

      const bookingData = new FormData();
      bookingData.append("UserId", userId);
      bookingData.append("DoctorId", doctorId);
      bookingData.append("Time", time);
      bookingData.append("BookingDate", bookingDate);
      bookingData.append("PaymentStatus", "Pending");

      fetch("https://localhost:44364/api/Booking/BookAnAppointment", {
        method: "POST",
        body: bookingData,
      })
        .then((response) => {
          if (!response.ok)
            throw new Error("Booking failed: " + response.statusText);
          return response.json();
        })
        .then((data) => {
          // Display booking confirmation
          document.getElementById(
            "confirmationMessage"
          ).innerText = `Booking confirmed for ${data.userName} with ${data.doctorName} on ${data.bookingDate} at ${data.time}.`;
          document.getElementById("bookingDetails").style.display = "block";
        })
        .catch((error) => {
          console.error("Error:", error);
          alert("Booking failed, please try again.");
        });
    });
});
