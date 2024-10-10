using MasterPeiceBackEnd.Models;
using System;
using System.Collections.Generic;

namespace MasterPieceBackEnd.Model;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public int? UserId { get; set; } // Make UserId nullable

    public int SpecialtyId { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } = null!;

    public byte[]? PasswordSalt { get; set; }

    public byte[]? PasswordHash { get; set; }

    public string Qualifications { get; set; } = null!;

    public string ClinicAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Availability { get; set; } = null; 
    public string? DoctorImage { get; set; }

    public string? Degree { get; set; }
    public string? University { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual ICollection<DoctorAd> DoctorAds { get; set; } = new List<DoctorAd>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Specialty Specialty { get; set; } = null!;

    public virtual ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();

    public virtual User User { get; set; } = null!;
    public virtual ICollection<Availability> Availabilities { get; set; } = new List<Availability>();
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();


}
