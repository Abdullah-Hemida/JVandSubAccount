using JVandSubAccount.Models;
using JVandSubAccount.ViewModel;
using Microsoft.EntityFrameworkCore;
using JVandSubAccount.Data.IRepositories;
using System.Linq;

namespace JVandSubAccount.Data.Repositories
{
    public class JVRepository : IJVRepository
    {
        private readonly AppDbContext _context;

        public JVRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<JVType>> GetJVTypesAsync()
            => await _context.JVTypes.AsNoTracking().ToListAsync();

        public async Task<List<JournalVoucher>> GetJVsAsync(int? jvTypeId = null)
        {
            var query = _context.JournalVouchers.AsQueryable().AsNoTracking();

            if (jvTypeId.HasValue)
                query = query.Where(jv => jv.JVTypeID == jvTypeId.Value);

            return await query
                .Include(jv => jv.JVType)
                .OrderByDescending(jv => jv.JVDate)
                .ToListAsync();
        }

        public async Task<JournalVoucher?> GetJVByIdAsync(int jvId)
        {
            return await _context.JournalVouchers
                .Include(jv => jv.JVType)
                .Include(jv => jv.JVDetails) 
                .FirstOrDefaultAsync(jv => jv.JVID == jvId);
        }

        public async Task<List<JVDetailViewModel>> GetJVDetailsByJVIDAsync(int jvId)
        {
            return await _context.JVDetails
                .Where(d => d.JVID == jvId)
                .Include(d => d.Account)    
                .Include(d => d.SubAccount)
                .Select(d => new JVDetailViewModel
                {
                    JVDetailID = d.JVDetailID,
                    AccountID = d.AccountID,
                    SubAccountID = d.SubAccountID,
                    Debit = d.Debit,
                    Credit = d.Credit,
                    IsDoc = d.IsDoc,
                    Notes = d.Notes,
                    AccountNameAr = d.Account.AccountNameAr,
                    AccountNameEn = d.Account.AccountNameEn,
                    SubAccountNameAr = d.SubAccount.SubAccountNameAr,
                    SubAccountNameEn = d.SubAccount.SubAccountNameEn
                }).ToListAsync();
        }
        public async Task<List<Account>> GetAccountsAsync()
            => await _context.Accounts.ToListAsync();

        public async Task<List<SubAccount>> GetSubAccountsAsync()
            => await _context.SubAccounts.ToListAsync();

        public async Task SaveJVAsync(JournalVoucher jv)
        {
            if (jv.JVID == 0)
            {
                _context.JournalVouchers.Add(jv);
            }
            else
            {
                var existing = await _context.JournalVouchers
                    .Include(j => j.JVDetails)
                    .FirstOrDefaultAsync(j => j.JVID == jv.JVID);

                if (existing != null)
                {
                    _context.Entry(existing).CurrentValues.SetValues(jv);
                    existing.JVDetails.Clear();
                    foreach (var detail in jv.JVDetails)
                    {
                        existing.JVDetails.Add(detail);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
