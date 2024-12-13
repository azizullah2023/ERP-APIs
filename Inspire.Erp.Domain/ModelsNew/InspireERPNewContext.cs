using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Inspire.Erp.Domain.ModelsNew
{
    public partial class InspireERPNewContext : DbContext
    {
        public InspireERPNewContext()
        {
        }

        public InspireERPNewContext(DbContextOptions<InspireERPNewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerDeliveryNote> CustomerDeliveryNote { get; set; }
        public virtual DbSet<CustomerDeliveryNoteDetails> CustomerDeliveryNoteDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=DESKTOP-AHIKFRV\\SQLEXPRESS;Database=InspireERPNew;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDeliveryNote>(entity =>
            {
                entity.HasKey(e => e.CustomerDeliveryNoteDeliveryId);

                entity.ToTable("Customer_Delivery_Note");

                entity.Property(e => e.CustomerDeliveryNoteDeliveryId)
                    .HasColumnName("Customer_Delivery_Note_Delivery_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerDeliveryNoteAttention)
                    .HasColumnName("Customer_Delivery_Note_Attention")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNoteCpoDate)
                    .HasColumnName("Customer_Delivery_Note_CPO_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerDeliveryNoteCpoId).HasColumnName("Customer_Delivery_Note_CPO_ID");

                entity.Property(e => e.CustomerDeliveryNoteCurrencyId).HasColumnName("Customer_Delivery_Note_Currency_ID");

                entity.Property(e => e.CustomerDeliveryNoteCustomerCode).HasColumnName("Customer_Delivery_Note_Customer_Code");

                entity.Property(e => e.CustomerDeliveryNoteCustomerName)
                    .HasColumnName("Customer_Delivery_Note_Customer_Name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNoteDelDetId).HasColumnName("Customer_Delivery_Note_Del_Det_ID");

                entity.Property(e => e.CustomerDeliveryNoteDelStatus).HasColumnName("Customer_Delivery_Note_DelStatus");

                entity.Property(e => e.CustomerDeliveryNoteDeliveryAddress)
                    .HasColumnName("Customer_Delivery_Note_Delivery_Address")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNoteDeliveryDate)
                    .HasColumnName("Customer_Delivery_Note_Delivery_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerDeliveryNoteDeliveryStatus)
                    .HasColumnName("Customer_Delivery_Note_Delivery_Status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNoteFsno).HasColumnName("Customer_Delivery_Note_FSNO");

                entity.Property(e => e.CustomerDeliveryNoteIssuedStatus).HasColumnName("Customer_Delivery_Note_Issued_Status");

                entity.Property(e => e.CustomerDeliveryNoteLocationId).HasColumnName("Customer_Delivery_Note_Location_ID");

                entity.Property(e => e.CustomerDeliveryNoteManuelPo).HasColumnName("Customer_Delivery_Note_Manuel_PO");

                entity.Property(e => e.CustomerDeliveryNoteNote)
                    .HasColumnName("Customer_Delivery_Note_Note")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNotePacking)
                    .HasColumnName("Customer_Delivery_Note_Packing")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNotePodId).HasColumnName("Customer_Delivery_Note_POD_ID");

                entity.Property(e => e.CustomerDeliveryNoteQuality)
                    .HasColumnName("Customer_Delivery_Note_Quality")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNoteRemarks)
                    .HasColumnName("Customer_Delivery_Note_Remarks")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNoteSalesManId).HasColumnName("Customer_Delivery_Note_Sales_Man_ID");

                entity.Property(e => e.CustomerDeliveryNoteTechnicalDetails)
                    .HasColumnName("Customer_Delivery_Note_Technical_Details")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNoteTerms)
                    .HasColumnName("Customer_Delivery_Note_Terms")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNoteTraining)
                    .HasColumnName("Customer_Delivery_Note_Training")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNoteUserId).HasColumnName("Customer_Delivery_Note_User_ID");

                entity.Property(e => e.CustomerDeliveryNoteWarranty)
                    .HasColumnName("Customer_Delivery_Note_Warranty")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerDeliveryNoteDetails>(entity =>
            {
                entity.HasKey(e => e.CustomerDeliveryNoteDetailsDetId);

                entity.ToTable("Customer_Delivery_Note_Details");

                entity.Property(e => e.CustomerDeliveryNoteDetailsDetId)
                    .HasColumnName("Customer_Delivery_Note_details_DetId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerDeliveryNoteDetailsBaseValue).HasColumnName("Customer_Delivery_Note_Details_Base_Value");

                entity.Property(e => e.CustomerDeliveryNoteDetailsCpoSlno).HasColumnName("Customer_Delivery_Note_Details_CPO_Slno");

                entity.Property(e => e.CustomerDeliveryNoteDetailsCustomerPoNo).HasColumnName("Customer_Delivery_Note_Details_Customer_PO_NO");

                entity.Property(e => e.CustomerDeliveryNoteDetailsDelStatus).HasColumnName("Customer_Delivery_Note_Details_DelStatus");

                entity.Property(e => e.CustomerDeliveryNoteDetailsDeliveryDetailId)
                    .HasColumnName("Customer_Delivery_Note_Details_Delivery_Detail_ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerDeliveryNoteDetailsDeliveryNo).HasColumnName("Customer_Delivery_Note_Details_Delivery_NO");

                entity.Property(e => e.CustomerDeliveryNoteDetailsDescription)
                    .HasColumnName("Customer_Delivery_Note_Details_Description")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDeliveryNoteDetailsFoc).HasColumnName("Customer_Delivery_Note_Details_FOC");

                entity.Property(e => e.CustomerDeliveryNoteDetailsFsno).HasColumnName("Customer_Delivery_Note_Details_FSNO");

                entity.Property(e => e.CustomerDeliveryNoteDetailsIsEdited).HasColumnName("Customer_Delivery_Note_Details_IsEdited");

                entity.Property(e => e.CustomerDeliveryNoteDetailsItemId).HasColumnName("Customer_Delivery_Note_Details_Item_ID");

                entity.Property(e => e.CustomerDeliveryNoteDetailsMatId2).HasColumnName("Customer_Delivery_Note_Details_Mat_ID2");

                entity.Property(e => e.CustomerDeliveryNoteDetailsPodId).HasColumnName("Customer_Delivery_Note_Details_POD_ID");

                entity.Property(e => e.CustomerDeliveryNoteDetailsQty)
                    .HasColumnName("Customer_Delivery_Note_Details_QTY")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerDeliveryNoteDetailsSlno).HasColumnName("Customer_Delivery_Note_Details_Slno");

                entity.Property(e => e.CustomerDeliveryNoteDetailsUnitId).HasColumnName("Customer_Delivery_Note_Details_Unit_ID");

                entity.Property(e => e.CustomerDeliveryNoteDetailsUnitId2).HasColumnName("Customer_Delivery_Note_Details_Unit_ID2");

                entity.HasOne(d => d.CustomerDeliveryNoteDetailsDeliveryNoNavigation)
                    .WithMany(p => p.CustomerDeliveryNoteDetails)
                    .HasForeignKey(d => d.CustomerDeliveryNoteDetailsDeliveryNo)
                    .HasConstraintName("FK_Customer_Delivery_Note_Details_Customer_Delivery_Note");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
