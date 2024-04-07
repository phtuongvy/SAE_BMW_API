using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class FavorisManager : IDataRepository<Favoris>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public FavorisManager() { }

        public FavorisManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Favoris>>> GetAllAsync()
        {
            return await bmwDBContext.Favoriss.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Favoris>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Favoriss.FirstOrDefaultAsync(u => u.IdCompteClient == id);
        }

        public async Task<ActionResult<Favoris>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Favoris>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<Favoris>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Favoris entity)
        {
            await bmwDBContext.Favoriss.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Favoris Favoris, Favoris entity)
        {
            bmwDBContext.Entry(Favoris).State = EntityState.Modified;

            Favoris.IdCompteClient = entity.IdCompteClient;
            Favoris.IdConcessionnaire = entity.IdConcessionnaire;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Favoris Favoris)
        {
            bmwDBContext.Favoriss.Remove(Favoris);
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
