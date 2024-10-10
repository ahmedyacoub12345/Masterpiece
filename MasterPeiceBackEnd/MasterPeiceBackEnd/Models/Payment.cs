﻿using MasterPieceBackEnd.Model;

namespace MasterPeiceBackEnd.Models
{
    public partial class UserPayment
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? TransactionId { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
