using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP4_1_Models_EntityFramework;

namespace TP4_1.Models.EntityFramework
{
    [Table("t_j_notation_not")]
    [PrimaryKey("FilmId", "UtilisateurId")]
    public class Notation
    {
        [Key]
        [Column("utl_id")]
        public int UtilisateurId { get; set; }

        [Key]
        [Column("flm_id")]
        public int FilmId { get; set; }

        [Column("not_note")]
        [Required]
        public int Note { get; set; }

        [ForeignKey("UtilisateurId")]
        [InverseProperty("NotesUtilisateur")]
        public virtual Utilisateur UtilisateurNotant { get; set; } = null!;


        [ForeignKey("FilmId")]
        [InverseProperty("NotesFilm")]
        public virtual Film FilmNote { get; set; } = null!;





    }
}
