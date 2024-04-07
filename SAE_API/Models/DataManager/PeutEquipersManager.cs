using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class PeutEquiperManager : IDataRepository<PeutEquiper>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public PeutEquiperManager() { }

        public PeutEquiperManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<PeutEquiper>>> GetAllAsync()
        {
            return await bmwDBContext.PeutEquipers.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<PeutEquiper>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<PeutEquiper>> GetByIdAsync(int id , int id2)
        {
            return await bmwDBContext.PeutEquipers.FirstOrDefaultAsync(u => u.IdPack == id && u.IdMoto == id2);
        }
        public async Task<ActionResult<PeutEquiper>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<PeutEquiper>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(PeutEquiper entity)
        {
            await bmwDBContext.PeutEquipers.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(PeutEquiper PeutEquiper, PeutEquiper entity)
        {
            bmwDBContext.Entry(PeutEquiper).State = EntityState.Modified;

            PeutEquiper.IdPack = entity.IdPack;
            PeutEquiper.IdMoto = entity.IdPack;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(PeutEquiper PeutEquiper)
        {
            bmwDBContext.PeutEquipers.Remove(PeutEquiper);
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
