using JVandSubAccount.Models;
using Microsoft.EntityFrameworkCore;
using JVandSubAccount.Data.IRepositories;

namespace JVandSubAccount.Data.Repositories
{
    public class SubAccountRepository : ISubAccountRepository
    {
        private readonly AppDbContext _context;

        public SubAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubAccount>> GetAllWithTreeAsync()
        {
            var accounts = await _context.SubAccounts
                .Include(s => s.SubAccountType)
                .Include(s => s.Branch)
                .Include(s => s.SubAccountDetails)
                    .ThenInclude(d => d.Account)
                .Include(s => s.Clients)
                    .ThenInclude(c => c.City)
                .ToListAsync();

            // Build hierarchy manually
            foreach (var acc in accounts)
            {
                acc.Children = accounts.Where(c => c.ParentID == acc.SubAccountID).ToList();
            }

            return accounts;
        }

        public async Task<SubAccount?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.SubAccounts
                .Include(s => s.SubAccountType)
                .Include(s => s.Branch)
                .Include(s => s.SubAccountDetails)
                    .ThenInclude(d => d.Account)
                .Include(s => s.Clients)
                    .ThenInclude(c => c.City)
                .FirstOrDefaultAsync(s => s.SubAccountID == id);
        }
    }
}
