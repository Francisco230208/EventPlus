using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class InstituicaoDTO
    {
        [Required(ErrorMessage = "o título do tipo de Instuituição é obrigatório!")]
        public string? NomeFantasia { get; set; }
        public string? Cnjp { get; set; }
        public string? Endereco { get; set; }
    }
}
