using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_detient_det")]
    public class Detient
    {
        [Key, Column("det_idequipement", Order = 0)]
        [ForeignKey(nameof(Equipement.IdEquipement))]
        public int IdEquipement { get; set; }

        [Key, Column("det_idconcessionnaire", Order = 1)]
        [ForeignKey("Concessionnaire")]
        public int IdConcessionnaire { get; set; }

        [InverseProperty(nameof(Equipement.DetientEquipement))]
        public virtual Equipement EquipementDetient { get; set; }

        [InverseProperty(nameof(Concessionnaire.DetientConcessionnaire))]
        public virtual Concessionnaire ConcessionnaireDetient { get; set; }
    }
}
