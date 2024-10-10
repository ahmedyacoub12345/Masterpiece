// Fill the Doctor ID and User ID from local storage
document.addEventListener("DOMContentLoaded", () => {
  const doctorId = localStorage.getItem("doctorID");
  const userId = localStorage.getItem("UserId");

  if (doctorId) {
    document.getElementById("doctorId").value = doctorId;
  }

  if (userId) {
    document.getElementById("userId").value = userId;
  }
  if (doctorId) {
    document.getElementById("doctorId1").value = doctorId;
  }

  if (userId) {
    document.getElementById("userId1").value = userId;
  }
});

document
  .getElementById("treatmentForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault();

    const form = event.target;
    const formData = new FormData(form);

    try {
      debugger;
      const response = await fetch(
        "https://localhost:44364/api/Treatments/AddTreatment",
        {
          method: "POST",
          body: formData,
        }
      );

      if (!response.ok) {
        const errorData = await response.json();
        console.error("Error details:", errorData);
        throw new Error("Network response was not ok");
      }

      const result = await response.json();
      alert("Treatment added successfully!");
      form.reset(); // Reset the form
    } catch (error) {
      console.error("Error adding treatment:", error);
      alert("Failed to add treatment.");
    }
  });
/////////////////////////////////////////////////////////////////////
document.addEventListener("DOMContentLoaded", async () => {
  const userId = localStorage.getItem("UserId");
  const treatmentsUrl = `https://localhost:44364/api/Treatments/GetTreatmentsByUserId/${userId}`;
  const treatmentsContainer = document.querySelector(".treatments-container");

  try {
    const treatmentsResponse = await fetch(treatmentsUrl);
    const treatments = await treatmentsResponse.json();

    treatmentsContainer.innerHTML = "";

    if (treatments.length === 0) {
      treatmentsContainer.innerHTML = `
          <div class="alert alert-warning" role="alert">
            No treatments found for this user.
          </div>
        `;
    } else {
      treatments.forEach((treatment) => {
        const treatmentCard = `
            <div class="card mb-3">
              <div class="card-header text-center">
                <strong>Treatment</strong> 
              </div><br>
              <div class="card-body">
                <p class="card-title"><strong>Description:</strong> ${
                  treatment.description
                }</p>
                <p class="card-text"><strong>Date:</strong> ${new Date(
                  treatment.date
                ).toLocaleDateString()}</p>
                <p class="card-text"><strong>Doctor:</strong> ${
                  treatment.doctorName
                }</p>
                <p class="card-text"><strong>User:</strong> 
                  ${treatment.userName}</p>
              </div>
            </div>
          `;
        treatmentsContainer.innerHTML += treatmentCard;
      });
    }
  } catch (error) {
    console.error("Error fetching treatments:", error);
    treatmentsContainer.innerHTML = `
        <div class="alert alert-danger" role="alert">
          Failed to load treatments. Please try again later.
        </div>
      `;
  }
});
