using JVandSubAccount.Models;
using JVandSubAccount.ServicesLayer.IServices;
using JVandSubAccount.ViewModel;
using JVandSubAccount.Data.IRepositories;

namespace JVandSubAccount.ServicesLayer.Services
{
    public class JVService : IJVService
    {
        private readonly IJVRepository _jvRepository;

        public JVService(IJVRepository jvRepository)
        {
            _jvRepository = jvRepository;
        }

        public async Task<List<JVType>> GetJVTypesAsync()
            => await _jvRepository.GetJVTypesAsync();

        public async Task<List<JournalVoucher>> GetJVsAsync(int? jvTypeId = null)
            => await _jvRepository.GetJVsAsync(jvTypeId);

        public async Task<JournalVoucher?> GetJVByIdAsync(int jvId)
            => await _jvRepository.GetJVByIdAsync(jvId);

        public async Task<List<JVDetailViewModel>> GetJVDetailsByJVIDAsync(int jvId)
            => await _jvRepository.GetJVDetailsByJVIDAsync(jvId);

        public async Task<List<Account>> GetAccountsAsync()
            => await _jvRepository.GetAccountsAsync();

        public async Task<List<SubAccount>> GetSubAccountsAsync()
            => await _jvRepository.GetSubAccountsAsync();

        public async Task SaveJVAsync(JournalVoucher jv)
        {
            await _jvRepository.SaveJVAsync(jv);
        }

    }
}
