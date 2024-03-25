// ACHOISI.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_j_achoisi_ach")]
    public class AChoisi
    {
        [Key, Column("ach_idpack", Order = 0)]
        [ForeignKey("Pack")]
        public int IDPack { get; set; }

        [Key, Column("ach_idconfigurationmoto", Order = 1)]
        [ForeignKey("ConfigurationMoto")]
        public int IDConfigurationMoto { get; set; }

        // Navigation properties
        [InverseProperty(nameof(Pack.ChoisiPack))]
        public virtual Pack PackChoisi { get; set; }
        [InverseProperty(nameof(ConfigurationMoto.AChoisiConfigurationMoto))]
        public virtual ConfigurationMoto ConfigurationMotoChoisi { get; set; }
    }
}
