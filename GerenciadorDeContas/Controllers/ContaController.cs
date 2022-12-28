using GerenciadorDeContas.DTOs;
using GerenciadorDeContas.Models;
using GerenciadorDeContas.Repositorys;
using GerenciadorDeContas.Repositorys.Interfaces;
using GerenciadorDeContas.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeContas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")] //define que a entrada é em json
    [Produces("application/json")] //define que a saida é em json
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;
        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }


        [HttpGet]
        public async Task<ActionResult<List<HistoricoContaDTO>>> HistoricoDeContas()
        {
            List<HistoricoContaDTO> contas = await _contaService.ListarHistoricoDeContas();
            return Ok(contas);
        }

        [HttpGet("ContasBancoDeDados")]
        public async Task<ActionResult<List<ContaModel>>> BuscarTodasAsContas()
        {
            List<ContaModel> contas = await _contaService.BuscarTodasAsContas();
            return Ok(contas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContaDTO>> BuscarConta(int id)
        {
            ContaDTO conta = await _contaService.BuscarConta(id);
            return Ok(conta);
        }

        [HttpPost]
        public async Task<ActionResult<ContaDTO>> CadastrarConta([FromBody] ContaDTO contaDTO)
        {
            ContaDTO conta = await _contaService.AdicionarConta(contaDTO);
            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContaDTO>> AtualizarConta([FromBody] ContaDTO contaDTO, int id)
        {
            ContaDTO conta = await _contaService.AtualizarConta(contaDTO, id);
            return Ok(conta);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletarConta(int id)
        {
            bool deletado = await _contaService.ApagarConta(id);
            return Ok(deletado);
        }

    }
}
