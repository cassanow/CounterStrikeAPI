using AutoMapper;
using CounterStrikeAPI.DTO;
using CounterStrikeAPI.Interface;
using CounterStrikeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CounterStrikeAPI.Controllers;



public class ArmasController : Controller
{
   private readonly IArmasRepository _armasRepository;
   private readonly IMapper _mapper;

   public ArmasController(IArmasRepository armasRepository, IMapper mapper)
   {
      _armasRepository = armasRepository;
      _mapper = mapper;
   }

   [HttpGet]
   [SwaggerOperation("Permite buscar todas as armas da API")]
   [ProducesResponseType(200, Type = typeof(IEnumerable<Armas>))]
   public async Task <IActionResult> GetArmas()
   {
      var armas = await _armasRepository.GetArmas();
      
      if(!ModelState.IsValid)
         return BadRequest(ModelState);

      return Ok(armas);
   }
   
   [HttpGet("{id:int}")]
   [SwaggerOperation("Permite buscar uma arma pelo ID")]
   [ProducesResponseType(200, Type = typeof(IEnumerable<Armas>))]
   public async Task<IActionResult> GetArmaById(int id)
   {
      var arma = await _armasRepository.GetArmasById(id);
      
      if(!ModelState.IsValid)
         return BadRequest(ModelState);
      
      if(!await _armasRepository.ArmasExists(id))
         return NotFound("Arma não encontrada");
      
      return Ok(arma);
   }

   [HttpGet("{nome}")]
   [SwaggerOperation("Permite buscar uma arma pelo nome")]
   [ProducesResponseType(200, Type = typeof(IEnumerable<Armas>))]
   public async Task <IActionResult> GetArmaByName(string nome)
   {
      var arma = await _armasRepository.GetArmasByName(nome);
   
      if(!ModelState.IsValid)
         return BadRequest(ModelState);
     
      return Ok(arma);
   }

   [HttpPost("adicionar")]
   [SwaggerOperation("Permite adicionar uma arma a API")]
   [ProducesResponseType(200, Type = typeof(IEnumerable<Armas>))]
   public async Task<IActionResult> PostArmas(ArmasDTO arma)
   {
      var armaDTO = _mapper.Map<ArmasDTO, Armas>(arma);
      await _armasRepository.PostArmas(armaDTO);
      
      if(!ModelState.IsValid)
         return BadRequest(ModelState);
      
      return Ok(arma);
   }

   [HttpPut("editar/{id:int}")]
   [SwaggerOperation("Permite editar uma arma já existente")]
   [ProducesResponseType(200, Type = typeof(IEnumerable<Armas>))]
   public async Task<IActionResult> PutArmas(Armas arma, int id)
   {
       
      if(!await _armasRepository.ArmasExists(id))
         return NotFound();
      
      var editarArma = await _armasRepository.PutArmas(arma, id);
      
      if(!ModelState.IsValid)
         return BadRequest(ModelState);
     
      return Ok(editarArma);
   }

   [HttpDelete("delete/{id:int}")]
   [SwaggerOperation("Permite deletar uma arma")]
   [ProducesResponseType(200, Type = typeof(IEnumerable<Armas>))]
   public async Task<IActionResult> DeleteArmas(int id)
   {
       
      if(!await _armasRepository.ArmasExists(id))
         return NotFound("Arma não encontrada");
        
      if(!ModelState.IsValid)
         return BadRequest(ModelState);


      var deleteArma = await _armasRepository.DeleteArmas(id);
    
      return Ok(deleteArma);
   }
}