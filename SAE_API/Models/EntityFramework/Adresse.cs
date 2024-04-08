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
        [RegularExpression(@"^[a-zA-Z0-9\s\-,'.]{1,50}$", ErrorMessage = "La rue client n'est pas valide.")]
        public string? RueClient { get; set; }

        [Column("ads_codepostal")]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-]{1,20}$", ErrorMessage = "Le code postal n'est pas valide.")]
        public string CodePostal { get; set; }

        [Column("ads_ville")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-,'.]{1,100}$", ErrorMessage = "La ville n'est pas valide.")]
        public string Ville { get; set; }

        [Column("ads_pays")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-,'.]{1,50}$", ErrorMessage = "Le pays n'est pas valide.")]
        public string Pays { get; set; }

        [Column("ads_typeadresse")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-,'.]{1,50}$", ErrorMessage = "Le type d'adresse n'est pas valide.")]
        public string TypeAdresse { get; set; }

        [ForeignKey(nameof(IdConcessionnaire))]
        [InverseProperty(nameof(Concessionnaire.AdresseConcessionnaire))]
        public virtual ICollection<Concessionnaire>? ConcessionnairesAdresse { get; set; }

        [ForeignKey(nameof(IdCompteClient))]
        [InverseProperty(nameof(CompteClient.AdresseCompteClient))]
        public virtual CompteClient? ClientAdresse { get; set; }



    }
}
