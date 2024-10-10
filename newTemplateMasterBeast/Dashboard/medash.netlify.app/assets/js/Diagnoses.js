document
  .getElementById("diagnosisForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault();

    const form = event.target;
    const formData = new FormData(form);

    try {
      debugger;
      const response = await fetch(
        "https://localhost:44364/api/diagnoses/AddDiagnoses",
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
      alert("Diagnosis added successfully!");
      form.reset();
    } catch (error) {
      console.error("Error adding diagnosis:", error);
      alert("Failed to add diagnosis.");
    }
  });
///////////////////////////////////////////////////////////////////////

document.addEventListener("DOMContentLoaded", async () => {
  const userId = localStorage.getItem("UserId");
  const diagnosesUrl = `https://localhost:44364/api/diagnoses/GetDiagnosesByUserId/${userId}`;
  const diagnosesContainer = document.querySelector(".diagnoses-container");

  try {
    const diagnosesResponse = await fetch(diagnosesUrl);
    const diagnoses = await diagnosesResponse.json();

    diagnosesContainer.innerHTML = "";

    if (diagnoses.length === 0) {
      diagnosesContainer.innerHTML = `
            <div class="alert alert-warning" role="alert">
              No diagnoses found for this user.
            </div>
          `;
    } else {
      diagnoses.forEach((diagnosis) => {
        const diagnosisCard = `
              <div class="card mb-3">
                <div class="card-header text-center">
                  <strong>Diagnosis</strong> 
                </div><br>
                <div class="card-body">
                  <p class="card-title"><strong>Diagnosis:</strong> ${
                    diagnosis.diagnosises
                  }</p>
                  <p class="card-text"><strong>Date:</strong> ${new Date(
                    diagnosis.date
                  ).toLocaleDateString()}</p>
                  <p class="card-text"><strong>Doctor:</strong> ${
                    diagnosis.doctorName
                  }</p>
                  <p class="card-text"><strong>User:</strong> ${
                    diagnosis.userName
                  }</p>
                </div>
              </div>
            `;
        diagnosesContainer.innerHTML += diagnosisCard;
      });
    }
  } catch (error) {
    console.error("Error fetching diagnoses:", error);
    diagnosesContainer.innerHTML = `
          <div class="alert alert-danger" role="alert">
            Failed to load diagnoses. Please try again later.
          </div>
        `;
  }
});
