using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    [Table("t_e_compteclient_ccl")]
    public partial class CompteClient
    {
        public CompteClient()
        {
            EnregistrerCompteClient = new HashSet<Enregistrer>();
            EffectuerCompteClient = new HashSet<Effectuer>();
            AcquisComptes = new HashSet<Acquerir>();
            FavorisCompteClient = new HashSet<Favoris>();
            TransationCompteClient = new HashSet<Transation>();
            EstimerCompteClient = new HashSet<Estimer>();
            RepriseMotoCompteClient = new HashSet<RepriseMoto>();

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ccl_idcompteclient")]
        public int IdCompteClient { get; set; }

        [Column("ccl_nomclient")]
        [StringLength(20)]
        public string? NomClient { get; set; }

        [Column("ccl_prenomclient")]
        [StringLength(20)]
        public string? PrenomClient { get; set; }

        [Column("ccl_civiliteclient")]
        [StringLength(3)]
        public string? CiviliteClient { get; set; }

        [Column("ccl_numeroclient")]
        [StringLength(20)]
        public string? NumeroClient { get; set; }

        [Column("ccl_email")]
        [StringLength(50)]
        public string? Email { get; set; }

        [Column("ccl_datenaissanceclient", TypeName = "date")]
        public DateTime? DatenaissanceClient { get; set; }

        [Column("ccl_password", TypeName = "bytea")]
        public byte[]? Password { get; set; }

        [Column("ccl_clientrole")]
        [StringLength(50)]
        public string? ClientRole { get; set; }

        [InverseProperty(nameof(Enregistrer.CompteClientEnregistrer))]
        public virtual ICollection<Enregistrer> EnregistrerCompteClient { get; set; }

        [InverseProperty(nameof(Effectuer.CompteClientEffectuer))]
        public virtual ICollection<Effectuer> EffectuerCompteClient { get; set; }

        [InverseProperty(nameof(Acquerir.ComptesAcquis))]
        public virtual ICollection<Acquerir> AcquisComptes { get; set; }

        [InverseProperty(nameof(Favoris.CompteClientFavoris))]
        public virtual ICollection<Favoris> FavorisCompteClient { get; set; }

        [InverseProperty(nameof(Transation.CompteClientTransation))]
        public virtual ICollection<Transation> TransationCompteClient { get; set; }

        [InverseProperty(nameof(Adresse.ClientAdresse))]
        //public virtual ICollection<Adresse> AdresseCompteClient { get; set; }
        public virtual Adresse AdresseCompteClient { get; set; }

        [InverseProperty(nameof(CompteAdmin.CompteClientCompteAdmin))]
        public virtual CompteAdmin CompteAdminCompteClient { get; set; }

        [InverseProperty(nameof(CompteClientPrive.CompteClientCompteClientPrive))]
        public virtual CompteClientPrive CompteClientPriveCompteClient { get; set; }
        [InverseProperty(nameof(CompteClientProfessionnel.CompteClientCompteClientProfessionnel))]
        public virtual CompteClientProfessionnel CompteClientProfessionnelCompteClient { get; set; }
        [InverseProperty(nameof(Estimer.CompteClientEstimer))]
        public virtual ICollection<Estimer> EstimerCompteClient { get; set; }

        [InverseProperty(nameof(RepriseMoto.CompteClientRepriseMoto))]
        public virtual ICollection<RepriseMoto> RepriseMotoCompteClient { get; set; }
    }
}


