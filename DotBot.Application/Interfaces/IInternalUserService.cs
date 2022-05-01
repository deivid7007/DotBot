using DotBot.Data.Models;

namespace DotBot.Application.Interfaces
{
    public interface IInternalUserService
    {
        Task RegisterUserAsync(InternalUser internalUser);

        Task RegisterMultipleUsersAsync(List<InternalUser> internalUsers);

        Task<List<InternalUser>> GetAllRegisteredUsers();

        Task<InternalUser> GetUserByExternalId(ulong externalId);
    }
}
