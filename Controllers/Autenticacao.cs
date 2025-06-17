using CounterStrikeAPI.Database;
using CounterStrikeAPI.DTO;
using CounterStrikeAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace CounterStrikeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class Autenticacao : ControllerBase
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;

    public Autenticacao(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var usuario = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == loginDTO.Usuario);
        
        if(usuario == null)
            return Unauthorized("Usuario invalido");
        
        var result = await _signInManager.CheckPasswordSignInAsync(usuario, loginDTO.Senha, false);
        
        if(!result.Succeeded)
            return Unauthorized("Usuario ou senha incorretos");

        return Ok();
    }
    
    
    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] UsuarioDTO usuario)
    {
        try
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = new Usuario
            {
                UserName = usuario.Usuario,
            };

            var usuarioCriado = await _userManager.CreateAsync(appUser, usuario.Senha);

            if (usuarioCriado.Succeeded)
            {
                var roles = await _userManager.AddToRoleAsync(appUser, "User");

                if (roles.Succeeded)
                {
                    return Ok("Usuario criado com sucesso!");
                }
                else
                {
                    return StatusCode(500, roles.Errors);
                }
            }
            else
            {
                return StatusCode(500, usuarioCriado.Errors);
            }
               
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
        
        
    }

}