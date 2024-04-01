using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_reprisemoto_rpm")]
    public class RepriseMoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("rpm_idestimationmoto")]
        public int IdEstimationMoto { get; set; }

        [Column("rpm_iddatelivraison")]
        [ForeignKey("DateLivraison")]
        public int IdDateLivraison { get; set; }

        [Column("rpm_idcompteclient")]
        [ForeignKey("CompteClient")]
        public int IdCompteClient { get; set; }

        [Column("rpm_marqueestimationmoto")]
        [StringLength(10)]
        public string MarqueEstimationMoto { get; set; }

        [Column("rpm_modeleestimationmoto")]
        [StringLength(10)]
        public string ModeleEstimationMoto { get; set; }

        [Column("rpm_moisimmatriculation")]
        public int? MoisImmatriculation { get; set; }

        [Column("rpm_anneimmatriculation")]
        public int? AnneImmatriculation { get; set; }

        [Column("rpm_prixestimationmoto", TypeName = "numeric")]
        public decimal? PrixEstimationMoto { get; set; }

        [Column("rpm_kilometrageestimationmoto", TypeName = "numeric")]
        public decimal? KilometrageEstimationMoto { get; set; }

        [Column("rpm_versionestimationmoto")]
        [StringLength(5)]
        public string VersionEstimationMoto { get; set; }


        [InverseProperty(nameof(DateLivraison.RepriseMotoDateLivraison))]
        public virtual DateLivraison? DateLivraisonRepriseMoto { get; set; }

        [InverseProperty(nameof(CompteClient.RepriseMotoCompteClient))]
        public virtual CompteClient? CompteClientRepriseMoto { get; set; }

        [InverseProperty(nameof(Estimer.RepriseMotoEstimer))]
        public virtual Estimer? EstimerRepriseMoto { get; set; }

    }
}
