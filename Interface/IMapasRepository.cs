using CounterStrikeAPI.Model;

namespace CounterStrikeAPI.Interface;

public interface IMapasRepository
{
    Task<ICollection<Mapas>> GetMapas();

    Task<Mapas> GetMapasById(int id);

    Task<Mapas> GetMapasByName(string nome);
    
    Task <bool> MapaExists(int mapaId);

    Task<Mapas> PostMapas(Mapas mapa);
    
    Task<Mapas> PutMapas(Mapas mapa, int id);
    
    Task<Mapas> DeleteMapas(int id);
}