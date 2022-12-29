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

            //Configurando dbContext e indicando string de conex�o
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<GerenciadorDeContasDBContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                ) ;

            builder.Services.RegisterServices();

            //habilitando cors

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("https://localhost:7101/api/Conta",
                                                          "https://localhost:7101/api/Conta/Atualizar");
                                  });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);

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
            //registrar o IMapper como um servi�o
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

    }
}
