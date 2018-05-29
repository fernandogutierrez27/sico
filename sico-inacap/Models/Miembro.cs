using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class Miembro
    {
        public Miembro()
        {
            Evento = new HashSet<Evento>();
        }

        public string Username { get; set; }
        public string Run { get; set; }
        public string Fono { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int CodigoCargo { get; set; }

        public Cargo CodigoCargoNavigation { get; set; }
        public Usuario UsernameNavigation { get; set; }
        public ICollection<Evento> Evento { get; set; }
    }
}
