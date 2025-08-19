using Application.Abstractions;
using Domain.Common;

namespace Application.Genres.DeleteGenre
{
    public class DeleteGenreCommandHandler : ICommandHandler<DeleteGenreCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenreRepository _genreRepo;

        public DeleteGenreCommandHandler(IUnitOfWork unitOfWork, IGenreRepository genreRepo)
        {
            _unitOfWork = unitOfWork;
            _genreRepo = genreRepo;
        }

        public async Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepo.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

            if (genre == null)
            {
                return Result.Failure(GenreApplicationErrors.GenreNotFound(request.Id));
            }

            _genreRepo.Delete(genre);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(genre.Id);
        }
    }
}
