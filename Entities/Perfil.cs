using System;

namespace CrudAPI.Entities;

public class Perfil
{
    public int IdPerfil { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public virtual ICollection<Empleado> EmpleadosReferencia { get; set; }
}
