using Application.Abstractions;
using Domain.Common;
using Domain.MoviePersonality;
using MediatR;

namespace Application.MovieRoles.AddMovieRole
{
    public class AddMovieRoleCommandHandler : ICommandHandler<AddMovieRoleCommand, AddMovieRoleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRoleRepository _movieRoleRepository;
        public AddMovieRoleCommandHandler(IUnitOfWork unitOfWork, IMovieRoleRepository movieRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _movieRoleRepository = movieRoleRepository;
        }

        async Task<Result<AddMovieRoleResponse>> IRequestHandler<AddMovieRoleCommand, Result<AddMovieRoleResponse>>.Handle(AddMovieRoleCommand request, CancellationToken cancellationToken)
        {
            var movieExists = await _movieRoleRepository.ExistsWithName(request.RoleName, cancellationToken);

            if (movieExists)
            {
                return Result.Failure<AddMovieRoleResponse>(MovieRolesApplicationErrors.MovieRoleAlreadyExists(request.RoleName));
            }

            var movieRole = MovieRole.Create(request.RoleName);

            if (movieRole.IsFailure)
            {
                return Result.Failure<AddMovieRoleResponse>(movieRole.Error);
            }

            await _movieRoleRepository.CreateAsync(movieRole.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new AddMovieRoleResponse(movieRole.Value.Id));

        }
    }
}
