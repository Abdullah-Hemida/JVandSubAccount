// Models/JVType.cs
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JVandSubAccount.Models
{
    public class JVType
    {
        public int JVTypeID { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
    }
    public class JVTypeConfiguration : IEntityTypeConfiguration<JVType>
    {
        public void Configure(EntityTypeBuilder<JVType> builder)
        {
            builder.ToTable("JVTypes");

            builder.HasKey(t => t.JVTypeID);

            builder.Property(t => t.NameEn)
                .HasColumnName("JVTypeNameEn")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.NameAr)
                .HasColumnName("JVTypeNameAr")
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
