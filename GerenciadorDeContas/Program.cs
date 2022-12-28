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

            //Configurando dbContext e indicando string de conex�o
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<GerenciadorDeContasDBContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                ) ;

            builder.Services.AddScoped<IContaRepository, ContaRepository>();
            builder.Services.AddScoped<IContaService, ContaService>();

            //Criando instancia do autoMapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContaModel, ContaDTO>();
                cfg.CreateMap<ContaDTO, ContaModel>(); 
                cfg.CreateMap<ContaModel, DetalheContaDTO>();
                cfg.CreateMap<DetalheContaDTO, ContaModel>();
            });
            //registrar o IMapper como um servi�o
            IMapper mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);



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

    }
}
