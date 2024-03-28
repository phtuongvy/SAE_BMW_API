using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class CompteClientPriveManager : IDataRepository<CompteClientPrive>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public CompteClientPriveManager() { }

        public CompteClientPriveManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<CompteClientPrive>>> GetAllAsync()
        {
            return await bmwDBContext.CompteClientPrives.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<CompteClientPrive>> GetByIdAsync(int id)
        {
            return await bmwDBContext.CompteClientPrives.FirstOrDefaultAsync(u => u.IdCompteClient == id);
        }

        public async Task<ActionResult<CompteClientPrive>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<CompteClientPrive>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<CompteClientPrive>> GetByIdAsync(int id, int id2, int id3)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(CompteClientPrive entity)
        {
            await bmwDBContext.CompteClientPrives.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(CompteClientPrive CompteClientPrive, CompteClientPrive entity)
        {
            bmwDBContext.Entry(CompteClientPrive).State = EntityState.Modified;

 
            CompteClientPrive.IdCompteClient = entity.IdCompteClient;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(CompteClientPrive CompteClientPrive)
        {
            bmwDBContext.CompteClientPrives.Remove(CompteClientPrive);
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
