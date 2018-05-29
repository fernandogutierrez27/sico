using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Asistencia = new HashSet<Asistencia>();
            Interes = new HashSet<Interes>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime FechaInscripcion { get; set; }

        public Administrador Administrador { get; set; }
        public Miembro Miembro { get; set; }
        public Simpatizante Simpatizante { get; set; }
        public ICollection<Asistencia> Asistencia { get; set; }
        public ICollection<Interes> Interes { get; set; }
    }
}
