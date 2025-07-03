using CPLAPI.Models;
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
    public async Task<ActionResult<IEnumerable<Operacion>>> GetOperaciones()
    {
        return await _context.Operaciones
            .OrderBy(x => x.Id)
            .Select(x => x)
            .ToListAsync();
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
    public async Task<ActionResult<int>> GetOperacionesCount()
    {
        int count = await _context.Operaciones.CountAsync();
        return count;
    }

    // Estaría elegante que el post devolviese la nueva lista de operaciones, por si alguien más modificó la lista
    // de operaciones al mismo tiempo
    [HttpPost]
    public async Task<ActionResult<IEnumerable<Operacion>>> PostOperacion(Operacion op)
    {
        if (op.Id == null) return BadRequest();
        if (OperacionExists(op.Id)) return UnprocessableEntity();
        if (op.Id?.Trim() == "")
            op.Id = (op.Nombre + '-' + op.FechaInicio + '-'+ op.FechaFin).Replace('/', '-');

        _context.Operaciones.Add(op);
        await _context.SaveChangesAsync();

        return await GetOperaciones();
    }

    // También devolvemos todas las operaciones actualizadas por si añadiesen nuevas
    [HttpPut("{id}")]
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
