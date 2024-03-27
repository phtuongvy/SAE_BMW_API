using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class StockManager : IDataRepository<Stock>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public StockManager() { }

        public StockManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Stock>>> GetAllAsync()
        {
            return await bmwDBContext.Stocks.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Stock>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Stocks.FirstOrDefaultAsync(u => u.IdStock == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Stock>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Stock entity)
        {
            await bmwDBContext.Stocks.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Stock stock, Stock entity)
        {
            bmwDBContext.Entry(stock).State = EntityState.Modified;

            stock.IdStock = entity.IdStock;
           

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Stock stock)
        {
            bmwDBContext.Stocks.Remove(stock);
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
