using System;
using System.Collections.Generic;

namespace FACPYA.Models
{
    public partial class Reservacion
    {
        public int IdReservacion { get; set; }

        public int ClienteId { get; set; }

        public int PaqueteId { get; set; }

        public DateTime? FechaReservacion { get; set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual PaqueteViaje? Paquete { get; set; }
    }
}
