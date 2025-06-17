using CounterStrikeAPI.Database;
using CounterStrikeAPI.Interface;
using CounterStrikeAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CounterStrikeAPI.Repository;

public class GranadasRepository : IGranadasRepository
{
    private readonly Context _context;

    public GranadasRepository(Context context)
    {
        _context = context;
    }

    public async Task<ICollection<Granadas>> GetGranadas()
    {
        return await _context.Granadas.OrderBy(g => g.Id).ToListAsync();
    }

    public async Task<Granadas> GetGranadasById(int id)
    {
        return await _context.Granadas.Where(g => g.Id == id).FirstOrDefaultAsync();        
    }

    public async Task<Granadas> GetGranadasByName(string nome)
    {
        return await _context.Granadas.Where(g => g.Nome == nome).FirstOrDefaultAsync();    
    }

    public async Task<bool> GranadasExists(int id)
    {
        return await _context.Granadas.AnyAsync(g => g.Id == id);   
    }

    public async Task<Granadas> PostGranadas(Granadas granadas)
    {
        _context.Granadas.Add(granadas);
        await _context.SaveChangesAsync();
        return granadas;
    }

    public async Task<Granadas> PutGranadas(Granadas granadas, int id)
    {
        _context.Entry(granadas).State = EntityState.Modified;
        await _context.SaveChangesAsync();  
        return granadas;    
    }

    public async Task<Granadas> DeleteGranadas(int id)
    {
        var granadaid = await GetGranadasById(id);
        _context.Granadas.Remove(granadaid);
        await _context.SaveChangesAsync();      
        return granadaid;
    }
        
}