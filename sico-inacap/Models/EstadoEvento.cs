using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class EstadoEvento
    {
        public EstadoEvento()
        {
            Evento = new HashSet<Evento>();
        }

        public int Codigo { get; set; }
        public string Nombre { get; set; }

        public ICollection<Evento> Evento { get; set; }
    }
}
