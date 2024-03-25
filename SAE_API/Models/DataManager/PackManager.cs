using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class PackManager : IDataRepository<Pack>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public PackManager() { }

        public PackManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Pack>>> GetAllAsync()
        {
            return await bmwDBContext.Packs.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Pack>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Packs.FirstOrDefaultAsync(u => u.PackId == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Pack>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Pack entity)
        {
            await bmwDBContext.Packs.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Pack pack, Pack entity)
        {
            bmwDBContext.Entry(pack).State = EntityState.Modified;

            pack.PackId = entity.PackId;
            pack.ColorisId = entity.ColorisId;
            pack.NomPack = entity.NomPack;
            pack.PrixPack = entity.PrixPack;

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Pack pack)
        {
            bmwDBContext.Packs.Remove(pack);
            await bmwDBContext.SaveChangesAsync();
        }
    }
}
