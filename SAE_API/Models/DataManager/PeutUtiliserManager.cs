using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class PeutUtiliserManager : IDataRepository<PeutUtiliser>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public PeutUtiliserManager() { }

        public PeutUtiliserManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<PeutUtiliser>>> GetAllAsync()
        {
            return await bmwDBContext.PeutUtilisers.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<PeutUtiliser>> GetByIdAsync(int id)
        {
            return await bmwDBContext.PeutUtilisers.FirstOrDefaultAsync(u => u.IdEquipementMoto == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<PeutUtiliser>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(PeutUtiliser entity)
        {
            await bmwDBContext.PeutUtilisers.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(PeutUtiliser peutUtiliser, PeutUtiliser entity)
        {
            bmwDBContext.Entry(peutUtiliser).State = EntityState.Modified;

            peutUtiliser.IdEquipementMoto = entity.IdEquipementMoto;
            peutUtiliser.IdPack = entity.IdPack;

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(PeutUtiliser peutUtiliser)
        {
            bmwDBContext.PeutUtilisers.Remove(peutUtiliser);
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
