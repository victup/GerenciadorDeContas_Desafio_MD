using GerenciadorDeContas.Models;

namespace GerenciadorDeContas.Repositorys.Interfaces
{
    public interface IContaRepository
    {
        Task<List<ContaModel>> BuscarTodasAsContas();
        Task<ContaModel> BuscarConta(int id);
        Task<ContaModel> AdicionarConta(ContaModel conta);
        Task<ContaModel> AtualizarConta(ContaModel conta, int id);
        Task<bool> ApagarConta(int id);
    }
}
