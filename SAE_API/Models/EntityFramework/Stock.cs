using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_stock_stk")]
    public class Stock
    {
        [Key]
        [Column("stk_idstock")]
        public int IdStock { get; set; }
        [InverseProperty(nameof(Concessionnaire.StockConcessionnaire))]
        public virtual ICollection<Concessionnaire> ConcessionnaireStock { get; set; }

        [InverseProperty(nameof(Disposer.StockDisposer))]
        public virtual ICollection<Disposer> DisposerStock { get; set; }

        [InverseProperty(nameof(EstDans.StockEstDans))]
        public virtual ICollection<EstDans> EstDansStock { get; set; }
    }
}
