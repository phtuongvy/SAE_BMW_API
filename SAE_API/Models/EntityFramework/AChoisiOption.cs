using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_achoisioption_aco")]
    public class AChoisiOption
    {
        [Key, Column("aco_idconfigurationmoto", Order = 0)]
        [ForeignKey("ConfigurationMoto")]
        public int IdConfigurationMoto { get; set; }

        [Key, Column("aco_idequipementmoto", Order = 1)]
        [ForeignKey("EquipementMoto")]
        public int IdEquipementMoto { get; set; }

        // Navigation properties
        [InverseProperty(nameof(ConfigurationMoto.AChoisiOptionsConfigurationMoto))]
        public virtual ConfigurationMoto ConfigurationMotoChoisiOption { get; set; }
        [InverseProperty(nameof(EquipementMotoOption.AChoisiOptionsEquipementMoto))]
        public virtual EquipementMotoOption EquipementMotoChoisiOption { get; set; }

        

    }
}
