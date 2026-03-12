using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class TipoUsuarioDTO
    {
        [Required(ErrorMessage = "o título do tipo de Usuario é obrigatório!")]
        public string Titulo { get; set; }
    }
}
