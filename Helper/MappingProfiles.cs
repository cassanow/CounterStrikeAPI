using AutoMapper;
using CounterStrikeAPI.DTO;
using CounterStrikeAPI.Model;

namespace CounterStrikeAPI.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Armas, ArmasDTO>().ReverseMap();
        CreateMap<Mapas, MapasDTO>().ReverseMap();
        CreateMap<Granadas, GranadasDTO>().ReverseMap();
    }
    
}