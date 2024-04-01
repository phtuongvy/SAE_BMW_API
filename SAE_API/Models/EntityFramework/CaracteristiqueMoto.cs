using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_caracteristiquemoto_crm")]
    public class CaracteristiqueMoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("crm_idcaracteristiquemoto")]
        public int IdCaracteristiqueMoto { get; set; }

        [Column("crm_idcategoriecaracteristiquemoto")]
        [Required]
        public int IdCategorieCaracteristiqueMoto { get; set; }

        [Column("crm_nomcaracteristiquemoto")]
        [StringLength(100)]
        public string NomCaracteristiqueMoto { get; set; }

        [Column("crm_valeurcaracteristiquemoto")]
        [StringLength(100)]
        public string ValeurCaracteristiqueMoto { get; set; }

        // Propriété de navigation pour la catégorie de caractéristique
        // Assurez-vous d'avoir un modèle CategorieCaracteristiqueMoto correspondant
        [ForeignKey(nameof(CategorieCaracteristiqueMoto.IdCategorieCaracteristiqueMoto))]
        [InverseProperty(nameof(CategorieCaracteristiqueMoto.CaracteristiquesMotoCategorieCaracteristiqueMoto))]
        public virtual CategorieCaracteristiqueMoto? CategorieCaracteristiqueMotoCaracteristiqueMoto { get; set; }

        // Collection pour gérer la relation avec APourValeur
        [InverseProperty(nameof(APourValeur.CaracteristiqueMotoPourValeur))]
        public virtual ICollection<APourValeur>? APourValeurCaracteristiqueMoto { get; set; }
    }
}
