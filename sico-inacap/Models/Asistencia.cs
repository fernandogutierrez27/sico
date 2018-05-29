using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class Asistencia
    {
        public int CodigoEvento { get; set; }
        public string UsernameAsistente { get; set; }

        public Evento CodigoEventoNavigation { get; set; }
        public Usuario UsernameAsistenteNavigation { get; set; }
    }
}
