﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TP4_1.Migrations
{
    /// <inheritdoc />
    public partial class CreationBDFilmRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_film_flm",
                columns: table => new
                {
                    flm_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    flm_titre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    flm_resume = table.Column<string>(type: "text", nullable: true),
                    flm_datesortie = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    flm_duree = table.Column<decimal>(type: "numeric", nullable: true),
                    flm_genre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_film", x => x.flm_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_utilisateur_utl",
                columns: table => new
                {
                    utl_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    utl_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    utl_prenom = table.Column<string>(type: "text", nullable: true),
                    utl_mobile = table.Column<string>(type: "char(10)", nullable: true),
                    utl_mail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    utl_pwd = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    utl_rue = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    utl_cp = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    utl_ville = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    utl_pays = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValue: "France"),
                    utl_latitude = table.Column<float>(type: "real", nullable: true),
                    utl_longitude = table.Column<float>(type: "real", nullable: true),
                    utl_datecreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "Current_date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_utilisateur", x => x.utl_id);
                    table.UniqueConstraint("AK_t_e_utilisateur_utl_utl_mail", x => x.utl_mail);
                });

            migrationBuilder.CreateTable(
                name: "t_j_notation_not",
                columns: table => new
                {
                    utl_id = table.Column<int>(type: "integer", nullable: false),
                    flm_id = table.Column<int>(type: "integer", nullable: false),
                    not_note = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notation", x => new { x.utl_id, x.flm_id });
                    table.CheckConstraint("Ck_notation_note", "not_note between 0 and  5");
                    table.ForeignKey(
                        name: "fk_note_FilmNote",
                        column: x => x.flm_id,
                        principalTable: "t_e_film_flm",
                        principalColumn: "flm_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_note_utilisateurNotant",
                        column: x => x.utl_id,
                        principalTable: "t_e_utilisateur_utl",
                        principalColumn: "utl_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_j_notation_not_flm_id",
                table: "t_j_notation_not",
                column: "flm_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_j_notation_not");

            migrationBuilder.DropTable(
                name: "t_e_film_flm");

            migrationBuilder.DropTable(
                name: "t_e_utilisateur_utl");
        }
    }
}
