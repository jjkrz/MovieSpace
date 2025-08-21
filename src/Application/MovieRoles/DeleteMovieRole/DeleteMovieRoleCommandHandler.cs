using Application.Abstractions;
using Domain.Common;

namespace Application.MovieRoles.DeleteMovieRole
{
    public class DeleteMovieRoleCommandHandler : ICommandHandler<DeleteMovieRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRoleRepository _repository;

        public DeleteMovieRoleCommandHandler(IUnitOfWork unitOfWork, IMovieRoleRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteMovieRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (role == null)
                return Result.Failure(MovieRolesApplicationErrors.MovieRoleNotFound(request.Id));

            _repository.Delete(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(role.Id);
        }
    }
}


