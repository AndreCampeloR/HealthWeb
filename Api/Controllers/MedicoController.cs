using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ApiPloomes.Dto.Requests;
using ApiPloomes.Dto.Responses;
using ApiPloomes.Repositorios.Interface;

namespace ApiPloomes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoRepositorio _medicoRepositorio;

        public MedicoController(IMedicoRepositorio medicoRepositorio)
        {
            _medicoRepositorio = medicoRepositorio ?? throw new ArgumentNullException(nameof(medicoRepositorio));
        }

        [HttpGet("ListarMedicos")]
        public async Task<IActionResult> ListarMedicos()
        {
            try
            {
                var medicos = await _medicoRepositorio.ListarMedicos();
                return Ok(medicos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar médicos: {ex.Message}");
            }
        }

        [HttpGet("BuscarMedicoPorId")]
        public async Task<IActionResult> BuscarMedicoPorId(int id)
        {
            try
            {
                var medico = await _medicoRepositorio.BuscarMedicoPorId(id) ?? throw new Exception($"O médico com Id : {id} não foi encontrado");
                return Ok(medico);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar médico por Id: {ex.Message}");
            }
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] MedicoRequest medicoDto)
        {
            try
            {
                if (medicoDto == null)
                    return BadRequest("Dados inválidos");

                var medicoCadastrado = await _medicoRepositorio.BuscarMedicoPorId(medicoDto.Id);
                if (medicoCadastrado != null)
                    return NotFound($"O médico com Id : {medicoDto.Id} já está cadastrado");

                var medicoResponse = await _medicoRepositorio.CadastrarMedico(medicoDto);
                return Ok(medicoResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cadastrar médico: {ex.Message}");
            }
        }

        [HttpPut("Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] MedicoRequest medicoDto, int id)
        {
            try
            {
                var medicoResponse = await _medicoRepositorio.AtualizarMedico(medicoDto, id);
                if (medicoResponse == null)
                    return BadRequest($"Erro ao atualizar dados do médico com Id : {id}");

                return Ok(medicoResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar médico: {ex.Message}");
            }
        }

        [HttpDelete("Deletar")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var medicoResponse = await _medicoRepositorio.DeletarMedico(id);
                if (medicoResponse == null)
                    return BadRequest($"Erro ao deletar médico com Id : {id}");

                return Ok(medicoResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar médico: {ex.Message}");
            }
        }
    }
}
