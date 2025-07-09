using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CPLAPI.Auth;
using CPLAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CPLAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly CyberPulseContext _context;
    private readonly PasswordHasher<Agente> _hasher;
    private readonly IConfiguration _configuration;
    public AuthController(CyberPulseContext context, IConfiguration configuration)
    {
        _context = context;
        _hasher = new PasswordHasher<Agente>();
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var agente = await _context.Agentes.SingleOrDefaultAsync(a => a.Email == request.Email);
        if (agente == null)
            return Unauthorized("Credenciales incorrectas");

        var resultado = _hasher.VerifyHashedPassword(agente, agente.Contraseña, request.Contraseña);

        if (resultado == PasswordVerificationResult.Failed)
            return Unauthorized("Credenciales incorrectas");

        var token = CreateToken(agente);
        return Ok(new AuthResponse
        {
            Token = token,
            Id = agente.Id,
            Nombre = agente.Nombre
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        var agente = new Agente
        {
            Nombre = request.Nombre,
            Activo = true,
            Email = request.Email,
            Contraseña = _hasher.HashPassword(null, request.Contraseña), // Guardamos la contraseña hasheada
        };

        _context.Agentes.Add(agente);
        await _context.SaveChangesAsync();

        var token = CreateToken(agente);
        return Ok(new AuthResponse
        {
            Token = token,
            Id = agente.Id,
            Nombre = agente.Nombre
        });
    }

    [HttpPatch("password/{id}")]
    public async Task<IActionResult> Password(int id, [FromBody] PasswordChangeRequest request)
    {
        Console.WriteLine(request.AgenteId);
        if (id != request.AgenteId)
            return BadRequest();

        var agente = await _context.Agentes.FindAsync(id);
        var resultado = _hasher.VerifyHashedPassword(agente, agente.Contraseña, request.ContraseñaAntigua);

        if (resultado == PasswordVerificationResult.Failed)
            return Unauthorized("Credenciales incorrectas");

        agente.Contraseña = _hasher.HashPassword(null, request.ContraseñaNueva);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [NonAction]
    public string CreateToken(Agente agente)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, agente.Id.ToString()),
                new Claim(ClaimTypes.Name, agente.Nombre),
            }),
            Expires = DateTime.UtcNow.AddMinutes(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"], // Add this line
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
