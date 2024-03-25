using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_apourtaille_apt")]
    public class APourTaille
    {
        [Key, Column("apt_idequipement")]
        //[ForeignKey("Equipement")]
        public int IdEquipement { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("apt_idtailleequipement")]
        public int IdTailleEquipement { get; set; }


        [ForeignKey(nameof(IdEquipement))]
        [InverseProperty(nameof(Equipement.APourTailleEquipement))]
        public virtual Equipement EquipementAPourTaille { get; set; }

        [ForeignKey(nameof(IdTailleEquipement))]
        [InverseProperty(nameof(TailleEquipement.APourTailleTailleEquipement))]
        public virtual TailleEquipement TailleEquipementAPourTaille { get; set; }
    }
}
