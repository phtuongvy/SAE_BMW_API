// Coloris.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_coloris_col")]
    public class Coloris
    {
        [Key]
        [Column("col_id")]
        public int IdColoris { get; set; }

        [Column("col_idphoto")]
        [ForeignKey("Photo")]
        public int IdPhoto { get; set; }

        [Column("col_nomcoloris")]
        [StringLength(50)]
        public string NomColoris { get; set; }

        [Column("col_descriptioncoloris")]
        [StringLength(500)]
        public string DescriptionColoris { get; set; }

        [Column("col_prixcoloris")]
        public decimal? PrixColoris { get; set; }

        [Column("col_typecoloris")]
        public int TypeColoris { get; set; }

        // Navigation properties
        [InverseProperty(nameof(Photo.ColorisPhoto))]
        public virtual Photo PhotoColoris { get; set; }


        [InverseProperty(nameof(PeutContenir.ColorisPeutContenir))]
        public virtual ICollection<PeutContenir> PeutContenirColoris { get; set; }

       //[InverseProperty(nameof(ConfigurationMoto.ColorisConfigurationMoto))]
        public virtual ICollection<ConfigurationMoto> ConfigurationsMotoColoris { get; set; }

    }
}
