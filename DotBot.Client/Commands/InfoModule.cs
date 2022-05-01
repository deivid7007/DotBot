using Discord.Commands;
using DotBot.Application.Interfaces;

namespace DotBot.Client.Commands
{
    [Group("info")]
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        private readonly IInternalUserService _internalUserService;

        public InfoModule(IInternalUserService internalUserService)
        {
            _internalUserService = internalUserService;
        }

        [Command("me")]
        [Summary("Show user info.")]
        public async Task ShowCoinsAsync()
        {
            var user = await _internalUserService.GetUserByExternalId(Context.User.Id);
            await ReplyAsync($"{Context.User.Mention} you have {user.NonCryptoCoins} NonCryptoCoins!");
        }
    }
}