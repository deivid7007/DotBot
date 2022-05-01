using Discord.Commands;

namespace DotBot.Client.Commands
{
    public class RootLevelModule : ModuleBase<SocketCommandContext>
    {
        private const string HelpTemplate = @"
        Commands legend:
        -------------
        !info me ---> shows info for your profile
        -------------
        !report swear {word} {locale} ---> reports swear word for locale
        -------------
        !joke chuck ---> says Chuck Norris joke";

        [Command("help")]
        [Summary("Show commands legend.")]
        public Task SayAsync()
            => ReplyAsync(HelpTemplate);
    }
}
