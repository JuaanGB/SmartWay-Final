namespace CPLAPI.Models;

public class Agente
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Rango { get; set; }
    public bool Activo { get; set; }
    public int? EquipoId { get; set; }
    public virtual Equipo? Equipo { get; set; }

}