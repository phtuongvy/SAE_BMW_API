using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class IllustrerManager : IDataRepository<Illustrer>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public IllustrerManager() { }

        public IllustrerManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Illustrer>>> GetAllAsync()
        {
            return await bmwDBContext.Illustrers.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Illustrer>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Illustrer>> GetByIdAsync(int id , int id2)
        {
            return await bmwDBContext.Illustrers.FirstOrDefaultAsync(u => u.IdMoto == id && u.IdPhoto == id2);
        }
        public async Task<ActionResult<Illustrer>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<Illustrer>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Illustrer entity)
        {
            await bmwDBContext.Illustrers.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Illustrer illustrer, Illustrer entity)
        {
            bmwDBContext.Entry(illustrer).State = EntityState.Modified;

            illustrer.IdMoto = entity.IdMoto;
            illustrer.IdPhoto = entity.IdPhoto;

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Illustrer illustrer)
        {
            bmwDBContext.Illustrers.Remove(illustrer);
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

        public Task<ActionResult<IEnumerable<Illustrer>>> GetByIdAsyncList(int id)
        {
            throw new NotImplementedException();
        }
    }
}
