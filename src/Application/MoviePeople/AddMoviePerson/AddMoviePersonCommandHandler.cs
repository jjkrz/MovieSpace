using Application.Abstractions;
using Domain.Common;
using Domain.MoviePeople;

namespace Application.MoviePeople.AddMoviePerson
{
    public class AddMoviePersonCommandHandler : ICommandHandler<AddMoviePersonCommand, AddMoviePersonResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMoviePersonRepository _repository;

        public AddMoviePersonCommandHandler(IUnitOfWork unitOfWork, IMoviePersonRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Result<AddMoviePersonResponse>> Handle(AddMoviePersonCommand request, CancellationToken cancellationToken)
        {
            var created = MoviePerson.Create(request.FirstName, request.LastName);
            if (created.IsFailure)
                return Result.Failure<AddMoviePersonResponse>(created.Error);

            await _repository.CreateAsync(created.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new AddMoviePersonResponse(created.Value.Id));
        }
    }
}

