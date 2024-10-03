using System.ComponentModel.DataAnnotations;

namespace MasterPeiceBackEnd.DTOs
{
    public class ContactRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
