// Models/SubAccount
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JVandSubAccount.Models
{
    public class SubAccount
    {
        public int SubAccountID { get; set; }
        public string SubAccountNumber { get; set; } = string.Empty;
        public string SubAccountNameAr { get; set; } = string.Empty;
        public string? SubAccountNameEn { get; set; }
        public int? ParentID { get; set; }
        public bool IsMain { get; set; }
        public int LevelID { get; set; }
        public int? SubAccountTypeID { get; set; }
        public int? BranchID { get; set; }

        // Navigation properties
        public SubAccount? Parent { get; set; }
        public SubAccountLevel Level { get; set; } = null!;
        public SubAccountType? SubAccountType { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<SubAccount> Children { get; set; } = new List<SubAccount>();
        public ICollection<SubAccountClient> Clients { get; set; } = new List<SubAccountClient>();
        public ICollection<SubAccountDetail> SubAccountDetails { get; set; } = new List<SubAccountDetail>();
    }

    public class SubAccountConfiguration : IEntityTypeConfiguration<SubAccount>
    {
        public void Configure(EntityTypeBuilder<SubAccount> builder)
        {
            builder.ToTable("SubAccounts");
            builder.HasKey(s => s.SubAccountID);

            builder.Property(s => s.SubAccountNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.SubAccountNameAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.SubAccountNameEn)
                .HasMaxLength(100);

            // Self-referencing relationship for parent/child
            builder.HasOne(s => s.Parent)
                .WithMany(s => s.Children)
                .HasForeignKey(s => s.ParentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationships
            builder.HasOne(s => s.Level)
                .WithMany()
                .HasForeignKey(s => s.LevelID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.SubAccountType)
                .WithMany(t => t.SubAccounts)
                .HasForeignKey(s => s.SubAccountTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Branch)
                .WithMany()
                .HasForeignKey(s => s.BranchID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
