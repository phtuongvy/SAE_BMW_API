// EquipementMotoOption.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_equipementmotooption_eqm")]
    public class EquipementMotoOption
    {
        [Key]
        [Column("eqm_id")]
        public int IdEquipementMoto { get; set; }

        [Column("eqm_idphoto")]
        [ForeignKey("Photo")]
        public int? IdPhoto { get; set; }

        [Column("eqm_nomequipement")]
        [StringLength(50)]
        public string NomEquipement { get; set; }

        [Column("eqm_descriptionequipementmoto")]
        [StringLength(500)]
        public string DescriptionEquipementMoto { get; set; }

        [Column("eqm_prixequipementmoto")]
        public decimal? PrixEquipementMoto { get; set; }

        [Column("eqm_equipementorigine")]
        public bool? EquipementOrigine { get; set; }

        // Navigation properties
        [InverseProperty(nameof(Photo.EquipementMotoOptionPhoto))]
        public virtual Photo PhotoEquipementMotoOption { get; set; }

        [InverseProperty(nameof(AChoisiOption.EquipementMotoChoisiOption))]
        public virtual ICollection<AChoisiOption>? AChoisiOptionsEquipementMoto { get; set; }

        [InverseProperty(nameof(Posseder.EquipementMotoOptionPosseder))]
        public virtual ICollection<Posseder>? PossederEquipementMotoOption { get; set; }

        [InverseProperty(nameof(PeutUtiliser.EquipementMotoOptionPeutUtiliser))]
        public virtual ICollection<PeutUtiliser>?  PeutUtiliserEquipementMotoOption { get; set; }

        [InverseProperty(nameof(EquipementOption.EquipementMotoOptionEquipementOption))]
        public virtual EquipementOption? EquipementOptionEquipementMotoOption { get; set; }

        [InverseProperty(nameof(EquipementAccessoire.EquipementMotoOptionEquipementAccessoire))]
        public virtual EquipementAccessoire? EquipementAccessoireEquipementMotoOption { get; set; }


    }
}
