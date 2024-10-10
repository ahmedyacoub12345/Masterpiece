using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection.Emit;
using MasterPeiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterPieceBackEnd.Model;

public partial class MedicalAppContext : DbContext
{
    public MedicalAppContext()
    {
    }

    public MedicalAppContext(DbContextOptions<MedicalAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminAction> AdminActions { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorAd> DoctorAds { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Specialty> Specialties { get; set; }

    public virtual DbSet<Treatment> Treatments { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public DbSet<Availability> Availabilities { get; set; }
    public DbSet<ContactUs> contactUs { get; set; }
    public virtual DbSet<UserPayment> Payments { get; set; }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KKHSHU9;Database=MedicalApp;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Admins_UserID");

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Admins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AdminAction>(entity =>
        {
            entity.HasIndex(e => e.AdminId, "IX_AdminActions_AdminID");

            entity.Property(e => e.AdminActionId).HasColumnName("AdminActionID");
            entity.Property(e => e.AdminId).HasColumnName("AdminID");

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminActions).HasForeignKey(d => d.AdminId);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasIndex(e => e.DoctorId, "IX_Appointments_DoctorID");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

            //entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments).HasForeignKey(d => d.DoctorId);
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            //entity.HasIndex(e => e.AppointmentId, "IX_Bookings_AppointmentID");

            entity.HasIndex(e => e.UserId, "IX_Bookings_UserID");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            //entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            //entity.HasOne(d => d.Appointment).WithMany(p => p.Bookings).HasForeignKey(d => d.AppointmentId);

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasIndex(e => e.DoctorId, "IX_Comments_DoctorID");

            entity.HasIndex(e => e.UserId, "IX_Comments_UserID");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Comments).HasForeignKey(d => d.DoctorId);

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasIndex(e => e.DoctorId, "IX_Diagnoses_DoctorID");

            entity.HasIndex(e => e.UserId, "IX_Diagnoses_UserID");

            entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Diagnoses).HasForeignKey(d => d.DoctorId);

            entity.HasOne(d => d.User).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasIndex(e => e.SpecialtyId, "IX_Doctors_SpecialtyID");

            entity.HasIndex(e => e.UserId, "IX_Doctors_UserID");

            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Doctors).HasForeignKey(d => d.SpecialtyId);

            entity.HasOne(d => d.User).WithMany(p => p.Doctors).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<DoctorAd>(entity =>
        {
            entity.HasKey(e => e.AdId);

            entity.HasIndex(e => e.DoctorId, "IX_DoctorAds_DoctorID");

            entity.Property(e => e.AdId).HasColumnName("AdID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorAds).HasForeignKey(d => d.DoctorId);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasIndex(e => e.DoctorId, "IX_Reviews_DoctorID");

            entity.HasIndex(e => e.UserId, "IX_Reviews_UserID");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Reviews).HasForeignKey(d => d.DoctorId);

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Treatment>(entity =>
        {
            entity.HasIndex(e => e.DoctorId, "IX_Treatments_DoctorID");

            entity.HasIndex(e => e.UserId, "IX_Treatments_UserID");

            entity.Property(e => e.TreatmentId).HasColumnName("TreatmentID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Treatments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.Treatments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserID).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });
        modelBuilder.Entity<Availability>()
          .HasOne(a => a.Doctor)
          .WithMany(d => d.Availabilities)
          .HasForeignKey(a => a.DoctorId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ContactUs>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__ContactU__C87C037CAC1E4233");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });
        modelBuilder.Entity<UserPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__ED1FC9EA12D776EF");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasColumnName("payment_status");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .HasColumnName("transaction_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__user_i__02084FDA");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}