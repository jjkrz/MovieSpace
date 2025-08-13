using Domain.Common;
using MediatR;

namespace Application.Abstractions
{
    public interface ICommandHandler<CommandT> : IRequestHandler<CommandT, Result>
        where CommandT : ICommand
    {

    }

    public interface ICommandHandler<CommandT, ResultT> : IRequestHandler<CommandT, Result<ResultT>>
        where CommandT : ICommand<ResultT>
    {

    }
}
