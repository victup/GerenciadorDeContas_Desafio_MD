using GerenciadorDeContas.Models;
using Xunit;
using Xunit.Sdk;

namespace GerenciadorDeContas.Services
{
    public class ContaServiceTests: ContaService
    {
        [Theory]
        [InlineData(1000, "Ate3", 3, 1023)]
        public void CalculoValido_QuandoCorrigirValorForChamado_RetornaValorCorrigido(double valorOriginal, string regra, int diasAtraso, double valorCorrigido)
        {
            //arrange
            ContaModel conta = new ContaModel();
            conta.ValorOriginal = valorOriginal;
            conta.Regra = regra;
            conta.Atraso = diasAtraso;

            //act 
            var valorMetodo = CorrigirValor(conta);

            //assert
            Assert.Equal(valorMetodo, valorCorrigido);

        }
    }
}
