using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_compteclientprofessionnel_cpp")]
    public partial class CompteClientProfessionnel
    {
        public CompteClientProfessionnel()
        {
            
        }


        [Key]
        [ForeignKey("CompteClient")]
        [Column("cpp_idcompteclient")]
        public int IdCompteClient { get; set; }

        [Column("cpp_nomcompagnie")]
        [StringLength(50)]
        public string NomCompagnie { get; set; }

        // Propriété de navigation réciproque
        [InverseProperty(nameof(CompteClient.CompteClientProfessionnelCompteClient))]
        public virtual CompteClient? CompteClientCompteClientProfessionnel { get; set; }

    }
}
