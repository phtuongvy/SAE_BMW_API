// PeutContenir.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_API.Models.EntityFramework
{
    
    [Table("t_e_peutcontenir_ptc")]
    public class PeutContenir
    {
        [Key, Column("ptc_idcoloris", Order = 0)]
        public int IdColoris { get; set; }

        [Key, Column("ptc_idmoto", Order = 1)]
        public int IdMoto { get; set; }

        [InverseProperty(nameof(Coloris.PeutContenirColoris))]
        public virtual Coloris ColorisPeutContenir { get; set; }


        [InverseProperty(nameof(Moto.PeutContenirMoto))]
        public virtual Moto MotoPeutContenir { get; set; }
    }
}
