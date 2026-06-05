using Application.Sessions.CreateSession;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("api/sessions")]
    public class SessionController : Controller
    {
        private readonly IMediator _mediator;
        public SessionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] CreateSessionCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(onSuccess: Ok);
        }
    }
}
