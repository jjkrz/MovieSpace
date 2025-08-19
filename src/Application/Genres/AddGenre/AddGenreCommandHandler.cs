using Application.Abstractions;
using Domain.Common;
using Domain.Movies;
using MediatR;

namespace Application.Genres.AddGenre
{
    public sealed class AddGenreCommandHandler: ICommandHandler<AddGenreCommand, AddGenreResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenreRepository _genreRepo;

        public AddGenreCommandHandler(IUnitOfWork unitOfWork, IGenreRepository genreRepo)
        {
            _unitOfWork = unitOfWork;
            _genreRepo = genreRepo;
        }

        async Task<Result<AddGenreResponse>> IRequestHandler<AddGenreCommand, Result<AddGenreResponse>>.Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            var genreExits = await _genreRepo.ExistsWithName(request.GenreName);

            if (genreExits)
            {
                return Result.Failure<AddGenreResponse>(GenreApplicationErrors.GenreAlreadyExists(request.GenreName));
            }

            var genre = Genre.CreateGenre(request.GenreName);

            if (genre.IsFailure)
            {   
                return Result.Failure<AddGenreResponse>(genre.Error);
            }

            await _genreRepo.CreateAsync(genre.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new AddGenreResponse(genre.Value.Id));
        }
    }
}
