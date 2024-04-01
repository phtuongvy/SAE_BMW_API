using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_priserendezvous_prv")]
    public class PriseRendezvous
    {
        [Key]
        [Column("prv_idreservationoffre")]
        public int IdReservationOffre { get; set; }

        [Column("prv_idconcessionnaire")]
        [ForeignKey("Concessionnaire")]
        public int? IdConcessionnaire { get; set; }

        [Column("prv_nomreservation")]
        [StringLength(50)]
        public string? NomReservation { get; set; }

        [Column("prv_prenomreservation")]
        [StringLength(50)]
        public string? PrenomReservation { get; set; }

        [Column("prv_civilreservation")]
        [StringLength(3)]
        public string? CiviliteReservation { get; set; }

        [Column("prv_emailreservation")]
        [StringLength(100)]
        public string? EmailReservation { get; set; }

        [Column("prv_telephonereservation")]
        [StringLength(100)]
        public string? TelephoneReservation { get; set; }

        [Column("prv_villereservation")]
        [StringLength(50)]
        public string? VilleReservation { get; set; }

        [Column("prv_typedepermis")]
        public int? TypeDePermis { get; set; }

        public Concessionnaire? ConcessionnairePriseRendezvous { get; set; }
        [InverseProperty(nameof(DemanderContact.PriseRendezvousDemanderContact))]
        public virtual DemanderContact? DemanderContactPriseRendezVous { get; set; }

        [InverseProperty(nameof(Reservation.PriseRendezvousReservation))]
        public virtual Reservation? ReservationPriseRendezvous { get; set; }
    }
}
