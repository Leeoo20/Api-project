﻿using System;
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

        
        [ForeignKey("Utilisateur")]
        [InverseProperty("Notation")]
        public virtual Notation NotesUtilisateur { get; set; } = null!;


    }
}