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
            .OnDelete(DeleteBehavior.SetNull)
            .HasPrincipalKey(e => e.Id)
            ;

        modelBuilder.Entity<Equipo>()
            .HasMany(e => e.Agentes)
            .WithOne(e => e.Equipo)
            .HasForeignKey(e => e.EquipoId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasPrincipalKey(e => e.Id)
            ;

        modelBuilder.Entity<Agente>()
            .HasIndex(a => a.Email)
            .IsUnique()
        ;

        base.OnModelCreating(modelBuilder);

    }

    public DbSet<Agente> Agentes { get; set; } = null!;
    public DbSet<Operacion> Operaciones { get; set; } = null!;
    public DbSet<Equipo> Equipos { get; set; } = null!;
}