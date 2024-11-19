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
                Titulo = "¡Gran descuento en cine!",
                Descripcion = "Obtén un 50% de descuento en tu próxima película.",
                ImagenUrl = "https://spaziocines.es/wp-content/uploads/2023/01/Diseno-sin-titulo-5.png"
            },
            new Oferta
            {
                Id = 2,
                Titulo = "Promo 2x1",
                Descripcion = "Compra un boleto y recibe otro gratis.",
                ImagenUrl = "https://spaziocines.es/wp-content/uploads/2023/01/Diseno-sin-titulo-5.png"
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
