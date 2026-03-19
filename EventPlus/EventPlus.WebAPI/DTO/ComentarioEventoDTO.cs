namespace EventPlus.WebAPI.DTO;

public class ComentarioEventoDTO
{
    public Guid? IdComentarioEvento { get; set; }
    public string ?Descricao { get; set; }
    public bool? Exibe { get; set; }
    public DateTime? DataComentarioEvento { get; set; }

    public Guid? IdUsuario { get; set; }

    public Guid? IdEvento { get; set; }

}
