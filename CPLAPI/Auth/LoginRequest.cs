using System.ComponentModel.DataAnnotations;

namespace CPLAPI.Auth;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Contraseña { get; set; }
}