namespace SicoInacap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agenda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agenda()
        {
            Bloque = new HashSet<Bloque>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Codigo { get; set; }

        public int CodigoEvento { get; set; }

        public int CodigoRecinto { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraTermino { get; set; }

        public virtual Evento Evento { get; set; }

        public virtual Recinto Recinto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bloque> Bloque { get; set; }
    }
}
