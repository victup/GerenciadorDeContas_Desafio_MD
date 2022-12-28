using GerenciadorDeContas.Enums;

namespace GerenciadorDeContas.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double ValorOriginal { get; set; }   
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento  { get; set; }
        public int Atraso { get; set; }
        public string Regra { get; set; }

        public ContaModel() { }

    
    }
}
