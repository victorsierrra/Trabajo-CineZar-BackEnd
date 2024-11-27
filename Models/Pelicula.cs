namespace CineZarAPI.Models;

using System.Linq;
using CineZarAPI.Controllers;

public class Pelicula
{
    public int Id { get; private set; } = 0;
    public string? Titulo { get; set; }
    public string? Sinopsis { get; set; }
    public string? Director { get; set; }
    public int Duracion { get; set; } = 0;
    public string? Portada { get; set; }
    public string? Genero { get; set; }
    public List<Sesion> sesiones { get; set; } = new List<Sesion>();
    public int Estreno { get; set; } = 0;
    int precioFinSemana = 0;

    private static int Identificador { get; set; } = 0;
    public Pelicula(string pTitulo, string pSinopsis, string pDirector, int pDuracion, string pPortada, string pGenero, int pEstreno, int NumeroSala)
    {
        Identificador++;
        Id = Identificador;
        Duracion = pDuracion;
        Titulo = pTitulo;
        Sinopsis = pSinopsis;
        Director = pDirector;
        Portada = pPortada;
        Genero = pGenero;
        Estreno = pEstreno;

        sesiones.Add(new Sesion(DateTime.Today.AddHours(14).AddMinutes(15), NumeroSala, 4.5 + precioFinSemana));
        sesiones.Add(new Sesion(DateTime.Today.AddHours(17).AddMinutes(15), NumeroSala, 5 + precioFinSemana));
        sesiones.Add(new Sesion(DateTime.Today.AddHours(20).AddMinutes(15), NumeroSala, 5.50 + precioFinSemana));
        for (int i = 1; i < 7; i++)
        {
            sesiones.Add(new Sesion(DateTime.Today.AddHours(16).AddMinutes(15).AddDays(i), NumeroSala, 4.5 + precioFinSemana));
            sesiones.Add(new Sesion(DateTime.Today.AddHours(19).AddMinutes(15).AddDays(i), NumeroSala, 5 + precioFinSemana));
            sesiones.Add(new Sesion(DateTime.Today.AddHours(22).AddMinutes(15).AddDays(i), NumeroSala, 5.50 + precioFinSemana));

        }

        SesionController.Sesiones.AddRange(sesiones);


    }

}
