namespace CineZarAPI.Models;

public class Pelicula
{
    public int Id { get; private set; } = 0;
    public string? Titulo { get; set; }
    public string? Sinopsis {get; set;}
    public string? Director { get; set; }
    public int Duracion {get; set;} = 0;
    public string? Portada {get; set;}
    public string? Genero {get; set;}
    public int Estreno {get; set;}

    private static int Identificador {get; set;} = 0;
    public Pelicula(string pTitulo, string pSinopsis, string pDirector, int pDuracion, string pPortada, string pGenero, int pEstreno)
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
        
    }
}