namespace CineZarAPI.Models;
using CineZarAPI.Controllers;

public class Sesion
{
    public int Id { get; private set; } = 0;
    private static int Identificador { get; set; } = 0;
    public List<Entrada> Entradas { get; set; }
    public List<Asiento> Asientos { get; set; }
    public int NumeroSala { get; set; } = 1;
    public DateTime HoraSesion { get; set; } = DateTime.Now;
    public double precioEntrada {get; set;} = 0;
    int precioFinSemana = 0;

    public Sesion(DateTime horaSesion, int numeroSala, double precioSesion)
    {
        Identificador++;
        NumeroSala = numeroSala;
        Id = Identificador;

        precioEntrada = precioSesion - getDescuentos(horaSesion);;
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
        HoraSesion = horaSesion;

    }
    private double getDescuentos(DateTime dia)
    {
        double DescuentoDiaEspectador = 3;
        double DescuentoDiaEntreSemana = 2; 
        string diaSemanaSesion = dia.DayOfWeek.ToString().ToLower();
        if(!checkFinde(diaSemanaSesion))
        {
            if(checkDiaEspectador(diaSemanaSesion))
            {
                return DescuentoDiaEspectador;
            }
            else return DescuentoDiaEntreSemana;
        }
        else return 0;
    }
        private bool checkFinde(string DayOfWeek)
    {
        List<string> diasFinde = ["friday", "saturday", "sunday"];
        if (diasFinde.Contains(DayOfWeek))
        {
            return true;
        }
        else return false;
    }

    private bool checkDiaEspectador(string DayOfWeek)
    {
        if(DayOfWeek == "wednesday")
        {
            return true;
        }
        return false;
    }
}