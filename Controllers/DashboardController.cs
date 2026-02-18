using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance___Work_Orders_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ISqlAssetRepo _assetRepo;
        private readonly ISqlWorkOrderRepo _workOrderRepo;

        public DashboardController(ISqlAssetRepo assetRepo, ISqlWorkOrderRepo workOrderRepo)
        {
            _assetRepo = assetRepo;
            _workOrderRepo = workOrderRepo;
        }

        [HttpGet]
        public IActionResult GetDashboardStats()
        {

            var stats = new DashboardStatsResponse
            {
                TotalAssets = _assetRepo.CountAssets(),
                ActiveAssets = _assetRepo.CountAssetsByStatus("Active"),
                AssetsInMaintenance = _assetRepo.CountAssetsByStatus("Maintenance"),
                OpenWorkOrders = _workOrderRepo.CountOpenWorkOrders(),
                HighPriorityOrders = _workOrderRepo.CountHighPriorityWorkOrders()
            };

            return Ok(stats);
        }
    }
}