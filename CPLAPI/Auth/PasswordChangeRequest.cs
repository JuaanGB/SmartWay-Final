using System.ComponentModel.DataAnnotations;

namespace CPLAPI.Auth;

public class PasswordChangeRequest
{
    public int AgenteId { get; set; }

    [Required]
    public string ContraseñaAntigua { get; set; }

    [Required]
    public string ContraseñaNueva { get; set; }
}