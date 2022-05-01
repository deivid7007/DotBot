using Discord.WebSocket;
using DotBot.Application.Interfaces;
using DotBot.Resources;

namespace DotBot.Infrastructure.Interfaces
{
    public interface ISwearService
    {
        Task SanitizeMessageAsync(SocketUserMessage message);
    }
}
