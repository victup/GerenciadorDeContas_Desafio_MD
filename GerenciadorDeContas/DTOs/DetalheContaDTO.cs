namespace GerenciadorDeContas.DTOs
{
    public class HistoricoContaDTO
    {
        public string? Nome { get; set; }
        public double? ValorOriginal { get; set; }
        public double? ValorCorrigido { get; set; }
        public int? Atraso { get; set; }
        public DateTime? DataPagamento { get; set; }

    }
}
