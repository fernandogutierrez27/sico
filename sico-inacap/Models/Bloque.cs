using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class Bloque
    {
        public Bloque()
        {
            AgendaBloque = new HashSet<AgendaBloque>();
        }

        public int Codigo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }

        public ICollection<AgendaBloque> AgendaBloque { get; set; }
    }
}
