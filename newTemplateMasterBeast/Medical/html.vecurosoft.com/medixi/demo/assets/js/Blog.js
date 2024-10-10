const url = "https://localhost:44364/api/Blogs";
async function GetAllBlogs() {
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error("Network response was not ok " + response.statusText);
    }
    const result = await response.json();
    const container = document.getElementById("BlogContainer");
    container.innerHTML = "";

    result.forEach(
      (element) =>
        (container.innerHTML += `  <div class="vs-blog blog-single"  >
              <div class="blog-img">
                <a href="blog-details.html"
                  ><img src="https://localhost:44364/${element.blogImage}" alt="Blog Image" style="width: 30rem; height: fit-content"
                /></a>
              </div>
              <div class="blog-content">
                
                <h2 class="blog-title h3">
                  <a href=""
                    >${element.title}</a
                  >
                </h2>
                <p>
                 ${element.content}
                </p>
                
              </div>
            </div>`)
    );
  } catch (error) {
    console.error("Error fetching doctors:", error);
  }
}
GetAllBlogs();
