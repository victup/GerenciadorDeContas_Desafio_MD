using AutoMapper;
using GerenciadorDeContas.DTOs;
using GerenciadorDeContas.Enums;
using GerenciadorDeContas.Models;
using GerenciadorDeContas.Repositorys.Interfaces;
using GerenciadorDeContas.Services.Interfaces;

namespace GerenciadorDeContas.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly IMapper _mapper;
        public ContaService(IContaRepository contaRepository, IMapper mapper)
        {
            _contaRepository = contaRepository;
            _mapper = mapper;
        }

        public async Task<List<HistoricoContaDTO>> ListarHistoricoDeContas()
        {
            List<ContaModel> listConta = await _contaRepository.BuscarTodasAsContas();
            Console.WriteLine(listConta);
            
            List<HistoricoContaDTO> listaHistoricoContas = new();
            HistoricoContaDTO historicoConta ;


            foreach (var conta in listConta)
            {
                historicoConta = new();
                historicoConta.Nome = conta.Nome;    
                historicoConta.ValorOriginal = conta.ValorOriginal;
                historicoConta.ValorCorrigido = 1100;
                historicoConta.DiasAtrasados = 2;
                historicoConta.DataPagamento = conta.DataPagamento;

                listaHistoricoContas.Add(historicoConta);
            }

            return listaHistoricoContas;

        }
        public async Task<ContaDTO> BuscarConta(int id)
        {
            return _mapper.Map<ContaDTO>(await _contaRepository.BuscarConta(id));
        }

        public async Task<List<ContaModel>> BuscarTodasAsContas()
        {
            return _mapper.Map<List<ContaModel>>(await _contaRepository.BuscarTodasAsContas());
        }
        public async Task<ContaDTO> AdicionarConta(ContaDTO conta)
        {
            ContaModel contaModel = _mapper.Map<ContaModel>(conta);
            contaModel.Atraso = 0;
            contaModel.Regra = RegraCalculo.Nenhum.ToString();
            return _mapper.Map<ContaDTO>(await _contaRepository.AdicionarConta(contaModel));
        }

        public async Task<bool> ApagarConta(int id)
        {
            return await _contaRepository.ApagarConta(id);
        }

        public async Task<ContaDTO> AtualizarConta(ContaDTO conta, int id)
        {
            ContaModel contaModel = _mapper.Map<ContaModel>(conta);

            contaModel.Id = id;

            return _mapper.Map<ContaDTO>(await _contaRepository.AtualizarConta(contaModel, id));
        }

    }
}
