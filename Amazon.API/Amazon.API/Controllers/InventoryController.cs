using Amazon.API.EC;
using Amazon.Library.DTO;
using Amazon.MAUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<ItemDTO>> Get()
        {
            return await new InventoryEC().Get();
        }

        [HttpPost()]
        public async Task<ItemDTO> AddOrUpdate([FromBody] ItemDTO i)
        {
            return await new InventoryEC().AddOrUpdate(i);
        }

        [HttpDelete("/{id}")]
        public async Task<ItemDTO?> Delete(int id)
        {
            return await new InventoryEC().Delete(id);
        }

        [HttpPut()]
        public async Task<ItemDTO?> AddESC([FromBody] ItemDTO i)
        {
            return await new InventoryEC().AddESC(i);
        }

    }
}
