﻿// <auto-generated />
using System;
using MasterPieceBackEnd.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MasterPeiceBackEnd.Migrations
{
    [DbContext(typeof(MedicalAppContext))]
    [Migration("20241007203158_EditBlogTable")]
    partial class EditBlogTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MasterPeiceBackEnd.Models.Availability", b =>
                {
                    b.Property<int>("AvailabilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AvailabilityId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("AvailabilityId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("MasterPeiceBackEnd.Models.ContactUs", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MessageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("SubmittedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("MessageId")
                        .HasName("PK__ContactU__C87C037CAC1E4233");

                    b.ToTable("contactUs");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AdminID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"));

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("AdminId");

                    b.HasIndex(new[] { "UserId" }, "IX_Admins_UserID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.AdminAction", b =>
                {
                    b.Property<int>("AdminActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AdminActionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminActionId"));

                    b.Property<string>("ActionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AdminId")
                        .HasColumnType("int")
                        .HasColumnName("AdminID");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("AdminActionId");

                    b.HasIndex(new[] { "AdminId" }, "IX_AdminActions_AdminID");

                    b.ToTable("AdminActions");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AppointmentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("DoctorID");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time");

                    b.HasKey("AppointmentId");

                    b.HasIndex(new[] { "DoctorId" }, "IX_Appointments_DoctorID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BlogID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogId"));

                    b.Property<string>("BlogImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("BlogId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BookingID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("BookingId");

                    b.HasIndex("DoctorId");

                    b.HasIndex(new[] { "UserId" }, "IX_Bookings_UserID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CommentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("DoctorID");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("CommentId");

                    b.HasIndex(new[] { "DoctorId" }, "IX_Comments_DoctorID");

                    b.HasIndex(new[] { "UserId" }, "IX_Comments_UserID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Diagnosis", b =>
                {
                    b.Property<int>("DiagnosisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DiagnosisID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiagnosisId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Diagnosises")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("DoctorID");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("DiagnosisId");

                    b.HasIndex(new[] { "DoctorId" }, "IX_Diagnoses_DoctorID");

                    b.HasIndex(new[] { "UserId" }, "IX_Diagnoses_UserID");

                    b.ToTable("Diagnoses");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DoctorID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"));

                    b.Property<string>("Availability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClinicAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Qualifications")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecialtyId")
                        .HasColumnType("int")
                        .HasColumnName("SpecialtyID");

                    b.Property<string>("University")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("DoctorId");

                    b.HasIndex(new[] { "SpecialtyId" }, "IX_Doctors_SpecialtyID");

                    b.HasIndex(new[] { "UserId" }, "IX_Doctors_UserID");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.DoctorAd", b =>
                {
                    b.Property<int>("AdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AdID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("DoctorID");

                    b.Property<DateTime>("PublishedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("AdId");

                    b.HasIndex(new[] { "DoctorId" }, "IX_DoctorAds_DoctorID");

                    b.ToTable("DoctorAds");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReviewID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("DoctorID");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Reviews")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("ReviewId");

                    b.HasIndex(new[] { "DoctorId" }, "IX_Reviews_DoctorID");

                    b.HasIndex(new[] { "UserId" }, "IX_Reviews_UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Specialty", b =>
                {
                    b.Property<int>("SpecialtyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SpecialtyID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpecialtyId"));

                    b.Property<string>("CategoryImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SpecialtyId");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Treatment", b =>
                {
                    b.Property<int>("TreatmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TreatmentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TreatmentId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("DoctorID");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("TreatmentId");

                    b.HasIndex(new[] { "DoctorId" }, "IX_Treatments_DoctorID");

                    b.HasIndex(new[] { "UserId" }, "IX_Treatments_UserID");

                    b.ToTable("Treatments");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MidName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MasterPeiceBackEnd.Models.Availability", b =>
                {
                    b.HasOne("MasterPieceBackEnd.Model.Doctor", "Doctor")
                        .WithMany("Availabilities")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Admin", b =>
                {
                    b.HasOne("MasterPieceBackEnd.Model.User", "User")
                        .WithMany("Admins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.AdminAction", b =>
                {
                    b.HasOne("MasterPieceBackEnd.Model.Admin", "Admin")
                        .WithMany("AdminActions")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Booking", b =>
                {
                    b.HasOne("MasterPieceBackEnd.Model.Doctor", "Doctor")
                        .WithMany("Bookings")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MasterPieceBackEnd.Model.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Comment", b =>
                {
                    b.HasOne("MasterPieceBackEnd.Model.Doctor", "Doctor")
                        .WithMany("Comments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MasterPieceBackEnd.Model.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Diagnosis", b =>
                {
                    b.HasOne("MasterPieceBackEnd.Model.Doctor", "Doctor")
                        .WithMany("Diagnoses")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MasterPieceBackEnd.Model.User", "User")
                        .WithMany("Diagnoses")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Doctor", b =>
                {
                    b.HasOne("MasterPieceBackEnd.Model.Specialty", "Specialty")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MasterPieceBackEnd.Model.User", "User")
                        .WithMany("Doctors")
                        .HasForeignKey("UserId");

                    b.Navigation("Specialty");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.DoctorAd", b =>
                {
                    b.HasOne("MasterPieceBackEnd.Model.Doctor", "Doctor")
                        .WithMany("DoctorAds")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Review", b =>
                {
                    b.HasOne("MasterPieceBackEnd.Model.Doctor", "Doctor")
                        .WithMany("Reviews")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MasterPieceBackEnd.Model.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Treatment", b =>
                {
                    b.HasOne("MasterPieceBackEnd.Model.Doctor", "Doctor")
                        .WithMany("Treatments")
                        .HasForeignKey("DoctorId")
                        .IsRequired();

                    b.HasOne("MasterPieceBackEnd.Model.User", "User")
                        .WithMany("Treatments")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Admin", b =>
                {
                    b.Navigation("AdminActions");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Doctor", b =>
                {
                    b.Navigation("Availabilities");

                    b.Navigation("Bookings");

                    b.Navigation("Comments");

                    b.Navigation("Diagnoses");

                    b.Navigation("DoctorAds");

                    b.Navigation("Reviews");

                    b.Navigation("Treatments");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.Specialty", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("MasterPieceBackEnd.Model.User", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Bookings");

                    b.Navigation("Comments");

                    b.Navigation("Diagnoses");

                    b.Navigation("Doctors");

                    b.Navigation("Reviews");

                    b.Navigation("Treatments");
                });
#pragma warning restore 612, 618
        }
    }
}
