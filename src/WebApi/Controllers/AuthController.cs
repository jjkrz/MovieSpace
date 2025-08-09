using Application.Users.Login;
using Application.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests;

namespace WebApi.Controllers
{

    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var result = await mediator.Send(new RegisterUserCommand(request.UserName, request.Email, request.Password));

            return result.IsSuccess ?
                Ok(result.Value) :
                BadRequest(result.Error);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var result = await mediator.Send(new LoginUserCommand(request.Email, request.Password));
            
            return result.IsSuccess ?
                Ok(result.Value) :
                BadRequest(result.Error);
        }
    }
}
