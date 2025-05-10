using System;
using System.Collections.Generic;

namespace FACPYA.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Telefono { get; set; }

    public int PaisOrigenId { get; set; }

    public virtual Pais PaisOrigen { get; set; } = null!;

    public virtual ICollection<Reservacion> Reservacions { get; set; } = new List<Reservacion>();
}
