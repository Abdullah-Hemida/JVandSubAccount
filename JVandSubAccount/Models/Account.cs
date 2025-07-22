// Models/Account.cs
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JVandSubAccount.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountNameAr { get; set; } = string.Empty;
        public string? AccountNameEn { get; set; }
        public int? BranchID { get; set; }

        // Navigation properties
        public Branch? Branch { get; set; }
        public ICollection<SubAccountDetail> SubAccountDetails { get; set; } = new List<SubAccountDetail>();
    }

    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(a => a.AccountID);

            builder.Property(a => a.AccountNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.AccountNameAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.AccountNameEn)
                .HasMaxLength(100);

            // Relationship
            builder.HasOne(a => a.Branch)
                .WithMany()
                .HasForeignKey(a => a.BranchID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
