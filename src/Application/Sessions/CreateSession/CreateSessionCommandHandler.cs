using Application.Abstractions;
using Application.Services;
using Domain.Common;
using Domain.Sessions;
using MediatR;

namespace Application.Sessions.CreateSession
{
    public class CreateSessionCommandHandler : ICommandHandler<CreateSessionCommand, CreateSessionResponse>
    {
        private readonly ISessionRepository _sessionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSessionCommandHandler(ISessionRepository sessionRepo, IUnitOfWork unitOfWork)
        {
            _sessionRepo = sessionRepo;
            _unitOfWork = unitOfWork;
        }

        async Task<Result<CreateSessionResponse>> IRequestHandler<CreateSessionCommand, Result<CreateSessionResponse>>.Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            for (int i = 0; i < 3; i++)
            {
                var sessionResult = Session.CreateSession(request.DisplayName, AccessCodeGenerator.GenerateUniqueAccessCode());

                if (sessionResult.IsFailure)
                    return Result.Failure<CreateSessionResponse>(sessionResult.Error);

                await _sessionRepo.CreateAsync(sessionResult.Value, cancellationToken);

                try
                {
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return Result.Success(
                        new CreateSessionResponse(sessionResult.Value.Id, sessionResult.Value.AccessCode));
                }
                catch (DuplicateAccessCodeException ex)
                {
                    continue;
                }
            }

            return Result.Failure<CreateSessionResponse>(Error.Conflict);
        }
    }
}
