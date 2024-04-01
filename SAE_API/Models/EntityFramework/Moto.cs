// Moto.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_moto_mto")]
    public class Moto
    {
        [Key]
        [Column("mto_id")]
        public int MotoId { get; set; }

        [Column("mto_idgammemoto")]
        [ForeignKey("GammeMoto")] // Remplacer GammeMoto par le modèle correspondant à IDGAMMEMOTO si nécessaire
        public int IdGammeMoto { get; set; }

        [Column("mto_nommoto")]
        [StringLength(50)]
        public string? NomMoto { get; set; }

        [Column("mto_descriptionmoto")]
        [StringLength(500)]
        public string? DescriptionMoto { get; set; }

        [Column("mto_prixmoto")]
        public decimal PrixMoto { get; set; }

        // Propriété de navigation pour GammeMoto, si tu as un modèle correspondant
        [InverseProperty(nameof(GammeMoto.MotoGammeMoto))]
        public virtual GammeMoto? GammeMotoMoto { get; set; }

        [InverseProperty(nameof(APourValeur.MotoAPourValeur))]
        public virtual ICollection<APourValeur>? APourValeurMoto { get; set; }

        [InverseProperty(nameof(PeutContenir.MotoPeutContenir))]
        public virtual ICollection<PeutContenir>? PeutContenirMoto { get; set; }

        [InverseProperty(nameof(PeutEquiper.MotoPeutEquiper))]
        public virtual ICollection<PeutEquiper>? PeutEquiperMoto { get; set; }

        [InverseProperty(nameof(ConfigurationMoto.MotoConfigurationMoto))]
        public virtual ICollection<ConfigurationMoto>? ConfigurationMotoMoto { get; set; }

        [InverseProperty(nameof(MotoDisponible.MotoMotoDisponible))]
        public virtual MotoDisponible? MotoDisponibleMoto { get; set; }

        [InverseProperty(nameof(Posseder.MotoPosseder))]
        public virtual ICollection<Posseder>? PossederMoto { get; set; }

        [InverseProperty(nameof(Illustrer.MotoIllustrer))]
        public virtual ICollection<Illustrer>? IllustrerMoto { get; set; }

        [InverseProperty(nameof(EstDans.MotoEstDans))]
        public virtual ICollection<EstDans>? EstDansMoto { get; set; }




    }
}
