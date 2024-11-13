namespace CineZarAPI.Models;

public class Asiento
{
    public char fila { get; set; }
    public int Id {get; private set;} = 0;
    private static int Identificador {get; set;} = 0;
    public int Numero { get; set; }
    public bool Comprado { get; set; } = false;
    public Asiento(char pFila, int pNumero)
    {
        fila = pFila;
        Numero = pNumero;
        Comprado = false;
        Identificador++;
        Id = Identificador;
    }
}