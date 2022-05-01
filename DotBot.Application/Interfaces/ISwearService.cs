using Discord.WebSocket;

namespace DotBot.Application.Interfaces
{
    public interface ISwearService
    {
        Task ReportSwearWordAsync(SocketUserMessage message, string locale, string swearWord);

        Task SanitizeMessageAsync(SocketUserMessage message);
    }
}
