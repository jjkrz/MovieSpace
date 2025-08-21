using Application.Abstractions;
using Domain.Common;

namespace Application.MovieRoles.UpdateMovieRole
{
    public class UpdateMovieRoleCommandHandler : ICommandHandler<UpdateMovieRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRoleRepository _repository;

        public UpdateMovieRoleCommandHandler(IUnitOfWork unitOfWork, IMovieRoleRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateMovieRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (role == null)
                return Result.Failure(MovieRolesApplicationErrors.MovieRoleNotFound(request.Id));

            var updateResult = role.Update(request.RoleName);
            if (updateResult.IsFailure)
                return Result.Failure(updateResult.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}


