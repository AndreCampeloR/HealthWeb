using ApiPloomes.Context;
using ApiPloomes.Dto.Requests;
using ApiPloomes.Dto.Responses;
using ApiPloomes.Repositorios.Interface;
using Microsoft.EntityFrameworkCore;
using ProjetoPloomes.Models;

namespace ApiPloomes.Repositorios
{
    public class OperadoraRepositorio : IOperadoraRepositorio
    {
        private readonly HealthWebDbContext _dbContext;

        public OperadoraRepositorio(HealthWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OperadoraResponse>> ListarOperadoras()
        {
            var operadoras = await _dbContext.Operadora.ToListAsync();

            if (operadoras.Count() <= 0)
                throw new Exception(" Não foi encontrada operadoras cadastradas");

            var operadorasResponse = operadoras.Select(operadora => new OperadoraResponse
            {
                Id = operadora.Id,
                EmpresaId = operadora.EmpresaId,
                Nome = operadora.Nome,
            }).ToList();

            return operadorasResponse;
        }

        public async Task<List<OperadoraResponse>> ListarOperadorasPorEmpresa(int empresaId)
        {
            var operadoras = _dbContext.Operadora.Where(operadora => operadora.EmpresaId == empresaId).OrderBy(operadora => operadora.Nome);

            if (operadoras.Count() <= 0)
                throw new Exception($" Não foi encontrada operadoras para a empresa {empresaId}");

            var operadorasResponse = operadoras.Select(operadora => new OperadoraResponse
            {
                Id = operadora.Id,
                EmpresaId = operadora.EmpresaId,
                Nome = operadora.Nome,
            }).ToList();

            return operadorasResponse;
        }

        public bool verificaDisponibilidadeDaEmpresa(int empresaId)
        {
            var operadoras = _dbContext.Operadora.Where(operadora => operadora.EmpresaId == empresaId).OrderBy(operadora => operadora.Nome);

            if (operadoras.Count() >= 2)
                return false;

            return true;
        }

        public async Task<Operadora> BuscarOperadoraPorId(int Id)
        {
            var operadora = _dbContext.Operadora.FirstOrDefault(x => x.Id == Id);
            return operadora;
        }

        public async Task<CadastroResponse> CadastrarOperadora(OperadoraRequest operadoraRequest)
        {

            Operadora operadora = new Operadora
            {
                EmpresaId = operadoraRequest.EmpresaId,
                Nome = operadoraRequest.Nome
            };

            _dbContext.Operadora.Add(operadora);
            await _dbContext.SaveChangesAsync();

            return new CadastroResponse
            {
                Id = operadora.Id,
                Mensagem = $"Operadora {operadora.Nome} cadastrada com sucesso"
            };
        }


        public async Task<OperadoraResponse> AtualizarOperadora(OperadoraRequest operadoraRequest, int id)
        {
            Operadora operadora = await _dbContext.Operadora.FirstOrDefaultAsync(x => x.Id == id);

            operadora.Nome = operadoraRequest.Nome;
            operadora.EmpresaId = operadoraRequest.EmpresaId;

            _dbContext.Operadora.Update(operadora);
            await _dbContext.SaveChangesAsync();

            return new OperadoraResponse
            {
                Id = operadora.Id,
                EmpresaId = operadora.EmpresaId,
                Nome = operadora.Nome,
                Mensagem = "Atualização realizada com sucesso"
            };
        }

        public async Task<OperadoraResponse> DeletarOperadora(int id)
        {
            Operadora operadora = await _dbContext.Operadora.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception($"A empresa com ID: {id} não foi encontrada");

            _dbContext.Operadora.Remove(operadora);
            await _dbContext.SaveChangesAsync();

            return new OperadoraResponse
            {
                Mensagem = "Operadora excluida com sucesso"
            };
        }
    }
}
