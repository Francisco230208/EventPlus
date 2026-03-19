using System.Formats.Tar;

namespace EventPlus.WebAPI.DTO;

public class EventoDTO
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime ? DataEvento { get; set; }
    public Guid ? IdTipoEvento { get; set; }
    public Guid ? IdInstituicao { get; set; }
  


}
