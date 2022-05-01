using DotBot.Application.Interfaces;
using DotBot.Application.ResponseModels;
using Newtonsoft.Json;

namespace DotBot.Application.Services
{
    public class JokeService : IJokeService
    {
        private readonly HttpClient _httpClient;
        private const string ChuckApiUrl = "https://api.chucknorris.io/jokes/random";

        public JokeService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetChuckJokeAsync()
        {
            var response = await _httpClient.GetAsync(ChuckApiUrl);
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<ChuckJokeResponse>(responseAsString);
            
            return responseObj.Value;
        }
    }
}
