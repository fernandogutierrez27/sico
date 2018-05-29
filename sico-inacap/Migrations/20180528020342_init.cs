using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace sicoInacap.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bloque",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false),
                    HoraInicio = table.Column<TimeSpan>(nullable: false),
                    HoraTermino = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bloque", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "EstadoEvento",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoEvento", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Recinto",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false),
                    Latitud = table.Column<string>(unicode: false, nullable: false),
                    Longitud = table.Column<string>(unicode: false, nullable: false),
                    Nombre = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recinto", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Username = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "datetime", nullable: false),
                    Password = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    Username = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Administrador_Usuario",
                        column: x => x.Username,
                        principalTable: "Usuario",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Miembro",
                columns: table => new
                {
                    Username = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CodigoCargo = table.Column<int>(nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime", nullable: false),
                    Fono = table.Column<string>(unicode: false, maxLength: 9, nullable: false),
                    RUN = table.Column<string>(unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miembro", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Miembro_Cargo",
                        column: x => x.CodigoCargo,
                        principalTable: "Cargo",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Miembro_Usuario",
                        column: x => x.Username,
                        principalTable: "Usuario",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Simpatizante",
                columns: table => new
                {
                    Username = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(unicode: false, nullable: false),
                    Email = table.Column<string>(unicode: false, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime", nullable: false),
                    Genero = table.Column<int>(nullable: false),
                    Nombres = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simpatizante", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Simpatizante_Usuario",
                        column: x => x.Username,
                        principalTable: "Usuario",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false),
                    CodigoCategoria = table.Column<int>(nullable: false),
                    CodigoEstado = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(unicode: false, nullable: false),
                    Icono = table.Column<string>(unicode: false, nullable: false),
                    Nombre = table.Column<string>(unicode: false, nullable: false),
                    UsernameOrganizador = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    UsernameResponsable = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Categoria",
                        column: x => x.CodigoCategoria,
                        principalTable: "Categoria",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_EstadoEvento",
                        column: x => x.CodigoEstado,
                        principalTable: "EstadoEvento",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Administrador",
                        column: x => x.UsernameOrganizador,
                        principalTable: "Administrador",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Miembro",
                        column: x => x.UsernameResponsable,
                        principalTable: "Miembro",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false),
                    CodigoEvento = table.Column<int>(nullable: false),
                    CodigoRecinto = table.Column<int>(nullable: false),
                    HoraInicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    HoraTermino = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Agenda_Evento",
                        column: x => x.CodigoEvento,
                        principalTable: "Evento",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agenda_Recinto",
                        column: x => x.CodigoRecinto,
                        principalTable: "Recinto",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asistencia",
                columns: table => new
                {
                    CodigoEvento = table.Column<int>(nullable: false),
                    UsernameAsistente = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencia", x => new { x.CodigoEvento, x.UsernameAsistente });
                    table.ForeignKey(
                        name: "FK_Asistencia_Evento",
                        column: x => x.CodigoEvento,
                        principalTable: "Evento",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistencia_Usuario",
                        column: x => x.UsernameAsistente,
                        principalTable: "Usuario",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interes",
                columns: table => new
                {
                    CodigoEvento = table.Column<int>(nullable: false),
                    UsernameInteresado = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interes", x => new { x.CodigoEvento, x.UsernameInteresado });
                    table.ForeignKey(
                        name: "FK_Interes_Evento",
                        column: x => x.CodigoEvento,
                        principalTable: "Evento",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interes_Usuario",
                        column: x => x.UsernameInteresado,
                        principalTable: "Usuario",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgendaBloque",
                columns: table => new
                {
                    CodigoAgenda = table.Column<int>(nullable: false),
                    CodigoBloque = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaBloque", x => new { x.CodigoAgenda, x.CodigoBloque });
                    table.ForeignKey(
                        name: "FK_AgendaBloque_Agenda",
                        column: x => x.CodigoAgenda,
                        principalTable: "Agenda",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgendaBloque_Bloque",
                        column: x => x.CodigoBloque,
                        principalTable: "Bloque",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_CodigoEvento",
                table: "Agenda",
                column: "CodigoEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_CodigoRecinto",
                table: "Agenda",
                column: "CodigoRecinto");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaBloque_CodigoBloque",
                table: "AgendaBloque",
                column: "CodigoBloque");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_UsernameAsistente",
                table: "Asistencia",
                column: "UsernameAsistente");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_CodigoCategoria",
                table: "Evento",
                column: "CodigoCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_CodigoEstado",
                table: "Evento",
                column: "CodigoEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_UsernameOrganizador",
                table: "Evento",
                column: "UsernameOrganizador");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_UsernameResponsable",
                table: "Evento",
                column: "UsernameResponsable");

            migrationBuilder.CreateIndex(
                name: "IX_Interes_UsernameInteresado",
                table: "Interes",
                column: "UsernameInteresado");

            migrationBuilder.CreateIndex(
                name: "IX_Miembro_CodigoCargo",
                table: "Miembro",
                column: "CodigoCargo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaBloque");

            migrationBuilder.DropTable(
                name: "Asistencia");

            migrationBuilder.DropTable(
                name: "Interes");

            migrationBuilder.DropTable(
                name: "Simpatizante");

            migrationBuilder.DropTable(
                name: "Agenda");

            migrationBuilder.DropTable(
                name: "Bloque");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Recinto");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "EstadoEvento");

            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "Miembro");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
