using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPLAPI.Models;

public class Equipo
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public string? Especialidad { get; set; }
    public string? OperacionId { get; set; }
    public virtual Operacion? Operacion { get; set; }

}