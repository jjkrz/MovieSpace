using Application.Abstractions;
using Domain.Common;

namespace Application.MoviePeople.UpdateMoviePerson
{
    public class UpdateMoviePersonCommandHandler : ICommandHandler<UpdateMoviePersonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMoviePersonRepository _repository;

        public UpdateMoviePersonCommandHandler(IUnitOfWork unitOfWork, IMoviePersonRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateMoviePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _repository.GetByIdAsync(request.Id, cancellationToken);
            
            if (person == null)
                return Result.Failure(new Error(ErrorType.NotFound, $"Movie person {request.Id} not found"));

            person.Update(request.FirstName, request.LastName);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

