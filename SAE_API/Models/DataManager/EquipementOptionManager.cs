using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;

namespace SAE_API.Models.DataManager
{
    public class EquipementOptionManager
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public EquipementOptionManager() { }

        public EquipementOptionManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<EquipementOption>>> GetAllAsync()
        {
            return await bmwDBContext.EquipementOptions.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<EquipementOption>> GetByIdAsync(int id)
        {
            return await bmwDBContext.EquipementOptions.FirstOrDefaultAsync(u => u.IdEquipementMoto == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<EquipementOption>> GetByStringAsync(string nom)
        {
            throw new Exception();
        }
        //ajoute une moto 
        public async Task AddAsync(EquipementOption entity)
        {
            await bmwDBContext.EquipementOptions.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(EquipementOption equipementOption, EquipementOption entity)
        {
            bmwDBContext.Entry(equipementOption).State = EntityState.Modified;

            equipementOption.IdEquipementMoto = entity.IdEquipementMoto;
           
           

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(EquipementOption equipementOption)
        {
            bmwDBContext.EquipementOptions.Remove(equipementOption);
            await bmwDBContext.SaveChangesAsync();
        }

    }
}
