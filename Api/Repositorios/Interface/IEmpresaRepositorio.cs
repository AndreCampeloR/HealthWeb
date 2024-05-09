using ApiPloomes.Dto.Requests;
using ApiPloomes.Dto.Responses;
using ProjetoPloomes.Models;
using System.Threading.Tasks;

namespace ApiPloomes.Repositorios.Interface
{
    public interface IEmpresaRepositorio
    {
        Task<Empresa> BuscarEmpresaPorEmail(string empresaEmail);
        Task<Empresa> ObterPorId(int id);
        Task<CadastroResponse> Cadastrar(EmpresaRequest empresaRequest);
        Task<EmpresaResponse> Atualizar(EmpresaRequest empresaRequest, int id);
    }
}
