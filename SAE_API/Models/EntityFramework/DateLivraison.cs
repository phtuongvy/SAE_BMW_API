using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_datelivraison_dlv")]
    public class DateLivraison
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("dlv_iddatelivraison")]
        public int IdDateLivraison { get; set; }

        [Column("dlv_date", TypeName = "date")]
        public DateTime? Date { get; set; }

        public virtual ICollection<RepriseMoto> RepriseMotoDateLivraison { get; set; }
    }
}
