using Discord.WebSocket;
using DotBot.Application.Interfaces;
using DotBot.Data.Interfaces;
using DotBot.Data.Models;
using DotBot.Resources;

namespace DotBot.Application.Services
{
    public class SwearService : ISwearService
    {
        private readonly IWordSuggestionRepository _wordSuggestionRepository;
        private readonly IInternalUserRepository _internalUserRepository;

        public SwearService(
            IWordSuggestionRepository wordSuggestionRepository,
            IInternalUserRepository internalUserRepository)
        {
            _wordSuggestionRepository = wordSuggestionRepository;
            _internalUserRepository = internalUserRepository;
        }

        public async Task ReportSwearWordAsync(
            SocketUserMessage message,
            string locale,
            string swearWord)
        {
            // Check if the Author is existing in the DB
            var user = await _internalUserRepository.GetByExternalId(message.Author.Id);

            if (user == null)
            {
                var internalUser = new InternalUser
                {
                    ExternalId = message.Author.Id,
                    UserName = message.Author.Username,
                    NonCryptoCoins = 1m,
                };

                await _internalUserRepository.AddAsync(internalUser);

                user = internalUser;
            }

            var suggestion = new WordSuggestion
            {
                Id = Guid.NewGuid(),
                Locale = locale,
                SwearWord = swearWord,
                UserId = user.Id,
            };


            await _wordSuggestionRepository.AddAsync(suggestion);

            user.NonCryptoCoins += 1m;
            await _internalUserRepository.UpdateAsync(user);
        }

        public async Task SanitizeMessageAsync(SocketUserMessage message)
        {
            if (MessageHavingBadWords(message.CleanContent))
            {
                await message.DeleteAsync();
                await message.Channel.SendMessageAsync($"Message from {message.Author.Username} is deleted, because it contains swear words!");
            }
        }

        private static bool MessageHavingBadWords(string message)
        {
            var words = message.Split(' ');

            var anySwearBg = words.Any(w => SwearWordsConstants.BulgarianSwearWords.Contains(w));
            var anySwearEn = words.Any(w => SwearWordsConstants.EnglishSwearWords.Contains(w));

            return anySwearBg || anySwearEn;
        }
    }
}