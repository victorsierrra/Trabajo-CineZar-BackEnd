using Microsoft.AspNetCore.Mvc;
using CineZarAPI.Models;
using Newtonsoft.Json;

namespace CineZarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EntradaController : ControllerBase
    {
        public static List<Entrada> entradas = new List<Entrada>();


        [HttpGet]
        public ActionResult<IEnumerable<Entrada>> GetEntradas()
        {
            return Ok(entradas);
        }

        [HttpGet("{id}")]
        public ActionResult<Entrada> GetEntrada(int id)
        {
            Entrada entrada = entradas.FirstOrDefault(e => e.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }
            return Ok(entrada);
        }
        
        /*
        [HttpPut("{id}")]
        public IActionResult UpdateEntrada(int id, bool pComprado)
        {
            Entrada entrada = entradas.FirstOrDefault(e => e.Id == id);
            if (entrada == null)
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

        */
        /*
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
        */
    }
}