using CPLAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgenteDTO>>> GetAgentes()
    {
        return await _context.Agentes
            .OrderBy(x => x.Nombre)
            .Select(x => AgenteToDTO(x))
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

        _context.Agentes.Add(newAg);
        await _context.SaveChangesAsync();

        return await GetAgentes();
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<AgenteDTO>>> PutAgente(string email, Agente newAg)
    {
        if (email != newAg.Email)
            return BadRequest();

        var ag = await _context.Agentes.FindAsync(email);
        if (ag == null)
            return NotFound();

        ag.Email = newAg.Email;
        ag.Nombre = newAg.Nombre;
        ag.Activo = newAg.Activo;
        ag.Rango = newAg.Rango;
        ag.EquipoId = newAg.EquipoId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!AgenteExists(email))
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


    private bool AgenteExists(string email)
    {
        return _context.Agentes.Any(e => e.Email == email);
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
