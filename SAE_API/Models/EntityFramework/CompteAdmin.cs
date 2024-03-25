using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_compteadmin_cad")]
    public class CompteAdmin
    {
        [Key]
        [ForeignKey("CompteClient")]
        [Column("cad_idcompteclient")]
        public int IdCompteClient { get; set; }

        // Propriété de navigation vers CompteClient
        [InverseProperty(nameof(CompteClient.CompteAdminCompteClient))]
        public virtual CompteClient CompteClientCompteAdmin { get; set; }
    }
}