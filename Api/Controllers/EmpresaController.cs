using Api.Dto.Requests;
using Api.Models;
using Api.Services;
using Api.Services.Interface;
using ApiPloomes.Dto.Requests;
using ApiPloomes.Dto.Responses;
using ApiPloomes.Repositorios.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ProjetoPloomes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IAutenticadorService _autenticadorService;

        public EmpresaController(IEmpresaRepositorio empresaRepositorio, IAutenticadorService AutenticadorService)
        {
            _empresaRepositorio = empresaRepositorio;
            _autenticadorService = AutenticadorService;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> Cadastrar([FromBody] EmpresaRequest empresaDto)
        {
            try
            {
                if(empresaDto == null)
                    return BadRequest("Dados invalidos");

                var empresaCadastrada = await _autenticadorService.ExisteEmpresa(empresaDto.Email);

                if (empresaCadastrada)
                    return BadRequest($"O email {empresaDto.Email} já está cadastrado em outra empresa");

                var empresa = await _empresaRepositorio.Cadastrar(empresaDto);

                if(empresa == null)
                    return BadRequest($"Erro ao cadastrar a empresa");

                return Ok(empresa);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao cadastrar a empresa: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] EmpresaRequest empresaDto)
        {
            try
            {
                var empresaResponse = await _empresaRepositorio.Atualizar(empresaDto, id);
                if (empresaResponse == null)
                {
                    return NotFound($"Empresa com id {id} não encontrada");
                }
                return Ok(empresaResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar dados da empresa: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AcessToken>> Login(Login loginDto)
        {
            var existeEmpresa = await _autenticadorService.ExisteEmpresa(loginDto.Email);
            if (!existeEmpresa)
                return Unauthorized($"Não existe empresa cadastrada com o email {loginDto.Email}");

            var autenticacao = await _autenticadorService.Autenticacao(loginDto.Email, loginDto.Senha);
            if (!autenticacao)
                return Unauthorized("Email ou senha inválidos");

            var empresa = await _autenticadorService.BuscarEmpresaPorEmail(loginDto.Email);

            var token = _autenticadorService.GerarToken(empresa.Id, empresa.Email);

            return new AcessToken
            {
                Token = token,
            };
        }
    }
}
