// Models/JVDetail.cs
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JVandSubAccount.Models
{
    public class JVDetail
    {
        public int JVDetailID { get; set; }
        public int JVID { get; set; }
        public int AccountID { get; set; }
        public int SubAccountID { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string? Notes { get; set; }
        public int? BranchID { get; set; }
        public bool IsDoc { get; set; } 

        // Navigation properties
        public JournalVoucher JournalVoucher { get; set; } = null!;

        [ForeignKey("AccountID")]
        public Account Account { get; set; } = null!;
        [ForeignKey("SubAccountID")]
        public SubAccount SubAccount { get; set; } = null!;
    }

    public class JVDetailConfiguration : IEntityTypeConfiguration<JVDetail>
    {
        public void Configure(EntityTypeBuilder<JVDetail> builder)
        {
            builder.ToTable("JVDetails");

            builder.HasKey(d => d.JVDetailID);

            builder.Property(d => d.Debit)
                .IsRequired()
                .HasColumnType("numeric(22,8)");

            builder.Property(d => d.IsDoc)
                .IsRequired()
                .HasColumnName("IsDoc");

            builder.Property(d => d.Credit)
                .IsRequired()
                .HasColumnType("numeric(22,8)");

            builder.Property(d => d.Notes)
                .HasMaxLength(1000);

            builder.HasOne(d => d.JournalVoucher)
                .WithMany(j => j.JVDetails)
                .HasForeignKey(d => d.JVID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Account)
                    .WithMany()
                    .HasForeignKey(d => d.AccountID)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.SubAccount)
                .WithMany()
                .HasForeignKey(d => d.SubAccountID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
