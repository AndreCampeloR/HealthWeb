using ApiPloomes.Dto.Requests;
using ApiPloomes.Dto.Responses;
using ProjetoPloomes.Models;
using System.Threading.Tasks;

namespace ApiPloomes.Repositorios.Interface
{
    public interface IOperadoraRepositorio
    {
        Task<List<OperadoraResponse>> ListarOperadoras();
        Task<List<OperadoraResponse>> ListarOperadorasPorEmpresa(int empresaId);
        Task<Operadora> BuscarOperadoraPorId(int Id);
        Task<CadastroResponse> CadastrarOperadora(OperadoraRequest operadoraRequest);
        Task<OperadoraResponse> AtualizarOperadora(OperadoraRequest operadoraRequest, int id);

        Task<OperadoraResponse> DeletarOperadora(int id);

    }
}
