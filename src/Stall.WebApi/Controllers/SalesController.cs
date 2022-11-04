using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Stall.BusinessLogic.Handlers.Commands.Sale;
using Stall.BusinessLogic.Handlers.Queries.Sale;

namespace Stall.WebApi.Controllers;

[ApiController]
[Route("api/sale")]
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

    [HttpGet("dashboard/all")]
    public async Task<IActionResult> GetForDashboard()
    {
        var query = new GetAllSalesDashboardQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
        
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddSaleCommand command)
    {
        var result = await _mediator.Send(command);
            
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Created(HttpContext.Request.GetDisplayUrl(), result);
    }
        
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateSaleCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }
            
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteSaleCommand {SaleId = id};
        var result = await _mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}