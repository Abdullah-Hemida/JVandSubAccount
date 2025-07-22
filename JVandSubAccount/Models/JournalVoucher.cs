// Models/JournalVoucher.cs
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JVandSubAccount.Models
{
    public class JournalVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JVID { get; set; }
        public string JVNo { get; set; } = string.Empty;
        public DateTime JVDate { get; set; } = DateTime.Today;
        public int? JVTypeID { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public string? ReceiptNo { get; set; }
        public string? Notes { get; set; }
        public int? BranchID { get; set; }

        // Navigation properties
        public JVType? JVType { get; set; }
        public ICollection<JVDetail> JVDetails { get; set; } = new List<JVDetail>();
    }

    public class JournalVoucherConfiguration : IEntityTypeConfiguration<JournalVoucher>
    {
        public void Configure(EntityTypeBuilder<JournalVoucher> builder)
        {
            builder.ToTable("JV");

            builder.HasKey(j => j.JVID);

            builder.Property(j => j.JVID)
                .ValueGeneratedOnAdd();

            builder.Property(j => j.JVNo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(j => j.JVDate)
                .IsRequired();

            builder.Property(j => j.TotalDebit)
                .IsRequired()
                .HasColumnType("numeric(22,8)");

            builder.Property(j => j.TotalCredit)
                .IsRequired()
                .HasColumnType("numeric(22,8)");

            builder.Property(j => j.ReceiptNo)
                .HasMaxLength(50);

            builder.Property(j => j.Notes)
                .HasMaxLength(1000);

            builder.HasOne(j => j.JVType)
                .WithMany()
                .HasForeignKey(j => j.JVTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(j => j.JVDetails)
                .WithOne(d => d.JournalVoucher)
                .HasForeignKey(d => d.JVID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
