using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Inspire.Erp.Domain.Entities
{
    public partial class InspireErpDBNewContext : DbContext
    {
        public InspireErpDBNewContext()
        {
        }

        public InspireErpDBNewContext(DbContextOptions<InspireErpDBNewContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server= 103.74.54.207,1434;Database=InspireErpDBNew;Trusted_Connection=False;MultipleActiveResultSets=true;User Id = sa; Password=B6^i6!5^Mdx0Z4V;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
