using GerenciadorDeContas.Data;
using GerenciadorDeContas.Repositorys;
using GerenciadorDeContas.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeContas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Configurando dbContext e indicando string de conexão
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<GerenciadorDeContasDBContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                ) ;

            builder.Services.AddScoped<IContaRepository, ContaRepository>();

           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<IContaRepository, ContaRepository>();
                
        }
    }
}
