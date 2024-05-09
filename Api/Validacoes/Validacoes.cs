using Microsoft.AspNetCore.Server.IIS.Core;
using System.Text.RegularExpressions;

namespace Api.Validacoes
{
    public class Validacoes : IValidacoes
    {
        private List<string> _erros = new List<string>();

        public bool ValidarCNPJ(string cnpj)
        {
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            if (!cnpj.All(char.IsDigit))
                return false;

            int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            int digito1 = (resto < 2) ? 0 : 11 - resto;

            if (int.Parse(cnpj[12].ToString()) != digito1)
                return false;

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            int digito2 = (resto < 2) ? 0 : 11 - resto;

            if (int.Parse(cnpj[13].ToString()) != digito2)
                return false;

            return true;
        }
        public bool ValidarTelefone(string telefone)
        {
            if (telefone.Length != 11)
                return false;

            if (!telefone.All(char.IsDigit))
                return false;

            string pattern = @"^\([1-9]{2}\) (?:[2-8]|9[0-9])[0-9]{3}\-[0-9]{4}$";
            if (!Regex.IsMatch(telefone, pattern))
                return false;

            return true;

        }

        public bool ValidarEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidarCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            if (!cpf.All(char.IsDigit))
                return false;

            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        public void AdicionarErro(string mensagem)
        {
            _erros.Add(mensagem);
        }

        public void ValidarCadastroEmpresa(string telefone, string email, string cnpj)
        {
            if (!ValidarTelefone(telefone))
                AdicionarErro("Telefone inválido.");

            if (!ValidarEmail(email))
                AdicionarErro("E-mail inválido.");

            if (!ValidarCNPJ(cnpj))
                AdicionarErro("CNPJ inválido.");

            if (_erros.Any())
            {
                string mensagemErro = string.Join(Environment.NewLine, _erros);
                throw new ValidacaoException(mensagemErro);
            }
        }

        public class ValidacaoException : Exception
        {
            public ValidacaoException(string message) : base(message)
            {
            }
        }
    }
}
