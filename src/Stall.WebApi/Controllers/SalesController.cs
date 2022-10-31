using Microsoft.AspNetCore.Mvc;
using Stall.BusinessLogic;
using Stall.BusinessLogic.Dtos;

namespace Stall.WebApi.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService ?? throw new ArgumentNullException(nameof(salesService));
        }

        [HttpGet("all")]
        public IEnumerable<SaleDto> Get()
        {
            return _salesService.GetAllSales();
        }
    }
}