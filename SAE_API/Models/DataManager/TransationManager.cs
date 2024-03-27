using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class TransationManager : IDataRepository<Transation>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public TransationManager() { }

        public TransationManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Transation>>> GetAllAsync()
        {
            return await bmwDBContext.Transations.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Transation>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Transations.FirstOrDefaultAsync(u => u.IdTransaction == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Transation>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Transation entity)
        {
            await bmwDBContext.Transations.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Transation cartebancaire, Transation entity)
        {
            bmwDBContext.Entry(cartebancaire).State = EntityState.Modified;

            cartebancaire.IdTransaction = entity.IdTransaction;
            cartebancaire.IdCompteClient = entity.IdCompteClient;
            cartebancaire.Montant = entity.Montant;
            cartebancaire.TypeDePayment = entity.TypeDePayment;
            cartebancaire.TypeDeTransaction = entity.TypeDeTransaction;
            

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Transation cartebancaire)
        {
            bmwDBContext.Transations.Remove(cartebancaire);
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
