const url = "https://localhost:44364/api/Users";
async function GetUsers() {
  var response = await fetch(url);

  var result = await response.json();

  var container = document.getElementById("AllPatients");

  result.forEach((element) => {
    container.innerHTML += `
    <div class="col-lg-4 col-md-6">
    <section class="box ">
                        <div class="content-body p">
                            <div class="row">
                                <div class="doctors-list patient relative">
                                    <div class="doctors-head relative text-center">
                                        <div class="patient-img img-circle">
                                            <img src="https://localhost:44364/${element.userImage}" class="rad-50 center-block" style = "width:120px;height:120px" alt="">
                                            <div class="stutas recent"></div>
                                        </div>
                                        <h3 class="header w-text relative bold">Name : ${element.username}</h3>
                                        
                                    </div>
                                    <div class="row">
                                        <div class="patients-info relative" >
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="patient-card has-shadow2">
                                                    <div class="doc-info-wrap">
                                                        <div class="patient-info">
                                                            <h5 class="bold">${element.address}</h5>
                                                            <h6>Patient Address</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="patient-card has-shadow2">
                                                    <div class="doc-info-wrap">
                                                        <div class="patient-info">
                                                            <h5 class="bold">${element.phoneNumber}</h5>
                                                            <h6>Phone Number</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div><!-- end row -->
                                    
                                    <div class="col-xs-12 mb-30">
                                        <div class="form-group no-mb">
                                            <a class="btn btn-primary btn-lg gradient-blue" href="hos-patient-dash.html" style="width:100%" onclick="getUserId(${element.userID})")> View Profile</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                    </div>`;
  });
}
GetUsers();

function getUserId(id) {
  localStorage.setItem("UserId", id);
}

//////////////////////////////////////////////////

async function showUserDetail() {
  try {
    const x = localStorage.getItem("UserId");
    const url = `https://localhost:44364/api/Users/ShowUserByID/${x}`;
    const response = await fetch(url);

    if (!response.ok) {
      throw new Error("Network response was not ok");
    }

    const result = await response.json();
    const container = document.getElementById("PatientDetails");

    // Check if container exists
    if (container) {
      container.innerHTML = `
                <section class="box">
                    <div class="content-body p">
                        <div class="row">
                            <div class="doctors-list patient relative">
                                <div class="doctors-head relative text-center">
                                    <div class="patient-img img-circle">
                                        <img src="https://localhost:44364/${result.userImage}" style = "width:120px;height:120px" class="rad-50 center-block" alt="">
                                        <div class="status"></div>
                                    </div>
                                    <h3 class="header w-text relative bold">Name: ${result.username}</h3>
                                </div>
                                <div class="row">
                                    <div class="patients-info relative">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="patient-card has-shadow2">
                                                <div class="doc-info-wrap">
                                                    <div class="patient-info">
                                                        <h6 class="bold">${result.phoneNumber}</h6>
                                                        <h6>Phone Number</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="patient-card has-shadow2">
                                                <div class="doc-info-wrap">
                                                    <div class="patient-info">
                                                        <h6 class="bold">${result.address}</h6>
                                                        <h6>Address</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="patient-card has-shadow2">
                                                <div class="doc-info-wrap">
                                                    <div class="patient-info">
                                                        <h6 class="bold">${result.email}</h6>
                                                        <h6>Email</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group no-mb">
                                    <a href="hos-edit-patient.html" class="btn btn-warning btn-lg gradient-blue" style="width:100%; border-radius: 25px; font-weight: bold;">Edit Profile</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            `;
    } else {
      console.error("Container with ID 'PatientDetails' not found.");
    }
  } catch (error) {
    console.error("Error fetching user details:", error);
  }
}

showUserDetail();

////////////////////////////////////////////////////////////////

document.addEventListener("DOMContentLoaded", async () => {
  const userId = localStorage.getItem("UserId");
  const userForm = document.getElementById("userForm");
  const saveButton = document.getElementById("saveButton");
  const successModal = document.getElementById("successModal");
  const closeModal = document.getElementById("closeModal");
  const modalMessage = document.getElementById("modalMessage");

  // Function to show user details
  async function showUserDetail() {
    try {
      const response = await fetch(
        `https://localhost:44364/api/Users/ShowUserByID/${userId}`
      );
      const result = await response.json();

      document.getElementById("firstName").value = result.firstName || "";
      document.getElementById("midName").value = result.midName || "";
      document.getElementById("lastName").value = result.lastName || "";
      document.getElementById("username").value = result.username || "";
      document.getElementById("email").value = result.email || "";
      document.getElementById("phoneNumber").value = result.phoneNumber || "";
      document.getElementById("address").value = result.address || "";

      // Display the current profile image
      const profileImage = document.getElementById("profileImage");
      profileImage.src = result.userImage
        ? `https://localhost:44364/${result.userImage}`
        : "../data/profile/avatar-5.png";
    } catch (error) {
      console.error("Error fetching user details:", error);
    }
  }

  // Function to update user details
  async function updateUser() {
    const formData = new FormData();
    formData.append("FirstName", document.getElementById("firstName").value);
    formData.append("MidName", document.getElementById("midName").value);
    formData.append("LastName", document.getElementById("lastName").value);
    formData.append("UserName", document.getElementById("username").value);
    formData.append("Email", document.getElementById("email").value);
    formData.append(
      "PhoneNumber",
      document.getElementById("phoneNumber").value
    );
    formData.append("Address", document.getElementById("address").value);

    const userImageFile = document.getElementById("userImage").files[0];
    if (userImageFile) {
      formData.append("UserImage", userImageFile);
    }

    try {
      const response = await fetch(
        `https://localhost:44364/api/Users/UpdateUser/${userId}`,
        {
          method: "PUT",
          body: formData,
        }
      );

      if (response.ok) {
        showModal("User updated successfully!");
      } else {
        const errorData = await response.json();
        console.error("Error updating user:", errorData);
        showModal("Failed to update user.");
      }
    } catch (error) {
      console.error("Error during update:", error);
      showModal("An error occurred. Please try again.");
    }
  }

  // Function to show the modal
  function showModal(message) {
    modalMessage.textContent = message;
    successModal.style.display = "block";
  }

  // Close modal when the user clicks on the close button
  closeModal.addEventListener("click", () => {
    successModal.style.display = "none";
  });

  // Fetch user details on page load
  await showUserDetail();

  // Attach the updateUser function to the save button
  saveButton.addEventListener("click", updateUser);
});
