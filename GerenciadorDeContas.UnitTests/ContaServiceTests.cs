using GerenciadorDeContas.Models;
using Xunit;
using Xunit.Sdk;

namespace GerenciadorDeContas.Services
{
    public class ContaServiceTests: ContaService
    {
        [Theory]
        [InlineData(1000, "Nenhum", 0, 1000)]
        [InlineData(1000, "Ate3", 3, 1023)]
        [InlineData(1000, "SuperiorA3", 5, 1040)]
        [InlineData(1000, "SuperiorA10", 25, 1125)]
        public void CalculoValido_QuandoCorrigirValorForChamado_RetornaValorCorrigido(double valorOriginal, string regra, int diasAtraso, double valorCorrigidoEsperado)
        {
            //arrange
            ContaModel conta = new ContaModel();
            conta.ValorOriginal = valorOriginal;
            conta.Regra = regra;
            conta.Atraso = diasAtraso;

            //act 
            var valorMetodo = CorrigirValor(conta);

            //assert
            Assert.Equal(valorMetodo, valorCorrigidoEsperado);

        }
    }
}
