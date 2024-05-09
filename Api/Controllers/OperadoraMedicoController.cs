using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ApiPloomes.Repositorios.Interface;

namespace ApiPloomes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoraMedicoController : ControllerBase
    {
        private readonly IOperadoraMedicoRepositorio _operadoraMedicoRepositorio;

        public OperadoraMedicoController(IOperadoraMedicoRepositorio operadoraMedicoRepositorio)
        {
            _operadoraMedicoRepositorio = operadoraMedicoRepositorio ?? throw new ArgumentNullException(nameof(operadoraMedicoRepositorio));
        }

        [HttpGet("ListarOperadorasPorMedico")]
        public async Task<IActionResult> ListarOperadorasPorMedico(int medicoId)
        {
            try
            {
                var operadoras = await _operadoraMedicoRepositorio.ListarOperadorasPorMedico(medicoId);
                return Ok(operadoras);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar operadoras por médico: {ex.Message}");
            }
        }

        [HttpGet("ListarMedicosPorOperadora")]
        public async Task<IActionResult> ListarMedicosPorOperadora(int operadoraId)
        {
            try
            {
                var medicos = await _operadoraMedicoRepositorio.ListarMedicosPorOperadora(operadoraId);
                return Ok(medicos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar médicos por operadora: {ex.Message}");
            }
        }

        [HttpPost("CadastrarMedicoAOperadora")]
        public async Task<IActionResult> CadastrarMedicoAOperadora(int operadoraId, int medicoId)
        {
            try
            {
                var resultado = await _operadoraMedicoRepositorio.CadastrarMedicoAOperadora(operadoraId, medicoId);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cadastrar médico à operadora: {ex.Message}");
            }
        }
    }
}
