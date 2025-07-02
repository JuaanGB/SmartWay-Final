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
            .OrderBy(x => x.Id)
            .Select(x => x)
            .ToListAsync();
    }

    // POST: api/equipo
    [HttpPost]
    public async Task<ActionResult<IEnumerable<Equipo>>> CreateEquipo(Equipo equipo) // Interesante el DTO aquí? Omitiendo el ID
    {
        if (string.IsNullOrEmpty(equipo.Id))
        {
            return BadRequest("El ID es obligatorio.");
        }

        _context.Equipos.Add(equipo);
        await _context.SaveChangesAsync();

        return await GetEquipos();
    }

    // PUT: api/equipos/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<Equipo>>> UpdateEquipo(string id, Equipo updatedEquipo)
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
        existingEquipo.Operacion = updatedEquipo.Operacion;

        await _context.SaveChangesAsync();

        return await GetEquipos();
    }


    // DELETE: api/equipo/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<IEnumerable<Equipo>>> DeleteEquipo(string id)
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



}
