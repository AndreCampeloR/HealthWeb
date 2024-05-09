using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ApiPloomes.Dto.Requests;
using ApiPloomes.Dto.Responses;
using ApiPloomes.Repositorios.Interface;
using Microsoft.AspNetCore.Authorization;

namespace ApiPloomes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoraController : ControllerBase
    {
        private readonly IOperadoraRepositorio _operadoraRepositorio;

        public OperadoraController(IOperadoraRepositorio operadoraRepositorio)
        {
            _operadoraRepositorio = operadoraRepositorio ?? throw new ArgumentNullException(nameof(operadoraRepositorio));
        }

        [HttpGet("ListarOperadoras")]
        public async Task<IActionResult> ListarOperadoras()
        {
            try
            {
                var operadoras = await _operadoraRepositorio.ListarOperadoras();
                return Ok(operadoras);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar operadoras: {ex.Message}");
            }
        }

        [HttpGet("ListarOperadorasPorEmpresa")]
        public async Task<IActionResult> ListarOperadorasPorEmpresa(int empresaId)
        {
            try
            {
                var operadoras = await _operadoraRepositorio.ListarOperadorasPorEmpresa(empresaId);
                return Ok(operadoras);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar operadoras por empresa: {ex.Message}");
            }
        }

        [HttpGet("BuscarOperadoraPorId")]
        public async Task<IActionResult> BuscarOperadoraPorId(int id)
        {
            try
            {
                var operadora = await _operadoraRepositorio.BuscarOperadoraPorId(id) ?? throw new Exception($"A operadora com Id : {id} não foi encontrada");
                return Ok(operadora);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar operadora por Id: {ex.Message}");
            }
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] OperadoraRequest operadoraDto)
        {
            try
            {
                if (operadoraDto == null)
                    return BadRequest("Dados inválidos");

                var operadoraCadastrada = await _operadoraRepositorio.BuscarOperadoraPorId(operadoraDto.Id);
                if (operadoraCadastrada != null)
                    return NotFound($"A operadora com Id : {operadoraDto.Id} já está cadastrada");

                var operadoraResponse = await _operadoraRepositorio.CadastrarOperadora(operadoraDto);

                return Ok(operadoraResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cadastrar operadora: {ex.Message}");
            }
        }

        [HttpPut("Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] OperadoraRequest operadoraDto, int id)
        {
            try
            {
                var operadoraResponse = await _operadoraRepositorio.AtualizarOperadora(operadoraDto, id);
                if (operadoraResponse == null)
                    return BadRequest($"Erro ao atualizar dados da operadora com Id : {id}");

                return Ok(operadoraResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar operadora: {ex.Message}");
            }
        }

        [HttpDelete("Deletar")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var operadoraResponse = await _operadoraRepositorio.DeletarOperadora(id);
                if (operadoraResponse == null)
                    return BadRequest($"Erro ao deletar operadora com Id : {id}");

                return Ok(operadoraResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar operadora: {ex.Message}");
            }
        }
    }
}
