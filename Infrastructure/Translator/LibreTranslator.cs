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
        }

        public async Task<string> TranslateAsync(string word, string source, string target, CancellationToken ct = default)
        {
            var request = new TranslateRequest(word, source, target);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/translate", request, ct);
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