using System;
using Microsoft.EntityFrameworkCore;
using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;

namespace CrudAPI.Services;

public class EmpleadoService
{
    private readonly AppDbContext _context;
    public EmpleadoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmpleadoDTO>> Get()
    {
        var listaDto = new List<EmpleadoDTO>();
        foreach (var item in await _context.Empleados.Include(p => p.PerfilReferencia).ToListAsync())
        {
            listaDto.Add(new EmpleadoDTO
            {
                IdEmpleado = item.IdEmpleado,
                NompreCompleto = item.NompreCompleto,
                Sueldo = item.Sueldo,
                IdPerfil = item.IdPerfil,
                NombrePerfil = item.PerfilReferencia.Nombre
            });
        }

        return listaDto;
    }

    public async Task<EmpleadoDTO?> First(int id)
    {
        var empleadoDto = new EmpleadoDTO();
        var empleadoDb = await _context.Empleados.Include(p => p.PerfilReferencia).Where(e => e.IdEmpleado == id).FirstOrDefaultAsync();
        if (empleadoDb is null) return null;
        empleadoDto.IdEmpleado = empleadoDb.IdEmpleado;
        empleadoDto.NompreCompleto = empleadoDb.NompreCompleto;
        empleadoDto.Sueldo = empleadoDb.Sueldo;
        empleadoDto.IdPerfil = empleadoDb.IdPerfil;
        empleadoDto.NombrePerfil = empleadoDb.PerfilReferencia.Nombre;
        return empleadoDto;
    }

    public async Task<EmpleadoDTO?> Create(EmpleadoDTO empleadoDTO)
    {
        var empleadoDb = new Empleado
        {
            NompreCompleto = empleadoDTO.NompreCompleto,
            Sueldo = empleadoDTO.Sueldo,
            IdPerfil = empleadoDTO.IdPerfil
        };
        await _context.Empleados.AddAsync(empleadoDb);
        if (await _context.SaveChangesAsync() == 0) return null;
        empleadoDTO.IdEmpleado = empleadoDb.IdEmpleado;
        return empleadoDTO;
    }

    public async Task<EmpleadoDTO?> Update(EmpleadoDTO empleadoDTO)
    {
        var empleadoDb = await _context.Empleados.Where(e => e.IdEmpleado == empleadoDTO.IdEmpleado).FirstOrDefaultAsync();
        if (empleadoDb is null) return null;
        empleadoDb.NompreCompleto = empleadoDTO.NompreCompleto;
        empleadoDb.Sueldo = empleadoDTO.Sueldo;
        empleadoDb.IdPerfil = empleadoDTO.IdPerfil;
        _context.Empleados.Update(empleadoDb);
        if (await _context.SaveChangesAsync() == 0) return null;
        return empleadoDTO;
    }

    public async Task<bool> Delete(int id)
    {
        var empleadoDb = await _context.Empleados.Where(e => e.IdEmpleado == id).FirstOrDefaultAsync();
        if (empleadoDb is null) return false;
        _context.Empleados.Remove(empleadoDb);
        return await _context.SaveChangesAsync() > 0;
    }
}
