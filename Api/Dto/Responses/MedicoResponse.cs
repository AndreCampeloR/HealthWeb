namespace ApiPloomes.Dto.Responses
{
    public class MedicoResponse : CadastroResponse
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
    }
}
