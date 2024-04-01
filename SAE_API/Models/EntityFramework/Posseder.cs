using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_posseder_pos")]
    public class Posseder
    {
        [Key, Column("pos_idmoto", Order = 0)]
        public int IdMoto { get; set; }

        [Key, Column("pos_idequipementmoto", Order = 1)]
        public int IdEquipementMoto { get; set; }

        // Assuming you have or will have the below classes defined
        // and they are related to these keys.
        [InverseProperty(nameof(Moto.PossederMoto))]
        public Moto? MotoPosseder { get; set; }


        [InverseProperty(nameof(EquipementMotoOption.PossederEquipementMotoOption))]
        public EquipementMotoOption? EquipementMotoOptionPosseder { get; set; }
    }
}
