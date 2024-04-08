using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class EstDanssManager : IDataRepository<EstDans>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public EstDanssManager() { }

        public EstDanssManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<EstDans>>> GetAllAsync()
        {
            return await bmwDBContext.EstDanss.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<EstDans>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<EstDans>> GetByIdAsync(int id , int id2)
        {
            return await bmwDBContext.EstDanss.FirstOrDefaultAsync(u => u.IdMoto == id && u.IdStock == id2);
        }
        public async Task<ActionResult<EstDans>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<EstDans>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(EstDans entity)
        {
            await bmwDBContext.EstDanss.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(EstDans estDans, EstDans entity)
        {
            bmwDBContext.Entry(estDans).State = EntityState.Modified;

            estDans.IdMoto = entity.IdMoto;
            estDans.IdStock = entity.IdStock;

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(EstDans estDans)
        {
            bmwDBContext.EstDanss.Remove(estDans);
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

        public Task<ActionResult<IEnumerable<EstDans>>> GetByIdAsyncList(int id)
        {
            throw new NotImplementedException();
        }
    }
}
