using SAE_API.Models.EntityFramework;
using SAE_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace SAE_API.Models.DataManager 
{
    public class MotoManager : IDataRepository<Moto>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public MotoManager() { }

        public MotoManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Moto>>> GetAllAsync()
        {
            return await bmwDBContext.Motos.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<object>>> GetAllAsync1()
        {
            var motos = await bmwDBContext.Motos
                .Select(m => new
                {
                    motoid = m.MotoId,
                    motonom = m.NomMoto,
                    motoprix = m.PrixMoto,
                    motogamme = m.GammeMotoMoto.NomGammeMoto, // Assurez-vous que la relation est correctement configurée
                    motophotos = m.IllustrerMoto.Select(i => i.PhotoIllustrer.LienPhoto).ToList(),

                    
                })
                .ToListAsync();

            return motos; 
        }


        //recherche par ID moto
        public async Task<ActionResult<object>> GetByIdCustomAsync1(int id)
        {
            var moto = await bmwDBContext.Motos
            .Where(m => m.MotoId == id)
            .Select(m => new
            {
                motoid = m.MotoId,
                motonom = m.NomMoto,
                motoprix = m.PrixMoto,
                motogamme = m.GammeMotoMoto.NomGammeMoto,
                motodescription = m.DescriptionMoto,
                motophotos = m.IllustrerMoto.Select(i => i.PhotoIllustrer.LienPhoto).ToList(),

                motocaracteristique = m.APourValeurMoto.Select(v => new
                {
                    caracteristiqueid = v.CaracteristiqueMotoPourValeur.IdCaracteristiqueMoto,
                    caracteristiquevaleur = v.CaracteristiqueMotoPourValeur.ValeurCaracteristiqueMoto,
                    caracteristiquenom = v.CaracteristiqueMotoPourValeur.NomCaracteristiqueMoto,
                    caracteristiquecategorienom = v.CaracteristiqueMotoPourValeur.CategorieCaracteristiqueMotoCaracteristiqueMoto.NomCategorieCaracteristiqueMoto
                }).ToList(),

                motocoloris = m.PeutContenirMoto.Select(c => new
                {
                    colorisid = c.ColorisPeutContenir.IdColoris,
                    colorisnom = c.ColorisPeutContenir.NomColoris,
                    colorisdescription = c.ColorisPeutContenir.DescriptionColoris,
                    colorisprix = c.ColorisPeutContenir.PrixColoris,
                    colorisphoto = c.ColorisPeutContenir.PhotoColoris.LienPhoto
                }).ToList(),

                motopacks = m.PeutEquiperMoto.Select(p => new
                {
                    packid = p.PackPeutEquiper.PackId,
                    packnom = p.PackPeutEquiper.NomPack,
                    packdecription = p.PackPeutEquiper.DescriptionPack,
                    packprix = p.PackPeutEquiper.PrixPack,
                }),

                motooption = m.PossederMoto.Select(p => new
                {
                    idequipement = p.EquipementMotoOptionPosseder.IdEquipementMoto,
                    nomequipement = p.EquipementMotoOptionPosseder.NomEquipement,
                    descriptionequipement = p.EquipementMotoOptionPosseder.DescriptionEquipementMoto,
                    prixequipement = p.EquipementMotoOptionPosseder.PrixEquipementMoto,

                    lienphoto = p.EquipementMotoOptionPosseder.PhotoEquipementMotoOption.LienPhoto,
                })
            }) 
            .FirstOrDefaultAsync();


            return new ActionResult<object>(moto);
        }

        public async Task<ActionResult<Moto>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
       
        //recherche par nom de moto
        public async Task<ActionResult<Moto>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.Motos.FirstOrDefaultAsync(u => u.NomMoto.ToUpper() == nom.ToUpper());
        }
        public async Task<ActionResult<Moto>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Moto entity)
        {
            await bmwDBContext.Motos.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Moto moto, Moto entity)
        {
            bmwDBContext.Entry(moto).State = EntityState.Modified;

            moto.MotoId = entity.MotoId;
            moto.PrixMoto = entity.PrixMoto;
            moto.IdGammeMoto = entity.IdGammeMoto;
            moto.NomMoto = entity.NomMoto;
            moto.DescriptionMoto = entity.DescriptionMoto;
            moto.PrixMoto = entity.PrixMoto;
            moto.GammeMotoMoto = entity.GammeMotoMoto;
            moto.APourValeurMoto = entity.APourValeurMoto;
            moto.PeutContenirMoto = entity.PeutContenirMoto;
            moto.PeutEquiperMoto = entity.PeutEquiperMoto;
            moto.ConfigurationMotoMoto = entity.ConfigurationMotoMoto;
            moto.PossederMoto = entity.PossederMoto;
            moto.MotoDisponibleMoto = entity.MotoDisponibleMoto;
            moto.PossederMoto = entity.PossederMoto;
            moto.IllustrerMoto = entity.IllustrerMoto;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Moto moto)
        {
            bmwDBContext.Motos.Remove(moto);
            await bmwDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<Moto>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Motos.FirstOrDefaultAsync(u => u.MotoId == id);
        }

        public Task<ActionResult<IEnumerable<Moto>>> GetByIdAsyncList(int id)
        {
            throw new NotImplementedException();
        }
    }
}
