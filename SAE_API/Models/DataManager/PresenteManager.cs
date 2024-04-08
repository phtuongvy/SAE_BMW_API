using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class PresenteManager : IDataRepository<Presente>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public PresenteManager() { }

        public PresenteManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Presente>>> GetAllAsync()
        {
            return await bmwDBContext.Presentes.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Presente>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Presentes.FirstOrDefaultAsync(u => u.IdEquipement == id);
        }
        public async Task<ActionResult<Presente>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Presente>> GetByIdAsync(int id , int? id2, int? id3)
        {
            return await bmwDBContext.Presentes.FirstOrDefaultAsync(u => u.IdPhoto == id && u.IdEquipement == id2 && u.IdCouleurEquipement == id3);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Presente>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Presente entity)
        {
            await bmwDBContext.Presentes.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Presente presente, Presente entity)
        {
            bmwDBContext.Entry(presente).State = EntityState.Modified;

            presente.IdPhoto = entity.IdPhoto;
            presente.IdCouleurEquipement = entity.IdCouleurEquipement;
            presente.IdEquipement = entity.IdEquipement;

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Presente presente)
        {
            bmwDBContext.Presentes.Remove(presente);
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

        public Task<ActionResult<IEnumerable<Presente>>> GetByIdAsyncList(int id)
        {
            throw new NotImplementedException();
        }
    }
}
