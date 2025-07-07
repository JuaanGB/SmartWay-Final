using CPLAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipo>>> GetEquipos()
    {
        return await _context.Equipos
            .Include(x => x.Agentes)
            .OrderBy(x => x.Id)
            .Select(x => x)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Equipo>> GetEquipo(int id)
    {
        var eq = await _context.Equipos.FindAsync(id);
        if (eq == null)
            return NotFound();

        return eq;
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetOperacionesCount()
    {
        int count = await _context.Equipos.CountAsync();
        return count;
    }

    // POST: api/equipo
    [HttpPost]
    public async Task<ActionResult<IEnumerable<Equipo>>> CreateEquipo(EquipoDTO equipo)
    {
        
        var nuevoEquipo = EquipoDtoToEquipo(equipo);
        _context.Equipos.Add(nuevoEquipo);
        await _context.SaveChangesAsync();

        return await GetEquipos();
    }

    // PUT: api/equipos/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<Equipo>>> UpdateEquipo(int id, Equipo updatedEquipo)
    {
        if (id != updatedEquipo.Id)
        {
            return BadRequest("El ID en la URL no coincide con el ID del equipo.");
        }

        var existingEquipo = await _context.Equipos.FindAsync(id);
        if (existingEquipo == null)
        {
            return NotFound();
        }

        existingEquipo.Nombre = updatedEquipo.Nombre;
        existingEquipo.Especialidad = updatedEquipo.Especialidad;
        existingEquipo.OperacionId = updatedEquipo.OperacionId;

        await _context.SaveChangesAsync();

        return await GetEquipos();
    }


    // DELETE: api/equipo/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<IEnumerable<Equipo>>> DeleteEquipo(int id)
    {
        var equipo = await _context.Equipos.FindAsync(id);
        if (equipo == null)
        {
            return NotFound();
        }
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
