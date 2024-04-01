// MoyenDePaiement.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_moyendepaiement_mdp")]
    public class MoyenDePaiement
    {
        [Key]
        [Column("mdp_idmoyendepaiement")]
        public int IdMoyenDePaiement { get; set; }

        [Column("mdp_libellemoyendepaiement")]
        [StringLength(50)]
        public string LibelleMoyenDePaiement { get; set; }

        [InverseProperty(nameof(Estimer.MoyenDePaiementEstimer))]
        public virtual ICollection<Estimer>? EstimationMoyenDePaiement { get; set; }

    }
}
