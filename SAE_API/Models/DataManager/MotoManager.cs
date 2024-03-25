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
        //recherche par ID moto
        public async Task<ActionResult<Moto>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Motos.FirstOrDefaultAsync(u => u.MotoId == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Moto>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.Motos.FirstOrDefaultAsync(u => u.NomMoto.ToUpper() == nom.ToUpper());
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
    }
}
