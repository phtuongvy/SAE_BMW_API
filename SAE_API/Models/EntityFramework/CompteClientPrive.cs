using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_compteclientprive_cpp")]
    public partial class CompteClientPrive
    {
        public CompteClientPrive()
        {
            
        }


        [Key]
        [ForeignKey("CompteClient")]
        [Column("cpp_idcompteclient")]
        public int IdCompteClient { get; set; }

        // Propriété de navigation vers CompteClient
        [InverseProperty(nameof(CompteClient.CompteClientPriveCompteClient))]
        public virtual CompteClient CompteClientCompteClientPrive { get; set; }
    }
}
