using System.ComponentModel.DataAnnotations;

namespace ApiPloomes.Dto.Requests
{
    public class EmpresaRequest
    {
        [Required]
        public string Cnpj { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        public string Telefone { get; set; }
    }
}
