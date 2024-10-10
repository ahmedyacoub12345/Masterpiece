namespace MasterPeiceBackEnd.DTOs
{
    public class AddBlogsDTO
    {
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public IFormFile? BlogImage { get; set; }

        public DateTime PublishedAt { get; set; }
    }
}
