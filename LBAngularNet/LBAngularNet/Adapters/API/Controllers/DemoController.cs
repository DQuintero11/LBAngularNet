using LBAngularNet.Core.Domain.Entities;
using LBAngularNet.Core.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace LBAngularNet.Adapters.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : Controller
    {

        private readonly _TestElasticSearch _elasticSearchService; 

        public DemoController(_TestElasticSearch elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        [HttpPost]
        public async Task<IActionResult> IndexarDemo([FromBody] Demo demo)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(demo.Id.ToString()))
                    return BadRequest(new { Message = "El id no puede estar vacio" });

                await _elasticSearchService.IndexarDemo(demo);
                return CreatedAtAction(nameof(demo), new { id = demo.Id - 1 }, new { id = demo.Id - 1, name = demo.Name });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error Interno del servidor", Error = ex.Message });
            }

        }


        [HttpGet("buscar/{nombre}")]
        public async Task<IActionResult> BuscarDemo(string nombre)
        {
            try
            {
                var demos = await _elasticSearchService.BuscarDemo(nombre);
                return Ok(demos);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "Error Interno del servidor", Error = ex.Message });
            }

        }



        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDemo(int id)
        {
            var demos = await _elasticSearchService.ObtenerDemo(id);
            if (demos == null) return NotFound();
            return Ok(demos);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDemo(int id)
        {
            bool Eliminado = await _elasticSearchService.EliminarDemo(id);
            if (!Eliminado) return NotFound();
            return Ok(new { Mensaje = "Demo Eliminado" });
        }


    }
}
