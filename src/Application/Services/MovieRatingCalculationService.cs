using Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class MovieRatingCalculationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MovieRatingCalculationService> _logger;
        private readonly TimeSpan _interval;
        private DateTime _lastRun = DateTime.MinValue;


        public MovieRatingCalculationService(
            IServiceProvider serviceProvider,
            ILogger<MovieRatingCalculationService> logger, 
            IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await UpdateMovieRatings(stoppingToken);
                    _lastRun = DateTime.UtcNow;
                    await Task.Delay(_interval, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating movie ratings");
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Retry after 5 mins
                }
            }
        }

        private async Task UpdateMovieRatings(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IMovieRepository>();

            _logger.LogInformation("Starting movie ratings calculation");

            List<MovieRatingData> moviesToUpdate;

            // Strategy: if ratings were last updated more than 24 hours ago, do a full recalculation.
            if (_lastRun == DateTime.MinValue || DateTime.UtcNow - _lastRun > TimeSpan.FromDays(1))
            {
                moviesToUpdate = await GetAllMoviesInBatches(repository, cancellationToken);
                _logger.LogInformation("Full recalculation: processing {Count} movies", moviesToUpdate.Count);
            }
            else
            {
                moviesToUpdate = await repository.GetMoviesWithNewRatingsAsync(_lastRun, cancellationToken);
                _logger.LogInformation("Incremental update: processing {Count} movies with new ratings", moviesToUpdate.Count);
            }

            if (!moviesToUpdate.Any())
            {
                _logger.LogInformation("No movies to update");
                return;
            }

            const int batchSize = 1000;
            for (int i = 0; i < moviesToUpdate.Count; i += batchSize)
            {
                var batch = moviesToUpdate.Skip(i).Take(batchSize)
                    .Select(m => new MovieRatingUpdate(m.MovieId, m.AverageRating, m.RatingCount))
                    .ToList();

                await repository.UpdateMovieRatingsAsync(batch, cancellationToken);

                _logger.LogDebug("Updated batch {BatchNumber}/{TotalBatches}",
                    (i / batchSize) + 1,
                    (moviesToUpdate.Count + batchSize - 1) / batchSize);
            }

            _logger.LogInformation("Completed movie ratings calculation for {Count} movies", moviesToUpdate.Count);
        }

        private async Task<List<MovieRatingData>> GetAllMoviesInBatches(IMovieRepository repository, CancellationToken cancellationToken)
        {
            const int batchSize = 5000;
            var allMovies = new List<MovieRatingData>();
            var totalCount = await repository.GetMoviesCountAsync(cancellationToken: cancellationToken);

            for (int skip = 0; skip < totalCount; skip += batchSize)
            {
                var batch = await repository.GetAllMoviesRatingDataAsync(skip, batchSize, cancellationToken);
                allMovies.AddRange(batch);

                if (cancellationToken.IsCancellationRequested)
                    break;
            }

            return allMovies;
        }
    }
}
