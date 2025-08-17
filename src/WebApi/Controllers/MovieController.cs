using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests;
using Application.Movies.AddMovie;
using WebApi.Extensions;
using Application.Movies.RateMovie;
using Microsoft.AspNetCore.Authorization;
using Application.Movies.GetMovies;
using Domain.Common;

namespace WebApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] AddMovieRequest request)
        {
            var result = await _mediator.Send(new AddMovieCommand(request.Title, request.Description, request.PosterUri, request.Duration, request.ReleaseDate));

            return result.Match(onSuccess: Ok);
        }

        [Authorize]
        [HttpPost("{movieId}/rate")]
        public async Task<IActionResult> RateMovie([FromRoute] Guid movieId, [FromBody] RateMovieRequest reqeust)
        {
            var result = await _mediator.Send(new RateMovieCommand(movieId, reqeust.Score));

            return result.Match(onSuccess: Ok);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _mediator.Send(new GetMoviesQuery(page, pageSize));

            return result.Match(onSuccess: Ok);
        }
    }
}
