using DotBot.Data.Interfaces;
using DotBot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DotBot.Data.Repositories
{
    public class InternalUserRepository : BaseRepository<InternalUser>, IInternalUserRepository
    {
        private readonly SqlDbContext _dbContext;

        public InternalUserRepository(SqlDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<InternalUser> GetByExternalId(ulong externalId)
        {
            return await _dbContext.Set<InternalUser>().FirstOrDefaultAsync(e => e.ExternalId == externalId);
        }
    }
}
