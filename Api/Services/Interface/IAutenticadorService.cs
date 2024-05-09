using ProjetoPloomes.Models;

namespace Api.Services.Interface
{
    public interface IAutenticadorService
    {
        Task<bool> Autenticacao(string email, string senha);
        Task<bool> ExisteEmpresa(string email);
        public string GerarToken(int id, string email);

        public Task<Empresa> BuscarEmpresaPorEmail(string email);
    }
}
