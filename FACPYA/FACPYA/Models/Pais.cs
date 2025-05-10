using System;
using System.Collections.Generic;

namespace FACPYA.Models;

public partial class Pais
{
    public int IdPais { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Region { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<PaqueteViaje> PaqueteViajes { get; set; } = new List<PaqueteViaje>();
}
