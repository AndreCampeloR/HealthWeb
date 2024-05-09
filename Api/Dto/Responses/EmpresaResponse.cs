namespace ApiPloomes.Dto.Responses
{
    public class EmpresaResponse : CadastroResponse
    {
        public string Cnpj { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
    }

}
