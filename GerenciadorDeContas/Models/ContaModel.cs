namespace GerenciadorDeContas.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public double? ValorOriginal { get; set; }   
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataPagamento  { get; set; }

    }
}
