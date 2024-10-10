const url = "https://localhost:44364/api/Specialty/GetAllSepecialties";
async function GetAllCategories() {
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error("Network response was not ok " + response.statusText);
    }
    const result = await response.json();
    const container = document.getElementById("SpecialtyContainer");
    result.forEach((element) => {
      container.innerHTML += `
        <div class="col-xl-4 mb-25">
          <div class="service-box">
            <div class="sr-img">
              <img
                src="https://localhost:44364/${element.categoryImage}"
                alt="Service Image"
                class="w-100"
              />
            </div>
            <div class="sr-content">
              <h3 class="h4">
                <a class="text-reset" href="team.html">${element.name}</a>
              </h3>
              <p class="fs-xs">
                ${element.description}
              </p>
            </div>
            <a href="team.html" class="icon-btn style4" onclick="storeSpecialtyId(${element.specialtyId})">
              <i class="far fa-long-arrow-alt-right"></i>
            </a>
          </div>
        </div>
      `;
    });
  } catch (error) {
    console.error("Error fetching categories:", error);
  }
}
function storeSpecialtyId(SpecialtyId) {
  localStorage.setItem("SpecialtyId", SpecialtyId);
  window.location.href("team.html");
}
GetAllCategories();
