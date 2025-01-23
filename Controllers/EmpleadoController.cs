using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmpleadoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<EmpleadoDTO>>> Get()
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
            return Ok(listaDto);
        }

        [HttpGet]
        [Route("lista/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> GetEmpleado(int id)
        {
            var empleadoDto = new EmpleadoDTO();
            var empleadoDb = await _context.Empleados.Include(p => p.PerfilReferencia).Where(e => e.IdEmpleado == id).FirstAsync();
            if (empleadoDb is null) return NotFound("Empleado no encontrado");
            empleadoDto.IdEmpleado = empleadoDb.IdEmpleado;
            empleadoDto.NompreCompleto = empleadoDb.NompreCompleto;
            empleadoDto.Sueldo = empleadoDb.Sueldo;
            empleadoDto.IdPerfil = empleadoDb.IdPerfil;
            empleadoDto.NombrePerfil = empleadoDb.PerfilReferencia.Nombre;
            return Ok(empleadoDto);
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult<EmpleadoDTO>> Guardar(EmpleadoDTO empleadoDTO)
        {
            var empleadoDb = new Empleado
            {
                NompreCompleto = empleadoDTO.NompreCompleto,
                Sueldo = empleadoDTO.Sueldo,
                IdPerfil = empleadoDTO.IdPerfil
            };

            await _context.Empleados.AddAsync(empleadoDb);
            await _context.SaveChangesAsync();
            return Ok("Empleado agregado");
        }

        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult<EmpleadoDTO>> Editar(EmpleadoDTO empleadoDTO)
        {
            var empleadoDb = await _context.Empleados.Include(p => p.PerfilReferencia).Where(e => e.IdEmpleado == empleadoDTO.IdEmpleado).FirstAsync();
            if(empleadoDb is null) return NotFound("Empleado no encontrado");
            empleadoDb.NompreCompleto = empleadoDTO.NompreCompleto;
            empleadoDb.Sueldo = empleadoDTO.Sueldo;
            empleadoDb.IdPerfil = empleadoDTO.IdPerfil;
            _context.Empleados.Update(empleadoDb);
            await _context.SaveChangesAsync();
            return Ok("Empleado modificado");
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> Eliminar(int id)
        {
            var empleadoDb = await _context.Empleados.FindAsync(id);
            if(empleadoDb is null) return NotFound("Empleado no encontrado");
            _context.Empleados.Remove(empleadoDb);
            await _context.SaveChangesAsync();
            return Ok("Empleado eliminado");
        }

    }
}
