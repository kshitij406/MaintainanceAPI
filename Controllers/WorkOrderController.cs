using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance___Work_Orders_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {
        private readonly IWorkOrderService _workOrderService;

        public WorkOrderController(IWorkOrderService workOrderService)
        {
            _workOrderService = workOrderService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var workOrders = _workOrderService.GetAllWorkOrders();
            return Ok(workOrders);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var workOrder = _workOrderService.GetWorkOrderById(id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return Ok(workOrder);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateWorkOrderRequest workOrder)
        {
            _workOrderService.CreateWorkOrder(workOrder);
            return StatusCode(StatusCodes.Status201Created, workOrder);
        }

        [HttpPut("{id:int}/status")]
        public IActionResult UpdateWorkOrderStatus(int id, [FromBody] UpdateWorkOrderStatusRequest request)
        {
            var existingWorkOrder = _workOrderService.GetWorkOrderById(id);
            if (existingWorkOrder == null)
            {
                return NotFound();
            }

            _workOrderService.UpdateWorkOrderStatus(id, request.Status);
            return NoContent();
        }

        [HttpPost("{id:int}/logs")]
        public IActionResult AddLogMessage(int id, [FromBody] AddWorkOrderLogRequest request)
        {
            var existingWorkOrder = _workOrderService.GetWorkOrderById(id);
            if (existingWorkOrder == null)
            {
                return NotFound();
            }

            _workOrderService.AddLogMessage(id, request.Message);
            return NoContent();
        }
    }
}
