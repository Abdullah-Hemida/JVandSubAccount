using JVandSubAccount.Models;
using JVandSubAccount.ViewModel;

namespace JVandSubAccount.ServicesLayer.IServices
{
    public interface IJVService
    {
        Task<List<JVType>> GetJVTypesAsync();
        Task<List<JournalVoucher>> GetJVsAsync(int? jvTypeId = null);
        Task<JournalVoucher?> GetJVByIdAsync(int jvId);
        Task<List<JVDetailViewModel>> GetJVDetailsByJVIDAsync(int jvId);
        Task<List<Account>> GetAccountsAsync();
        Task<List<SubAccount>> GetSubAccountsAsync();
        Task SaveJVAsync(JournalVoucher jv);
    }
}
