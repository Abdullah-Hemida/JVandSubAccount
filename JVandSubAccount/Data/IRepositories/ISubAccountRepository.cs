using JVandSubAccount.Models;

namespace JVandSubAccount.Data.IRepositories
{
    public interface ISubAccountRepository
    {
        Task<List<SubAccount>> GetAllWithTreeAsync();
        Task<SubAccount?> GetByIdWithDetailsAsync(int id);
    }
}
