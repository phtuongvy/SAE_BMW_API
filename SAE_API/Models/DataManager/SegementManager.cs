using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class SegementManager : IDataRepository<Segement>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public SegementManager() { }

        public SegementManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Segement>>> GetAllAsync()
        {
            return await bmwDBContext.Segements.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Segement>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Segements.FirstOrDefaultAsync(u => u.IdSegement == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Segement>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.Segements.FirstOrDefaultAsync(u => u.NomSegement.ToUpper() == nom.ToUpper());
        }
        //ajoute une moto 
        public async Task AddAsync(Segement entity)
        {
            await bmwDBContext.Segements.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Segement segement, Segement entity)
        {
            bmwDBContext.Entry(segement).State = EntityState.Modified;

            segement.IdSegement = entity.IdSegement;
            segement.NomSegement = entity.NomSegement;
            

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Segement segement)
        {
            bmwDBContext.Segements.Remove(segement);
            await bmwDBContext.SaveChangesAsync();
        }


    }
}
