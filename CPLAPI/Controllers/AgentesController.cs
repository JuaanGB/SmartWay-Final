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

    // GET: api/Agentes
    [NonAction]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<IEnumerable<AgenteDTO>>> GetAgentes()
    {
        return await _context.Agentes
            .OrderBy(x => x.Nombre)
            .Select(x => AgenteToDTO(x))
            .ToListAsync();
    }

    [HttpGet]
    [Authorize(Roles = "USER")]
    public async Task<ActionResult<IEnumerable<AgenteDTO>>> GetAgentesCompañeros()
    {
        string decoded = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

        Agente agente = await _context.Agentes.FindAsync(Request.Headers[HeaderNames.Authorization]);
        Console.WriteLine(Int32.Parse(ClaimTypes.NameIdentifier));

        return await _context.Agentes
            .OrderBy(x => x.Nombre)
            .Select(x => AgenteToDTO(x))
            .Where(x => x.EquipoId == agente.EquipoId)
            .ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Agente>> GetAgente(int id)
    {
        var ag = await _context.Agentes.FindAsync(id);
        if (ag == null)
            return NotFound();

        return ag;
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetAgentesCount()
    {
        int count = await _context.Agentes.CountAsync();
        return count;
    }

    [HttpPost]
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


    [HttpPatch("{id}")]
    public async Task<ActionResult<IEnumerable<AgenteDTO>>> PutAgente(int id, AgenteDTO newAg)
    {
        if (id != newAg.Id)
            return BadRequest();

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

    [HttpDelete("{id}")]
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
