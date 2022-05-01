using Discord.Commands;

namespace DotBot.Commands
{
	[Group("info")]
	public class InfoModule : ModuleBase<SocketCommandContext>
	{
		[Command("help")]
		[Summary("Shows legend for all commands available.")]
		public Task SayAsync()
			=> ReplyAsync("Commands legends is not yet available.");
	}
}