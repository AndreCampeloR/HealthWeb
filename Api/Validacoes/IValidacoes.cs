namespace Api.Validacoes
{
    public interface IValidacoes
    {
        public bool ValidarCPF(string cpf);
        public bool ValidarCNPJ(string cnpj);
        public bool ValidarTelefone(string telefone);
        public bool ValidarEmail(string email);
        public void ValidarCadastroEmpresa(string telefone, string email, string cnpj);
    }
}
