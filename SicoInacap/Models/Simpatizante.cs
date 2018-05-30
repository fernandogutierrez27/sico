namespace SicoInacap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Simpatizante")]
    public partial class Simpatizante
    {
        [Key]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int Genero { get; set; }

        [Required]
        public string Nombres { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
