using ApiPloomes.Context;
using ApiPloomes.Dto.Requests;
using ApiPloomes.Dto.Responses;
using ApiPloomes.Repositorios.Interface;
using Microsoft.EntityFrameworkCore;
using ProjetoPloomes.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPloomes.Repositorios
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        private readonly HealthWebDbContext _dbContext;

        public EmpresaRepositorio(HealthWebDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Empresa> BuscarEmpresaPorEmail(string empresaEmail)
        {
            return await _dbContext.Empresa.FirstOrDefaultAsync(x => x.Email == empresaEmail);
        }

        public async Task<Empresa> ObterPorId(int id)
        {
            return await _dbContext.Empresa.FindAsync(id);
        }

        public async Task<CadastroResponse> Cadastrar(EmpresaRequest empresaRequest)
        {
            var empresa = new Empresa
            {
                Cnpj = empresaRequest.Cnpj,
                Nome = empresaRequest.Nome,
                Email = empresaRequest.Email,
                Senha = empresaRequest.Senha,
                Telefone = empresaRequest.Telefone
            };

            _dbContext.Empresa.Add(empresa);
            await _dbContext.SaveChangesAsync();

            return new CadastroResponse
            {
                Id = empresa.Id,
                Mensagem = $"Empresa {empresa.Nome} cadastrada com sucesso"
            };
        }

        public async Task<EmpresaResponse> Atualizar(EmpresaRequest empresaRequest, int id)
        {
            var empresa = await _dbContext.Empresa.FindAsync(id)
                          ?? throw new Exception($"A empresa com ID: {id} não foi encontrada");

            empresa.Cnpj = empresaRequest.Cnpj;
            empresa.Nome = empresaRequest.Nome;
            empresa.Email = empresaRequest.Email;
            empresa.Telefone = empresaRequest.Telefone;
            empresa.Senha = empresaRequest.Senha;

            await _dbContext.SaveChangesAsync();

            return new EmpresaResponse
            {
                Id = empresa.Id,
                Cnpj = empresa.Cnpj,
                Nome = empresa.Nome,
                Email = empresa.Email,
                Senha = empresa.Senha,
                Telefone = empresa.Telefone,
                Mensagem = "Atualização realizada com sucesso"
            };
        }
    }
}
