namespace SicoInacap.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SicoModel : DbContext
    {
        public SicoModel()
            : base("name=SicoModel")
        {
        }

        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Agenda> Agenda { get; set; }
        public virtual DbSet<Bloque> Bloque { get; set; }
        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<EstadoEvento> EstadoEvento { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Miembro> Miembro { get; set; }
        public virtual DbSet<Recinto> Recinto { get; set; }
        public virtual DbSet<Simpatizante> Simpatizante { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Administrador>()
                .HasMany(e => e.Evento)
                .WithRequired(e => e.Administrador)
                .HasForeignKey(e => e.UsernameOrganizador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Agenda>()
                .HasMany(e => e.Bloque)
                .WithMany(e => e.Agenda)
                .Map(m => m.ToTable("AgendaBloque").MapLeftKey("CodigoAgenda").MapRightKey("CodigoBloque"));

            modelBuilder.Entity<Cargo>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Cargo>()
                .HasMany(e => e.Miembro)
                .WithRequired(e => e.Cargo)
                .HasForeignKey(e => e.CodigoCargo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Evento)
                .WithRequired(e => e.Categoria)
                .HasForeignKey(e => e.CodigoCategoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EstadoEvento>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<EstadoEvento>()
                .HasMany(e => e.Evento)
                .WithRequired(e => e.EstadoEvento)
                .HasForeignKey(e => e.CodigoEstado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Evento>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.Icono)
                .IsUnicode(false);

            modelBuilder.Entity<Evento>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Evento>()
                .Property(e => e.UsernameOrganizador)
                .IsUnicode(false);

            modelBuilder.Entity<Evento>()
                .Property(e => e.UsernameResponsable)
                .IsUnicode(false);

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Agenda)
                .WithRequired(e => e.Evento)
                .HasForeignKey(e => e.CodigoEvento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Usuario)
                .WithMany(e => e.Evento)
                .Map(m => m.ToTable("Asistencia").MapLeftKey("CodigoEvento").MapRightKey("UsernameAsistente"));

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Usuario1)
                .WithMany(e => e.Evento1)
                .Map(m => m.ToTable("Interes").MapLeftKey("CodigoEvento").MapRightKey("UsernameInteresado"));

            modelBuilder.Entity<Miembro>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Miembro>()
                .Property(e => e.Fono)
                .IsUnicode(false);

            modelBuilder.Entity<Miembro>()
                .Property(e => e.RUN)
                .IsUnicode(false);

            modelBuilder.Entity<Miembro>()
                .HasMany(e => e.Evento)
                .WithOptional(e => e.Miembro)
                .HasForeignKey(e => e.UsernameResponsable);

            modelBuilder.Entity<Recinto>()
                .Property(e => e.Latitud)
                .IsUnicode(false);

            modelBuilder.Entity<Recinto>()
                .Property(e => e.Longitud)
                .IsUnicode(false);

            modelBuilder.Entity<Recinto>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Recinto>()
                .HasMany(e => e.Agenda)
                .WithRequired(e => e.Recinto)
                .HasForeignKey(e => e.CodigoRecinto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Simpatizante>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Simpatizante>()
                .Property(e => e.Apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<Simpatizante>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Simpatizante>()
                .Property(e => e.Nombres)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasOptional(e => e.Administrador)
                .WithRequired(e => e.Usuario);

            modelBuilder.Entity<Usuario>()
                .HasOptional(e => e.Miembro)
                .WithRequired(e => e.Usuario);

            modelBuilder.Entity<Usuario>()
                .HasOptional(e => e.Simpatizante)
                .WithRequired(e => e.Usuario);
        }
    }
}
