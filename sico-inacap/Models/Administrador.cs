using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class Administrador
    {
        public Administrador()
        {
            Evento = new HashSet<Evento>();
        }

        public string Username { get; set; }

        public Usuario UsernameNavigation { get; set; }
        public ICollection<Evento> Evento { get; set; }
    }
}
