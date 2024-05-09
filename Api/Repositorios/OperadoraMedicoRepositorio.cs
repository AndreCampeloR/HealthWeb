using ApiPloomes.Context;
using ApiPloomes.Dto.Requests;
using ApiPloomes.Dto.Responses;
using ApiPloomes.Repositorios.Interface;
using Microsoft.EntityFrameworkCore;
using ProjetoPloomes.Models;

namespace ApiPloomes.Repositorios
{
    public class OperadoraMedicoRepositorio : IOperadoraMedicoRepositorio
    {
        private readonly HealthWebDbContext _dbContext;

        public OperadoraMedicoRepositorio(HealthWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OperadoraResponse>> ListarOperadorasPorMedico(int medicoId)
        {
            var operadoras = await _dbContext.Operadora
                .Join(
                    _dbContext.OperadoraMedico,
                    operadora => operadora.Id,
                    operadoraMedico => operadoraMedico.OperadoraId,
                    (operadora, operadoraMedico) => new { Operadora = operadora, OperadoraMedico = operadoraMedico }
                )
                .Where(join => join.OperadoraMedico.MedicoId == medicoId)
                .Select(join => new OperadoraResponse
                {
                    Id = join.Operadora.Id,
                    EmpresaId = join.Operadora.EmpresaId,
                    Nome = join.Operadora.Nome,
                })
                .OrderBy(operadora => operadora.Id)
                .ToListAsync();

            if (operadoras.Count <= 0)
                throw new Exception($"Não foi encontrada operadoras para o médico de ID {medicoId}");

            return operadoras;
        }

        public async Task<List<MedicoResponse>> ListarMedicosPorOperadora(int operadoraId)
        {
            var medicos = await _dbContext.Medico
                .Join(
                    _dbContext.OperadoraMedico,
                    medico => medico.Id,
                    operadoraMedico => operadoraMedico.MedicoId,
                    (medico, operadoraMedico) => new { Medico = medico, OperadoraMedico = operadoraMedico }
                )
                .Where(join => join.OperadoraMedico.OperadoraId == operadoraId)
                .Select(join => new MedicoResponse
                {
                    Id = join.Medico.Id,
                    Nome = join.Medico.Nome,
                    Cpf = join.Medico.Cpf,
                    Email = join.Medico.Email,
                    Telefone = join.Medico.Telefone
                })
                .OrderBy(medico => medico.Id)
                .ToListAsync();

            if (medicos.Count <= 0)
                throw new Exception($"Não foi encontrado médicos para a operadora de ID {operadoraId}");

            return medicos;
        }

        public async Task<OperadoraMedico> CadastrarMedicoAOperadora(int operadoraId, int medicoId)
        {
            OperadoraMedico operadoraMedico = new OperadoraMedico
            {
                OperadoraId = operadoraId,
                MedicoId = medicoId, 
            };

            _dbContext.OperadoraMedico.Add(operadoraMedico);
            await _dbContext.SaveChangesAsync();

            return operadoraMedico;
        }
    }
}
