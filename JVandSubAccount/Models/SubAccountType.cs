// Models/SubAccountType.cs
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JVandSubAccount.Models
{
    public class SubAccountType
    {
        public int SubAccountTypeID { get; set; }
        public string SubAccountTypeNameAr { get; set; } = string.Empty;
        public string? SubAccountTypeNameEn { get; set; }
        public int? BranchID { get; set; }
        public ICollection<SubAccount> SubAccounts { get; set; } = new List<SubAccount>();
        // Navigation property
        public Branch? Branch { get; set; }
    }

    public class SubAccountTypeConfiguration : IEntityTypeConfiguration<SubAccountType>
    {
        public void Configure(EntityTypeBuilder<SubAccountType> builder)
        {
            builder.ToTable("SubAccountsTypes");
            builder.HasKey(t => t.SubAccountTypeID);

            builder.Property(t => t.SubAccountTypeNameAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.SubAccountTypeNameEn)
                .HasMaxLength(100);

            // Relationship
            builder.HasOne(t => t.Branch)
                .WithMany(b => b.SubAccountTypes)
                .HasForeignKey(t => t.BranchID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
