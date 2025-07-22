// Models/SubAccountDetail.cs
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JVandSubAccount.Models
{
    public class SubAccountDetail
    {
        public int SubAccountDetailID { get; set; }
        public int SubAccountID { get; set; }
        public int AccountID { get; set; }
        public int? BranchID { get; set; }

        // Navigation properties
        public SubAccount SubAccount { get; set; } = null!;
        public Account Account { get; set; } = null!;
        public Branch? Branch { get; set; }
    }

    public class SubAccountDetailConfiguration : IEntityTypeConfiguration<SubAccountDetail>
    {
        public void Configure(EntityTypeBuilder<SubAccountDetail> builder)
        {
            builder.ToTable("SubAccounts_Details");
            builder.HasKey(d => d.SubAccountDetailID);

            // Relationships
            builder.HasOne(d => d.SubAccount)
                .WithMany(d => d.SubAccountDetails)
                .HasForeignKey(d => d.SubAccountID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Account)
                .WithMany(d => d.SubAccountDetails)
                .HasForeignKey(d => d.AccountID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Branch)
                .WithMany(b => b.SubAccountDetails)
                .HasForeignKey(d => d.BranchID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
