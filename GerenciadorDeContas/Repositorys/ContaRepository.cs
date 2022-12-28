using GerenciadorDeContas.Data;
using GerenciadorDeContas.Enums;
using GerenciadorDeContas.Models;
using GerenciadorDeContas.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeContas.Repositorys
{
    public class ContaRepository : IContaRepository
    {
        private readonly GerenciadorDeContasDBContext _dbContext;
        public ContaRepository(GerenciadorDeContasDBContext gerenciadorDeConstasDBContext)
        {
            _dbContext = gerenciadorDeConstasDBContext;
        }

        public async Task<ContaModel> BuscarConta(int id)
        {
           return await _dbContext.Conta.FirstOrDefaultAsync(x => x.Id == id);    
        }
        public async Task<List<ContaModel>> BuscarTodasAsContas()
        {
            return await _dbContext.Conta.ToListAsync();
        }
        public async Task<ContaModel> AdicionarConta(ContaModel conta)
        {
            await _dbContext.Conta.AddAsync(conta);
            await _dbContext.SaveChangesAsync(); // confirma a operação na bd

            return conta;
        }
        public async Task<ContaModel> AtualizarConta(ContaModel newConta, int id)
        {
           ContaModel conta = await BuscarConta(id);
           
            if(conta == null)
            {
                throw new Exception($"Conta não encontrada no banco de dados para o id informado: {id}");
            }

            conta.Nome = newConta.Nome;
            conta.ValorOriginal= newConta.ValorOriginal;   
            conta.DataVencimento= newConta.DataVencimento;
            conta.DataPagamento = newConta.DataPagamento;
        
            _dbContext.Conta.Update(conta);
            await _dbContext.SaveChangesAsync(); //confirma operação
            return conta;
        }
        public async Task<bool> ApagarConta(int id)
        {
            ContaModel conta = await BuscarConta(id);

            if (conta == null)
            {
                throw new Exception($"Conta não encontrada no banco de dados para o id informado: {id}");
            }

            _dbContext.Conta.Remove(conta);
            await _dbContext.SaveChangesAsync();
            return true;

        }
    }
}
