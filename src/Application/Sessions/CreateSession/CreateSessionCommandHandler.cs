using Application.Abstractions;
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
            for (int i = 0; i < 3; i++) // three chances to avoid duplicate session key to avoid collision
            {
                var session = Session.CreateSession(request.DisplayName);

                if (session.IsFailure)
                {
                    return Result.Failure<CreateSessionResponse>(session.Error);
                }

                await _sessionRepo.CreateAsync(session.Value, cancellationToken);
                
                try {
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("duplicate key value violates unique constraint"))
                    {
                        continue; // try again with a new session key
                    }
                    return Result.Failure<CreateSessionResponse>(new Error("An error occurred while creating the session."));
                }
            }
            
            return Result.Success(new CreateSessionResponse());
        }
    }
}
