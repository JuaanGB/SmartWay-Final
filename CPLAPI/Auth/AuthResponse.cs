using System.ComponentModel.DataAnnotations;

namespace CPLAPI.Auth;

public class AuthResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Token { get; set; }
}