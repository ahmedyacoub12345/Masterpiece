using MasterPeiceBackEnd.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;

namespace MasterPieceBackEnd.Model;

public partial class User
{
    public int UserID { get; set; }
    public string FirstName { get; set; } = null!;
    public string MidName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte[]? PasswordSalt { get; set; }

    public byte[]? PasswordHash { get; set; }

    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? UserImage { get; set; }
    public string? Role { get; set; } 

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
    public virtual ICollection<UserPayment> Payments { get; set; } = new List<UserPayment>();


}
