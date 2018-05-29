using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class AgendaBloque
    {
        public int CodigoAgenda { get; set; }
        public int CodigoBloque { get; set; }

        public Agenda CodigoAgendaNavigation { get; set; }
        public Bloque CodigoBloqueNavigation { get; set; }
    }
}
