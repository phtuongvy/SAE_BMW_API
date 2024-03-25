// CarteBancaire.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_cartebancaire_cbt")]
    public partial class CarteBancaire
    {
        public CarteBancaire()
        {
            AcquisCB = new HashSet<Acquerir>();
        }

        [Key]
        [Column("cbt_id")]
        public int IdCb { get; set; }

        [Column("cbt_nomcarte")]
        [StringLength(50)]
        public string NomCarte { get; set; }

        [Column("cbt_numerocb")]
        [StringLength(200)] // Utilisé pour le cryptage
        public string NumeroCb { get; set; }

        [Column("cbt_moisexpiration")]
        public int? MoisExpiration { get; set; }

        [Column("cbt_anneeexpiration")]
        public int? AnneeExpiration { get; set; }

        [Column("cbt_cryptocb")]
        [StringLength(200)] // Utilisé pour le cryptage
        public string CryptoCb { get; set; }

        // Navigation properties pour les relations avec d'autres entités si nécessaire

        [InverseProperty(nameof(Acquerir.CBAcquis))]
        public virtual ICollection<Acquerir> AcquisCB { get; set; }
    }
}
