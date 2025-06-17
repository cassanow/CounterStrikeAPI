using System.ComponentModel.DataAnnotations;

namespace CounterStrikeAPI.DTO;

public class UsuarioDTO
{
    [Required]
    public string? Nome { get; set; }
    
    [Required]
    public string? Usuario { get; set; }
    
    [Required]
    public string? Senha { get; set; }
}