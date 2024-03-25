// Pack.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_pack_pck")]
    public class Pack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("pck_idpack")]
        public int PackId { get; set; }

        [Column("pck_idcoloris")]
        public int? ColorisId { get; set; }

        [StringLength(30)]
        [Column("pck_nompack")]
        public string NomPack { get; set; }

        [StringLength(500)]
        [Column("pck_descriptionpack")]
        public string DescriptionPack { get; set; }

        [Column("pck_prixpack", TypeName = "numeric")]
        public decimal? PrixPack { get; set; }

        [InverseProperty(nameof(AChoisi.PackChoisi))]
        public virtual ICollection<AChoisi> ChoisiPack { get; set; }

        [InverseProperty(nameof(PeutEquiper.PackPeutEquiper))]
        public virtual ICollection<PeutEquiper> PeutEquiperPack { get; set; }

        [InverseProperty(nameof(PeutUtiliser.PackPeutUtiliser))]
        public virtual ICollection<PeutUtiliser> PeutUtiliserPack { get; set; }
    }
}
