using ApiPloomes.Dto.Requests;
using ApiPloomes.Dto.Responses;
using ProjetoPloomes.Models;

namespace ApiPloomes.Repositorios.Interface
{
    public interface IMedicoRepositorio
    {
        Task<List<MedicoResponse>> ListarMedicos();
        Task<Medico> BuscarMedicoPorId(int Id);
        Task<CadastroResponse> CadastrarMedico(MedicoRequest medicoRequest);
        Task<MedicoResponse> AtualizarMedico(MedicoRequest medicoRequest, int id);

        Task<MedicoResponse> DeletarMedico(int id);
    }
}
