using DotBot.Application.Interfaces;
using DotBot.Data.Interfaces;
using DotBot.Data.Models;

namespace DotBot.Application.Services
{
    public class InternalUserService : IInternalUserService
    {
        private readonly IInternalUserRepository _internalUserRepository;

        public InternalUserService(IInternalUserRepository internalUserRepository)
        {
            _internalUserRepository = internalUserRepository;
        }

        public async Task<List<InternalUser>> GetAllRegisteredUsers()
        {
            return await _internalUserRepository.GetAllAsync();
        }

        public async Task<InternalUser> GetUserByExternalId(ulong externalId)
        {
            return await _internalUserRepository.GetByExternalId(externalId);
        }

        public async Task RegisterMultipleUsersAsync(List<InternalUser> internalUsers)
        {
            await _internalUserRepository.AddRangeAsync(internalUsers);
        }

        public async Task RegisterUserAsync(InternalUser internalUser)
        {
            var user = await _internalUserRepository.GetByExternalId(internalUser.ExternalId);

            if (user == null)
            {
                await _internalUserRepository.AddAsync(internalUser);
            }
        }


    }
}
