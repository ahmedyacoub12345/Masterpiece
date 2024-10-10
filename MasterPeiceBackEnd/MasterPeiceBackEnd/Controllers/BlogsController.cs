using MasterPeiceBackEnd.DTOs;
using MasterPieceBackEnd.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MasterPeiceBackEnd.Shared.ImageSaver;

namespace MasterPeiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly MedicalAppContext _db;
        public BlogsController(MedicalAppContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var data = _db.Blogs.ToList();
            return Ok(data);
        }
        [HttpPost("AddNewBlog")]
        public IActionResult PostBlogs([FromForm] AddBlogsDTO blog)
        {
            var data = new Blog
            {
                Title = blog.Title,
                Content = blog.Content,
                PublishedAt = blog.PublishedAt,
                BlogImage = SaveImage(blog.BlogImage)
            };
            _db.Blogs.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }
        [HttpPut("UpdateBlog/{id}")]
        public IActionResult editBlogs(int id, [FromForm] AddBlogsDTO blog)
        {
            var data = _db.Blogs.Find(id);
            if (data == null)
                return NotFound();
            if (blog.Title != null)
                data.Title = blog.Title;
            if (blog.Content != null)
                data.Content = blog.Content;
            if (blog.PublishedAt != null)
                data.PublishedAt = blog.PublishedAt;
            if (blog.BlogImage != null)
                data.BlogImage = SaveImage(blog.BlogImage);

            _db.Blogs.Update(data);
            _db.SaveChanges();
            return Ok(data);
        }
        [HttpDelete("DeleteBlog/{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var data = _db.Blogs.Find(id);
            _db.Blogs.Remove(data);
            _db.SaveChanges();
            return Ok(data);
        }
        [HttpGet("GetBlog/{id}")]
        public IActionResult GetBlog(int id) 
        {
            var data = _db.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
            return Ok(data);
        }
    }
}
