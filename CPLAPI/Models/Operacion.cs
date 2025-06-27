namespace CPLAPI.Models;

public class Operacion
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public EstadoOperacion Estado { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFin { get; set; }

}