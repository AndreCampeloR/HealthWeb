using Api.Services.Interface;
using ApiPloomes.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoPloomes.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services
{
    public class AutenticadorService : IAutenticadorService
    {
        private readonly HealthWebDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AutenticadorService(HealthWebDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration; 
        }

        public async Task<bool> Autenticacao(string email, string senha)
        {
            var empresa = BuscarEmpresaPorEmail(email);
            if (empresa == null)
            {
                return false;
            }

            var senhaCorreta = await _dbContext.Empresa.Where(x => x.Senha == senha).FirstOrDefaultAsync();
            if (senhaCorreta == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Empresa> BuscarEmpresaPorEmail(string email)
        {
            return await _dbContext.Empresa.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<bool> ExisteEmpresa(string email)
        {
            var empresa = await _dbContext.Empresa.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if (empresa != null)
            {
                return true;
            }

            return false;
        }

        public string GerarToken(int id, string email)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_configuration["JwtToken:SecretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(15);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: _configuration["JwtToken:issuer"],
               audience: _configuration["JwtToken:audience"],
               claims: claims,
               expires: expiration,
               signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
