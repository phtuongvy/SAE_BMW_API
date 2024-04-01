// Adresse.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_adresse_ads")]
    public class Adresse
    {
        [Key]
        [Column("ads_id")]
        public int IdAdresse { get; set; }

        [Column("ads_idcompteclient")]
        public int? IdCompteClient { get; set; }

        [Column("ads_idconcessionnaire")]
        public int? IdConcessionnaire { get; set; }

        [Column("ads_numero")]
        public int? Numero { get; set; }

        [Column("ads_rueclient")]
        [StringLength(50)]
        public string? RueClient { get; set; }

        [Column("ads_codepostal")]
        [StringLength(20)]
        public string CodePostal { get; set; }

        [Column("ads_ville")]
        [StringLength(100)]
        public string Ville { get; set; }

        [Column("ads_pays")]
        [StringLength(50)]
        public string Pays { get; set; }

        [Column("ads_typeadresse")]
        [StringLength(50)]
        public string TypeAdresse { get; set; }

        [ForeignKey(nameof(IdConcessionnaire))]
        [InverseProperty(nameof(Concessionnaire.AdresseConcessionnaire))]
        public virtual ICollection<Concessionnaire>? ConcessionnairesAdresse { get; set; }

        [ForeignKey(nameof(IdCompteClient))]
        [InverseProperty(nameof(CompteClient.AdresseCompteClient))]
        public virtual CompteClient? ClientAdresse { get; set; }



    }
}
