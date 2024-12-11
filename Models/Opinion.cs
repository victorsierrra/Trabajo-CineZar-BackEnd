namespace CineZarAPI.Models;

public class Opinion
{
    public int Id { get; private set; } = 0;
    private static int Identificador { get; set; } = 0;
    public int Valoracion { get; set; } = 1;
    public string? Comentario { get; set; }
    public DateTime fechaCreacion { get; set; } = DateTime.Now;
    public string? Usuario { get; set; }
    public Opinion(int iValoracion, string strComentario, string strUsuario)
    {
        Identificador++;
        Id = Identificador;
        Valoracion = iValoracion;
        Comentario = strComentario;
        Usuario = strUsuario;
    }
}