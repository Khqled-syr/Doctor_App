using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Final_Project.Databases
{
    public partial class databaseContext : DbContext
    {
        public databaseContext()
        {
        }

        public databaseContext(DbContextOptions<databaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TAppointment> TAppointments { get; set; } = null!;
        public virtual DbSet<TPatient> TPatients { get; set; } = null!;
        public virtual DbSet<TUser> TUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=database.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAppointment>(entity =>
            {
                entity.HasKey(e => e.AppointmentId);

                entity.ToTable("t_appointments");

                entity.HasIndex(e => e.AppointmentId, "IX_t_appointments_appointment_ID")
                    .IsUnique();

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_ID");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.PatientId).HasColumnName("patient_ID");

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TAppointments)
                    .HasForeignKey(d => d.PatientId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TAppointments)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<TPatient>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.ToTable("t_patients");

                entity.HasIndex(e => e.PatientId, "IX_t_patients_patient_ID")
                    .IsUnique();

                entity.Property(e => e.PatientId).HasColumnName("patient_ID");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Number).HasColumnName("number");
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("t_users");

                entity.HasIndex(e => e.UserId, "IX_t_users_user_ID")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Password).HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
