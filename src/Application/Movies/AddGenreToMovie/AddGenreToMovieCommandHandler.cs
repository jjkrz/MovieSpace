using Application.Abstractions;
using Domain.Common;
using Domain.Movies;

namespace Application.Movies.AddGenreToMovie
{
    public class AddGenreToMovieCommandHandler : ICommandHandler<AddGenreToMovieCommand>
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IRepository<Genre> _genreRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AddGenreToMovieCommandHandler(IMovieRepository movieRepo, IRepository<Genre> genreRepo, IUnitOfWork unitOfWork)
        {
            _movieRepo = movieRepo;
            _genreRepo = genreRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddGenreToMovieCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepo.GetByIdAsync(request.genreId, cancellationToken: cancellationToken);

            if (genre is null)
            {
                return Result.Failure(MovieApplicationErrors.GenreNotFound);
            }

            var movie = await _movieRepo.GetByIdWithGenres(request.movieId, cancellationToken);

            if (movie is null)
            {
                return Result.Failure(MovieApplicationErrors.MovieNotFound);
            }

            var result = movie.AddGenre(genre);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
