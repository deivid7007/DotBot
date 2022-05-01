using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DotBot.Application.Interfaces;
using DotBot.Application.Services;
using DotBot.Data;
using DotBot.Data.Interfaces;
using DotBot.Data.Models;
using DotBot.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotBot.Client
{
    public class Program
    {
        private const char CommandSpecialSymbol = '!';
        private const ulong GuildId = 0;
        private const string Token = "";

        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;
        private readonly ISwearService _swearService;
        private readonly IInternalUserService _internalUserService;

        private Program()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                GatewayIntents = GatewayIntents.All,
            });

            _commands = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info,
                CaseSensitiveCommands = false,
            });

            // Subscribe the logging handler to both the client and the CommandService.
            _client.Log += LogAsync;
            _commands.Log += LogAsync;

            // Setup DI container.
            _services = ConfigureServices();
            _swearService = _services.GetService(typeof(ISwearService)) as ISwearService;
            _internalUserService = _services.GetService(typeof(IInternalUserService)) as IInternalUserService;
        }

        private static IServiceProvider ConfigureServices()
        {
            var map = new ServiceCollection()
                .AddDbContext<SqlDbContext>()
                .AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>))
                .AddSingleton<IWordSuggestionRepository, WordSuggestionRepository>()
                .AddSingleton<IInternalUserRepository, InternalUserRepository>()
                .AddSingleton<ISwearService, SwearService>()
                .AddSingleton<IInternalUserService, InternalUserService>()
                .AddSingleton<IJokeService, JokeService>();

            return map.BuildServiceProvider();
        }

        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync()
        {
            await LoginAsync();

            _client.MessageReceived += HandleCommandAsync;
            _client.UserJoined += HandleUserJoinedAsync;
            _client.Connected += HandleBotConnected;

            await _commands.AddModulesAsync(
                assembly: Assembly.GetEntryAssembly(),
                services: _services);

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task LoginAsync()
        {
            await _client.LoginAsync(TokenType.Bot, Token);
            await _client.StartAsync();
        }

        private Task LogAsync(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            if (arg is not SocketUserMessage message)
            {
                return;
            }

            // Checking message for swear words
            await _swearService.SanitizeMessageAsync(message);

            // Prevent recursion
            if (message.Author.Id == _client.CurrentUser.Id || message.Author.IsBot)
            {
                return;
            }

            int position = 0;
            if (message.HasCharPrefix(CommandSpecialSymbol, ref position))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, position, _services);

                if (!result.IsSuccess)
                {
                    await message.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }

        private async Task HandleUserJoinedAsync(SocketGuildUser user)
        {
            var internalUser = new InternalUser
            {
                ExternalId = user.Id,
                UserName = user.Username,
            };

            await _internalUserService.RegisterUserAsync(internalUser);
        }

        private async Task HandleBotConnected()
        {
            await Task.Delay(5000);
            var discordUsers = _client.GetGuild(GuildId).Users;
            var registeredUsers = await _internalUserService.GetAllRegisteredUsers();

            var unregisteredUsers = discordUsers
                .Where(du => !registeredUsers.Any(ru => ru.ExternalId == du.Id))
                .Select(u => new InternalUser
                {
                    ExternalId = u.Id,
                    UserName = u.Username,
                })
                .ToList();

            await _internalUserService.RegisterMultipleUsersAsync(unregisteredUsers);
        }
    }
}