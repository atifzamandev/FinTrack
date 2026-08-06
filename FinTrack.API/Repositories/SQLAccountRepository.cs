using FinTrack.API.Data;
using FinTrack.API.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.API.Repositories
{
    public class SQLAccountRepository : IAccountRepository
    {
        private readonly FinTrackDbContext finTrackDbContext;

        public SQLAccountRepository(FinTrackDbContext finTrackDbContext)
        {
            this.finTrackDbContext = finTrackDbContext;
        }
        public async Task<List<Account>> GetAllAccountsAsync()
        { 
            return await finTrackDbContext.Accounts.ToListAsync();
        }
    }
}
