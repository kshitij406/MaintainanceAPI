using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance___Work_Orders_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceScheduleController : ControllerBase
    {
        private readonly IServiceScheduleService _service;

        public ServiceScheduleController(IServiceScheduleService service)
        {
            _service = service;
        }

        [HttpGet("asset/{assetId}")]
        public IActionResult GetByAsset(int assetId)
        {
            return Ok(_service.GetSchedulesByAsset(assetId));
        }

        [HttpGet("overdue")]
        public IActionResult GetOverdue()
        {
            return Ok(_service.GetOverdueServices());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateServiceScheduleRequest request)
        {
            _service.CreateSchedule(request);
            return StatusCode(201);
        }

        [HttpPost("{id}/complete")]
        public IActionResult CompleteService(int id, [FromQuery] int currentOdometer)
        {
            _service.CompleteService(id, currentOdometer);
            return Ok("Service record updated and next due date calculated.");
        }
    }
}