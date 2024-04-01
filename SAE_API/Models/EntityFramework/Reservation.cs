using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_reservation_res")]
    public class Reservation
    {
        [Key]
        [ForeignKey(nameof(IdReservationOffre))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("res_idreservationoffre")]
        public int IdReservationOffre { get; set; }

        [Column("res_idconcessionnaire")]
        public int? IdConcessionnaire { get; set; }

        [Column("res_financementreservationoffre")]
        [StringLength(20)]
        public string? FinancementReservationOffre { get; set; }

        [Column("res_assurancereservation")]
        [StringLength(50)]
        public string? AssuranceReservation { get; set; }


        [InverseProperty(nameof(PriseRendezvous.ReservationPriseRendezvous))]
        public virtual PriseRendezvous? PriseRendezvousReservation { get; set; }
    }
}
