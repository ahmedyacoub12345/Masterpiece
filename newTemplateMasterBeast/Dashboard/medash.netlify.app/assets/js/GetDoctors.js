const url = "https://localhost:44364/api/Doctors/GetAllDoctors";
async function GetDoctors() {
  var response = await fetch(url);

  var result = await response.json();

  var container = document.getElementById("AllDoctors");

  result.forEach((element) => {
    container.innerHTML += `<div class="col-lg-4 col-md-6">
                    <section class="box ">
                        <div class="content-body p">
                            <div class="row">
                                <div class="doctors-list patient relative">
                                    <div class="doctors-head relative text-center">
                                        <div class="doctor-card has-shadow">
                                            <div class="doc-info-wrap text-left">
                                                <div class="doctor-img">
                                                    <img src="https://localhost:44364/${element.doctorImage}" alt="" style="height: 80px;width: 80px;">
                                                </div>
                                                <div class="doc-info">
                                                    <h4 class="bold">${element.name}</h4>
                                                    <h5>${element.clinicAddress}</h5>
                                                    <div class="doc-rating">
                                                        <i class="fa fa-star"></i>
                                                        <i class="fa fa-star"></i>
                                                        <i class="fa fa-star"></i>
                                                        <i class="fa fa-star"></i>
                                                        <i class="fa fa-star"></i>
                                                        <span>4.8</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div class="row">
                                        <div class="patients-info relative" >
                                            <div class="col-sm-4 col-xs-12 no-padding-right">
                                                <div class="patient-card has-shadow2">
                                                    <div class="doc-info-wrap">
                                                        <div class="patient-info">
                                                            <h6 class = "bold" style="height: 30px;width: 90px; text-align: center;">${element.qualifications}</h6>
                                                            <h6>Qualifications</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 col-xs-12 pl-7 pr-7">
                                                <div class="patient-card has-shadow2">
                                                    <div class="doc-info-wrap">
                                                        <div class="patient-info">
                                                            <h6 class = "bold" style="height:30px;width: 90px; text-align: center;">${element.university}</h6>
                                                            <h6>University</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 col-xs-12 no-padding-left">
                                                <div class="patient-card has-shadow2">
                                                    <div class="doc-info-wrap">
                                                        <div class="patient-info">
                                                            <h6 class = "bold"style="height: 30px;width: 90px; text-align: center;">${element.phone}</h6>
                                                            <h6>Phone</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div><!-- end row -->
                                    
                                    <div class="col-xs-12 mb-30">
                                        <div class="form-group no-mb">
                                            <a href="hos-doctor-profile.html" class="btn btn-primary btn-lg gradient-blue" style="width:100%" onclick="getDoctorId(${element.doctorId})")> View Profile</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>`;
  });
}
GetDoctors();

function getDoctorId(id) {
  localStorage.setItem("doctorId", id);
}

/////////////////////////////////////////////////////////////////////////

async function showDoctorDetail() {
  const x = localStorage.getItem("doctorId");
  var url = `https://localhost:44364/api/Doctors/GetDoctorById/${x}`;
  var response = await fetch(url);

  var result = await response.json();

  var container = document.getElementById("DoctorDetails");

  container.innerHTML = `
<section class="box">
    <div class="content-body p">
        <div class="row">
            <div class="doctors-list v2 relative">
                <div class="doctors-head relative text-left mb-0">
                    <div class="doc-img img-circle">
                        <img src="https://localhost:44364/${result.doctorImage}" class="img-thumbnail center-block" alt="" style="height:fit-content;width: fit-content;">
                        <div class="stutas"></div>
                    </div>
                    <h3 class="header relative bold">Dr: ${result.name}</h3>
                    <h5 class="boldy">${result.clinicAddress}</h5>
                    <p class="desc relative mb-15">${result.description}.</p>
                    <div class="doc-rating mb-30">
                        <i class="fa fa-star"></i>
                        <i class="fa fa-star"></i>
                        <i class="fa fa-star"></i>
                        <i class="fa fa-star"></i>
                        <i class="fa fa-star"></i>
                        <span>4.8</span>
                    </div>

                    <div class="email-div">
                        <i class="fa fa-envelope f-s-14 mr-10"></i><span>Email</span>
                        <a href="#" class="blue-text">
                            <h5 class="text-info">${result.email}</h5>
                        </a>
                    </div>  

                    <div class="form-group no-mb">
                        <a href="hos-edit-doctor.html" class="btn btn-warning btn-lg gradient-blue" style="width:100%; border-radius: 25px; font-weight: bold;">Edit Profile</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>


                `;
}
showDoctorDetail();

////////////////////////////////////////////////////////////

async function AddNewDoctor() {
  const form = document.getElementById("addDoctorForm");
  const formData = new FormData(form);

  try {
    const response = await fetch(
      "https://localhost:44364/api/Doctors/AddNewDoctor",
      {
        method: "POST",
        body: formData,
      }
    );

    if (!response.ok) {
      throw new Error("Network response was not ok " + response.statusText);
    }

    const data = await response.json();
    console.log("Doctor added:", data);
    alert("Doctor added successfully!");

    // Optionally, reset the form after successful submission
    form.reset();
  } catch (error) {
    console.error("There was a problem with the fetch operation:", error);
    alert("An error occurred while adding the doctor.");
  }
}
AddNewDoctor();

///////////////////////////////////////////////////////////

document.addEventListener("DOMContentLoaded", function () {
  async function doctorInfo() {
    try {
      const response = await fetch(
        `https://localhost:44364/api/Doctors/GetDoctorById/${localStorage.getItem(
          "doctorId"
        )}`
      );

      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      const data = await response.json();
      console.log("Doctor Data:", data);

      // Set form field values with checks
      document.getElementById("name").value = data.name || "";
      document.getElementById("specialty").value = data.specialtyId || "";
      document.getElementById("qualifications").value =
        data.qualifications || "";
      document.getElementById("clinicAddress").value = data.clinicAddress || "";
      document.getElementById("phone").value = data.phone || "";
      document.getElementById("email").value = data.email || "";
      document.getElementById("description").value = data.description || "";
      document.getElementById("degree").value = data.degree || "";
      document.getElementById("university").value = data.university || "";

      const imgElement = document.querySelector(".img-responsive"); // Your image element
      imgElement.src = data.doctorImage
        ? `https://localhost:44364/${data.doctorImage}`
        : "https://via.placeholder.com/150";
    } catch (error) {
      console.error("Error fetching doctor information:", error);
    }
  }

  async function updateDoctor(event) {
    event.preventDefault();

    const id = localStorage.getItem("doctorId");
    const doctor = {
      name: document.getElementById("name").value.trim(),
      specialtyId: parseInt(document.getElementById("specialty").value.trim()), // Ensure it's a number
      qualifications: document.getElementById("qualifications").value.trim(),
      clinicAddress: document.getElementById("clinicAddress").value.trim(),
      phone: document.getElementById("phone").value.trim(),
      email: document.getElementById("email").value.trim(),
      description: document.getElementById("description").value.trim(),
      degree: document.getElementById("degree").value.trim(),
      university: document.getElementById("university").value.trim(),
      doctorImage: document.getElementById("doctorImage").files[0],
      userId: 1, // Adjust if needed
      availability: "Available",
    };

    // Log the doctor object to verify all fields are populated correctly
    console.log("Doctor data being sent:", doctor);

    const formData = new FormData();
    for (const key in doctor) {
      if (doctor[key] !== null && doctor[key] !== "") {
        // Only append if the value is not null or empty
        formData.append(key, doctor[key]);
      }
    }

    try {
      const response = await fetch(
        `https://localhost:44364/api/Doctors/UpdateDoctor/${id}`,
        {
          method: "PUT",
          body: formData,
        }
      );

      const modalMessage = document.getElementById("modalMessage");
      const modal = document.getElementById("successModal");

      if (response.ok) {
        modalMessage.textContent = "Doctor updated successfully!";
      } else {
        const errorData = await response.json();
        console.error("Validation errors:", errorData.errors); // Log specific validation errors
        modalMessage.textContent =
          "Failed to update doctor. " + JSON.stringify(errorData.errors);
      }
      modal.style.display = "block";
    } catch (error) {
      const modalMessage = document.getElementById("modalMessage");
      modalMessage.textContent = "Error updating doctor: " + error.message;
      modal.style.display = "block";
    }
  }

  doctorInfo();

  document
    .getElementById("editDoctorForm")
    .addEventListener("submit", updateDoctor);

  document.getElementById("closeModal").onclick = function () {
    document.getElementById("successModal").style.display = "none";
  };

  window.onclick = function (event) {
    if (event.target == document.getElementById("successModal")) {
      document.getElementById("successModal").style.display = "none";
    }
  };
});
