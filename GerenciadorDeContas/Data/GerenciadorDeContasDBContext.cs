using GerenciadorDeContas.Data.Maps;
using GerenciadorDeContas.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeContas.Data
{
    public class GerenciadorDeContasDBContext : DbContext
    {

        public GerenciadorDeContasDBContext(DbContextOptions<GerenciadorDeContasDBContext> options)
            :base (options)
        {

        }

        //O ORM traz toda a estrutura de entidades. Depois cria-se as tabelas. 
        public DbSet<ContaModel> Conta { get; set; } // Representa uma tabela.
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
