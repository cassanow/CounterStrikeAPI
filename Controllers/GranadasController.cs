using AutoMapper;
using CounterStrikeAPI.DTO;
using CounterStrikeAPI.Interface;
using CounterStrikeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CounterStrikeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GranadasController : Controller
{
    private readonly IGranadasRepository _granadasRepository;
    private readonly IMapper _mapper;

    public GranadasController(IGranadasRepository granadasRepository, IMapper mapper)
    {
        _granadasRepository = granadasRepository;
        _mapper = mapper;       
    }

    [HttpGet]
    [SwaggerOperation("Permite listar todas as granadas adicionadas na APi")]
    [ProducesResponseType(typeof(List<Mapas>), 200)]
    [ProducesResponseType(typeof(List<Mapas>), 404)]
    public async Task<IActionResult> GetGranadas()
    {
        var granadas = await _granadasRepository.GetGranadas();
     
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        return Ok(granadas);            
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Permite listar uma granada especifica pelo seu ID")]
    [ProducesResponseType(typeof(List<Mapas>), 200)]
    [ProducesResponseType(typeof(List<Mapas>), 404)]
    public async Task<ActionResult<List<Mapas>>> GetGranadasById(int id)
    {
        var granada = await _granadasRepository.GetGranadasById(id);
        
        if(!await _granadasRepository.GranadasExists(id))
            return NotFound(); 
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(granada);
    }

    [HttpGet("{nome}")]
    [SwaggerOperation("Permite listar uma granada especifica pelo seu nome")]
    [ProducesResponseType(typeof(List<Mapas>), 200)]
    [ProducesResponseType(typeof(List<Mapas>), 404)]
    public async Task<IActionResult> GetGranadasByNome(string nome)
    {
        var granada = await _granadasRepository.GetGranadasByName(nome);

        if (!await _granadasRepository.GranadasExists(granada.Id))
            return NotFound();


        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(granada);
    }


    [HttpPost("adicionar")]
    [SwaggerOperation("Adicionar uma granada")]
    [ProducesResponseType(typeof(Granadas), 200)]
    [ProducesResponseType(typeof(Granadas), 400)]
    public async Task<IActionResult> PostGranada([FromBody] GranadasDTO granadasDTO)
    {
        var granada = _mapper.Map<GranadasDTO, Granadas>(granadasDTO);
        
        await _granadasRepository.PostGranadas(granada);
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(granada);     
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation("Atualizar uma granada")]
    [ProducesResponseType(typeof(Granadas), 200)]
    [ProducesResponseType(typeof(Granadas), 400)]
    public async Task<IActionResult> PutGranada(Granadas granadas, int id)
    {
        var granadaid = await _granadasRepository.GetGranadasById(id);
        
        if(!await _granadasRepository.GranadasExists(granadaid.Id))  
            return NotFound();  
        
        var editarGranada = await _granadasRepository.PutGranadas(granadas, granadaid.Id);
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);  
        
        return Ok(editarGranada);   
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation("Deletar uma granada")]
    [ProducesResponseType(typeof(Granadas), 204)]
    [ProducesResponseType(typeof(Granadas), 400)]
    public async Task<IActionResult> DeleteGranada(int id)
    {
        var granadaid = await _granadasRepository.GetGranadasById(id);  
        
        if(!await _granadasRepository.GranadasExists(granadaid.Id)) 
            return NotFound();  
        
        await _granadasRepository.DeleteGranadas(granadaid.Id);
        
        return NoContent();
    }
}