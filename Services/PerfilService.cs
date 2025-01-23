using System;
using Microsoft.EntityFrameworkCore;
using CrudAPI.Context;
using CrudAPI.DTOs;

namespace CrudAPI.Services;

public class PerfilService
{
    private readonly AppDbContext _context;
    public PerfilService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PerfilDTO>> Lista()
    {
        var listaDto = new List<PerfilDTO>();
        foreach (var item in await _context.Perfiles.ToListAsync())
        {
            listaDto.Add(new PerfilDTO
            {
                IdPerfil = item.IdPerfil,
                Nombre = item.Nombre
            });
        }
        return listaDto;
    }
}
