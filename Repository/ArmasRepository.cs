using CounterStrikeAPI.Database;
using CounterStrikeAPI.Interface;
using CounterStrikeAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CounterStrikeAPI.Repository;

public class ArmasRepository : IArmasRepository
{
    private readonly Context _context;

    public ArmasRepository(Context context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Armas>> GetArmas()
    {
        return await _context.Armas.OrderBy(a => a.Id).ToListAsync();
    }

    public async Task<Armas> GetArmasById(int id)
    {
        return await _context.Armas.Where(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Armas> GetArmasByName(string nome)
    {
        return await _context.Armas.Where(a => a.Nome.ToUpper() == nome.ToUpper()).FirstOrDefaultAsync();
    }

    public async Task<bool> ArmasExists(int armaId)
    {
        return await _context.Armas.AnyAsync(a => a.Id == armaId);
    }


    public async Task<Armas> PostArmas(Armas armas)
    {
       await _context.Armas.AddAsync(armas);
       await _context.SaveChangesAsync();
       return armas;
    }

    public async Task<Armas> PutArmas(Armas armas, int id)
    {
        _context.Entry(armas).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return armas;
    }

    public async Task<Armas> DeleteArmas(int id)
    {
        var arma = await GetArmasById(id);
        _context.Armas.Remove(arma);
        await _context.SaveChangesAsync();
        return arma;
    }
}