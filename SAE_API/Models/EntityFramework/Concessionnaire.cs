// Concessionnaire.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_concessionnaire_csn")]
    public class Concessionnaire
    {
        [Key]
        [Column("csn_id")]
        public int IdConcessionnaire { get; set; }

        [Column("csn_idadresse")]
        [ForeignKey("Adresse")]
        public int IdAdresse { get; set; }

        [Column("csn_idstock")]
        [ForeignKey("Stock")]
        public int IdStock { get; set; }

        [Column("csn_nom")]
        [StringLength(50)]
        public string NomConcessionnaire { get; set; }


        // Navigation properties
        [InverseProperty(nameof(Adresse.ConcessionnairesAdresse))]
        public virtual Adresse AdresseConcessionnaire { get; set; }


        [InverseProperty(nameof(Stock.ConcessionnaireStock))]
        public virtual Stock StockConcessionnaire { get; set; }

        [InverseProperty(nameof(Favoris.ConcessionnaireFavoris))]
        public virtual ICollection<Favoris> FavorisConcessionnaire { get; set; }


        [InverseProperty(nameof(Provenance.ConcessionnaireProvenance))]
        public virtual ICollection<Provenance>  ProvenanceConcessionnaire { get; set; }


        [InverseProperty(nameof(PriseRendezvous.ConcessionnairePriseRendezvous))]
        public virtual ICollection<PriseRendezvous> PriseRendezvousConcessionnaire { get; set; }

        [InverseProperty(nameof(Detient.ConcessionnaireDetient))]
        public virtual ICollection<Detient> DetientConcessionnaire { get; set; }

    }
}
