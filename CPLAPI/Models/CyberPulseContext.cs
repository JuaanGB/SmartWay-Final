using Microsoft.EntityFrameworkCore;

namespace CPLAPI.Models;

public class CyberPulseContext : DbContext
{
    public CyberPulseContext(DbContextOptions<CyberPulseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Operacion>()
            .HasMany(e => e.Equipos)
            .WithOne(e => e.Operacion)
            .HasForeignKey(e => e.OperacionId)
            .HasPrincipalKey(e => e.Id)
            ;
    }

    public DbSet<Agente> Agentes { get; set; } = null!;
    public DbSet<Operacion> Operaciones { get; set; } = null!;
    public DbSet<Equipo> Equipos { get; set; } = null!;
}