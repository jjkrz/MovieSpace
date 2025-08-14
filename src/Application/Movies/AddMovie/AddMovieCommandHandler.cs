using Application.Abstractions;
using Domain.Common;
using Domain.Movies;

namespace Application.Movies.AddMovie
{
    public class AddMovieCommandHandler : ICommandHandler<AddMovieCommand, AddMovieResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRepository _movieRepo;

        public AddMovieCommandHandler(IUnitOfWork unitOfWork, IMovieRepository movieRepo)
        {
            _unitOfWork = unitOfWork;
            _movieRepo = movieRepo;
        }

        public async Task<Result<AddMovieResponse>> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = Movie.CreateMovie(
                request.Title,
                request.Description,
                request.PosterUri,
                request.Duration,
                request.ReleaseDate);

            if (movie.IsFailure)
                return Result.Failure<AddMovieResponse>(movie.Error);

            await _movieRepo.CreateAsync(movie.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new AddMovieResponse
            {
                Id = movie.Value.Id
            });
        }
    }

}
