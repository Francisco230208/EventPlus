namespace EventPlus.WebAPI.DTO;

public class UsuarioDTO
{
    public string ? Nome { get; set; }
    public string ? Email { get; set; }
    public string ? Senha { get; set; }
    public Guid ? IdTipoUSuario { get; set; }
}
