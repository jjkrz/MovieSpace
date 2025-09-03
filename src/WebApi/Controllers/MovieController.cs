using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests;
using Application.Movies.AddMovie;
using WebApi.Extensions;
using Application.Movies.RateMovie;
using Microsoft.AspNetCore.Authorization;
using Application.Movies.GetMovies;
using Application.Movies.AddGenreToMovie;
using Application.Movies.AddCastToMovie;
using Application.Movies.AddProductionCountryToMovie;

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

        [HttpPatch("{movieId}/genres/{genreId}")]
        public async Task<IActionResult> AddGenreToMovie([FromRoute] Guid movieId, [FromRoute] Guid genreId)
        {
            var result = await _mediator.Send(new AddGenreToMovieCommand(movieId, genreId));
            return result.Match(onSuccess: Ok);
        }

        [HttpPatch("{movieId}/cast/{moviePersonId}")]
        public async Task<IActionResult> AddCastToMovie([FromRoute] Guid movieId, [FromRoute] Guid moviePersonId, [FromBody] AddCastRequest request)
        {
            var result = await _mediator.Send(new AddCastCommand(movieId, moviePersonId, request.MovieRoleId, request.CharacterName));

            return result.Match(onSuccess: Ok);
        }

        [HttpPatch("{movieId}/reviews")]
        public async Task<IActionResult> AddReviewToMovie([FromRoute] Guid movieId, [FromBody] AddReviewRequest request)
        {
            var result = await _mediator.Send(new Application.Movies.AddReviewToMovie.AddReviewCommand(movieId, request.Content));
            return result.Match(onSuccess: Ok);
        }

        [HttpPatch("{movieId}/add-production-country/{productionCountryId}")]
        public async Task<IActionResult> AddProductionCountryToMovie([FromRoute] Guid movieId, [FromRoute] Guid productionCountryId)
        {
            var result = await _mediator.Send(new AddProductionCountryToMovieCommand(movieId, productionCountryId));
            return result.Match(onSuccess: Ok);
        }

    }
}
