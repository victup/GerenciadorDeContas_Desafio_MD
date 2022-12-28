using GerenciadorDeContas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeContas.Data.Maps
{
    public class ContaMap : IEntityTypeConfiguration<ContaModel>
    {
        public void Configure(EntityTypeBuilder<ContaModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ValorOriginal).IsRequired();
            builder.Property(x => x.DataVencimento).IsRequired();
            builder.Property(x => x.Atraso).IsRequired();
            builder.Property(x => x.Regra).IsRequired();
        }
    }
}
