using GerenciadorDeContas.DTOs;
using GerenciadorDeContas.Models;
using AutoMapper;

namespace GerenciadorDeContas.Mappings
{
    public class MappingProfile: Profile
    {
        MappingProfile()
        {
            CreateMap<ContaModel, ContaDTO>().ReverseMap();
            CreateMap<ContaModel, HistoricoContaDTO>().ReverseMap();
        }
    }
}
