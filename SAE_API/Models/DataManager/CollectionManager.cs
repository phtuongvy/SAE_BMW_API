using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class CollectionManager : IDataRepository<Collection>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public CollectionManager() { }

        public CollectionManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Collection>>> GetAllAsync()
        {
            return await bmwDBContext.Collections.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Collection>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Collections.FirstOrDefaultAsync(u => u.IdCollection == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Collection>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.Collections.FirstOrDefaultAsync(u => u.NomCollection.ToUpper() == nom.ToUpper());
        }
        //ajoute une moto 
        public async Task AddAsync(Collection entity)
        {
            await bmwDBContext.Collections.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Collection Collection, Collection entity)
        {
            bmwDBContext.Entry(Collection).State = EntityState.Modified;

            Collection.IdCollection = entity.IdCollection;
            Collection.NomCollection = entity.NomCollection;
            

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Collection Collection)
        {
            bmwDBContext.Collections.Remove(Collection);
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
