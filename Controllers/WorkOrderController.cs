using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Maintenance___Work_Orders_API.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        [HttpGet("{id}")]
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
            return CreatedAtAction(nameof(Get),workOrder);
        }
        [HttpPut("{id}/status")]
        public IActionResult UpdateWorkOrderStatus(int id, string status)
        { 
            _workOrderService.UpdateWorkOrderStatus(id, status);
            return NoContent();
        }
        [HttpPost("{id}/logs")]
        public IActionResult AddLogMessage(int id, string message)
        {
            _workOrderService.AddLogMessage(id, message);
            return NoContent();
        }
    }
}
