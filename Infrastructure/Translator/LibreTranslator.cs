using Application.Interfaces.RepositoryInterfaces;
using System.Net.Http.Json;

namespace Infrastructure.Translator
{
    public partial class LibreTranslator : ITranslator
    {
        private readonly HttpClient _httpClient;

        public LibreTranslator(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("LT_URL") ?? throw new Exception("Libre translate address is not set."));
        }

        public async Task<string> TranslateAsync(string word, string source, string target, CancellationToken ct = default)
        {
            var request = new TranslateRequest(word, source, target);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("translate", request, ct); //http://localhost:5555/translate
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<TranslateResponse>(ct);
                return result?.translatedText ?? word;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return word;
            }
        }
    }
}