// GammeMoto.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_gammemoto_gmm")]
    public class GammeMoto
    {
        [Key]
        [Column("gmm_id")]
        public int IdGammeMoto { get; set; }

        [Column("gmm_nom")]
        [StringLength(50)]
        public string NomGammeMoto { get; set; }

        [InverseProperty(nameof(Moto.GammeMotoMoto))]
        public virtual ICollection<Moto> MotoGammeMoto { get; set; }

    }
}
