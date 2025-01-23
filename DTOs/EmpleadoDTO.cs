using System;

namespace CrudAPI.DTOs;

public class EmpleadoDTO
{
    public int IdEmpleado { get; set; }
    public string NompreCompleto { get; set; } = string.Empty;
    public int Sueldo { get; set; }
    public int IdPerfil { get; set; }
    public string NombrePerfil { get; set; } = string.Empty;
}
