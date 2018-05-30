namespace SicoInacap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Evento = new HashSet<Evento>();
            Evento1 = new HashSet<Evento>();
        }

        [Key]
        [StringLength(50)]
        public string Username { get; set; }

        public DateTime FechaInscripcion { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual Administrador Administrador { get; set; }

        public virtual Miembro Miembro { get; set; }

        public virtual Simpatizante Simpatizante { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evento> Evento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evento> Evento1 { get; set; }
    }
}
