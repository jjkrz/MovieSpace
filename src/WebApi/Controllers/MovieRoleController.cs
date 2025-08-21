using Application.MovieRoles.AddMovieRole;
using Application.MovieRoles.DeleteMovieRole;
using Application.MovieRoles.GetMovieRoles;
using Application.MovieRoles.UpdateMovieRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Requests;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/movie-roles")]
    public class MovieRoleController : Controller
    {
        private readonly IMediator _mediator;

        public MovieRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddMovieRoleRequest request)
        {
            var result = await _mediator.Send(new AddMovieRoleCommand(request.RoleName));

            return result.Match(onSuccess: Ok);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 50, [FromQuery] string? search = null)
        {
            var result = await _mediator.Send(new GetMovieRolesQuery(page, pageSize, search));
            return result.Match(onSuccess: Ok);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMovieRoleRequest request)
        {
            var result = await _mediator.Send(new UpdateMovieRoleCommand(id, request.RoleName));
            return result.Match(onSuccess: Ok);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteMovieRoleCommand(id));
            return result.Match(onSuccess: Ok);
        }

    }
}
