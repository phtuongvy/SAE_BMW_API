using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_presente_pre")]
    public class Presente
    {
        [Key, Column("pre_idphoto", Order = 0)]
        public int IdPhoto { get; set; }

        [Key, Column("pre_idequipement", Order = 1)]
        public int IdEquipement { get; set; }

        [Key, Column("pre_idcouleurequipement", Order = 2)]
        public int IdCouleurEquipement { get; set; }

        // Assuming you have or will have the below classes defined
        // and they are related to these keys.
        [InverseProperty(nameof(Photo.PresentePhoto))]
        public Photo PhotoPresente { get; set; }

        [InverseProperty(nameof(Equipement.PresenteEquipement))]
        public Equipement EquipementPresente { get; set; }

        [InverseProperty(nameof(CouleurEquipement.PresenteCouleurEquipement))]
        public CouleurEquipement CouleurEquipementPresente { get; set; }
    }
}


