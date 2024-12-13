using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Inspire.Erp.Domain.Models;

namespace Inspire.Erp.Domain.Context
{
    public partial class dummyContext : DbContext
    {
        public dummyContext()
        {
        }

        public dummyContext(DbContextOptions<dummyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GeneralSettings> GeneralSettings { get; set; }
        public virtual DbSet<ProgramSettings> ProgramSettings { get; set; }

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
            modelBuilder.Entity<GeneralSettings>(entity =>
            {
                entity.ToTable("General_Settings");

                entity.Property(e => e.GeneralSettingsId).HasColumnName("General_Settings_ID");

                entity.Property(e => e.GeneralSettingsBoolValue).HasColumnName("General_Settings_Bool_Value");

                entity.Property(e => e.GeneralSettingsCategory)
                    .HasColumnName("General_Settings_Category")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GeneralSettingsDelStatus).HasColumnName("General_Settings_DelStatus");

                entity.Property(e => e.GeneralSettingsDescription)
                    .HasColumnName("General_Settings_Description")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.GeneralSettingsKeyValue)
                    .HasColumnName("General_Settings_Key_Value")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.GeneralSettingsNumValue).HasColumnName("General_Settings_Num_Value");

                entity.Property(e => e.GeneralSettingsTableName)
                    .HasColumnName("General_Settings_Table_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GeneralSettingsTextValue)
                    .HasColumnName("General_Settings_Text_Value")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GeneralSettingsValueType)
                    .HasColumnName("General_Settings_Value_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProgramSettings>(entity =>
            {
                entity.HasKey(e => e.ProgramSettingsGenId);

                entity.ToTable("Program_Settings");

                entity.Property(e => e.ProgramSettingsGenId).HasColumnName("Program_Settings_GenID");

                entity.Property(e => e.ProgramSettingsBoolValue).HasColumnName("Program_Settings_Bool_Value");

                entity.Property(e => e.ProgramSettingsCategory)
                    .HasColumnName("Program_Settings_Category")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramSettingsDelStatus).HasColumnName("Program_Settings_DelStatus");

                entity.Property(e => e.ProgramSettingsDescription)
                    .HasColumnName("Program_Settings_Description")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramSettingsFormId)
                    .HasColumnName("Program_Settings_FormID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramSettingsKeyValue)
                    .HasColumnName("Program_Settings_Key_Value")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramSettingsNumValue).HasColumnName("Program_Settings_Num_Value");

                entity.Property(e => e.ProgramSettingsTableName)
                    .HasColumnName("Program_Settings_Table_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramSettingsTextValue)
                    .HasColumnName("Program_Settings_Text_Value")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramSettingsValueType)
                    .HasColumnName("Program_Settings_Value_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
