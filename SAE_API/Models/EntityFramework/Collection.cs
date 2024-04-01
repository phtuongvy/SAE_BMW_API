// Collection.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_collection_clt")]
    public class Collection
    {
        [Key]
        [Column("clt_id")]
        public int IdCollection { get; set; }

        [Column("clt_nomcollection")]
        [StringLength(50)]
        public string NomCollection { get; set; }

        [InverseProperty(nameof(Equipement.CollectionEquipement))]

        public virtual ICollection<Equipement>? EquipementCollection { get; set; }
    }
}
