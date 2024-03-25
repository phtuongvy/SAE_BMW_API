using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class EquipementMotoOptionManager : IDataRepository<EquipementMotoOption>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public EquipementMotoOptionManager() { }

        public EquipementMotoOptionManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<EquipementMotoOption>>> GetAllAsync()
        {
            return await bmwDBContext.EquipementMotoOptions.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<EquipementMotoOption>> GetByIdAsync(int id)
        {
            return await bmwDBContext.EquipementMotoOptions.FirstOrDefaultAsync(u => u.IdEquipementMoto == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<EquipementMotoOption>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(EquipementMotoOption entity)
        {
            await bmwDBContext.EquipementMotoOptions.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(EquipementMotoOption equipementMotoOption, EquipementMotoOption entity)
        {
            bmwDBContext.Entry(equipementMotoOption).State = EntityState.Modified;

            equipementMotoOption.IdEquipementMoto = entity.IdEquipementMoto;
            equipementMotoOption.IdPhoto = entity.IdPhoto;
           
            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(EquipementMotoOption equipementMotoOption)
        {
            bmwDBContext.EquipementMotoOptions.Remove(equipementMotoOption);
            await bmwDBContext.SaveChangesAsync();
        }
    }
}
