﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TP4_1.Models.EntityFramework;

#nullable disable

namespace TP4_1.Migrations
{
    [DbContext(typeof(FilmRatingDBContext))]
    [Migration("20230221093508_NewAnnotations1")]
    partial class NewAnnotations1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TP4_1.Models.EntityFramework.Film", b =>
                {
                    b.Property<int>("FilmID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("flm_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FilmID"));

                    b.Property<DateTime?>("DateSortie")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("flm_datesortie");

                    b.Property<decimal?>("Duree")
                        .HasColumnType("numeric")
                        .HasColumnName("flm_duree");

                    b.Property<string>("Genre")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("flm_genre");

                    b.Property<string>("Resume")
                        .HasColumnType("text")
                        .HasColumnName("flm_resume");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("flm_titre");

                    b.HasKey("FilmID")
                        .HasName("pk_film");

                    b.ToTable("t_e_film_flm");
                });

            modelBuilder.Entity("TP4_1.Models.EntityFramework.Notation", b =>
                {
                    b.Property<int>("UtilisateurId")
                        .HasColumnType("integer")
                        .HasColumnName("utl_id");

                    b.Property<int>("FilmId")
                        .HasColumnType("integer")
                        .HasColumnName("flm_id");

                    b.Property<int>("Note")
                        .HasColumnType("integer")
                        .HasColumnName("not_note");

                    b.HasKey("UtilisateurId", "FilmId")
                        .HasName("pk_notation");

                    b.HasIndex("FilmId");

                    b.ToTable("t_j_notation_not", t =>
                        {
                            t.HasCheckConstraint("Ck_notation_note", "not_note between 0 and  5");
                        });
                });

            modelBuilder.Entity("TP4_1_Models_EntityFramework.Utilisateur", b =>
                {
                    b.Property<int>("UtilisateurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("utl_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UtilisateurId"));

                    b.Property<string>("CodePostal")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("utl_cp");

                    b.Property<DateTime>("DateCreation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("utl_datecreation")
                        .HasDefaultValueSql("Current_date");

                    b.Property<float?>("Laititude")
                        .HasColumnType("real")
                        .HasColumnName("utl_latitude");

                    b.Property<float?>("Longitude")
                        .HasColumnType("real")
                        .HasColumnName("utl_longitude");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("utl_mail");

                    b.Property<string>("Mobile")
                        .HasColumnType("char(10)")
                        .HasColumnName("utl_mobile");

                    b.Property<string>("Nom")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("utl_nom");

                    b.Property<string>("Pays")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("France")
                        .HasColumnName("utl_pays");

                    b.Property<string>("Prenom")
                        .HasColumnType("text")
                        .HasColumnName("utl_prenom");

                    b.Property<string>("Pwd")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("utl_pwd");

                    b.Property<string>("Rue")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("utl_rue");

                    b.Property<string>("Ville")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("utl_ville");

                    b.HasKey("UtilisateurId")
                        .HasName("pk_utilisateur");

                    b.HasAlternateKey("Mail");

                    b.ToTable("t_e_utilisateur_utl");
                });

            modelBuilder.Entity("TP4_1.Models.EntityFramework.Notation", b =>
                {
                    b.HasOne("TP4_1.Models.EntityFramework.Film", "FilmNote")
                        .WithMany("NotesFilm")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("fk_note_FilmNote");

                    b.HasOne("TP4_1_Models_EntityFramework.Utilisateur", "UtilisateurNotant")
                        .WithMany("NotesUtilisateur")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("fk_note_utilisateurNotant");

                    b.Navigation("FilmNote");

                    b.Navigation("UtilisateurNotant");
                });

            modelBuilder.Entity("TP4_1.Models.EntityFramework.Film", b =>
                {
                    b.Navigation("NotesFilm");
                });

            modelBuilder.Entity("TP4_1_Models_EntityFramework.Utilisateur", b =>
                {
                    b.Navigation("NotesUtilisateur");
                });
#pragma warning restore 612, 618
        }
    }
}
