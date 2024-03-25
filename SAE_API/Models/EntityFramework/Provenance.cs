using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_provenance_prv")]
    public class Provenance
    {
        [Key, Column("prv_idcommande", Order = 0)]
        [ForeignKey("Commande")]
        public int IdCommande { get; set; }

        [Key, Column("prv_idconcessionnaire", Order = 1)]
        [ForeignKey("Concessionnaire")]
        public int IdConcessionnaire { get; set; }


        [InverseProperty(nameof(Commande.ProvenanceCommande))]
        public virtual Commande CommandeProvenance { get; set; }

        [InverseProperty(nameof(Concessionnaire.ProvenanceConcessionnaire))]
        public virtual Concessionnaire ConcessionnaireProvenance { get; set;}
    }
}