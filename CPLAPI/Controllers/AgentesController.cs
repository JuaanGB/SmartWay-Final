using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CPLAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace CPLAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgentesController : ControllerBase
{
    private readonly CyberPulseContext _context;
    public AgentesController(CyberPulseContext context)
    {
        _context = context;
    }

    // GET: api/Agentes.
    // USO: Listar agentes en la vista de agentes
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<AgenteDTO>>> GetAgentes()
    {
        var rol = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(id))
            return Unauthorized("Necesitas estar autenticado.");

        if (rol == "ADMIN") // Devolvemos todos los usuarios
        {
            return await _context.Agentes
                .OrderBy(x => x.Nombre)
                .Select(x => AgenteToDTO(x))
                .ToListAsync();
        }
        else if (rol == "USER") // Devolvemos solo los que tengan que ver con el agente
        {
            int agenteId = int.Parse(id);
            var agente = await _context.Agentes.FindAsync(agenteId);

            if (agente == null) return NotFound();

            return await _context.Agentes
                .Where(x => x.EquipoId == agente.EquipoId)
                .OrderBy(x => x.Nombre)
                .Select(x => AgenteToDTO(x))
                .ToListAsync();
        }
        else
        {
            return Forbid();
        }
            
    }

    // USO: Obtener el agente al hacer login.
    // Debe tener acceso todo el mundo.
    [HttpGet("{id}")]
    public async Task<ActionResult<Agente>> GetAgente(int id)
    {
        var ag = await _context.Agentes.FindAsync(id);
        if (ag == null)
            return NotFound();

        return ag;
    }

    // USO: Vista principal para obtener número de op/eq/ag
    // Mejor limitado a alguien con rol.
    [HttpGet("count")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<int>> GetAgentesCount()
    {
        int count = await _context.Agentes.CountAsync();
        return count;
    }

    // USO: Crear agente desde la vista de agentes. 
    // Limitado a rol ADMIN
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<IEnumerable<AgenteDTO>>> PostAgente(AgenteDTO ag)
    {
        var newAg = AgenteDTOToAgente(ag);

        var hasher = new PasswordHasher<Agente>();
        newAg.Contraseña = hasher.HashPassword(newAg, "contraseña");

        newAg.Email = newAg.Nombre.ToLower() + "@cyberpulselabs.com";
        newAg.Rol = "USER";

        _context.Agentes.Add(newAg);
        await _context.SaveChangesAsync();

        return await GetAgentes();
    }

    // USO: Editar agente, desde vista de agentes o perfil.
    // ADMIN en vista de agentes, USER en perfil.
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<AgenteDTO>>> PutAgente(int id, AgenteDTO newAg)
    {
        if (id != newAg.Id)
            return BadRequest("El ID del cuerpo no coincide con el de la URL.");

        var rol = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(userIdClaim))
            return Unauthorized("No se pudo identificar al usuario.");

        int userId = int.Parse(userIdClaim);

        // Si es USER, solo puede editarse a sí mismo
        if (rol == "USER" && id != userId)
            return Forbid();

        // ADMIN puede editar a cualquier agente
        var ag = await _context.Agentes.FindAsync(id);
        if (ag == null)
            return NotFound();

        ag.Nombre = newAg.Nombre;
        ag.Activo = newAg.Activo;
        ag.Rango = newAg.Rango;
        ag.EquipoId = newAg.EquipoId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!AgenteExists(id))
        {
            return NotFound();
        }

        return await GetAgentes();
    }


    // USO: Borrar agentes dándole al icono de la papelera.
    // Limitado a admin.
    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<IEnumerable<AgenteDTO>>> DeleteAgente(int id)
    {
        var ag = await _context.Agentes.FindAsync(id);
        if (ag == null)
            return NotFound();

        _context.Agentes.Remove(ag);
        await _context.SaveChangesAsync();

        return await GetAgentes();
    }


    private bool AgenteExists(int id)
    {
        return _context.Agentes.Any(e => e.Id == id);
    }

    public static Agente AgenteDTOToAgente(AgenteDTO dto)
    {
        return new Agente
        {
            Nombre = dto.Nombre,
            Activo = dto.Activo,
            Rango = dto.Rango,
            EquipoId = dto.EquipoId
        };
    }

    public static AgenteDTO AgenteToDTO(Agente ag)
    {
        return new AgenteDTO
        {
            Id = ag.Id,
            Nombre = ag.Nombre,
            Activo = ag.Activo,
            EquipoId = ag.EquipoId,
            Rango = ag.Rango
        };
    }
}
