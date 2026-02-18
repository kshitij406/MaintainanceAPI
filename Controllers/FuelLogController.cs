using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance___Work_Orders_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelLogController : ControllerBase
    {
        private readonly IFuelLogService _fuelService;

        public FuelLogController(IFuelLogService fuelService)
        {
            _fuelService = fuelService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_fuelService.GetAllFuelLogs());
        }

        [HttpGet("asset/{assetId}")]
        public IActionResult GetByAsset(int assetId)
        {
            return Ok(_fuelService.GetFuelLogsByAsset(assetId));
        }

        [HttpPost]
        public IActionResult LogFuel([FromBody] CreateFuelLogRequest request)
        {
            _fuelService.LogFuel(request);
            return StatusCode(201);
        }
    }
}