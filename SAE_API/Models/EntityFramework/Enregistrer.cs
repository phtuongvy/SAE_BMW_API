using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_enregistrer_enr")]
    public class Enregistrer
    {
        [Key, Column("enr_idconfigurationmoto", Order = 0)]
        [ForeignKey("ConfigurationMoto")]
        public int IdConfigurationMoto { get; set; }

        [Key, Column("enr_idcompteclient", Order = 1)]
        [ForeignKey("CompteClient")]
        public int IdCompteClient { get; set; }

        [Column("enr_nomconfiguration")]
        [StringLength(50)]
        public string NomConfiguration { get; set; }
        [InverseProperty(nameof(ConfigurationMoto.EnregistrerConfigurationMoto))]
        public virtual ConfigurationMoto ConfigurationMotoEnregistrer { get; set; }

        [InverseProperty(nameof(CompteClient.EnregistrerCompteClient))]
        public virtual CompteClient CompteClientEnregistrer { get; set; }
        

    }
}
