// EquipementOption.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_equipementoption_eqo")]
    public class EquipementOption
    {
        [Key]
        [Column("eqo_idequipementmoto")]
        [ForeignKey("EquipementMotoOption")]
        public int IdEquipementMoto { get; set; }

        [InverseProperty(nameof(EquipementMotoOption.EquipementOptionEquipementMotoOption))]
        public virtual EquipementMotoOption EquipementMotoOptionEquipementOption { get; set; }
    }
}
