// Models/SubAccountClient.cs
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JVandSubAccount.Models
{
    public class SubAccountClient
    {
        public int ClientID { get; set; }
        public string ClientNo { get; set; } = string.Empty;
        public int SubAccountID { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityID { get; set; }
        public decimal? CreditLimit { get; set; }
        public bool IsDiscountTax { get; set; }
        public bool IsActive { get; set; }
        public string? Notes { get; set; }
        public int? BranchID { get; set; }

        // Navigation properties
        public SubAccount SubAccount { get; set; } = null!;
        public City? City { get; set; }
        public Branch? Branch { get; set; }
    }

    public class SubAccountClientConfiguration : IEntityTypeConfiguration<SubAccountClient>
    {
        public void Configure(EntityTypeBuilder<SubAccountClient> builder)
        {
            builder.ToTable("SubAccountsClient");
            builder.HasKey(c => c.ClientID);

            builder.Property(c => c.ClientNo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.CreditLimit)
                .HasColumnType("numeric(18,8)");

            builder.Property(c => c.Notes)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(c => c.SubAccount)
                .WithMany(sa => sa.Clients)
                .HasForeignKey(c => c.SubAccountID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.City)
                .WithMany(city => city.Clients)
                .HasForeignKey(c => c.CityID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Branch)
                .WithMany(b => b.Clients)
                .HasForeignKey(c => c.BranchID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
