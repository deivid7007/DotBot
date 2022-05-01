namespace DotBot.Data.Models
{
    public class InternalUser : BaseModel
    {
        public ulong ExternalId { get; set; }

        public string UserName { get; set; }

        public decimal NonCryptoCoins { get; set; } = 0m;
    }
}
