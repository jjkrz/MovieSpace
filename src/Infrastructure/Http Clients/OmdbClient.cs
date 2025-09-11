using Application.Abstractions;
using Application.Common.Dto;
using Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Infrastructure.Http_Clients
{
    public class OmdbClient : IMovieRatingService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;
        private readonly ILogger<OmdbClient> _logger;


        public OmdbClient(HttpClient httpClient, IConfiguration configuration, ILogger<OmdbClient> logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://www.omdbapi.com/");
            _apiKey = configuration["Omdb:ApiKey"] ?? throw new ArgumentNullException("API key is missing");
            _logger = logger;
        }

        public async Task<Result<float>> GetImdbMovieRatingAsync(string title, int year)
        {
            var response = await _httpClient.GetAsync($"?t={Uri.EscapeDataString(title)}&y={year}&apikey={_apiKey}");

            var content = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"Omdb api responded with: {content}");

            if (!response.IsSuccessStatusCode)
            {   
                return Result.Failure<float>(HttpClientErrors.ImdbRatingFetchingFailed);
            }

            var dto = await response.Content.ReadFromJsonAsync<OmdbResponseDto>();

            if (dto == null) return Result.Failure<float>(HttpClientErrors.ImdbRatingFetchingFailed);

            return float.Parse(dto.imdbRating!);
        }
    }
}
