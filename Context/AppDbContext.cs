using System;
using CrudAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Perfil> Perfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Perfil>(tb =>
        {
            tb.HasKey(col => col.IdPerfil); //Indicar cual es el primary key (Solamente cuando el id tiene otro nombre)
            tb.Property(col => col.IdPerfil).UseIdentityColumn().ValueGeneratedOnAdd(); //Para que sea auto incrementable (lo mismo que el primary key)
            tb.Property(col => col.Nombre).HasMaxLength(50);
            // tb.ToTable("Perfil"); //Para cambiar el nombre de la tabla
            tb.HasData(
                new Perfil { IdPerfil = 1, Nombre = "Programador JR" },
                new Perfil { IdPerfil = 2, Nombre = "Programador MID" },
                new Perfil { IdPerfil = 3, Nombre = "Programador Senior" }
            );
        });

        modelBuilder.Entity<Empleado>(tb =>
        {
            tb.HasKey(col => col.IdEmpleado); //Indicar cual es el primary key (Solamente cuando el id tiene otro nombre)
            tb.Property(col => col.IdEmpleado).UseIdentityColumn().ValueGeneratedOnAdd(); //Para que sea auto incrementable (lo mismo que el primary key)
            tb.Property(col => col.NompreCompleto).HasMaxLength(50);
            tb.HasOne(col => col.PerfilReferencia).WithMany(p => p.EmpleadosReferencia).HasForeignKey(col => col.IdPerfil); //Relacionando que un empleado tiene un perfil y a su vez el perfil tiene muchos empleados
        });
    }
}
