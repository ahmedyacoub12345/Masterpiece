namespace MasterPeiceBackEnd.DTOs
{
    public class SpecialtyRequestDTO
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public IFormFile? CategoryImage { get; set; }
    }
}
