using Microsoft.AspNetCore.Mvc;
using CineZarAPI.Models;
using Newtonsoft.Json;

namespace CineZarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AsientoController : ControllerBase
    {
        private static List<Asiento> asientos = new List<Asiento>();


        [HttpGet]
        public ActionResult<IEnumerable<Asiento>> GetAsientos()
        {
            return Ok(asientos);
        }

        [HttpGet("{id}")]
        public ActionResult<Asiento> GetAsiento(int id)
        {
            Asiento asiento = asientos.FirstOrDefault(a => a.Id == id);
            if (asiento == null)
            {
                return NotFound();
            }
            return Ok(asiento);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAsiento(int id, bool pComprado)
        {
            Asiento asiento = asientos.FirstOrDefault(a => a.Id == id);
            if (asiento == null)
            {
                return NotFound();
            }
            else if (pComprado == false)
            {
                return BadRequest("No se puede devolver una entrada");
            }
            else if(asiento.Comprado == true)
            {
                return BadRequest("Este asiento ya está comprado");
            }
            asiento.Comprado = pComprado;

            return NoContent();
        }


        public static void InicializarDatos()
        {
            char[] Letras = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

            for (int y = 0; y < Constantes.Columnas; y++)
            {
                for (int x = 0; x < Constantes.Filas; x++)
                {
                    asientos.Add(new Asiento(Letras[y], x + 1));
                }
            }
        }
    }
}