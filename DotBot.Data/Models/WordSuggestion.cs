#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DotBot.Data.Models
{
    public class WordSuggestion : BaseModel
    {
        public string SwearWord { get; set; }

        public string Locale { get; set; }

        public Guid UserId { get; set; }

        public virtual InternalUser User { get; set; }

    }
}
