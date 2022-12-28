using GerenciadorDeContas.DTOs;
using GerenciadorDeContas.Models;

namespace GerenciadorDeContas.Services.Interfaces
{
    public interface IContaService
    {
        Task<List<ContaModel>> BuscarTodasAsContas();
        Task<List<DetalheContaDTO>> ListarHistoricoDeContas();
        Task<ContaDTO> BuscarConta(int id);
        Task<ContaDTO> AdicionarConta(ContaDTO conta);
        Task<ContaDTO> AtualizarConta(ContaDTO conta, int id);
        Task<bool> ApagarConta(int id);
    }
}
