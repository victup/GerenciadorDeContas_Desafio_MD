using AutoMapper;
using GerenciadorDeContas.DTOs;
using GerenciadorDeContas.Enums;
using GerenciadorDeContas.Models;
using GerenciadorDeContas.Repositorys.Interfaces;
using GerenciadorDeContas.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

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
            List<ContaModel> listContaModel = await _contaRepository.BuscarTodasAsContas();
           
            List<HistoricoContaDTO> listContaDTO = new();

            HistoricoContaDTO contaDTO;

            


            foreach (var conta in listContaModel)
            {
                
                contaDTO = _mapper.Map<HistoricoContaDTO>(conta);
                contaDTO.ValorCorrigido = CorrigirValor(conta);
                listContaDTO.Add(contaDTO);
            }

            return listContaDTO;

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
            contaModel.Atraso = DiasEmAtraso(Convert.ToDateTime(conta.DataVencimento));
           
            switch (contaModel.Atraso)
            {
                case 0:
                    contaModel.Regra = RegraCalculo.Nenhum.ToString();
                break;

                case <= 3:
                    contaModel.Regra = RegraCalculo.Ate3.ToString();
                break;

                case <= 10:
                    contaModel.Regra = RegraCalculo.SuperiorA3.ToString();
                break;

                default: 
                    contaModel.Regra = RegraCalculo.SuperiorA10.ToString();
                break;

            }
             
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

        private static int DiasEmAtraso(DateTime vencimento)
        {
          int dias = vencimento.Subtract(DateTime.Now).Days;
            return Math.Abs(dias); //valor absoluto sem sinais

        }
        
        private static double CorrigirValor(ContaModel contaModel)
        {
            if(contaModel == null) return 0;

            RegraCalculo regra = (RegraCalculo)Enum.Parse(typeof(RegraCalculo), contaModel.Regra);  

            switch (regra)
            {
                case RegraCalculo.Nenhum:
                    return (double)contaModel.ValorOriginal;
                    break;

                case RegraCalculo.Ate3:
                    return (double)(contaModel.ValorOriginal * 2);
                    break;

                case RegraCalculo.SuperiorA3:
                    return (double)(contaModel.ValorOriginal * 4);
                    break;

                default:
                   return (double)(contaModel.ValorOriginal * 8);
                    break;

            }

        }

    }

      
}
