using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_categoriecaracteristiquemoto_ccm")]
    public class CategorieCaracteristiqueMoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ccm_idcategoriecaracteristiquemoto")]
        public int IdCategorieCaracteristiqueMoto { get; set; }

        [Column("ccm_nomcategoriecaracteristiquemoto")]
        [StringLength(50)]
        public string NomCategorieCaracteristiqueMoto { get; set; }

        // Relation avec CaracteristiqueMoto
        public virtual ICollection<CaracteristiqueMoto> CaracteristiquesMotoCategorieCaracteristiqueMoto { get; set; }


    }
}
