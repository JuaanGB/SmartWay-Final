using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CPLAPI.Models;

public class Agente
{

    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Rango { get; set; }
    public bool Activo { get; set; }
    [AllowNull]
    public int? EquipoId { get; set; }
    public virtual Equipo? Equipo { get; set; }

    // Nuevos campos para autenticación 
    public string Contraseña { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}