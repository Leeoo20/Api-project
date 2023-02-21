using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TP41.Migrations
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
                    flmid = table.Column<int>(name: "flm_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    flmtitre = table.Column<string>(name: "flm_titre", type: "character varying(100)", maxLength: 100, nullable: false),
                    flmresume = table.Column<string>(name: "flm_resume", type: "text", nullable: true),
                    flmdatesortie = table.Column<DateTime>(name: "flm_datesortie", type: "timestamp with time zone", nullable: true),
                    flmduree = table.Column<decimal>(name: "flm_duree", type: "numeric", nullable: true),
                    flmgenre = table.Column<string>(name: "flm_genre", type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_film", x => x.flmid);
                });

            migrationBuilder.CreateTable(
                name: "t_e_utilisateur_utl",
                columns: table => new
                {
                    utlid = table.Column<int>(name: "utl_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    utlnom = table.Column<string>(name: "utl_nom", type: "character varying(100)", maxLength: 100, nullable: true),
                    utlprenom = table.Column<string>(name: "utl_prenom", type: "text", nullable: true),
                    utlmobile = table.Column<string>(name: "utl_mobile", type: "char(10)", nullable: true),
                    utlmail = table.Column<string>(name: "utl_mail", type: "character varying(100)", maxLength: 100, nullable: false),
                    utlpwd = table.Column<string>(name: "utl_pwd", type: "character varying(64)", maxLength: 64, nullable: false),
                    utlrue = table.Column<string>(name: "utl_rue", type: "character varying(200)", maxLength: 200, nullable: true),
                    utlcp = table.Column<string>(name: "utl_cp", type: "character varying(5)", maxLength: 5, nullable: true),
                    utlville = table.Column<string>(name: "utl_ville", type: "character varying(50)", maxLength: 50, nullable: true),
                    utlpays = table.Column<string>(name: "utl_pays", type: "character varying(50)", maxLength: 50, nullable: true),
                    utllatitude = table.Column<float>(name: "utl_latitude", type: "real", nullable: true),
                    utllongitude = table.Column<float>(name: "utl_longitude", type: "real", nullable: true),
                    utldatecreation = table.Column<DateTime>(name: "utl_datecreation", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_utilisateur", x => x.utlid);
                    table.UniqueConstraint("AK_t_e_utilisateur_utl_utl_mail", x => x.utlmail);
                });

            migrationBuilder.CreateTable(
                name: "t_j_notation_not",
                columns: table => new
                {
                    utlid = table.Column<int>(name: "utl_id", type: "integer", nullable: false),
                    flmid = table.Column<int>(name: "flm_id", type: "integer", nullable: false),
                    notnote = table.Column<int>(name: "not_note", type: "integer", nullable: false),
                    utlid1 = table.Column<int>(name: "utl_id1", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notation", x => new { x.utlid, x.flmid });
                    table.ForeignKey(
                        name: "fk_note_FilmNote",
                        column: x => x.flmid,
                        principalTable: "t_e_film_flm",
                        principalColumn: "flm_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_note_utilisateurNotant",
                        column: x => x.utlid1,
                        principalTable: "t_e_utilisateur_utl",
                        principalColumn: "utl_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_j_notation_not_flm_id",
                table: "t_j_notation_not",
                column: "flm_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_notation_not_utl_id1",
                table: "t_j_notation_not",
                column: "utl_id1");
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
