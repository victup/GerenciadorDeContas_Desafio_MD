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

        public ContaService()
        {

        }

        public ContaService(IContaRepository contaRepository, IMapper mapper)
        {
            _contaRepository = contaRepository;
            _mapper = mapper;
        }


        public async Task<List<DetalheContaDTO>> ListarHistoricoDeContas()
        {
            List<ContaModel> listContaModel = await _contaRepository.BuscarTodasAsContas();
           
            List<DetalheContaDTO> listContaDTO = new();

            DetalheContaDTO contaDTO;

            foreach (var conta in listContaModel)
            {
                
                contaDTO = _mapper.Map<DetalheContaDTO>(conta);

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
        
      
        protected static double CorrigirValor(ContaModel contaModel)
        {
            if(contaModel == null) return 0;

            RegraCalculo regra = (RegraCalculo)Enum.Parse(typeof(RegraCalculo), contaModel.Regra);

            double multa, juros, novoValor, valorOriginal = (double)contaModel.ValorOriginal;

            int atrasoDias = (int)contaModel.Atraso;

            switch (regra)
            {
                case RegraCalculo.Nenhum:
                    return valorOriginal;
                   
                case RegraCalculo.Ate3:
                    multa = valorOriginal * 0.02;
                    juros = valorOriginal * 0.001;
                    novoValor = valorOriginal + (multa + (juros * atrasoDias));

                    return novoValor;
                   
                case RegraCalculo.SuperiorA3:
                    multa = valorOriginal * 0.03;
                    juros = valorOriginal * 0.002;
                    novoValor = valorOriginal + (multa + (juros * atrasoDias));

                    return novoValor;


                default:
                    multa = valorOriginal * 0.05;
                    juros = valorOriginal * 0.003;
                    novoValor = valorOriginal + (multa + (juros * atrasoDias));

                    return novoValor;


            }

        }

    }

      
}
