namespace SicoInacap.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AgendaBloque
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Codigo { get; set; }

        public int CodigoAgenda { get; set; }

        public int CodigoBloque { get; set; }

        public DateTime Fecha { get; set; }

        public virtual Agenda Agenda { get; set; }

        public virtual Bloque Bloque { get; set; }

    }
}