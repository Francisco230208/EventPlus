using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "O email do usuário é obrigatório!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha do usuário é obrigatória!")]
        public string? Senha { get; set; }

        /*[Required(ErrorMessage = "O titulo do usuário é obrigatória!")]
        public string? Titulo { get; set; }*/
    }
}
