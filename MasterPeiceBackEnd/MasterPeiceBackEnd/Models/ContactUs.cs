using System.ComponentModel.DataAnnotations;

namespace MasterPeiceBackEnd.Models
{
    public partial class ContactUs
    {
        [Key]
        public int MessageId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }

        public DateTime? SubmittedAt { get; set; }
    }
}
