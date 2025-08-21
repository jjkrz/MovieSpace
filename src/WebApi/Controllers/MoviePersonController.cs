
using Application.MoviePeople.AddMoviePerson;
using Application.MoviePeople.DeleteMoviePerson;
using Application.MoviePeople.GetMoviePeople;
using Application.MoviePeople.UpdateMoviePerson;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Requests;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/movie-people")]
    public class MoviePersonController : Controller
    {
        private readonly IMediator _mediator;

        public MoviePersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddMoviePersonRequest request)
        {
            var result = await _mediator.Send(new AddMoviePersonCommand(request.FirstName, request.LastName));
            return result.Match(onSuccess: Ok);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 50, [FromQuery] string? search = null)
        {
            var result = await _mediator.Send(new GetMoviePeopleQuery(page, pageSize, search));
            return result.Match(onSuccess: Ok);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMoviePersonRequest request)
        {
            var result = await _mediator.Send(new UpdateMoviePersonCommand(id, request.FirstName, request.LastName));
            return result.Match(onSuccess: Ok);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteMoviePersonCommand(id));
            return result.Match(onSuccess: Ok);
        }
    }
}

