using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;
using CrudAPI.Services;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoService _empleadoService;
        public EmpleadoController(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<EmpleadoDTO>>> Get()
        {
            return Ok(await _empleadoService.Get());
        }

        [HttpGet]
        [Route("lista/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> GetEmpleado(int id)
        {
            var empleadoDto = await _empleadoService.First(id);
            if (empleadoDto is null) return NotFound("Empleado no encontrado");
            return Ok(empleadoDto);
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult<EmpleadoDTO>> Guardar(EmpleadoDTO empleadoDTO)
        {
            if(await _empleadoService.Create(empleadoDTO) is null) return NotFound("No se pudo guardar el empleado.");
            return Ok("Empleado agregado");
        }

        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult<EmpleadoDTO>> Editar(EmpleadoDTO empleadoDTO)
        {
            if (await _empleadoService.Update(empleadoDTO) is null) return NotFound("No se pudo actualizar el empleado");
            return Ok("Empleado modificado");
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> Eliminar(int id)
        {
            if(await _empleadoService.Delete(id) is false) return NotFound("No se pudo eliminar el empleado");
            return Ok("Empleado eliminado");
        }

    }
}
