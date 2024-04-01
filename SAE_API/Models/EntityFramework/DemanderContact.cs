using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_demandercontact_dmc")]
    public class DemanderContact
    {
        [Key]
        [Column("dmc_idreservationoffre")]
        public int IdReservationOffre { get; set; }

        [Column("dmc_objetdelademande")]
        [StringLength(100)]
        public string ObjetDeLaDemande { get; set; }

        [Column("dmc_objet")]
        [StringLength(50)]
        public string Objet { get; set; }

        // Propriété de navigation vers ReservationOffre (assumant l'existence d'un modèle ReservationOffre)
        [ForeignKey(nameof(IdReservationOffre))]
        [InverseProperty(nameof(PriseRendezvous.DemanderContactPriseRendezVous))]
        public virtual PriseRendezvous? PriseRendezvousDemanderContact { get; set; }

    }
}
