using FinTrack.API.Models.Domains;

namespace FinTrack.API.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAccountsAsync();
    }
}
