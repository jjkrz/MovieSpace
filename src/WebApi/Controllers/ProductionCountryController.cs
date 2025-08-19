using Application.ProductionCountries.AddProductionCountry;
using Application.ProductionCountries.DeleteProductionCountry;
using Application.ProductionCountries.GetProductionCountries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Requests;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/production-countries")]
    public class ProductionCountryController : Controller
    {
        private readonly IMediator _mediator;

        public ProductionCountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductionCountry([FromBody] AddProductionCountryRequest request)
        {
            var result = await _mediator.Send(new AddProductionCountryCommand(request.Name));

            return result.Match(onSuccess: Ok);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductionCountries([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var result = await _mediator.Send(new GetProductionCountriesQuery(page, pageSize));

            return result.Match(onSuccess: Ok);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionCountry([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteProductionCountryCommand(id));

            return result.Match(onSuccess: Ok);
        }
    }
}


