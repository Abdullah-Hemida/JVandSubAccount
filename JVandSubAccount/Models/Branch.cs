// Models/Branch.cs
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JVandSubAccount.Models
{
    public class Branch
    {
        public int BranchID { get; set; }
        public string BranchCode { get; set; } = string.Empty;
        public string BranchNameAr { get; set; } = string.Empty;
        public string? BranchNameEn { get; set; }

        // Navigation properties
        public ICollection<SubAccountClient> Clients { get; set; } = new List<SubAccountClient>();
        public ICollection<SubAccountDetail> SubAccountDetails { get; set; } = new List<SubAccountDetail>();
        public ICollection<SubAccountLevel> SubAccountLevels { get; set; } = new List<SubAccountLevel>();
        public ICollection<SubAccountType> SubAccountTypes { get; set; } = new List<SubAccountType>();
    }

    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches");
            builder.HasKey(b => b.BranchID);

            builder.Property(b => b.BranchCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.BranchNameAr)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.BranchNameEn)
                .HasMaxLength(50);
        }
    }
}
