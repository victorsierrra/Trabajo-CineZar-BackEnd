namespace CineZarAPI.Models;

public class Entrada
{
    public int Id { get; private set; } = 0;
    private static int Identificador { get; set; } = 0;
    public Asiento asiento { get; set; }
    public double Precio {get; set;} = 0;
    public DateTime fechaCompra {get; set;}
    public Entrada (Asiento _asiento, double _precio)
    {
        Identificador ++;
        Id = Identificador;
        asiento = _asiento;
        Precio = _precio;
        fechaCompra = DateTime.Now;
    }
}