using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_estimer_est")]
    public class Estimer
    {
        [Key, Column("est_idcompteclient", Order = 0)]
        [ForeignKey("CompteClient")]
        public int IdCompteClient { get; set; }

        [Key, Column("est_idestimationmoto", Order = 1)]
        [ForeignKey("EstimationMoto")]
        public int IdEstimationMoto { get; set; }

        [Key, Column("est_idmoyendepaiement", Order = 2)]
        [ForeignKey("MoyenDePaiement")]
        public int IdMoyenDePaiement { get; set; }

        [InverseProperty(nameof(CompteClient.EstimerCompteClient))]
        public virtual CompteClient CompteClientEstimer { get; set; }

        [InverseProperty(nameof(RepriseMoto.EstimerRepriseMoto))]
        public virtual RepriseMoto RepriseMotoEstimer { get; set; }

        [InverseProperty(nameof(MoyenDePaiement.EstimationMoyenDePaiement))]
        public virtual MoyenDePaiement MoyenDePaiementEstimer { get; set; }
    }
}
