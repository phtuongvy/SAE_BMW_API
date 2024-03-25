using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_disposer_dsp")]
    public class Disposer
    {
        [Key, Column("dsp_idstock", Order = 0)]
        [ForeignKey("Stock")]
        public int IdStock { get; set; }

        [Key, Column("dsp_idequipement", Order = 1)]
        [ForeignKey("Equipement")]
        public int IdEquipement { get; set; }

        [Column("dsp_quantitestock")]
        public int? QuantiteStock { get; set; }
        [InverseProperty(nameof(Stock.DisposerStock))]
        public virtual Stock StockDisposer { get; set; }
        [InverseProperty(nameof(Equipement.DisposerEquipement))]
        public virtual Equipement EquipementDisposer { get; set; }
    }
}
