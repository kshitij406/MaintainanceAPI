using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance___Work_Orders_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var drivers = _driverService.GetAllDrivers();
            return Ok(drivers);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var driver = _driverService.GetDriverById(id);
            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDriverRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var createdDriver = _driverService.CreateDriver(request);

            return CreatedAtAction(nameof(GetById), new { id = createdDriver.Id }, createdDriver);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] UpdateDriverRequest request)
        {
            var existingDriver = _driverService.GetDriverById(id);
            if (existingDriver == null)
            {
                return NotFound();
            }

            _driverService.UpdateDriver(id, request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existingDriver = _driverService.GetDriverById(id);
            if (existingDriver == null)
            {
                return NotFound();
            }

            _driverService.DeleteDriver(id);
            return NoContent();
        }

    }
}