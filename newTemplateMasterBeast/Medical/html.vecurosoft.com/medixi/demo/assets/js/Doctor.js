let SpecialtyId = localStorage.getItem("SpecialtyId");
const url = `https://localhost:44364/Api/Products/GetDoctorsBySpecialtyId/${SpecialtyId}`;
async function GetAllDoctors() {
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error("Network response was not ok " + response.statusText);
    }
    const result = await response.json();
    const container = document.getElementById("TeamContainer");

    // Clear the container before adding new content
    container.innerHTML = "";

    result.forEach(
      (element) =>
        (container.innerHTML += `
            <div class="col-md-6 col-xl-4 mb-30 wow fadeInUp" data-wow-delay="0.3s">
                <div class="team-card">
                    <div class="team-head">
                        <img src="https://localhost:44364/${element.doctorImage}" alt="Team Area" class="w-100" style="height: 15rem;width: fit-content; margin-bottom:1.5rem"/>
                        
                    </div>
                    <div class="team-body">
                        <h3 class="h4 mb-0">
                            <a href="team-details.html" class="text-reset" onclick="storeDoctorId(${element.doctorId})">${element.name}</a>
                        </h3>
                        <p class="fs-xs degi text-theme mb-2">${element.specialtyName}</p>
                        <p class="fs-xs">${element.qualifications}</p>
                        <p class="fs-xs">${element.clinicAddress}</p>
                        <p class="fs-xs">${element.description}</p>
                        <div class="">
                            <p class="fs-xs team-info">
                                <i class="fas fa-phone text-theme"></i>
                                <a class="text-reset" href="tel:+592201520156">${element.phone}</a>
                            </p>
                            <p class="fs-xs team-info">
                                <i class="fas fa-envelope text-theme"></i>
                                <a class="text-reset" href="mailto:info.example@mail.com">${element.email}</a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        `)
    );
  } catch (error) {
    console.error("Error fetching doctors:", error);
  }
}
function storeDoctorId(DoctorId) {
  localStorage.setItem("DoctorId", DoctorId);
  window.location.href("team-details.html");
}
// Call the function to fetch doctors
GetAllDoctors();

let DoctorId = localStorage.getItem("DoctorId");
const url1 = `https://localhost:44364/Api/Products/GetDoctorDetails/${DoctorId}`;
async function GetDoctorImage() {
  try {
    const response = await fetch(url1);
    if (!response.ok) {
      throw new Error("Network response was not ok " + response.statusText);
    }
    const result = await response.json();
    const container = document.getElementById("DoctorImage");

    container.innerHTML = "";

    result.forEach(
      (element) =>
        (container.innerHTML += `
            <div class="member-details-img">
                <img
                  src="https://localhost:44364/${element.doctorImage}"
                  alt="Member Image"
                  class="w-100"
                  style="height: 30rem;"
                />
              </div>

        `)
    );
  } catch (error) {
    console.error("Error fetching doctors:", error);
  }
}
GetDoctorImage();

const url2 = `https://localhost:44364/Api/Products/GetDoctorDetails/${DoctorId}`;
async function GetDoctorDetails() {
  try {
    const response = await fetch(url2);
    if (!response.ok) {
      throw new Error("Network response was not ok " + response.statusText);
    }
    const result = await response.json();
    const container = document.getElementById("DoctorDetails");

    container.innerHTML = "";

    result.forEach(
      (element) =>
        (container.innerHTML += `
            <h2 class="mb-0 mt-n2">${element.name}</h2>
                <p class="text-theme fs-xs">${element.degree}</p>
                <p class="fs-md text-title">
                  ${element.description}
                </p>
        `)
    );
  } catch (error) {
    console.error("Error fetching doctors:", error);
  }
}
GetDoctorDetails();

const url3 = `https://localhost:44364/Api/Products/GetDoctorDetails/${DoctorId}`;
async function GetDoctorMoreDetails() {
  try {
    const response = await fetch(url3);
    if (!response.ok) {
      throw new Error("Network response was not ok " + response.statusText);
    }
    const result = await response.json();
    const container = document.getElementById("TableDoctorDetails");

    container.innerHTML = "";

    result.forEach(
      (element) =>
        (container.innerHTML += `
            <tbody>
                  <tr>
                    <th><strong>Speciality</strong></th>
                    <td>${element.specialtyName}</td>
                  </tr>
                  <tr>
                    <th><strong>Degrees</strong></th>
                    <td><span class="text-theme">${element.degree}</span></td>
                  </tr>
                  
                  <tr>
                    <th><strong>Office</strong></th>
                    <td>${element.clinicAddress}</td>
                  </tr>
                  <tr>
                    <th><strong>University</strong></th>
                    <td>${element.university}</td>
                  </tr>
                  <tr>
                    <th><strong>Phone Number</strong></th>
                    <td>${element.phone}</td>
                  </tr>
                </tbody>
        `)
    );
  } catch (error) {
    console.error("Error fetching doctors:", error);
  }
}
GetDoctorMoreDetails();

async function GetDoctorName() {
  try {
    const response = await fetch(url3);
    if (!response.ok) {
      throw new Error("Network response was not ok " + response.statusText);
    }
    const result = await response.json();
    const container = document.getElementById("NameDoctor");

    container.innerHTML = "";

    result.forEach(
      (element) =>
        (container.innerHTML += `
          <h1 class="breadcumb-title">${element.name}</h1>
          
        `)
    );
  } catch (error) {
    console.error("Error fetching doctors:", error);
  }
}
GetDoctorName();
