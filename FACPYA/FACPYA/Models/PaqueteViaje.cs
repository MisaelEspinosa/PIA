using System;
using System.Collections.Generic;

namespace FACPYA.Models;

public partial class PaqueteViaje
{
    public int IdPaquete { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int DestinoPaisId { get; set; }

    public virtual Pais DestinoPais { get; set; } = null!;

    public virtual ICollection<Reservacion> Reservacions { get; set; } = new List<Reservacion>();
}
