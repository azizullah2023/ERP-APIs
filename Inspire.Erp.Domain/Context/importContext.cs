using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Inspire.Erp.Domain.Models;

namespace Inspire.Erp.Domain.Context
{
    public partial class importContext : DbContext
    {
        public importContext()
        {
        }

        public importContext(DbContextOptions<importContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ImportTimeSheet> ImportTimeSheet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-H04AS89;Database=InspireErpDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImportTimeSheet>(entity =>
            {
                entity.HasKey(e => e.SerialNo);

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasMaxLength(50);

                entity.Property(e => e.ExpectedHours).HasMaxLength(50);

                entity.Property(e => e.Hours).HasMaxLength(50);

                entity.Property(e => e.StartTime).HasMaxLength(50);

                entity.Property(e => e.Task).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
