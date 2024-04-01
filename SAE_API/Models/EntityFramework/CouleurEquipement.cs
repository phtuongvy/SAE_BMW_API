// CouleurEquipement.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_couleurequipement_ceq")]
    public class CouleurEquipement
    {
        [Key]
        [Column("ceq_idcouleurequipement")]
        public int IdCouleurEquipement { get; set; }

        [Column("ceq_nomcouleurequipement")]
        [StringLength(50)]
        public string NomCouleurEquipement { get; set; }

        [InverseProperty(nameof(APourCouleur.CouleurEquipementAPourCouleur))]
        public virtual ICollection<APourCouleur>? APourCouleurCouleurEquipement { get; set; }

        [InverseProperty(nameof(Presente.CouleurEquipementPresente))]
        public virtual ICollection<Presente>? PresenteCouleurEquipement { get; set; }

    }
}
