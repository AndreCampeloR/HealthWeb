using ApiPloomes.Dto.Responses;
using ProjetoPloomes.Models;

namespace ApiPloomes.Repositorios.Interface
{
    public interface IOperadoraMedicoRepositorio
    {
        Task<List<OperadoraResponse>> ListarOperadorasPorMedico(int medicoId);
        Task<List<MedicoResponse>> ListarMedicosPorOperadora(int operadoraId);

        Task<OperadoraMedico> CadastrarMedicoAOperadora(int operadoraId,int medicoId);

    }
}
