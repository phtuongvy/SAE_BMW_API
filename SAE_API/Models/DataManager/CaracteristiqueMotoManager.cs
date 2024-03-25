using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class CaracteristiqueMotoManager : IDataRepository<CaracteristiqueMoto>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public CaracteristiqueMotoManager() { }

        public CaracteristiqueMotoManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<CaracteristiqueMoto>>> GetAllAsync()
        {
            return await bmwDBContext.CaracteristiqueMotos.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<CaracteristiqueMoto>> GetByIdAsync(int id)
        {
            return await bmwDBContext.CaracteristiqueMotos.FirstOrDefaultAsync(u => u.IdCaracteristiqueMoto == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<CaracteristiqueMoto>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(CaracteristiqueMoto entity)
        {
            await bmwDBContext.CaracteristiqueMotos.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(CaracteristiqueMoto caracteristiqueMoto, CaracteristiqueMoto entity)
        {
            bmwDBContext.Entry(caracteristiqueMoto).State = EntityState.Modified;

            caracteristiqueMoto.IdCategorieCaracteristiqueMoto = entity.IdCategorieCaracteristiqueMoto;
            caracteristiqueMoto.IdCaracteristiqueMoto = entity.IdCaracteristiqueMoto;
 

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(CaracteristiqueMoto caracteristiqueMoto)
        {
            bmwDBContext.CaracteristiqueMotos.Remove(caracteristiqueMoto);
            await bmwDBContext.SaveChangesAsync();
        }
    }
}
