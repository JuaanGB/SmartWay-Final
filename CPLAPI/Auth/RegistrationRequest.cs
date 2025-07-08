using System.ComponentModel.DataAnnotations;

namespace CPLAPI.Auth;

public class RegistrationRequest
{

    [Required]
    public string Nombre { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Contraseña { get; set; }
}