using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class DetientManager : IDataRepository<Detient>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public DetientManager() { }

        public DetientManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Detient>>> GetAllAsync()
        {
            return await bmwDBContext.Detients.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Detient>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Detient>> GetByIdAsync(int id, int id2)
        {
            return await bmwDBContext.Detients.FirstOrDefaultAsync(u => u.IdEquipement == id && u.IdConcessionnaire == id2 );
        }
        public async Task<ActionResult<Detient>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<Detient>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Detient entity)
        {
            await bmwDBContext.Detients.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Detient Detient, Detient entity)
        {
            bmwDBContext.Entry(Detient).State = EntityState.Modified;

            Detient.IdConcessionnaire = entity.IdConcessionnaire;
            Detient.IdEquipement = entity.IdEquipement;
            Detient.EquipementDetient = entity.EquipementDetient;
            Detient.ConcessionnaireDetient = entity.ConcessionnaireDetient;
           

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Detient Detient)
        {
            bmwDBContext.Detients.Remove(Detient);
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
