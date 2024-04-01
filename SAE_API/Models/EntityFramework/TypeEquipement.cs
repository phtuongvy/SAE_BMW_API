using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_typeequipement_typ")]
    public class TypeEquipement
    {
        [Key]
        [Column("typ_idtypeequipement")]
        public int IdTypeEquipement { get; set; }

        [Column("typ_idsurtypeequipement")]
        public int? IdSurTypeEquipement { get; set; }

        [Column("typ_nomtypeequipement")]
        [StringLength(50)]
        public string? NomTypeEquipement { get; set; }


        [InverseProperty(nameof(Equipement.TypeEquipementEquipement))]
        public virtual ICollection<Equipement>? EquipementTypeEquipement { get; set; }

        [InverseProperty(nameof(TypeEquipement.TypeEquipements))]
        public virtual ICollection<TypeEquipement>? TypeEquipementTypeEquipement { get; set; }

        [InverseProperty(nameof(TypeEquipement.TypeEquipementTypeEquipement))]
        public virtual TypeEquipement? TypeEquipements { get; set; }


    }
}