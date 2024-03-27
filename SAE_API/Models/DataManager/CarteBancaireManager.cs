using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class CarteBancaireManager :  IDataRepository<CarteBancaire>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public CarteBancaireManager() { }

        public CarteBancaireManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<CarteBancaire>>> GetAllAsync()
        {
            return await bmwDBContext.CartesBancaires.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<CarteBancaire>> GetByIdAsync(int id)
        {
            return await bmwDBContext.CartesBancaires.FirstOrDefaultAsync(u => u.IdCb == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<CarteBancaire>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.CartesBancaires.FirstOrDefaultAsync(u => u.NomCarte.ToUpper() == nom.ToUpper());
        }
        //ajoute une moto 
        public async Task AddAsync(CarteBancaire entity)
        {
            await bmwDBContext.CartesBancaires.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(CarteBancaire cartebancaire, CarteBancaire entity)
        {
            bmwDBContext.Entry(cartebancaire).State = EntityState.Modified;

            cartebancaire.IdCb = entity.IdCb;
            cartebancaire.NomCarte = entity.NomCarte;
            cartebancaire.NumeroCb = entity.NumeroCb;
            cartebancaire.MoisExpiration = entity.MoisExpiration;
            cartebancaire.AnneeExpiration = entity.AnneeExpiration;
            cartebancaire.CryptoCb = entity.CryptoCb;
            cartebancaire.AcquisCB = entity.AcquisCB;
            
            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(CarteBancaire cartebancaire)
        {
            bmwDBContext.CartesBancaires.Remove(cartebancaire);
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
