using Application.Genres.AddGenre;
using Application.Genres.GetGenres;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Requests;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/genres")]
    public class GenreController : Controller
    {
        private readonly IMediator _mediator;
        public GenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] AddGenreRequest request)
        {
            var result = await _mediator.Send(new AddGenreCommand(request.GenreName));
            
            return result.Match(onSuccess: Ok);
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var result = await _mediator.Send(new GetGenresQuery(page, pageSize));

            return result.Match(onSuccess: Ok);
        }

    }
}
