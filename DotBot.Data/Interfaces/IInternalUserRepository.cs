using DotBot.Data.Models;

namespace DotBot.Data.Interfaces
{
    public interface IInternalUserRepository : IBaseRepository<InternalUser>
    {
        Task<InternalUser> GetByExternalId(ulong externalId);
    }
}
