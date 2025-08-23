using Application.Abstractions;
using Domain.Common;

namespace Application.Movies.AddCastToMovie
{
    public class AddCastCommandHandler : ICommandHandler<AddCastCommand, AddCastResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRepository _movieRepo;
        private readonly IMoviePersonRepository _moviePersonRepo;
        private readonly IMovieRoleRepository _movieRoleRepo;
        public AddCastCommandHandler(IUnitOfWork unitOfWork, IMovieRepository movieRepo, IMoviePersonRepository moviePersonRepo, IMovieRoleRepository movieRoleRepo)
        {
            _unitOfWork = unitOfWork;
            _movieRepo = movieRepo;
            _moviePersonRepo = moviePersonRepo;
            _movieRoleRepo = movieRoleRepo;
        }

        public async Task<Result<AddCastResponse>> Handle(AddCastCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepo.GetByIdWithCast(request.MovieId, cancellationToken);

            if (movie is null)
            {
                return Result.Failure<AddCastResponse>(MovieApplicationErrors.MovieNotFound);
            }

            var moviePerson = await _moviePersonRepo.GetByIdAsync(request.MoviePersonId, cancellationToken);

            if (moviePerson is null)
            {
                return Result.Failure<AddCastResponse>(Error.NotFound);
            }

            var movieRole = await _movieRoleRepo.GetByIdAsync(request.MovieRoleId, cancellationToken);

            if (movieRole == null)
            {
                return Result.Failure<AddCastResponse>(Error.NotFound);
            }

            var result = movie.AddCastMember(moviePerson, movieRole, request.CharacterName);

            if (result.IsFailure)
            {
                return Result.Failure<AddCastResponse>(result.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new AddCastResponse(
                movie.Title,
                $"{moviePerson.FirstName} {moviePerson.LastName}",
                movieRole.RoleName,
                request.CharacterName));
        }
    }
}
