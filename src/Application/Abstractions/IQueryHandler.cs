using Domain.Common;
using MediatR;

namespace Application.Abstractions
{
    public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, Result>
        where TQuery : IQuery
    {
    }

    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
        where TQuery : IQuery<TResult>
    {
    }
}
