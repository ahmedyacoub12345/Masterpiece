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
      debugger;
      document.getElementById("userName").value = data.username;
      document.getElementById("userEmail").value = data.email;
      document.getElementById("userPhone").value = data.phoneNumber;
      document.getElementById("doctorName").value = data1.name;
      // document.getElementById("doctorEmail").value = data1.email;
      // document.getElementById("doctorPhone").value = data1.phone;

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
        availabilityMessage.style.display = "block"; // Show message
        return; // Exit the function if no times are available
      } else {
        availabilityMessage.style.display = "none"; // Hide message
      }

      // Remove unavailable times from the select options
      data2.forEach((element) => {
        const optionToRemove = Array.from(select.options).find(
          (option) => option.value === element
        );
        if (optionToRemove) {
          select.remove(optionToRemove.index);
        }
      });

      // If all options are removed, show the message
      if (select.options.length === 1) {
        // Only the default option remains
        availabilityMessage.style.display = "block"; // Show message
      } else {
        availabilityMessage.style.display = "none"; // Hide message
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
    event.preventDefault(); // Prevent default form submission

    const userId = localStorage.getItem("userId");
    const doctorId = localStorage.getItem("DoctorId");
    const time = document.getElementById("bookingTime").value;

    // Ensure a time is selected before proceeding
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

    try {
      const response = await fetch(
        "https://localhost:44364/api/Booking/BookAnAppointment",
        {
          method: "POST",
          body: bookingData,
        }
      );

      if (!response.ok) {
        throw new Error("Booking failed: " + response.statusText);
      }

      const data = await response.json();
      alert(
        `Booking confirmed for ${data.userName} with ${data.doctorName} on ${data.bookingDate} at ${data.time}.`
      );
    } catch (error) {
      console.error("Error:", error);
      alert("Failed to make a booking. Please try again.");
    }
  });
