using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stall.BusinessLogic.Handlers.Commands;
using Stall.BusinessLogic.Handlers.Queries;

namespace Stall.WebApi.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllSalesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddSaleCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (result.Error)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}