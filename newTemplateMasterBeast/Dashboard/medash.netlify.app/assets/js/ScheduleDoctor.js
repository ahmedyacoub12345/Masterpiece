$(document).ready(function () {
  let validDoctorId = null;

  const doctorId = localStorage.getItem("doctorID");
  if (doctorId) {
    fetchDoctorData(doctorId);
    fetchAvailability(doctorId);
  }

  $("#add_event_form").on("submit", function (e) {
    e.preventDefault();

    const doctorId = parseInt($('input[name="DoctorId"]').val());
    const date = $('input[name="Date"]').val();
    const startTime = $('input[name="StartTime"]').val();
    const endTime = $('input[name="EndTime"]').val();

    if (!isValidTime(startTime, endTime)) {
      alert("Start time must be before end time.");
      return;
    }
    // Validate doctor ID
    if (doctorId !== validDoctorId) {
      alert("Invalid Doctor ID. Please select a valid doctor.");
      return;
    }

    const formData = {
      DoctorId: doctorId,
      Date: date,
      StartTime: startTime,
      EndTime: endTime,
    };

    addAvailability(formData);
  });

  function addAvailability(formData) {
    const form = document.createElement("form");
    form.action = "https://localhost:44364/api/Availability/AddAvailability";
    form.method = "POST";
    form.style.display = "none";

    for (const key in formData) {
      if (formData.hasOwnProperty(key)) {
        const input = document.createElement("input");
        input.type = "hidden";
        input.name = key;
        input.value = formData[key];
        form.appendChild(input);
      }
    }

    document.body.appendChild(form);
    form.submit();

    alert("Availability added successfully!");
    fetchAvailability(formData.DoctorId);
  }

  function fetchAvailability(doctorId) {
    $.ajax({
      url: `https://localhost:44364/api/Availability/GetAllAvailabilityDateByDoctorId/${doctorId}`,
      type: "GET",
      success: function (data) {
        const scheduleContainer = $("#scheduleContainer");
        scheduleContainer.empty();

        if (data.length > 0) {
          data.forEach((event) => {
            const formattedDate = formatDate(event.date);
            const formattedTime = `${formatTime(
              event.startTime
            )} - ${formatTime(event.endTime)}`;

            const scheduleItem = `
                          <div class="schedule-item" style="margin: 10px; padding: 10px; border: 1px solid #ccc;">
                              <strong>Date:</strong> ${formattedDate}<br>
                              <strong>Hours:</strong> ${formattedTime}<br>
                          </div>
                      `;
            scheduleContainer.append(scheduleItem);
          });
        } else {
          scheduleContainer.append("<p>No available slots.</p>");
        }
      },
      error: function (xhr, status, error) {
        console.error("Error fetching schedule:", xhr.responseText);
        alert("Error fetching schedule: " + xhr.responseText);
      },
    });
  }

  function fetchDoctorData(doctorId) {
    $.ajax({
      url: `https://localhost:44364/api/Doctors/GetDoctorById/${doctorId}`,
      type: "GET",
      success: function (data) {
        if (data) {
          validDoctorId = data.doctorId;
          $("#DoctorId").val(data.doctorId);
          $("#DoctorName").val(data.name);
        } else {
          alert("Doctor not found.");
        }
      },
      error: function (xhr, status, error) {
        console.error("Error fetching doctor data:", xhr.responseText);
        alert("Error fetching doctor data: " + xhr.responseText);
      },
    });
  }

  function formatDate(dateString) {
    const options = {
      weekday: "long",
      year: "numeric",
      month: "long",
      day: "numeric",
    };
    return new Date(dateString).toLocaleDateString("en-US", options);
  }

  function formatTime(timeString) {
    if (!timeString) return "N/A"; // Handle empty time case
    const [hours, minutes] = timeString.split(":");
    const date = new Date();
    date.setHours(hours);
    date.setMinutes(minutes);
    return date.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" });
  }

  function isValidTime(startTime, endTime) {
    const start = new Date(`1970-01-01T${startTime}:00`);
    const end = new Date(`1970-01-01T${endTime}:00`);
    return start < end;
  }
});
