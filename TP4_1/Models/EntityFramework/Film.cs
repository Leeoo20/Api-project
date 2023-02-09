using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TP4_1.Models.EntityFramework;
using TP4_1_Models_EntityFramework;

namespace TP4_1.Models.EntityFramework
{

    [Table("t_e_film_flm")]
    public partial class Film
    {
        [Key]
        [Column("flm_id")]
        public int FilmID { get; set; }

        [Column("flm_titre")]
        [StringLength(100)]
        [Required]
        public string Titre { get; set; } = null!;

        [Column("flm_resume")]
        public string Resume { get; set; } = null!;

        [Column("flm_datesortie")]
        public DateTime DateSortie { get; set; }

        [Column("flm_resume")]
        public decimal Duree { get; set; }


        [Column("flm_genre")]
        [StringLength(30)]
        public string Genre { get; set; } = null!;


        [ForeignKey("Utilisateur")]
        [InverseProperty("Notation")]
        public virtual Notation NotesFilm { get; set; } = null!;




    }
}