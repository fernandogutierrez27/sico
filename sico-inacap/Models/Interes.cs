using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class Interes
    {
        public int CodigoEvento { get; set; }
        public string UsernameInteresado { get; set; }

        public Evento CodigoEventoNavigation { get; set; }
        public Usuario UsernameInteresadoNavigation { get; set; }
    }
}
