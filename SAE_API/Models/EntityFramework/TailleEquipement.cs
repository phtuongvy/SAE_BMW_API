using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_tailleequipement_tel")]
    public class TailleEquipement
    {
        [Key]
        [Column("tel_idtailleequipement")]
        public int IdTailleEquipement { get; set; }

        [Column("tel_nomtailleequipement")]
        [StringLength(50)]
        public string? NomTailleEquipement { get; set; }

        //[InverseProperty(nameof(APourTaille.TailleEquipementAPourTaille))]
        public virtual ICollection<APourTaille>? APourTailleTailleEquipement { get; set; }

    }
}
