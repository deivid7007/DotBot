using Newtonsoft.Json;

namespace DotBot.Application.ResponseModels
{
    public class ChuckJokeResponse
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
