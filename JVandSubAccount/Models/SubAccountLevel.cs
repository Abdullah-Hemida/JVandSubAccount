// Models/SubAccountLevel.cs
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JVandSubAccount.Models
{
    public class SubAccountLevel
    {
        public int LevelID { get; set; }
        public int Width { get; set; }
        public int? BranchID { get; set; }

        // Navigation property
        public Branch? Branch { get; set; }
    }

    public class SubAccountLevelConfiguration : IEntityTypeConfiguration<SubAccountLevel>
    {
        public void Configure(EntityTypeBuilder<SubAccountLevel> builder)
        {
            builder.ToTable("SubAccounts_Levels");
            builder.HasKey(l => l.LevelID);

            builder.Property(l => l.Width)
                .IsRequired();

            // Relationship
            builder.HasOne(l => l.Branch)
                .WithMany(b => b.SubAccountLevels)
                .HasForeignKey(l => l.BranchID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
