using System;
using System.Collections.Generic;

namespace sicoInacap.Models
{
    public partial class Evento
    {
        public Evento()
        {
            Agenda = new HashSet<Agenda>();
            Asistencia = new HashSet<Asistencia>();
            Interes = new HashSet<Interes>();
        }

        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public int CodigoCategoria { get; set; }
        public string UsernameOrganizador { get; set; }
        public string UsernameResponsable { get; set; }
        public int CodigoEstado { get; set; }

        public Categoria CodigoCategoriaNavigation { get; set; }
        public EstadoEvento CodigoEstadoNavigation { get; set; }
        public Administrador UsernameOrganizadorNavigation { get; set; }
        public Miembro UsernameResponsableNavigation { get; set; }
        public ICollection<Agenda> Agenda { get; set; }
        public ICollection<Asistencia> Asistencia { get; set; }
        public ICollection<Interes> Interes { get; set; }
    }
}
