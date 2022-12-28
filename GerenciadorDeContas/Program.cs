using AutoMapper;
using GerenciadorDeContas.Data;
using GerenciadorDeContas.DTOs;
using GerenciadorDeContas.Models;
using GerenciadorDeContas.Repositorys;
using GerenciadorDeContas.Repositorys.Interfaces;
using GerenciadorDeContas.Services;
using GerenciadorDeContas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeContas
{
    internal static class Program
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

            builder.Services.RegisterServices();



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

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<IContaService, ContaService>();

            //Criando instancia do autoMapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContaModel, ContaDTO>();
                cfg.CreateMap<ContaDTO, ContaModel>();
                cfg.CreateMap<ContaModel, DetalheContaDTO>();
                cfg.CreateMap<DetalheContaDTO, ContaModel>();
            });
            //registrar o IMapper como um serviço
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

    }
}
