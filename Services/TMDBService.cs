using System.Text.Json;

namespace movie_backend.Services
{
    public class TMDBService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;


        public TMDBService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Tmdb:ApiKey"];
            _baseUrl = configuration["Tmdb:BaseUrl"];
        }


        public async Task<object> GetPopularMoviesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/movie/popular?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<object>(json); // Replace with a proper model later
            return data;
        }

        public async Task<object> SearchMoviesAsync(string query)
        {
            var encodedQuery = System.Web.HttpUtility.UrlEncode(query);
            var response = await _httpClient.GetAsync($"{_baseUrl}/search/movie?api_key={_apiKey}&query={encodedQuery}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<object>(json);
        }


    }
}
