// Commande.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_commande_cmd")]
    public partial class Commande
    {
        public Commande()
        {
            ProvenanceCommande = new HashSet<Provenance>();
            //EffectuerCommande = new HashSet<Effectuer>();
        }

        [Key]
        [Column("cmd_id")]
        public int IdCommande { get; set; }

        [Column("cmd_prixfraislivraison")]
        public decimal? PrixFraisLivraison { get; set; }

        [Column("cmd_date")]
        [DataType(DataType.Date)]
        public DateTime DateCommande { get; set; }

        [Column("cmd_prixtotal")]
        public decimal? PrixTotal { get; set; }

        [InverseProperty(nameof(Effectuer.CommandeEffectuer))]
        public virtual ICollection<Effectuer> EffectuerCommande { get; set; }
        [InverseProperty(nameof(Provenance.CommandeProvenance))]
        public virtual ICollection<Provenance> ProvenanceCommande { get; set; }

    }
}
