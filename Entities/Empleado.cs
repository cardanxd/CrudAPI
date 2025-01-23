using System;

namespace CrudAPI.Entities;

public class Empleado
{
    public int IdEmpleado { get; set; }
    public string NompreCompleto { get; set; } = string.Empty;
    public int Sueldo { get; set; }
    public int IdPerfil { get; set; }
    public virtual Perfil PerfilReferencia { get; set; }
}
