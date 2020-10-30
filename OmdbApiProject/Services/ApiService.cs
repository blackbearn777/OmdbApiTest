using Newtonsoft.Json;
using OmdbApiProject.Entities;
using System.Net.Http;
using System.Threading.Tasks;

namespace OmdbApiProject.Services
{
    public class ApiService
    {
        private const string _token = "ead5f305";
        private const string _url = "http://www.omdbapi.com/";
        private HttpClient _httpClient;
        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Movie> GetMovieById(string title)
        {
            string param = $"?apikey={_token}&i={title}";
            using (var response = await _httpClient.GetAsync(_url + param))
            {
                return JsonConvert.DeserializeObject<Movie>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<MoviesResponse> GetMovieBySearch(string searchParam)
        {
            string param = $"?apikey={_token}&s={searchParam}";
            using (var response = await _httpClient.GetAsync(_url + param))
            {
                return JsonConvert.DeserializeObject<MoviesResponse>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}