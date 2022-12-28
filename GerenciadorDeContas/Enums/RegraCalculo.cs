using System.ComponentModel;

namespace GerenciadorDeContas.Enums
{
    public enum RegraCalculo
    {
        [Description("Nenhum")]
        Nenhum,
        [Description("Até 3 dias")]
        Ate3,
        [Description("Superior a 3 dias")]
        SuperiorA3,
        [Description("Superior a 10 dias")]
        SuperiorA10

    }
}
