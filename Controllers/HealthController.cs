using Microsoft.AspNetCore.Mvc;
using Maintenance___Work_Orders_API.Infrastructure.DB;

namespace Maintenance___Work_Orders_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public HealthController(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using var connection = _dbConnectionFactory.CreateConnection();
                connection.Open();
                return Ok(new { status = "Connection Successfull" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = "Couldn't connect to the database" });
            }
        }
    }
}
