using System.Diagnostics.CodeAnalysis;

namespace CPLAPI.Models;

public class Equipo
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Especialidad { get; set; }
    public string? OperacionId { get; set; }
    public virtual Operacion? Operacion { get; set; }
    public ICollection<Agente> Agentes { get; } = new List<Agente>();

}