namespace DotBot.Application.Interfaces
{
    public interface IJokeService
    {
        Task<string> GetChuckJokeAsync();
    }
}
