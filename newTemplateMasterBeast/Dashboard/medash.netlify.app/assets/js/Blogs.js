const apiUrl = "https://localhost:44364/api/Blogs";

async function fetchBlogs() {
  const response = await fetch(apiUrl);
  const blogs = await response.json();
  const blogList = document.getElementById("blogs");
  blogList.innerHTML = "";

  blogs.forEach((blog) => {
    const col = document.createElement("div");
    col.classList.add("col-md-4", "mb-4");

    col.innerHTML = `
                <div class="card">
                    <img src="https://localhost:44364/${
                      blog.blogImage
                    }" class="card-img-top" alt="${blog.title}">
                    <div class="card-body">
                        <h5 class="card-title">${blog.title}</h5>
                        <p class="card-text">${blog.content}</p>
                        <small class="text-muted">Published at: ${new Date(
                          blog.publishedAt
                        ).toLocaleString()}</small><br>
                        <button class="btn btn-warning mt-2" onclick="editBlog(${
                          blog.blogId
                        })">Edit</button>
                        <button class="btn btn-danger mt-2" onclick="deleteBlog(${
                          blog.blogId
                        })">Delete</button>
                    </div>
                </div>
            `;
    blogList.appendChild(col);
  });
}

document
  .getElementById("addBlogForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault();

    const formData = new FormData();
    formData.append("Title", document.getElementById("blogTitle").value);
    formData.append("Content", document.getElementById("blogContent").value);
    formData.append(
      "PublishedAt",
      new Date(document.getElementById("blogPublishedAt").value).toISOString()
    );
    formData.append("BlogImage", document.getElementById("blogImage").files[0]);

    await fetch(`${apiUrl}/AddNewBlog`, {
      method: "POST",
      body: formData,
    });

    fetchBlogs();
    this.reset();
  });

async function editBlog(blogId) {
  const title = prompt("Enter new title:");
  const content = prompt("Enter new content:");
  const publishedAt = prompt("Enter new published date (YYYY-MM-DDTHH:MM):");
  debugger;
  const formData = new FormData();
  if (title) formData.append("Title", title);
  if (content) formData.append("Content", content);
  if (publishedAt)
    formData.append("PublishedAt", new Date(publishedAt).toISOString());

  await fetch(`${apiUrl}/UpdateBlog/${blogId}`, {
    method: "PUT",
    body: formData,
  });
  window.location.reload();
  fetchBlogs();
}

fetchBlogs();

async function deleteBlog(blogId) {
  debugger;
  const confirmDelete = confirm("Are you sure you want to delete this blog?");

  if (confirmDelete) {
    try {
      const response = await fetch(
        `https://localhost:44364/api/Blogs/DeleteBlog/${blogId}`,
        {
          method: "Delete",
        }
      );

      if (response.ok) {
        alert("Blog deleted successfully.");
        fetchBlogs();
      } else {
        alert("Error deleting blog: " + response.statusText);
      }
    } catch (error) {
      console.error("Error:", error);
      alert("An error occurred while trying to delete the blog.");
    }
  }
}

///////////////////////////////////////////////////////////////////////

// const apiUrl = "https://localhost:44364/api/Blogs";

// // Fetch all blogs and display them
// async function fetchBlogs() {
//   const response = await fetch(apiUrl);
//   const blogs = await response.json();
//   const blogList = document.getElementById("blogs");
//   blogList.innerHTML = "";

//   blogs.forEach((blog) => {
//     const col = document.createElement("div");
//     col.classList.add("col-md-4", "mb-4");

//     col.innerHTML = `
//       <div class="card">
//         <img src="https://localhost:44364/${
//           blog.blogImage
//         }" class="card-img-top" alt="${blog.title}">
//         <div class="card-body">
//           <h5 class="card-title">${blog.title}</h5>
//           <p class="card-text">${blog.content}</p>
//           <small class="text-muted">Published at: ${new Date(
//             blog.publishedAt
//           ).toLocaleString()}</small><br>
//           <button class="btn btn-warning mt-2" onclick="editBlog(${
//             blog.blogId
//           })">Edit</button>
//           <button class="btn btn-danger mt-2" onclick="deleteBlog(${
//             blog.blogId
//           })">Delete</button>
//         </div>
//       </div>
//     `;
//     blogList.appendChild(col);
//   });
// }

// // Add new blog
// document
//   .getElementById("addBlogForm")
//   .addEventListener("submit", async function (event) {
//     event.preventDefault();

//     const formData = new FormData();
//     formData.append("Title", document.getElementById("blogTitle").value);
//     formData.append("Content", document.getElementById("blogContent").value);
//     formData.append(
//       "PublishedAt",
//       new Date(document.getElementById("blogPublishedAt").value).toISOString()
//     );
//     formData.append("BlogImage", document.getElementById("blogImage").files[0]);

//     await fetch(`${apiUrl}/AddNewBlog`, {
//       method: "POST",
//       body: formData,
//     });

//     fetchBlogs();
//     this.reset();
//   });

// // Edit blog
// async function editBlog(blogId) {
//   debugger;
//   const response = await fetch(`${apiUrl}/GetBlog/${blogId}`); // Ensure this endpoint exists
//   const blog = await response.json();

//   document.getElementById("editBlogTitle").value = blog.title;
//   document.getElementById("editBlogContent").value = blog.content;
//   document.getElementById("editBlogPublishedAt").value = new Date(
//     blog.publishedAt
//   )
//     .toISOString()
//     .slice(0, 16);
//   document.getElementById("editBlogImage").value = ""; // Reset file input

//   const modal = new bootstrap.Modal(document.getElementById("editBlogModal"));
//   modal.show();

//   document.getElementById("editBlogForm").onsubmit = async function (event) {
//     event.preventDefault();

//     const formData = new FormData();
//     formData.append("Title", document.getElementById("editBlogTitle").value);
//     formData.append(
//       "Content",
//       document.getElementById("editBlogContent").value
//     );
//     formData.append(
//       "PublishedAt",
//       new Date(
//         document.getElementById("editBlogPublishedAt").value
//       ).toISOString()
//     );

//     const fileInput = document.getElementById("editBlogImage");
//     if (fileInput.files.length > 0) {
//       formData.append("BlogImage", fileInput.files[0]);
//     }

//     await fetch(`${apiUrl}/UpdateBlog/${blogId}`, {
//       method: "PUT",
//       body: formData,
//     });

//     modal.hide(); // Hide the modal
//     fetchBlogs(); // Refresh the list of blogs
//   };
// }

// // Delete blog
// async function deleteBlog(blogId) {
//   debugger;
//   const confirmDelete = confirm("Are you sure you want to delete this blog?");

//   if (confirmDelete) {
//     try {
//       const response = await fetch(`${apiUrl}/DeleteBlog/${blogId}`, {
//         method: "POST", // Adjust if your API uses DELETE method
//       });

//       if (response.ok) {
//         alert("Blog deleted successfully.");
//         fetchBlogs(); // Refresh the list of blogs after deletion
//       } else {
//         alert("Error deleting blog: " + response.statusText);
//       }
//     } catch (error) {
//       console.error("Error:", error);
//       alert("An error occurred while trying to delete the blog.");
//     }
//   }
// }

// // Initial fetch to load blogs
// fetchBlogs();
