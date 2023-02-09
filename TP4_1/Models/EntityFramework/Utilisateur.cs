using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using TP4_1.Models.EntityFramework;

namespace TP4_1_Models_EntityFramework
{

    [Table("t_e_utilisateur_utl")]
    public class Utilisateur
    {
        [Key]
        [Column("utl_id")]
        public int UtilisateurId { get; set; }

        [Column("utl_nom")]
        [StringLength(100)]
        public string? Nom { get; set; } = null!;

        [Column("utl_prenom")]
        public string? Prenom { get; set; } = null!;

        [Column("utl_mobile", TypeName = "char(10)")]
        public string? Mobile { get; set; }


        [Column("utl_mail")]
        [StringLength(100)]
        
        [Required]
        public string? Mail { get; set; } = null!;

        [Column("utl_pwd")]
        [StringLength(64)]
        [Required]
        public string? Pwd { get; set; } = null!;


        [Column("utl_rue")]
        [StringLength(200)]
        public string? Rue { get; set; }

        [Column("utl_cp")]
        [StringLength(5)]
        [MaxLength(5)]
        public string? CodePostal { get; set; }

        [Column("utl_ville")]
        [StringLength(50)]
        public string? Ville { get; set; }

        [Column("utl_pays")]
        [StringLength(50)]
        public string? Pays { get; set; } = "France";

        [Column("utl_latitude")]
        public float? Laititude { get; set; }


        [Column("utl_longitude")]
        
        public float? Longitude { get; set; }

        [Column("utl_datecreation")]
        [Required]
        public DateTime DateCreation { get; set; } = DateTime.Now;





        [ForeignKey("Utilisateur")]
        [InverseProperty("Notation")]
        public virtual ICollection<Notation> NotesUtilisateur { get; set; } = null!;


    }
}