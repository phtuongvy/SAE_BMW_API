using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class AChoisiOptionManager : IDataRepository<AChoisiOption>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public AChoisiOptionManager() { }

        public AChoisiOptionManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<AChoisiOption>>> GetAllAsync()
        {
            return await bmwDBContext.AChoisiOptions.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<AChoisiOption>> GetByIdAsync(int id)
        {
            return await bmwDBContext.AChoisiOptions.FirstOrDefaultAsync(u => u.IdEquipementMoto == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<AChoisiOption>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(AChoisiOption entity)
        {
            await bmwDBContext.AChoisiOptions.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(AChoisiOption AChoisiOption, AChoisiOption entity)
        {
            bmwDBContext.Entry(AChoisiOption).State = EntityState.Modified;

            AChoisiOption.IdConfigurationMoto = entity.IdConfigurationMoto;
            AChoisiOption.IdEquipementMoto = entity.IdEquipementMoto;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(AChoisiOption AChoisiOption)
        {
            bmwDBContext.AChoisiOptions.Remove(AChoisiOption);
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
