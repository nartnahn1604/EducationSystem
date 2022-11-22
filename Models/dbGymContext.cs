using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
namespace IT008_UIT.Models
{
    public partial class dbGymContext : DbContext
    {
        public dbGymContext()
        {
        }

        public dbGymContext(DbContextOptions<dbGymContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Contract> Contracts { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Facility> Facilities { get; set; } = null!;
        public virtual DbSet<Pt> Pts { get; set; } = null!;
        public virtual DbSet<Ptcontract> Ptcontracts { get; set; } = null!;
        public virtual DbSet<Ptcourse> Ptcourses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<StaffId> StaffIds { get; set; } = null!;
        public virtual DbSet<TypesOfFacility> TypesOfFacilities { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseSqlServer("Server=(local);Database=dbGym;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_Accounts_StaffID");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.PtcontractId).HasColumnName("PTContractID");

                entity.Property(e => e.Ptid).HasColumnName("PTID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Booking_Customers");

                entity.HasOne(d => d.Ptcontract)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PtcontractId)
                    .HasConstraintName("FK_Booking_PTContracts");

                entity.HasOne(d => d.PtcontractNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PtcontractId)
                    .HasConstraintName("FK_Booking_PTCourses");

                entity.HasOne(d => d.Pt)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.Ptid)
                    .HasConstraintName("FK_Booking_PTs");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FinishDate).HasColumnType("date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Contracts_Courses");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Contracts_Customers");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Avatar).HasMaxLength(255);


                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Facilities)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Facilities_TypesOfFacility");
            });

            modelBuilder.Entity<Pt>(entity =>
            {
                entity.ToTable("PTs");

                entity.Property(e => e.Ptid).HasColumnName("PTID");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ptcontract>(entity =>
            {
                entity.ToTable("PTContracts");

                entity.Property(e => e.PtcontractId).HasColumnName("PTContractID");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FinishDate).HasColumnType("date");

                entity.Property(e => e.PtcourseId).HasColumnName("PTCourseID");

                entity.Property(e => e.Ptid).HasColumnName("PTID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Ptcontracts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_PTContracts_Customers");

                entity.HasOne(d => d.Ptcourse)
                    .WithMany(p => p.Ptcontracts)
                    .HasForeignKey(d => d.PtcourseId)
                    .HasConstraintName("FK_PTContracts_PTCourses");

                entity.HasOne(d => d.Pt)
                    .WithMany(p => p.Ptcontracts)
                    .HasForeignKey(d => d.Ptid)
                    .HasConstraintName("FK_PTContracts_PTs");
            });

            modelBuilder.Entity<Ptcourse>(entity =>
            {
                entity.ToTable("PTCourses");

                entity.Property(e => e.PtcourseId).HasColumnName("PTCourseID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<StaffId>(entity =>
            {
                entity.HasKey(e => e.StaffId1);

                entity.ToTable("StaffID");

                entity.Property(e => e.StaffId1).HasColumnName("StaffID");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.StaffIds)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_StaffID_Roles");
            });

            modelBuilder.Entity<TypesOfFacility>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("TypesOfFacility");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
