using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class EnregistrerManager : IDataRepository<Enregistrer>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public EnregistrerManager() { }

        public EnregistrerManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Enregistrer>>> GetAllAsync()
        {
            return await bmwDBContext.Enregistrers.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Enregistrer>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Enregistrers.FirstOrDefaultAsync(u => u.IdConfigurationMoto == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Enregistrer>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Enregistrer entity)
        {
            await bmwDBContext.Enregistrers.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Enregistrer Enregistrer, Enregistrer entity)
        {
            bmwDBContext.Entry(Enregistrer).State = EntityState.Modified;

            Enregistrer.IdConfigurationMoto = entity.IdConfigurationMoto;
            Enregistrer.IdCompteClient = entity.IdCompteClient;
           

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Enregistrer Enregistrer)
        {
            bmwDBContext.Enregistrers.Remove(Enregistrer);
            await bmwDBContext.SaveChangesAsync();
        }

    }
}
