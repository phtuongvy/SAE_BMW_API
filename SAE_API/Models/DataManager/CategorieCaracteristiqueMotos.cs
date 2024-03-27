using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class CategorieCaracteristiqueMotoManager : IDataRepository<CategorieCaracteristiqueMoto>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public CategorieCaracteristiqueMotoManager() { }

        public CategorieCaracteristiqueMotoManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<CategorieCaracteristiqueMoto>>> GetAllAsync()
        {
            return await bmwDBContext.CategorieCaracteristiqueMotos.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<CategorieCaracteristiqueMoto>> GetByIdAsync(int id)
        {
            return await bmwDBContext.CategorieCaracteristiqueMotos.FirstOrDefaultAsync(u => u.IdCategorieCaracteristiqueMoto == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<CategorieCaracteristiqueMoto>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(CategorieCaracteristiqueMoto entity)
        {
            await bmwDBContext.CategorieCaracteristiqueMotos.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(CategorieCaracteristiqueMoto CategorieCaracteristiqueMoto, CategorieCaracteristiqueMoto entity)
        {
            bmwDBContext.Entry(CategorieCaracteristiqueMoto).State = EntityState.Modified;

            CategorieCaracteristiqueMoto.IdCategorieCaracteristiqueMoto = entity.IdCategorieCaracteristiqueMoto;
            CategorieCaracteristiqueMoto.NomCategorieCaracteristiqueMoto = entity.NomCategorieCaracteristiqueMoto;



            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(CategorieCaracteristiqueMoto CategorieCaracteristiqueMoto)
        {
            bmwDBContext.CategorieCaracteristiqueMotos.Remove(CategorieCaracteristiqueMoto);
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
