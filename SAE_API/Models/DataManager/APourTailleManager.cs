using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class APourTailleManager : IDataRepository<APourTaille>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public APourTailleManager() { }

        public APourTailleManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<APourTaille>>> GetAllAsync()
        {
            return await bmwDBContext.APourTailles.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<APourTaille>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<APourTaille>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par ID moto
        public async Task<ActionResult<APourTaille>> GetByIdAsync(int id, int id2)
        {
            return await bmwDBContext.APourTailles.FirstOrDefaultAsync(u =>u.IdEquipement == id && u.IdTailleEquipement == id2);
        }
        //recherche par nom de moto
        public async Task<ActionResult<APourTaille>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(APourTaille entity)
        {
            await bmwDBContext.APourTailles.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(APourTaille aPourTailles, APourTaille entity)
        {
            bmwDBContext.Entry(aPourTailles).State = EntityState.Modified;

            aPourTailles.IdTailleEquipement = entity.IdTailleEquipement;
            aPourTailles.IdEquipement = entity.IdEquipement;
           
            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(APourTaille aPourTailles)
        {
            bmwDBContext.APourTailles.Remove(aPourTailles);
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
    }
}
