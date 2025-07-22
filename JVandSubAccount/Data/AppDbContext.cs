// Data/AppDbContext.cs
using JVandSubAccount.Models;
using Microsoft.EntityFrameworkCore;

namespace JVandSubAccount.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<JournalVoucher> JournalVouchers { get; set; }
        public DbSet<JVDetail> JVDetails { get; set; }
        public DbSet<JVType> JVTypes { get; set; }
        public DbSet<SubAccount> SubAccounts { get; set; }
        public DbSet<SubAccountClient> SubAccountClients { get; set; }
        public DbSet<SubAccountDetail> SubAccountDetails { get; set; }
        public DbSet<SubAccountLevel> SubAccountLevels { get; set; }
        public DbSet<SubAccountType> SubAccountTypes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply configurations
            builder.ApplyConfiguration(new JournalVoucherConfiguration());
            builder.ApplyConfiguration(new JVDetailConfiguration());
            builder.ApplyConfiguration(new JVTypeConfiguration());
            builder.ApplyConfiguration(new BranchConfiguration());
            builder.ApplyConfiguration(new SubAccountClientConfiguration());
            builder.ApplyConfiguration(new SubAccountDetailConfiguration());
            builder.ApplyConfiguration(new SubAccountLevelConfiguration());
            builder.ApplyConfiguration(new SubAccountTypeConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new SubAccountConfiguration());

        }
    }
}
