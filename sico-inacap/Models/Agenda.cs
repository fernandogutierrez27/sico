using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class Agenda
    {
        public Agenda()
        {
            AgendaBloque = new HashSet<AgendaBloque>();
        }

        public int Codigo { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraTermino { get; set; }
        public int CodigoEvento { get; set; }
        public int CodigoRecinto { get; set; }

        public Evento CodigoEventoNavigation { get; set; }
        public Recinto CodigoRecintoNavigation { get; set; }
        public ICollection<AgendaBloque> AgendaBloque { get; set; }
    }
}
