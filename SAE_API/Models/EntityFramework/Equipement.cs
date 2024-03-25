using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_equipement_eqp")]
    public class Equipement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("eqp_idequipement")]
        public int IdEquipement { get; set; }

        [Column("eqp_idsegment")]
        [Required]
        public int IdSegment { get; set; }

        [Column("eqp_idcollection")]
        [Required]
        public int IdCollection { get; set; }

        [Column("eqp_idtypeequipement")]
        [Required]
        public int IdTypeEquipement { get; set; }

        [Column("eqp_nomequipement")]
        [StringLength(100)]
        public string NomEquipement { get; set; }

        [Column("eqp_descriptionequipement")]
        [StringLength(500)]
        public string DescriptionEquipement { get; set; }

        [Column("eqp_detailequipement")]
        [StringLength(500)]
        public string DetailEquipement { get; set; }

        [Column("eqp_dureeequipement")]
        [StringLength(100)]
        public string DureeEquipement { get; set; }

        [Column("eqp_prixequipement", TypeName = "numeric")]
        public decimal? PrixEquipement { get; set; }

        [Column("eqp_sexe")]
        [Required]
        [StringLength(1)]
        public string Sexe { get; set; }


        [InverseProperty(nameof(Segement.EquipementSegement))]
        public virtual Segement SegementEquipement { get; set; }


        [InverseProperty(nameof(Collection.EquipementCollection))]
        public virtual Collection CollectionEquipement { get; set; }

        [InverseProperty(nameof(TypeEquipement.EquipementTypeEquipement))]
        public virtual TypeEquipement TypeEquipementEquipement { get; set; }


        [InverseProperty(nameof(Presente.EquipementPresente))]
        public virtual ICollection<Presente> PresenteEquipement { get; set; }


        [InverseProperty(nameof(APourTaille.EquipementAPourTaille))]
        public virtual ICollection<APourTaille> APourTailleEquipement { get; set; }

        [InverseProperty(nameof(APourCouleur.EquipementAPourCouleur))]
        public virtual ICollection<APourCouleur> APourCouleurEquipement { get; set; }

        [InverseProperty(nameof(Commander.EquipementCommander))]
        public virtual ICollection<Commander> CommanderEquipement { get; set; }

        [InverseProperty(nameof(Detient.EquipementDetient))]
        public virtual ICollection<Detient> DetientEquipement { get; set; }

        [InverseProperty(nameof(Disposer.EquipementDisposer))]
        public virtual ICollection<Disposer> DisposerEquipement { get; set; }
        // Constructor pour initialiser les collections
        public Equipement()
        {
            this.DetientEquipement = new HashSet<Detient>();
        }
    }
}
