using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class Simpatizante
    {
        public string Username { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Genero { get; set; }

        public Usuario UsernameNavigation { get; set; }
    }
}
