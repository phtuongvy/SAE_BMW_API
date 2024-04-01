using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class EquipementAccessoireManager : IDataRepository<EquipementAccessoire>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public EquipementAccessoireManager() { }

        public EquipementAccessoireManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<EquipementAccessoire>>> GetAllAsync()
        {
            return await bmwDBContext.EquipementAccessoires.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<EquipementAccessoire>> GetByIdAsync(int id)
        {
            return await bmwDBContext.EquipementAccessoires.FirstOrDefaultAsync(u => u.IdEquipementMoto == id);
        }

        public async Task<ActionResult<EquipementAccessoire>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<EquipementAccessoire>> GetByIdAsync(int id, int id2, int id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<EquipementAccessoire>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(EquipementAccessoire entity)
        {
            await bmwDBContext.EquipementAccessoires.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(EquipementAccessoire equipementAccessoire, EquipementAccessoire entity)
        {
            bmwDBContext.Entry(equipementAccessoire).State = EntityState.Modified;

            equipementAccessoire.IdEquipementMoto = entity.IdEquipementMoto;
           

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(EquipementAccessoire equipementAccessoire)
        {
            bmwDBContext.EquipementAccessoires.Remove(equipementAccessoire);
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
