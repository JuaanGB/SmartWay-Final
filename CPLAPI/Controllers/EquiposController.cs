using CPLAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CPLAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquiposController : ControllerBase
{
    private readonly CyberPulseContext _context;
    public EquiposController(CyberPulseContext context)
    {
        _context = context;
    }

    // GET: api/equipos
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Equipo>>> GetEquipos()
    {
        var rol = User.FindFirst(ClaimTypes.Role)?.Value;
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (rol == "ADMIN") // Todos los equipos
        {
            return await _context.Equipos
                .Include(x => x.Agentes)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }
        else if (rol == "USER" && int.TryParse(userIdClaim, out int userId)) // Sólo el equipo del agente
        {
            var agente = await _context.Agentes.FindAsync(userId);
            if (agente == null || agente.EquipoId == null)
                return NotFound("No se encontró el agente o no tiene equipo asignado.");

            var equipo = await _context.Equipos
                .Include(x => x.Agentes)
                .FirstOrDefaultAsync(x => x.Id == agente.EquipoId);

            return equipo != null ? new List<Equipo> { equipo } : NotFound("No se encontró el equipo.");
        }

        return Forbid();
    }

    // GET: api/equipos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Equipo>> GetEquipo(int id)
    {
        var equipo = await _context.Equipos
            .Include(x => x.Agentes)
            .FirstOrDefaultAsync(x => x.Id == id);

        return equipo == null ? NotFound() : equipo;
    }

    // GET: api/equipos/count
    [HttpGet("count")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<int>> GetEquiposCount()
    {
        return await _context.Equipos.CountAsync();
    }

    // POST: api/equipos
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<IEnumerable<Equipo>>> CreateEquipo(EquipoDTO equipo)
    {
        var nuevoEquipo = EquipoDtoToEquipo(equipo);
        _context.Equipos.Add(nuevoEquipo);
        await _context.SaveChangesAsync();

        return await GetEquipos();
    }

    // PATCH: api/equipos/5
    [HttpPatch("{id}")]
    [Authorize(Roles = "ADMIN")] // A lo mejor es algo bruto así, se podría considerar un rol 'jefe de equipo' y
    // que él también lo pudiese editar
    public async Task<ActionResult<IEnumerable<Equipo>>> UpdateEquipo(int id, Equipo updatedEquipo)
    {
        if (id != updatedEquipo.Id)
            return BadRequest("El ID en la URL no coincide con el equipo enviado.");

        var existingEquipo = await _context.Equipos.FindAsync(id);
        if (existingEquipo == null)
            return NotFound();

        existingEquipo.Nombre = updatedEquipo.Nombre;
        existingEquipo.Especialidad = updatedEquipo.Especialidad;
        existingEquipo.OperacionId = updatedEquipo.OperacionId;

        await _context.SaveChangesAsync();
        return await GetEquipos();
    }

    // DELETE: api/equipos/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<IEnumerable<Equipo>>> DeleteEquipo(int id)
    {
        var equipo = await _context.Equipos.FindAsync(id);
        if (equipo == null)
            return NotFound();

        _context.Equipos.Remove(equipo);
        await _context.SaveChangesAsync();

        return await GetEquipos();
    }

    public static Equipo EquipoDtoToEquipo(EquipoDTO dto)
    {
        return new Equipo
        {
            Nombre = dto.Nombre,
            Especialidad = dto.Especialidad,
            OperacionId = dto.OperacionId
        };
    }
}
