using Discord.Commands;
using DotBot.Application.Interfaces;

namespace DotBot.Client.Commands
{
    [Group("report")]
    public class ReportModule : ModuleBase<SocketCommandContext>
    {
        private readonly ISwearService _swearService;

        public ReportModule(ISwearService swearService)
        {
            _swearService = swearService;
        }

        [Command("swear")]
        [Summary("Report a swear word")]
        public async Task ReportAsync(string swearWord, string locale)
        {
            await _swearService.ReportSwearWordAsync(Context.Message, locale, swearWord);
            await ReplyAsync($"Thanks {Context.Message.Author.Mention} for reporting swear word :)");
            await ReplyAsync($"{Context.Message.Author.Mention} earned 1 NonCryptoCoin :)");
        }
    }
}
