using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace sicoInacap.Models
{
    public partial class SicoDbContext : DbContext
    {
        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Agenda> Agenda { get; set; }
        public virtual DbSet<AgendaBloque> AgendaBloque { get; set; }
        public virtual DbSet<Asistencia> Asistencia { get; set; }
        public virtual DbSet<Bloque> Bloque { get; set; }
        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<EstadoEvento> EstadoEvento { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Interes> Interes { get; set; }
        public virtual DbSet<Miembro> Miembro { get; set; }
        public virtual DbSet<Recinto> Recinto { get; set; }
        public virtual DbSet<Simpatizante> Simpatizante { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=sicoserver.gogps.cl,80;Database=sico_db;User ID=sicoadmin;Password=sicoinacap;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.UsernameNavigation)
                    .WithOne(p => p.Administrador)
                    .HasForeignKey<Administrador>(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Administrador_Usuario");
            });

            modelBuilder.Entity<Agenda>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.HoraInicio).HasColumnType("datetime");

                entity.Property(e => e.HoraTermino).HasColumnType("datetime");

                entity.HasOne(d => d.CodigoEventoNavigation)
                    .WithMany(p => p.Agenda)
                    .HasForeignKey(d => d.CodigoEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agenda_Evento");

                entity.HasOne(d => d.CodigoRecintoNavigation)
                    .WithMany(p => p.Agenda)
                    .HasForeignKey(d => d.CodigoRecinto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agenda_Recinto");
            });

            modelBuilder.Entity<AgendaBloque>(entity =>
            {
                entity.HasKey(e => new { e.CodigoAgenda, e.CodigoBloque });

                entity.HasOne(d => d.CodigoAgendaNavigation)
                    .WithMany(p => p.AgendaBloque)
                    .HasForeignKey(d => d.CodigoAgenda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgendaBloque_Agenda");

                entity.HasOne(d => d.CodigoBloqueNavigation)
                    .WithMany(p => p.AgendaBloque)
                    .HasForeignKey(d => d.CodigoBloque)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgendaBloque_Bloque");
            });

            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.HasKey(e => new { e.CodigoEvento, e.UsernameAsistente });

                entity.Property(e => e.UsernameAsistente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoEventoNavigation)
                    .WithMany(p => p.Asistencia)
                    .HasForeignKey(d => d.CodigoEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asistencia_Evento");

                entity.HasOne(d => d.UsernameAsistenteNavigation)
                    .WithMany(p => p.Asistencia)
                    .HasForeignKey(d => d.UsernameAsistente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asistencia_Usuario");
            });

            modelBuilder.Entity<Bloque>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo).ValueGeneratedNever();
            });

            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstadoEvento>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Icono)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UsernameOrganizador)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsernameResponsable)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoCategoriaNavigation)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.CodigoCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evento_Categoria");

                entity.HasOne(d => d.CodigoEstadoNavigation)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.CodigoEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evento_EstadoEvento");

                entity.HasOne(d => d.UsernameOrganizadorNavigation)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.UsernameOrganizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evento_Administrador");

                entity.HasOne(d => d.UsernameResponsableNavigation)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.UsernameResponsable)
                    .HasConstraintName("FK_Evento_Miembro");
            });

            modelBuilder.Entity<Interes>(entity =>
            {
                entity.HasKey(e => new { e.CodigoEvento, e.UsernameInteresado });

                entity.Property(e => e.UsernameInteresado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoEventoNavigation)
                    .WithMany(p => p.Interes)
                    .HasForeignKey(d => d.CodigoEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interes_Evento");

                entity.HasOne(d => d.UsernameInteresadoNavigation)
                    .WithMany(p => p.Interes)
                    .HasForeignKey(d => d.UsernameInteresado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interes_Usuario");
            });

            modelBuilder.Entity<Miembro>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.FechaIngreso).HasColumnType("datetime");

                entity.Property(e => e.Fono)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Run)
                    .IsRequired()
                    .HasColumnName("RUN")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoCargoNavigation)
                    .WithMany(p => p.Miembro)
                    .HasForeignKey(d => d.CodigoCargo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Miembro_Cargo");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithOne(p => p.Miembro)
                    .HasForeignKey<Miembro>(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Miembro_Usuario");
            });

            modelBuilder.Entity<Recinto>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Latitud)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Longitud)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Simpatizante>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithOne(p => p.Simpatizante)
                    .HasForeignKey<Simpatizante>(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Simpatizante_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.FechaInscripcion).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);
            });
        }
    }
}
