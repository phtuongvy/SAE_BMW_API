using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_commander_cmdr")]
    public class Commander
    {
        [Key]
        [Column("cmdr_idcommande")]
        public int IdCommande { get; set; }

        [Column("cmdr_idequipement")]
        public int? IdEquipement { get; set; }

        [Column("cmdr_idconfigurationmoto")]
        public int? IdConfigurationMoto { get; set; }

        [Column("cmdr_qtecommande")]
        public int? QteCommande { get; set; }

        // Propriétés de navigation
        [ForeignKey("IdEquipement")]
        [InverseProperty(nameof(Equipement.CommanderEquipement))]
        public virtual Equipement? EquipementCommander { get; set; }


        [ForeignKey("IdConfigurationMoto")]
        [InverseProperty(nameof(ConfigurationMoto.CommanderConfigurationMoto))]
        public virtual ConfigurationMoto? ConfigurationMotoCommander { get; set; }


    }
}
