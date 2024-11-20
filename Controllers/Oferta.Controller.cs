// Controllers/OfertaController.cs
using Microsoft.AspNetCore.Mvc;
namespace CineZarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase
    {
        // Lista estática de ofertas (puedes cambiarla por un repositorio o base de datos real)
        private static List<Oferta> ofertas = new List<Oferta>
        {
            new Oferta
            {
                Id = 1,
                Titulo = "¡Combo Dulce!",
                Descripcion = "Con nuestro combo Dulce tendras nuestras palomitas en su tamaño MAS grande junto a un dulce a elegir y un refresco!!",
                ImagenUrl = "https://i.etsystatic.com/25684300/r/il/34d66c/6008075368/il_fullxfull.6008075368_l9cf.jpg"
            },
            new Oferta
            {
                Id = 2,
                Titulo = "Promo 2x1",
                Descripcion = "Compra uno de nuestros pack´s dulces y llevate el segundo G R A T I S.",
                ImagenUrl = "https://i.etsystatic.com/38396355/r/il/7c3bc3/5411909938/il_fullxfull.5411909938_tmwr.jpg"
            },
            new Oferta
            {
                Id = 2,
                Titulo = "Pack Cine Supreme",
                Descripcion = "Para los mas golosos!!. Con nuestro pack Supreme tendras dos cubos de palomitas junto a tus snacks favoritos!!   .",
                ImagenUrl = "https://i.etsystatic.com/26585998/r/il/ccd262/2809401285/il_570xN.2809401285_pqv6.jpg"
            }
        };

        // Método GET para obtener todas las ofertas
        [HttpGet]
        public ActionResult<List<Oferta>> GetOfertas()
        {
            return Ok(ofertas);
        }
    }
}
