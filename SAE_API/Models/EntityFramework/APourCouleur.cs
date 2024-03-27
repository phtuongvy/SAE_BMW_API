// APourCouleur.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_j_apourcouleur_apc")]
    public class APourCouleur
    {
        [Key, Column("apc_idequipement", Order = 0)]
        [ForeignKey("Equipement")]
        public int IdEquipement { get; set; }

        [Key, Column("apc_idcouleurequipement", Order = 1)]
        [ForeignKey("CouleurEquipement")]
        public int IdCouleurEquipement { get; set; }

        // Propriétés de navigation
        [InverseProperty(nameof(Equipement.APourCouleurEquipement))]
        public virtual Equipement EquipementAPourCouleur { get; set; }

        [InverseProperty(nameof(CouleurEquipement.APourCouleurCouleurEquipement))]
        public virtual CouleurEquipement CouleurEquipementAPourCouleur { get; set; }
        
    }
}
