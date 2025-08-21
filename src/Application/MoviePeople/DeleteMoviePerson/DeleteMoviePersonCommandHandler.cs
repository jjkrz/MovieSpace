using Application.Abstractions;
using Domain.Common;

namespace Application.MoviePeople.DeleteMoviePerson
{
    public class DeleteMoviePersonCommandHandler : ICommandHandler<DeleteMoviePersonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMoviePersonRepository _repository;

        public DeleteMoviePersonCommandHandler(IUnitOfWork unitOfWork, IMoviePersonRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteMoviePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (person == null)
                return Result.Failure(new Error(ErrorType.NotFound, $"Movie person {request.Id} not found"));

            _repository.Delete(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(person.Id);
        }
    }
}

