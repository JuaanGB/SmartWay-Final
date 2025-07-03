namespace CPLAPI.Models;

public class EquipoDTO // Como solo lo uso para crear, y el id se crea automáticamente no lo incluyo
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Especialidad { get; set; }
    public string? OperacionId { get; set; }

}