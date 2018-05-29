using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class Recinto
    {
        public Recinto()
        {
            Agenda = new HashSet<Agenda>();
        }

        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }

        public ICollection<Agenda> Agenda { get; set; }
    }
}
