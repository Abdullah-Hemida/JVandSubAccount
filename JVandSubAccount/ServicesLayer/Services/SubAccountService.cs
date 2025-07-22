using JVandSubAccount.Models;
using JVandSubAccount.ServicesLayer.IServices;
using JVandSubAccount.Data.IRepositories;

namespace JVandSubAccount.ServicesLayer.Services
{
    public class SubAccountService : ISubAccountService
    {
        private readonly ISubAccountRepository _repository;

        public SubAccountService(ISubAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SubAccount>> GetSubAccountTreeAsync()
        {
            return await _repository.GetAllWithTreeAsync();
        }

        public async Task<SubAccount?> GetSubAccountWithDetailsAsync(int id)
        {
            return await _repository.GetByIdWithDetailsAsync(id);
        }
    }
}
