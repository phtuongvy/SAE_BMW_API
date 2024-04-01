using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_effectuer_eff")]
    public class Effectuer
    {
        [Key, Column("eff_idcommande", Order = 1)]
        [ForeignKey("Commande")]
        public int IdCommande { get; set; }

        [Key, Column("eff_idcompteclient", Order = 0)]
        [ForeignKey("CompteClient")]
        public int IdCompteClient { get; set; }

        [InverseProperty(nameof(Commande.EffectuerCommande))]
        public virtual Commande? CommandeEffectuer { get; set; }

        [InverseProperty(nameof(CompteClient.EffectuerCompteClient))]
        public virtual CompteClient? CompteClientEffectuer { get; set; }

        
    }
}
