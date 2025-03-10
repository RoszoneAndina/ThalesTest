using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThalesBLL.Services;
using ThalesDTO.Models;

namespace ThalesTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService service) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser un número positivo.");

            var item = await service.GeyByID(id);

            if (item == null)
                return NotFound($"No se encontró un producto con ID {id}.");

            return Ok(item);
        }

        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetList()
        {
            var list = await service.GetList();
            return Ok(list);
        }

    }
}
