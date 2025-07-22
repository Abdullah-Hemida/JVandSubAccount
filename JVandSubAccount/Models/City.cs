// Models/City.cs
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JVandSubAccount.Models
{
    public class City
    {
        public int CityID { get; set; }
        public string? CityCode { get; set; }
        public string? CityNameAr { get; set; }
        public string? CityNameEn { get; set; }
        public int? BranchID { get; set; }

        // Navigation properties
        public Branch? Branch { get; set; }
        public ICollection<SubAccountClient> Clients { get; set; } = new List<SubAccountClient>();
    }

    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");
            builder.HasKey(c => c.CityID);

            builder.Property(c => c.CityCode)
                .HasMaxLength(50);

            builder.Property(c => c.CityNameAr)
                .HasMaxLength(100);

            builder.Property(c => c.CityNameEn)
                .HasMaxLength(100);

            // Relationship
            builder.HasOne(c => c.Branch)
                .WithMany()
                .HasForeignKey(c => c.BranchID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
