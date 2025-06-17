using CounterStrikeAPI.Database;
using CounterStrikeAPI.Interface;
using CounterStrikeAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CounterStrikeAPI.Repository;

public class MapasRepository : IMapasRepository
{
    private readonly Context _context;

    public MapasRepository(Context context)
    {
        _context = context;
    }

    public async Task<ICollection<Mapas>> GetMapas()
    {
        return await _context.Mapas.OrderBy(m => m.Id).ToListAsync();
    }

    public async Task<Mapas> GetMapasById(int id)
    {
        return await _context.Mapas.Where(m => m.Id == id).FirstOrDefaultAsync();   
    }

    public async Task<Mapas> GetMapasByName(string nome)
    {
        return await _context.Mapas.Where(m => m.Nome.ToUpper() == nome.ToUpper()).FirstOrDefaultAsync();
    }

    public async Task<bool> MapaExists(int id)
    {
        return await _context.Mapas.AnyAsync(a => a.Id == id);
    }

    public async Task<Mapas> PostMapas(Mapas mapa)
    {
        await _context.Mapas.AddAsync(mapa);
        await _context.SaveChangesAsync();
        return mapa;
    }

    public async Task<Mapas> PutMapas(Mapas mapa, int id)
    {
        _context.Entry(mapa).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return mapa;
    }

    public async Task<Mapas> DeleteMapas(int id)
    {
        var mapa = await GetMapasById(id);
        _context.Mapas.Remove(mapa);
        await _context.SaveChangesAsync();
        return mapa;
    }
}