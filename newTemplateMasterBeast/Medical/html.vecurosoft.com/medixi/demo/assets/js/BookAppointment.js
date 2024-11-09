document.addEventListener("DOMContentLoaded", function () {
  async function userInfo() {
    try {
      const response = await fetch(
        `https://localhost:44364/api/Users/ShowUserByID/${localStorage.getItem(
          "userId"
        )}`
      );

      const response1 = await fetch(
        `https://localhost:44364/api/Doctors/GetDoctorById/${localStorage.getItem(
          "DoctorId"
        )}`
      );
      const data1 = await response1.json();

      const data = await response.json();
      document.getElementById("userName").value = data.username;
      document.getElementById("userEmail").value = data.email;
      document.getElementById("userPhone").value = data.phoneNumber;
      document.getElementById("doctorName").value = data1.name;
      const response2 = await fetch(
        `https://localhost:44364/api/Booking/GetTime/${localStorage.getItem(
          "DoctorId"
        )}`
      );
      const data2 = await response2.json();

      const select = document.getElementById("bookingTime");
      const availabilityMessage = document.getElementById(
        "availabilityMessage"
      );

      // Check if there are no available times
      if (data2.length === 0) {
        availabilityMessage.style.display = "block";
        return;
      } else {
        availabilityMessage.style.display = "none";
      }

      // Remove unavailable options
      data2.forEach((element) => {
        const optionToRemove = Array.from(select.options).find(
          (option) => option.value === element
        );
        if (optionToRemove) {
          select.remove(optionToRemove.index);
        }
      });

      // Show/hide the availability message based on remaining options
      if (select.options.length === 1) {
        availabilityMessage.style.display = "block";
      } else {
        availabilityMessage.style.display = "none";
      }
    } catch (error) {
      console.log("Error fetching user or doctor information:", error);
    }
  }
  userInfo();
});

document
  .getElementById("submitBooking")
  .addEventListener("click", async function (event) {
    event.preventDefault();

    const userId = localStorage.getItem("userId");
    const doctorId = localStorage.getItem("DoctorId");
    const time = document.getElementById("bookingTime").value;

    if (!time) {
      alert("Please select a time for your appointment.");
      return;
    }

    const bookingDate = new Date().toISOString();
    const paymentStatus = "Pending";

    const bookingData = new FormData();
    bookingData.append("UserId", userId);
    bookingData.append("DoctorId", doctorId);
    bookingData.append("Time", time);
    bookingData.append("BookingDate", bookingDate);
    bookingData.append("PaymentStatus", paymentStatus);

    localStorage.setItem("AppointmentTime", time);
    localStorage.setItem("BookingDate", bookingDate);
    localStorage.setItem("amountForPay", 25);
    window.location.href = "paypal.html";

    try {
      const response = await fetch(
        "https://localhost:44364/api/Booking/BookAnAppointments",
        {
          method: "POST",
          body: bookingData,
        }
      );

      if (!response.ok) {
        throw new Error("Booking failed: " + response.statusText);
      }

      const data = await response.json();
    } catch (error) {
      console.error("Error:", error);
    }
  });
