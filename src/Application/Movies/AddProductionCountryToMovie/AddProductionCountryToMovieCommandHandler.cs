using Application.Abstractions;
using Domain.Common;

namespace Application.Movies.AddProductionCountryToMovie
{
    public class AddProductionCountryToMovieCommandHandler : ICommandHandler<AddProductionCountryToMovieCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRepository _movieRepo;
        private readonly IProductionCountryRepository _productionCountryRepo;
        
        public AddProductionCountryToMovieCommandHandler(IUnitOfWork unitOfWork, IMovieRepository movieRepo, IProductionCountryRepository productionCountryRepo)
        {
            _unitOfWork = unitOfWork;
            _movieRepo = movieRepo;
            _productionCountryRepo = productionCountryRepo;
        }
        public async Task<Result> Handle(AddProductionCountryToMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepo.GetByIdWithProductionCountriesAsync(request.MovieId, cancellationToken);

            if (movie == null)
            {
                 return Result.Failure(MovieApplicationErrors.MovieNotFound);
            }

            var country = await _productionCountryRepo.GetByIdAsync(request.ProductionCountryId, cancellationToken);
        
            if (country == null)
            {
                return Result.Failure(Error.NotFound);
            }

            var result = movie.AddProductionCountry(country);

            if (result.IsFailure)
                return Result.Failure(result.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
