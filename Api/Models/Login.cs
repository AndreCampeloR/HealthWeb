using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Login
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
