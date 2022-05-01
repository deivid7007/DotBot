using Discord.Commands;
using DotBot.Application.Interfaces;

namespace DotBot.Client.Commands
{
    [Group("joke")]
    public class JokeModule : ModuleBase<SocketCommandContext>
    {
        private readonly IJokeService _jokeService;

        public JokeModule(IJokeService jokeService)
        {
            _jokeService = jokeService;
        }

        [Command("chuck")]
        [Summary("Say Chuck Norris joke.")]
        public async Task SayChuckJokeAsync()
        {
            var joke = await _jokeService.GetChuckJokeAsync();
            await ReplyAsync(joke);
        }
    }
}
