using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_apourvaleur_apv")]
    public class APourValeur
    {
        [Key, Column("apv_idmoto", Order = 0)]
        [ForeignKey("Moto")]
        public int IdMoto { get; set; }

        [Key, Column("apv_idcaracteristiquemoto", Order = 1)]
        [ForeignKey("CaracteristiqueMoto")]
        public int IdCaracteristiqueMoto { get; set; }

        [InverseProperty(nameof(Moto.APourValeurMoto))]
        public virtual Moto? MotoAPourValeur { get; set; }

        [InverseProperty(nameof(CaracteristiqueMoto.APourValeurCaracteristiqueMoto))]
        public virtual CaracteristiqueMoto? CaracteristiqueMotoPourValeur { get; set; }
    }
}
