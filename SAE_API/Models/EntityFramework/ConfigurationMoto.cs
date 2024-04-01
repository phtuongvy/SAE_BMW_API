// ConfigurationMoto.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_configurationmoto_cfm")]
    public partial class ConfigurationMoto
    {
        public ConfigurationMoto()
        {
            AChoisiConfigurationMoto = new HashSet<AChoisi>();
            AChoisiOptionsConfigurationMoto = new HashSet<AChoisiOption>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("cfm_idconfigurationmoto")]
        public int IdConfigurationMoto { get; set; }

        [Column("cfm_idreservationoffre")]
        [ForeignKey("IdReservationOffre")]
        public int? IdReservationOffre { get; set; }

        [Column("cfm_idmoto")]
        public int IdMoto { get; set; }

        [Column("cfm_idcoloris")]
        public int? IdColoris { get; set; }

        [Column("cfm_prixtotalconfiguration", TypeName = "numeric")]
        public decimal? PrixTotalConfiguration { get; set; }

        [Column("cfm_dateconfiguration", TypeName = "date")]
        public DateTime? DateConfiguration { get; set; }

        // Propriétés de navigation
        [ForeignKey("IdMoto")]
        [InverseProperty(nameof(Moto.ConfigurationMotoMoto))]
        public virtual Moto? MotoConfigurationMoto { get; set; }

        [ForeignKey("IdColoris")]
        [InverseProperty(nameof(Coloris.ConfigurationsMotoColoris))]
        public virtual Coloris? ColorisConfigurationMoto { get; set; }

        [InverseProperty(nameof(Enregistrer.ConfigurationMotoEnregistrer))]
        public virtual ICollection<Enregistrer>? EnregistrerConfigurationMoto { get; set; }

        [InverseProperty(nameof(AChoisi.ConfigurationMotoChoisi))]
        public virtual ICollection<AChoisi> AChoisiConfigurationMoto { get; set; }
        [InverseProperty(nameof(AChoisiOption.ConfigurationMotoChoisiOption))]
        public virtual ICollection<AChoisiOption> AChoisiOptionsConfigurationMoto { get; set; }

        [InverseProperty(nameof(Commander.ConfigurationMotoCommander))]
        public virtual Commander? CommanderConfigurationMoto { get; set; }

    }
}
