using System.ComponentModel.DataAnnotations;

namespace CPLAPI.Auth;

public class AuthResponse
{
    public string Email { get; set; }
    public string Nombre { get; set; }
    public string Token { get; set; }
}