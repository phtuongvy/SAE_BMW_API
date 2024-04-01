using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_transation_tra")]
    public class Transation
    {
        [Key]
        [Column("tra_idtransaction")]
        public int IdTransaction { get; set; }

        [Column("tra_idcompteclient")]
        [Required]
        public int IdCompteClient { get; set; }

        [Column("tra_montant", TypeName = "numeric")]
        [Required]
        public decimal Montant { get; set; }

        [Column("tra_typdepayment")]
        [StringLength(10)]
        [Required]
        public string TypeDePayment { get; set; }

        [Column("tra_typedetransaction")]
        [StringLength(10)]
        [Required]
        public string TypeDeTransaction { get; set; }

        public virtual CompteClient? CompteClientTransation { get; set; }




    }
}