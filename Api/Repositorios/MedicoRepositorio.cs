using ApiPloomes.Context;
using ApiPloomes.Dto.Requests;
using ApiPloomes.Dto.Responses;
using ApiPloomes.Repositorios.Interface;
using Microsoft.EntityFrameworkCore;
using ProjetoPloomes.Models;

namespace ApiPloomes.Repositorios
{
    public class MedicoRepositorio : IMedicoRepositorio
    {
        private readonly HealthWebDbContext _dbContext;

        public MedicoRepositorio(HealthWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MedicoResponse>> ListarMedicos()
        {
            var medicos = await _dbContext.Medico.ToListAsync();

            if (medicos.Count() <= 0)
                throw new Exception(" Não foi encontrado medicos cadastrados");

            var medicosResponse = medicos.Select(medico => new MedicoResponse
            {
                Id = medico.Id,
                Nome = medico.Nome,
                Cpf = medico.Cpf,
                Email = medico.Email,
                Telefone = medico.Telefone, 
            }).ToList();

            return medicosResponse;
        }

        public async Task<Medico> BuscarMedicoPorId(int Id)
        {
            var medico = _dbContext.Medico.FirstOrDefault(x => x.Id == Id);
            return medico;
        }

        public async Task<CadastroResponse> CadastrarMedico(MedicoRequest medicoRequest)
        {

            Medico medico = new Medico
            {
                Nome = medicoRequest.Nome, 
                Cpf = medicoRequest.Cpf,
                Email = medicoRequest.Email,
                Telefone= medicoRequest.Telefone,                
            };

            _dbContext.Medico.Add(medico);
            await _dbContext.SaveChangesAsync();

            return new CadastroResponse
            {
                Id = medico.Id,
                Mensagem = $"Medico {medico.Nome} cadastrado com sucesso"
            };
        }


        public async Task<MedicoResponse> AtualizarMedico(MedicoRequest medicoRequest, int id)
        {
            Medico medico = await _dbContext.Medico.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception($"O medico com ID: {id} não foi encontrada");

            medico.Nome = medicoRequest.Nome;
            medico.Cpf = medicoRequest.Cpf;
            medico.Email = medicoRequest.Email;
            medico.Telefone = medicoRequest.Telefone;

            _dbContext.Medico.Update(medico);
            await _dbContext.SaveChangesAsync();

            return new MedicoResponse
            {
                Id = medico.Id,
                Nome = medico.Nome,
                Cpf = medico.Cpf,
                Email = medico.Email,
                Telefone = medico.Telefone,
                Mensagem = "Atualização realizada com sucesso"
            };
        }

        public async Task<MedicoResponse> DeletarMedico(int id)
        {
            Medico medico = await _dbContext.Medico.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception($"O medico com ID: {id} não foi encontrado");

            _dbContext.Medico.Remove(medico);
            await _dbContext.SaveChangesAsync();

            return new MedicoResponse
            {
                Mensagem = "Medico excluido com sucesso"
            };
        }
    }
}
