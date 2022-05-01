using DotBot.Data.Interfaces;
using DotBot.Data.Models;

namespace DotBot.Data.Repositories
{
    public class WordSuggestionRepository :  BaseRepository<WordSuggestion>, IWordSuggestionRepository
    {
        public WordSuggestionRepository(SqlDbContext context) : base(context)
        {
        }
    }
}
