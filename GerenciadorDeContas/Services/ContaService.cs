using GerenciadorDeContas.Models;
using GerenciadorDeContas.Repositorys.Interfaces;
using GerenciadorDeContas.Services.Interfaces;

namespace GerenciadorDeContas.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;
        public ContaService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<ContaModel> AdicionarConta(ContaModel conta)
        {
            return await _contaRepository.AdicionarConta(conta);
        }

        public async Task<bool> ApagarConta(int id)
        {
            return await _contaRepository.ApagarConta(id);
        }

        public async Task<ContaModel> AtualizarConta(ContaModel conta, int id)
        {
            return await _contaRepository.AtualizarConta(conta, id);
        }

        public async Task<ContaModel> BuscarConta(int id)
        {
            return await _contaRepository.BuscarConta(id);
        }

        public async Task<List<ContaModel>> BuscarTodasAsContas()
        {
            return await _contaRepository.BuscarTodasAsContas();
        }
    }
}
