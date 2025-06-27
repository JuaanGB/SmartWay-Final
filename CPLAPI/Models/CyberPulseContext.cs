using Microsoft.EntityFrameworkCore;

namespace CPLAPI.Models;

public class CyberPulseContext : DbContext
{
    public CyberPulseContext(DbContextOptions<CyberPulseContext> options)
        : base(options)
    {
    }

    // public DbSet<Agente> Agentes { get; set; } = null!;
    public DbSet<Operacion> Operaciones { get; set; } = null!;
    // public DbSet<Equipo> Equipos { get; set; } = null!;
}