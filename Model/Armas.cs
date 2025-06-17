﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CounterStrikeAPI.Model;

public class Armas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Nome { get; set; }
    
    public string Categoria { get; set; }
    
    public decimal Valor { get; set; }
}