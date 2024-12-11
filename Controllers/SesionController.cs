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
        public static List<Sesion> Sesiones = new List<Sesion>();


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
            Sesion.NumeroSala = updatedSesion.NumeroSala;
            Sesion.HoraSesion = updatedSesion.HoraSesion;

            return NoContent();
        }

        [HttpPut("{id}/DevolverEntrada")]
        public IActionResult DevolverEntrada(int id, int[] idAsientos)
        {
            Sesion sesion = Sesiones.FirstOrDefault(s => s.Id == id);

            if (sesion == null)
            {
                return NotFound();
            }
            foreach (var _idAsiento in idAsientos)
            {
                Asiento prueba = sesion.Asientos.FirstOrDefault(a => a.Id == _idAsiento);
                if (prueba.Comprado == false)
                {
                    return BadRequest("Uno de los asientos seleccionados no está comprado");
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
                    else if (asientoEntrada.Comprado == false)
                    {
                        return BadRequest("El asiento no ha sido comprado");
                    }
                }
                else
                {
                    return NotFound();
                }
                Entrada entrada = sesion.Entradas.FirstOrDefault(en => en.asiento == asientoEntrada);
                sesion.Entradas.Remove(entrada);
                asientoEntrada.Comprado = false;
                sesion.Asientos[posicion] = asientoEntrada;
            }

            return Ok(sesion.Asientos);
        }

        [HttpPut("{id}/ComprarEntrada")]
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
                Entrada entrada = new Entrada(asientoEntrada, sesion.precioEntrada);
                sesion.Asientos[posicion] = asientoEntrada;
                sesion.Entradas.Add(entrada);
            }


            //EnviarEntrada(id);

            EntradaController.entradas.AddRange(sesion.Entradas);

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
        }/*

        static void EnviarEntrada(int idSesion)
        {
            try
            {
                Sesion sesion = Sesiones.FirstOrDefault(s => s.Id == idSesion);
                Entrada entrada = sesion.Entradas.Last();
                string to = "a27300@svalero.com";
                string asunto = "Prueba";
                string cuerpo = $"Enhorabuena por adquirir las entradas para ver {sesion} a las {sesion.HoraSesion}.\n Ha seleccionado sentarse en la fila {entrada.asiento.fila} en el asiento {entrada.asiento.Numero} y tiene que acudir a la sala {sesion.NumeroSala}, se le recomienda llegar con 30 minutos de antelación.\n ¡Que disfrute la experiencia cineZar!";
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


        }*/
    }
}