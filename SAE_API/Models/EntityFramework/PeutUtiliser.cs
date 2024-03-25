using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_peututiliser_put")]
    public class PeutUtiliser
    {
        [Key, Column("put_idpack", Order = 0)]
        public int IdPack { get; set; }

        [Key, Column("put_idequipementmoto", Order = 1)]
        public int IdEquipementMoto { get; set; }

        [InverseProperty(nameof(Pack.PeutUtiliserPack))]
        public virtual Pack PackPeutUtiliser { get; set; }


        [InverseProperty(nameof(EquipementMotoOption.PeutUtiliserEquipementMotoOption))]
        public virtual EquipementMotoOption EquipementMotoOptionPeutUtiliser { get; set; }
    }
}
