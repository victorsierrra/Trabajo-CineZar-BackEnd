namespace CineZarAPI.Models;

public class Sesion
{
    public Pelicula pelicula { get; set; }
    public int Id { get; private set; } = 0;
    private static int Identificador { get; set; } = 0;
    public List<Entrada> Entradas { get; set; }
    public List<Asiento> Asientos { get; set; }
    public int NumeroSala { get; set; } = 1;
    public string Hora { get; set; } = DateTime.Now.ToString("HH:mm");

    public Sesion(Pelicula peli, string hora, int numeroSala)
    {
        pelicula = peli;
        Identificador++;
        Id = Identificador;
        Entradas = new List<Entrada>();
        Asientos = new List<Asiento>();

        char[] Letras = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
        'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };


        for (int y = 0; y < Constantes.Filas; y++)
        {
            for (int x = 0; x < Constantes.Columnas; x++)
            {
                Asientos.Add(new Asiento(Letras[y], x + 1));
            }
        }


        NumeroSala = numeroSala;
        Hora = hora;

    }
}