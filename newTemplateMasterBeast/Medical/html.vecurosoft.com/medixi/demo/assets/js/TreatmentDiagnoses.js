document.addEventListener("DOMContentLoaded", async () => {
  const userId = localStorage.getItem("userId");
  const diagnosesUrl = `https://localhost:44364/api/diagnoses/GetDiagnosesByUserId/${userId}`;
  const treatmentsUrl = `https://localhost:44364/api/Treatments/GetTreatmentsByUserId/${userId}`;

  const diagnosesContainer = document.querySelector(".diagnoses-container");
  const treatmentsContainer = document.querySelector(".treatments-container");

  try {
    const [diagnosesResponse, treatmentsResponse] = await Promise.all([
      fetch(diagnosesUrl),
      fetch(treatmentsUrl),
    ]);

    const diagnoses = await diagnosesResponse.json();
    const treatments = await treatmentsResponse.json();

    // Clear containers
    diagnosesContainer.innerHTML = "";
    treatmentsContainer.innerHTML = "";

    // Display Diagnoses
    if (diagnoses.length === 0) {
      diagnosesContainer.innerHTML = `
                  <div class="alert alert-warning col-12" role="alert">
                      No diagnoses found for this user.
                  </div>
              `;
    } else {
      diagnoses.forEach((diagnosis) => {
        const diagnosisCard = `
                      <div class="card mb-3 col-md-10 m-5">
                          <div class="card-header text-center">
                              <strong>Diagnosis</strong>
                          </div>
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

    // Display Treatments
    if (treatments.length === 0) {
      treatmentsContainer.innerHTML = `
                  <div class="alert alert-warning col-12" role="alert">
                      No treatments found for this user.
                  </div>
              `;
    } else {
      treatments.forEach((treatment) => {
        const treatmentCard = `
                      <div class="card mb-3 col-md-10 m-5">
                          <div class="card-header text-center">
                              <strong>Treatment</strong>
                          </div>
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
                              <p class="card-text"><strong>User:</strong> ${
                                treatment.userName
                              }</p>
                          </div>
                      </div>
                  `;
        treatmentsContainer.innerHTML += treatmentCard;
      });
    }
  } catch (error) {
    console.error("Error fetching data:", error);
    diagnosesContainer.innerHTML = `
              <div class="alert alert-danger col-12" role="alert">
                  Failed to load diagnoses. Please try again later.
              </div>
          `;
    treatmentsContainer.innerHTML = `
              <div class="alert alert-danger col-12" role="alert">
                  Failed to load treatments. Please try again later.
              </div>
          `;
  }
});
