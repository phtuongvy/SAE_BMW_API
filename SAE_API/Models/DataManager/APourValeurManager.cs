using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class APourValeurManager : IDataRepository<APourValeur>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public APourValeurManager() { }

        public APourValeurManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<APourValeur>>> GetAllAsync()
        {
            return await bmwDBContext.APourValeurs.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<APourValeur>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<APourValeur>> GetByIdAsync(int id, int id2)
        {
            return await bmwDBContext.APourValeurs.FirstOrDefaultAsync(u => u.IdCaracteristiqueMoto == id && u.IdMoto == id2);
        }
        public async Task<ActionResult<APourValeur>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<APourValeur>> GetByStringAsync(string nom)
        {
           throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(APourValeur entity)
        {
            await bmwDBContext.APourValeurs.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(APourValeur APourValeur, APourValeur entity)
        {
            bmwDBContext.Entry(APourValeur).State = EntityState.Modified;

            APourValeur.IdCaracteristiqueMoto = entity.IdCaracteristiqueMoto;
            APourValeur.IdCaracteristiqueMoto = entity.IdCaracteristiqueMoto;

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(APourValeur APourValeur)
        {
            bmwDBContext.APourValeurs.Remove(APourValeur);
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

        public Task<ActionResult<IEnumerable<APourValeur>>> GetByIdAsyncList(int id)
        {
            throw new NotImplementedException();
        }
    }
}
