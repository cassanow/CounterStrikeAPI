using CounterStrikeAPI.Model;

namespace CounterStrikeAPI.Interface;

public interface IGranadasRepository
{
    Task<ICollection<Granadas>> GetGranadas();
    
    Task<Granadas> GetGranadasById(int id);     
    
    Task<Granadas> GetGranadasByName(string nome);  
    
    Task<bool> GranadasExists(int id);
    
    Task<Granadas> PostGranadas(Granadas granadas);
    
    Task<Granadas> PutGranadas(Granadas granada, int id);
    
    Task<Granadas> DeleteGranadas(int id);
    
}