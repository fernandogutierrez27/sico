namespace SicoInacap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Miembro")]
    public partial class Miembro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Miembro()
        {
            Evento = new HashSet<Evento>();
        }

        [Key]
        [StringLength(50)]
        public string Username { get; set; }

        public int CodigoCargo { get; set; }

        public DateTime FechaIngreso { get; set; }

        [Required]
        [StringLength(9)]
        public string Fono { get; set; }

        [Required]
        [StringLength(10)]
        public string RUN { get; set; }

        public virtual Cargo Cargo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evento> Evento { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
