using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class PeutContenirManager : IDataRepository<PeutContenir>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public PeutContenirManager() { }

        public PeutContenirManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<PeutContenir>>> GetAllAsync()
        {
            return await bmwDBContext.PeutContenirs.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<PeutContenir>> GetByIdAsync(int id)
        {
            return await bmwDBContext.PeutContenirs.FirstOrDefaultAsync(u => u.IdMoto == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<PeutContenir>> GetByStringAsync(string nom)
        {
            throw new Exception();
        }
        //ajoute une moto 
        public async Task AddAsync(PeutContenir entity)
        {
            await bmwDBContext.PeutContenirs.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(PeutContenir cartebancaire, PeutContenir entity)
        {
            bmwDBContext.Entry(cartebancaire).State = EntityState.Modified;

            cartebancaire.IdMoto = entity.IdMoto;
            cartebancaire.IdColoris = entity.IdColoris;
           

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(PeutContenir cartebancaire)
        {
            bmwDBContext.PeutContenirs.Remove(cartebancaire);
            await bmwDBContext.SaveChangesAsync();
        }

    }
}
