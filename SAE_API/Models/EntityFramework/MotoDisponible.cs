using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_motodisponible_mtd")]
    public class MotoDisponible
    {
        [Key]
        [Column("mtd_idmoto")]
        public int IdMoto { get; set; }

        [Column("mtd_prixmoto", TypeName = "numeric")]
        public decimal? PrixMoto { get; set; }

        [ForeignKey("IdMoto")]
        [InverseProperty(nameof(Moto.MotoDisponibleMoto))]
        public virtual Moto MotoMotoDisponible { get; set; }
    }
}
