using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager 
{
    public class APourCouleurManager: IDataRepository<APourCouleur>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public APourCouleurManager() { }

        public APourCouleurManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<APourCouleur>>> GetAllAsync()
        {
            return await bmwDBContext.APourCouleurs.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<APourCouleur>> GetByIdAsync(int id)
        {
           throw new NotImplementedException();
        }
        public async Task<ActionResult<APourCouleur>> GetByIdAsync(int id, int id2)
        {
            return await bmwDBContext.APourCouleurs.FirstOrDefaultAsync(u => u.IdEquipement == id && u.IdCouleurEquipement == id2);
        }
        //recherche par nom de moto
        public async Task<ActionResult<APourCouleur>> GetByStringAsync(string nom)
        {
           throw new NotImplementedException();
        }
        public async Task<ActionResult<APourCouleur>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(APourCouleur entity)
        {
            await bmwDBContext.APourCouleurs.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(APourCouleur aPourCouleur, APourCouleur entity)
        {
            bmwDBContext.Entry(aPourCouleur).State = EntityState.Modified;

            aPourCouleur.IdCouleurEquipement = entity.IdCouleurEquipement;
            aPourCouleur.IdEquipement = entity.IdEquipement;

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(APourCouleur aPourCouleur)
        {
            bmwDBContext.APourCouleurs.Remove(aPourCouleur);
            await bmwDBContext.SaveChangesAsync();
        }
        public Task<ActionResult<Object>> GetByIdCustomAsync1(Int32 id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<IEnumerable<Object>>> GetAllAsync1()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<APourCouleur>>> GetByIdAsyncList(int id)
        {
            throw new NotImplementedException();
        }
    }
}
