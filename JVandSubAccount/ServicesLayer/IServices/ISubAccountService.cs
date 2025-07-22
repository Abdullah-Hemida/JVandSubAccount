using JVandSubAccount.Models;

namespace JVandSubAccount.ServicesLayer.IServices
{
    public interface ISubAccountService
    {
        Task<List<SubAccount>> GetSubAccountTreeAsync();
        Task<SubAccount?> GetSubAccountWithDetailsAsync(int id);
    }
}
