using Microsoft.AspNetCore.Mvc;
using CineZarAPI.Models;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;

namespace CineZarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SesionController : ControllerBase
    {
        private static List<Sesion> Sesiones = new List<Sesion>();
        private static List<Pelicula> peliculas = new List<Pelicula>();


        [HttpGet]
        public ActionResult<IEnumerable<Sesion>> GetSesiones()
        {
            return Ok(Sesiones);
        }

        [HttpGet("{id}")]
        public ActionResult<Sesion> GetSesion(int id)
        {
            Sesion Sesion = Sesiones.FirstOrDefault(s => s.Id == id);
            if (Sesion == null)
            {
                return NotFound();
            }
            return Ok(Sesion);
        }


        [HttpPost]
        public ActionResult<Sesion> CreateSesion(Sesion Sesion)
        {
            Sesiones.Add(Sesion);
            return CreatedAtAction(nameof(GetSesion), new { id = Sesion.Id }, Sesion);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSesion(int id, Sesion updatedSesion)
        {
            Sesion Sesion = Sesiones.FirstOrDefault(s => s.Id == id);
            if (Sesion == null)
            {
                return NotFound();
            }
            Sesion.Asientos = updatedSesion.Asientos;
            Sesion.pelicula = updatedSesion.pelicula;
            Sesion.NumeroSala = updatedSesion.NumeroSala;
            Sesion.Hora = updatedSesion.Hora;

            return NoContent();
        }


        [HttpPut("ComprarEntrada/{id}")]
        public IActionResult ComprarEntrada(int id, int[] idAsientos)
        {
            Sesion sesion = Sesiones.FirstOrDefault(s => s.Id == id);

            if (sesion == null)
            {
                return NotFound();
            }
            foreach (var _idAsiento in idAsientos)
            {
                Asiento prueba = sesion.Asientos.FirstOrDefault(a => a.Id == _idAsiento);
                if (prueba.Comprado == true)
                {
                    return BadRequest("Uno de los asientos seleccionados no está disponible");
                }
            }
            foreach (int idAsiento in idAsientos)
            {
                Asiento asientoEntrada = sesion.Asientos.FirstOrDefault(a => a.Id == idAsiento);

                int posicion = sesion.Asientos.IndexOf(asientoEntrada);

                if (posicion != -1)
                {
                    if (asientoEntrada == null)
                    {
                        return NotFound();
                    }
                    else if (asientoEntrada.Comprado == true)
                    {
                        return BadRequest("El asiento ya ha sido comprado");
                    }
                }
                else
                {
                    return NotFound();
                }
                asientoEntrada.Comprado = true;
                Entrada entrada = new Entrada(asientoEntrada, 4.50);
                sesion.Asientos[posicion] = asientoEntrada;
                sesion.Entradas.Add(entrada);
            }


            //EnviarEntrada();

            return Ok(sesion.Entradas);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSesion(int id)
        {
            Sesion sesion = Sesiones.FirstOrDefault(s => s.Id == id);
            if (sesion == null)
            {
                return NotFound();
            }
            Sesiones.Remove(sesion);
            return NoContent();
        }

        public static void InicializarDatos()
        {
            Pelicula Cars = new Pelicula("Cars", "El aspirante a campeón de carreras Rayo McQueen parece que está a punto de conseguir el éxito. Su actitud arrogante se desvanece cuando llega a una pequeña comunidad olvidada que le enseña las cosas importantes de la vida que había olvidado.",
    "Brian Free", 117, "https://es.web.img2.acsta.net/pictures/14/05/28/11/24/435900.jpg", "Animación, Infantil, Familiar, Acción, Comedia", 2006);

            Pelicula Torrente = new Pelicula("Torrente, El brazo tonto de la ley", "Torrente es un policía español, machista, racista y alcohólico. Este magnífico representante de las fuerzas del orden vive, con su padre hemipléjico, en Madrid. Gracias a su olfato, descubre en su propio barrio una importante red de narcotraficantes.",
                "Santiago Segura", 97, "https://play-lh.googleusercontent.com/jOSN3SUTJStEHHKBNZ8Hidy_ZTsW8eiOYE30BYh7jWxKPK-RcoGyZFKbTZjezSZSwfsY", "Acción, Comedia, Suspense, Comedia negra", 1998);

            Pelicula MientrasDureLaGuerra = new Pelicula("Mientras dure la guerra", "España, 1936. El célebre escritor Miguel de Unamuno decide apoyar públicamente la sublevación militar. Inmediatamente es destituido por el gobierno republicano como rector de la Universidad de Salamanca. Mientras, el general Franco consigue sumar sus tropas al frente sublevado e inicia una exitosa campaña.",
                "Alejandro Amenábar", 107, "https://pics.filmaffinity.com/Mientras_dure_la_guerra-641777203-large.jpg", "Acción, Drama, Historia, Cine bélico", 2019);

            Pelicula ProyectoX = new Pelicula("Proyecto X", "Tres amigos del instituto deciden organizar una fiesta salvaje en casa de uno de ellos, aprovechando que sus padres no están. Quieren hacer que la fiesta sea inolvidable, así que deciden grabarlo todo. Parece que la fiesta sea todo un éxito: todo el mundo está bebiendo y los ánimos están por los aires. Sin embargo, una serie de complicaciones imprevistas harán que la fiesta se descontrole.",
                "Nourizadeh Nima", 88, "https://pics.filmaffinity.com/Proyecto_X-393876705-large.jpg", "Comedia, Adolescente", 2012);

            Pelicula Ted = new Pelicula("Ted", "John Bennett y su oso de peluche Ted han sido siempre inseparables, pero su amistad se pondrá a prueba cuando Lori, la novia de John de cuatro años, pida más de su relación.",
                "Seth MacFarlane", 106, "https://i.etsystatic.com/43710319/r/il/fd83b0/6146689600/il_fullxfull.6146689600_olbp.jpg", "Comedia, Fantasía", 2012);

            Pelicula FastAndFurious = new Pelicula("The Fast and the Furious: Tokyo Drift", "Shaun Boswell es un chico rebelde cuya única conexión con el mundo es a través de las carreras ilegales. Cuando la policía le amenaza con encarcelarle, se va a pasar una temporada con su tío, un militar destinado en Japón.",
                "Justin Lin", 104, "https://play-lh.googleusercontent.com/FZHvrDnFWT8Cuc06_mVO72SE8igxA2P5B4DP3Yoa4D1k_-AvuVxIvx0dK7jd9eqTKOSD", "Acción, Crimen, Suspense", 2006);

            Pelicula Resacon = new Pelicula("Resacón en Las Vegas", "Cuatro amigos celebran la despedida de soltero de uno de ellos en Las Vegas. Pero, cuando a la mañana siguiente no pueden encontrar al novio y no recuerdan nada, deberán intentar volver sobre sus pasos, antes de que llegue la hora de la boda.",
                "Todd Phillips", 100, "https://pics.filmaffinity.com/Resacaon_en_Las_Vegas-825442102-large.jpg", "Comedia", 2009);

            Pelicula Purga = new Pelicula("The First Purge", "La crisis social y económica que atenaza a Estados Unidos ha llevado al poder al partido populista Nuevos Padres Fundadores de América y a su discurso del miedo. Una de sus primeras medidas será un experimento: una noche de crimen legalizado en la zona de Staten Island. ¡Que comience la purga!",
                "Gerard McMurray", 98, "https://es.web.img3.acsta.net/pictures/18/06/12/12/08/0619875.jpg", "Terror, Suspense, Ciencia ficción", 2018);



            peliculas.Add(Cars);
            peliculas.Add(Torrente);
            peliculas.Add(MientrasDureLaGuerra);
            peliculas.Add(ProyectoX);
            peliculas.Add(Ted);
            peliculas.Add(FastAndFurious);
            peliculas.Add(Resacon);
            peliculas.Add(Purga);



            Sesion Cars1 = new Sesion(Cars, "16:00", 1);
            Sesion Cars2 = new Sesion(Cars, "19:00", 1);
            Sesion Torrente1 = new Sesion(Torrente, "17:00", 2);
            Sesion Torrente2 = new Sesion(Torrente, "20:00", 2);
            Sesiones.Add(Cars1);
            Sesiones.Add(Cars2);
            Sesiones.Add(Torrente1);
            Sesiones.Add(Torrente2);
        }

        static void EnviarEntrada()
        {
            try
            {
                Sesion sesion = Sesiones.FirstOrDefault(s => s.Id == 1);
                Entrada entrada = sesion.Entradas.Last();
                string to = "a27300@svalero.com";
                string asunto = "Prueba";
                string cuerpo = $"Enhorabuena por adquirir las entradas para ver {sesion.pelicula.Titulo} a las {sesion.Hora}.\n Ha seleccionado sentarse en la fila {entrada.asiento.fila} en el asiento {entrada.asiento.Numero} y tiene que acudir a la sala {sesion.NumeroSala}, se le recomienda llegar con 30 minutos de antelación.\n ¡Que disfrute la experiencia cineZar!";
                string host = "smtp.gmail.com";
                int puerto = 587;
                SmtpClient client = new SmtpClient(host);

                client.Port = puerto;

                client.Credentials = new NetworkCredential(Constantes.CorreoEntradas, Constantes.PasCorreoEntradas);

                MailMessage mensaje = new MailMessage(Constantes.CorreoEntradas, to, asunto, cuerpo);

                mensaje.IsBodyHtml = false;

                client.Send(mensaje);
                Console.WriteLine("Correo enviado correctamente");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }


        }
    }
}