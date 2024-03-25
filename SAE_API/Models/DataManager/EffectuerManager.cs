using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;


namespace SAE_API.Models.DataManager
{
    public class EffectuerManager : IDataRepository<Effectuer>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public EffectuerManager() { }

        public EffectuerManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Effectuer>>> GetAllAsync()
        {
            return await bmwDBContext.Effectuers.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Effectuer>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Effectuers.FirstOrDefaultAsync(u => u.IdCompteClient == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Effectuer>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Effectuer entity)
        {
            await bmwDBContext.Effectuers.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Effectuer effectuers, Effectuer entity)
        {
            bmwDBContext.Entry(effectuers).State = EntityState.Modified;

            effectuers.IdCommande = entity.IdCommande;
            effectuers.IdCompteClient = entity.IdCompteClient;
            

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Effectuer effectuers)
        {
            bmwDBContext.Effectuers.Remove(effectuers);
            await bmwDBContext.SaveChangesAsync();
        }
    }
}
