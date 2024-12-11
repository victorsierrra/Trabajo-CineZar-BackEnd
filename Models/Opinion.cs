namespace CineZarAPI.Models;

public class Opinion{
    public int Valoracion {get; set;} = 1;
    public string? Comentario {get; set;}
    public DateTime fechaCreacion {get; set;} = DateTime.Now;
    public string? Usuario {get; set;}
    public Opinion (int iValoracion, string strComentario, string strUsuario)
    {
        Valoracion = iValoracion;
        Comentario = strComentario;
        Usuario = strUsuario;
    }
}