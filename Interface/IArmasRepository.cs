using CounterStrikeAPI.Model;

namespace CounterStrikeAPI.Interface;

public interface IArmasRepository
{
    Task<ICollection<Armas>> GetArmas();

    Task<Armas> GetArmasById(int id);

    Task<Armas> GetArmasByName(string nome);
    
    Task <bool> ArmasExists(int armaId);

    Task<Armas> PostArmas(Armas arma);
    
    Task<Armas> PutArmas(Armas arma, int id);
    
    Task<Armas> DeleteArmas(int id);
}