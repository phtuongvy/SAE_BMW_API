// Illustrer.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_j_illustrer_ilr")]
    public class Illustrer
    {
        [Key, Column("ilr_idmoto", Order = 0)]
        [ForeignKey("Moto")]
        public int IdMoto { get; set; }

        [Key, Column("ilr_idphoto", Order = 1)]
        [ForeignKey("Photo")]
        public int IdPhoto { get; set; }

        // Navigation properties
        [InverseProperty(nameof(Moto.IllustrerMoto))]
        public virtual Moto MotoIllustrer { get; set; }

        [InverseProperty(nameof(Photo.IllustrerPhoto))]
        public virtual Photo PhotoIllustrer { get; set; }
    }
}
