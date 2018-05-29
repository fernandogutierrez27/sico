using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class Cargo
    {
        public Cargo()
        {
            Miembro = new HashSet<Miembro>();
        }

        public int Codigo { get; set; }
        public string Nombre { get; set; }

        public ICollection<Miembro> Miembro { get; set; }
    }
}
