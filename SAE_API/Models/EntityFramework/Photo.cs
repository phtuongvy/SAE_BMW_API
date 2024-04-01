// Photo.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_photo_pho")]
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("pho_id")]
        public int PhotoId { get; set; }

        [Column("pho_lienphoto")]
        [StringLength(1000)]
        public string LienPhoto { get; set; }

        // Other properties and navigation properties, if needed
        [InverseProperty(nameof(Coloris.PhotoColoris))]
        public virtual Coloris? ColorisPhoto { get; set; }

        [InverseProperty(nameof(Presente.PhotoPresente))]
        public virtual ICollection<Presente>? PresentePhoto { get; set; }

        [InverseProperty(nameof(Illustrer.PhotoIllustrer))]
        public virtual ICollection<Illustrer>? IllustrerPhoto { get; set; }

        [InverseProperty(nameof(EquipementMotoOption.PhotoEquipementMotoOption))]
        public virtual ICollection<EquipementMotoOption>? EquipementMotoOptionPhoto { get; set; }




    }
}
