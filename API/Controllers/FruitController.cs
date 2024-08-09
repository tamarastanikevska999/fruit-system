using API.DTO;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/fruits")]
    public class FruitsController : ControllerBase
    {
        private readonly IFruitService _fruitService;

        public FruitsController(IFruitService fruitService)
        {
            _fruitService = fruitService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllFruits()
        {
            try
            {
                var fruits = await _fruitService.GetAllFruitsAsync();
                return Ok(fruits);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetFruit(string name)
        {
            try
            {
                var fruit = await _fruitService.GetFruitByNameAsync(name);
                if (fruit == null)
                {
                    return NotFound("Fruit not found.");
                }
                return Ok(fruit);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{name}/metadata")]
        public async Task<IActionResult> AddMetadata(string name, [FromBody] MetadataDto metadata)
        {
            try
            {
                await _fruitService.AddMetadataAsync(name, metadata.Key, metadata.Value);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{name}/metadata/{key}")]
        public async Task<IActionResult> RemoveMetadata(string name, string key)
        {
            try
            {
                await _fruitService.RemoveMetadataAsync(name, key);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{name}/metadata")]
        public async Task<IActionResult> UpdateMetadata(string name, [FromBody] MetadataDto metadata)
        {
            try
            {
                await _fruitService.UpdateMetadataAsync(name, metadata.Key, metadata.Value);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
