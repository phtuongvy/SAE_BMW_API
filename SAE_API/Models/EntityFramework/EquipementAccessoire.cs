// EquipementAccessoire.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_equipementaccessoire_eqa")]
    public class EquipementAccessoire
    {
        [Key]
        [Column("eqa_idequipementmoto")]
        [ForeignKey("EquipementMotoOption")]
        public int IdEquipementMoto { get; set; }

        [InverseProperty(nameof(EquipementMotoOption.EquipementAccessoireEquipementMotoOption))]
        public virtual EquipementMotoOption? EquipementMotoOptionEquipementAccessoire { get; set; }

    }
}
