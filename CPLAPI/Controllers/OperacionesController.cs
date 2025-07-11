using System.Security.Claims;
using CPLAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPLAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperacionesController : ControllerBase
{
    private readonly CyberPulseContext _context;
    public OperacionesController(CyberPulseContext context)
    {
        _context = context;
    }

    // GET: api/Operaciones
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Operacion>>> GetOperaciones()
    {
        var rol = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(id))
            return Unauthorized("Necesitas estar autenticado");

        if (rol == "ADMIN")
        {
            return await _context.Operaciones
                .OrderBy(x => x.Id)
                .Select(x => x)
                .ToListAsync();
        }
        else if (rol == "USER")
        {
            int agenteId = int.Parse(id);
            var agente = await _context.Agentes.FindAsync(agenteId);
            if (agente == null) return NotFound();

            var equipo = await _context.Equipos.FindAsync(agente.EquipoId);
            if (equipo == null) return Ok(); // Devolvemos 200 porque puede ser que el agente no tenga ningun equipo asignado

            var operacion = await _context.Operaciones.FirstOrDefaultAsync(x => x.Id == equipo.OperacionId);
            if (operacion == null) return Ok(); // Equipo puede no tener operacion

            return new List<Operacion> { operacion };
        }
        else
        {
            return Forbid();
        }

        
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Operacion>> GetOperacion(string id)
    {
        var op = await _context.Operaciones.FindAsync(id);
        if (op == null)
            return NotFound();

        return op;
    }

    [HttpGet("count")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<int>> GetOperacionesCount()
    {
        int count = await _context.Operaciones.CountAsync();
        return count;
    }

    // Estaría elegante que el post devolviese la nueva lista de operaciones, por si alguien más modificó la lista
    // de operaciones al mismo tiempo
    [HttpPost]
    [Authorize(Roles = "ADMIN")] // Si existiese un jefe de equipo, también debería él poder crear ops.
    public async Task<ActionResult<IEnumerable<Operacion>>> PostOperacion(Operacion op)
    {
        if (op.Id == null) return BadRequest();
        if (OperacionExists(op.Id)) return UnprocessableEntity();
        if (op.Id?.Trim() == "")
            op.Id = (op.Nombre + '-' + op.FechaInicio + '-' + op.FechaFin).Replace('/', '-');

        _context.Operaciones.Add(op);
        await _context.SaveChangesAsync();

        return await GetOperaciones();
    }

    // También devolvemos todas las operaciones actualizadas por si añadiesen nuevas
    [HttpPatch("{id}")]
    [Authorize(Roles = "ADMIN")] // Jefe de equipo (si existiese) también podría
    public async Task<ActionResult<IEnumerable<Operacion>>> PutOperacion(string id, Operacion newOp)
    {
        if ((id != newOp.Id) || newOp.FechaFin <= newOp.FechaInicio)
            return BadRequest();

        var op = await _context.Operaciones.FindAsync(id);
        if (op == null)
            return NotFound();

        op.Id = newOp.Id;
        op.Nombre = newOp.Nombre;
        op.Estado = newOp.Estado;
        op.FechaInicio = newOp.FechaInicio;
        op.FechaFin = newOp.FechaFin;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!OperacionExists(id))
        {
            return NotFound();
        }

        return await GetOperaciones();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<IEnumerable<Operacion>>> DeleteOperacion(string id)
    {
        var op = await _context.Operaciones.FindAsync(id);
        if (op == null)
            return NotFound();

        _context.Operaciones.Remove(op);
        await _context.SaveChangesAsync();

        return await GetOperaciones();
    }

    
    private bool OperacionExists(string id)
    {
        return _context.Operaciones.Any(e => e.Id == id);
    }
}
