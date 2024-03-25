using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_peutequiper_peq")]
    public class PeutEquiper
    {
        [Key, Column("peq_idpack", Order = 0)]
        public int IdPack { get; set; }

        [Key, Column("peq_idmoto", Order = 1)]
        public int IdMoto { get; set; }

        [InverseProperty(nameof(Pack.PeutEquiperPack))]
        public virtual Pack PackPeutEquiper { get; set; }

        [InverseProperty(nameof(Moto.PeutEquiperMoto))]
        public virtual Moto MotoPeutEquiper { get; set; }
    }
}