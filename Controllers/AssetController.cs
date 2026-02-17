using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance___Work_Orders_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var assets = _assetService.GetAllAssets();
            return Ok(assets);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var asset = _assetService.GetAssetById(id);
            if (asset == null)
            {
                return NotFound();
            }

            return Ok(asset);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateAssetRequest asset)
        {
            _assetService.CreateAsset(asset);
            return StatusCode(StatusCodes.Status201Created, asset);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateAssetRequest asset)
        {
            var existingAsset = _assetService.GetAssetById(id);
            if (existingAsset == null)
            {
                return NotFound();
            }

            _assetService.UpdateAsset(id, asset);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existingAsset = _assetService.GetAssetById(id);
            if (existingAsset == null)
            {
                return NotFound();
            }

            _assetService.DeleteAsset(id);
            return NoContent();
        }
    }
}
