using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_favoris_fav")]
    public class Favoris
    {
        [Key, Column("fav_idcompteclient", Order = 0)]
        [ForeignKey("CompteClient")]
        public int IdCompteClient { get; set; }

        [Key, Column("fav_idconcessionnaire", Order = 1)]
        [ForeignKey("Concessionnaire")]
        public int IdConcessionnaire { get; set; }


        [InverseProperty(nameof(CompteClient.FavorisCompteClient))]
        public virtual CompteClient CompteClientFavoris { get; set; }

        [InverseProperty(nameof(Concessionnaire.FavorisConcessionnaire))]
        public virtual Concessionnaire ConcessionnaireFavoris { get; set; }
    }
}
