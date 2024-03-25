using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_segement_seg")]
    public class Segement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("seg_idsegement")]
        public int IdSegement { get; set; }

        [Column("seg_nomsegement")]
        [StringLength(50)]
        public string? NomSegement { get; set; }
        public virtual ICollection<Equipement> EquipementSegement { get; set; }
    }
}