namespace SicoInacap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Evento")]
    public partial class Evento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Evento()
        {
            Agenda = new HashSet<Agenda>();
            Usuario = new HashSet<Usuario>();
            Usuario1 = new HashSet<Usuario>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Codigo { get; set; }

        public int CodigoCategoria { get; set; }

        public int CodigoEstado { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Icono { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string UsernameOrganizador { get; set; }

        [StringLength(50)]
        public string UsernameResponsable { get; set; }

        public virtual Administrador Administrador { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agenda> Agenda { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual EstadoEvento EstadoEvento { get; set; }

        public virtual Miembro Miembro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario1 { get; set; }
    }
}
