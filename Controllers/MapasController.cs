using AutoMapper;
using CounterStrikeAPI.Database;
using CounterStrikeAPI.DTO;
using CounterStrikeAPI.Interface;
using CounterStrikeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CounterStrikeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MapasController : Controller
{
   private readonly IMapasRepository _mapasRepository;
   private readonly IMapper _mapper;

   public MapasController(IMapasRepository mapasRepository, IMapper mapper)
   {
      _mapasRepository = mapasRepository;
      _mapper = mapper;
   }

   [HttpGet]
   [SwaggerOperation("Permite listar todos os mapas adicionados na API")]
   [ProducesResponseType(typeof(List<Mapas>), 200)]
   [ProducesResponseType(404)]
   public async Task<IActionResult> GetAllMapas()
   {
      var mapas = await _mapasRepository.GetMapas();
      
      if(!ModelState.IsValid)
         return BadRequest(ModelState);
      
      return Ok(mapas);
   }

   [HttpGet("{id:int}")]
   [SwaggerOperation("Permite pesquisar um mapa especifico pelo id")]
   [ProducesResponseType(typeof(List<Mapas>), 200)]
   [ProducesResponseType(404)]
   public async Task<IActionResult> GetMapasById(int id)
   {
      var mapa = await _mapasRepository.GetMapasById(id);
      
      if(!ModelState.IsValid)
         return BadRequest(ModelState);
      
      if(!await _mapasRepository.MapaExists(id))
         return NotFound();
      
      return Ok(mapa);
      
   }

   [HttpGet("{mapa}")]
   [SwaggerOperation("Permite pesquisar um mapa especifico pelo nome")]
   [ProducesResponseType(typeof(List<Mapas>), 200)]
   [ProducesResponseType(404)]
   public async Task<IActionResult> GetMapasByName(string mapas)
   {
      var mapa = await _mapasRepository.GetMapasByName(mapas);
      
      if(!ModelState.IsValid)
         return BadRequest(ModelState);
      
      return Ok(mapa);
   }

   [HttpPost("adicionar")]
   [SwaggerOperation("Adicionar um mapa")]
   [ProducesResponseType(typeof(MapasDTO), 200)]
   [ProducesResponseType(400)]
   public async Task<IActionResult> AdicionarMapa([FromBody] MapasDTO mapaDTO)
   {
      var mapa = _mapper.Map<MapasDTO, Mapas>(mapaDTO);

      await _mapasRepository.PostMapas(mapa);
      
      if(!ModelState.IsValid)
         return BadRequest(ModelState);

      return Ok(mapa);
   }

   [HttpPut("editar/{id:int}")]
   [SwaggerOperation("Editar um mapa")]
   [ProducesResponseType(typeof(Mapas), 200)]
   [ProducesResponseType(400)]
   public async Task<IActionResult> EditarMapa(Mapas mapa, int id)
   {
      if(!await _mapasRepository.MapaExists(id))
         return NotFound();
      
      var editarMapa = await _mapasRepository.PutMapas(mapa, id);
      
      if(!ModelState.IsValid)
         return BadRequest(ModelState);
      
      return Ok(editarMapa);
   }

   [HttpDelete("delete/{id:int}")]
   [SwaggerOperation("Deletar um mapa")]
   [ProducesResponseType(204)]
   [ProducesResponseType(400)]
   public async Task<IActionResult> DeleteMapa(int id)
   {
      if(!await _mapasRepository.MapaExists(id))
         return NotFound();  
      
      if(!ModelState.IsValid)
         return BadRequest(ModelState);
      
      var deleteMapa = await _mapasRepository.DeleteMapas(id);
      
      return Ok(deleteMapa);
   }
}