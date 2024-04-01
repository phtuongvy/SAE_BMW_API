// EstDans.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_j_estdans_esd")]
    public class EstDans
    {
        [Key, Column("esd_idmoto", Order = 0)]
        [ForeignKey("Moto")]
        public int IdMoto { get; set; }

        [Key, Column("esd_idstock", Order = 1)]
        [ForeignKey("Stock")]
        public int IdStock { get; set; }

        [Column("esd_quantitestockdisponible")]
        public int? QuantiteStockDisponible { get; set; }

        [Column("esd_quantitestockmoto")]
        public int? QuantiteStockMoto { get; set; }

        // Navigation properties
        [InverseProperty(nameof(Moto.EstDansMoto))]
        public virtual Moto? MotoEstDans { get; set; }

        [InverseProperty(nameof(Stock.EstDansStock))]
        public virtual Stock? StockEstDans { get; set; }
    }
}
