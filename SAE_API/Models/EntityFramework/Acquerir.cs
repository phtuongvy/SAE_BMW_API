// Acquerir.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SAE_API.Models.EntityFramework
{
    [Table("t_j_acquerir_acq")]
    public class Acquerir
    {
        [Key, Column("acq_idcb", Order = 0)]
        [ForeignKey("CarteBancaire")]
        public int IdCb { get; set; }

        [Key, Column("acq_idcompteclient", Order = 1)]
        [ForeignKey("CompteClient")]
        public int IdCompteClient { get; set; }

        // Propriétés de navigation
        [InverseProperty(nameof(CarteBancaire.AcquisCB))]
        public virtual CarteBancaire CBAcquis { get; set; }
        [InverseProperty(nameof(CompteClient.AcquisComptes))]
        public virtual CompteClient ComptesAcquis { get; set; }



    }
}
