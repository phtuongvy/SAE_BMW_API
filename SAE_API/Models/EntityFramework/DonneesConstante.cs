using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_donneesconstante_dnc")]
    public class DonneesConstante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("dnc_iddonneesconstante")]
        public int IdDonnesConstante { get; set; }

        [Column("dnc_fraislivraisoncommande", TypeName = "numeric")]
        public decimal? FraisLivraisonCommande { get; set; }

        [Column("dnc_tvanormal", TypeName = "numeric")]
        public decimal? TVANormal { get; set; }

        [Column("dnc_tvaintermediaire", TypeName = "numeric")]
        public decimal? TVAIntermediaire { get; set; }

        [Column("dnc_tvareduit", TypeName = "numeric")]
        public decimal? TVAReduit { get; set; }

        [Column("dnc_tvaparticulier", TypeName = "numeric")]
        public decimal? TVAParticulier { get; set; }
    }
}
