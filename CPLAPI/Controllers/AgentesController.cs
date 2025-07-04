using CPLAPI.Models;
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
    public async Task<ActionResult<IEnumerable<Agente>>> GetAgentes()
    {
        return await _context.Agentes
            .OrderBy(x => x.Nombre)
            .Select(x => x)
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
    public async Task<ActionResult<IEnumerable<Agente>>> PostAgente(AgenteDTO ag)
    {

        var newAg = AgenteDTOToAgente(ag);

        _context.Agentes.Add(newAg);
        await _context.SaveChangesAsync();

        return await GetAgentes();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<Agente>>> PutAgente(int id, Agente newAg)
    {
        if (id != newAg.Id)
            return BadRequest();

        var ag = await _context.Agentes.FindAsync(id);
        if (ag == null)
            return NotFound();

        ag.Id = newAg.Id;
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
    public async Task<ActionResult<IEnumerable<Agente>>> DeleteAgente(int id)
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
}
