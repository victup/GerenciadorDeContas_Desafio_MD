using GerenciadorDeContas.Models;
using GerenciadorDeContas.Repositorys;
using GerenciadorDeContas.Repositorys.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeContas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepository _contaRepository;
        public ContaController(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContaModel>>> BuscarTodasAsContas()
        {
            List<ContaModel> contas = await _contaRepository.BuscarTodasAsContas();
            return Ok(contas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContaModel>> BuscarConta(int id)
        {
            ContaModel conta = await _contaRepository.BuscarConta(id);
            return Ok(conta);
        }

        [HttpPost]
        public async Task<ActionResult<ContaModel>> CadastrarConta([FromBody] ContaModel contaModel)
        {
            ContaModel conta = await _contaRepository.AdicionarConta(contaModel);
            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContaModel>> AtualizarConta([FromBody] ContaModel contaModel, int id)
        {
            contaModel.Id = id;
            ContaModel conta = await _contaRepository.AtualizarConta(contaModel, id);
            return Ok(conta);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ContaModel>> DeletarConta(int id)
        {
            bool deletado = await _contaRepository.ApagarConta(id);
            return Ok(deletado);
        }

    }
}
