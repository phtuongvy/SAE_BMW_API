using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class MotoDisponibleManager : IDataRepository<MotoDisponible>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public MotoDisponibleManager() { }

        public MotoDisponibleManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<MotoDisponible>>> GetAllAsync()
        {
            return await bmwDBContext.MotoDisponibles.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<MotoDisponible>> GetByIdAsync(int id)
        {
            return await bmwDBContext.MotoDisponibles.FirstOrDefaultAsync(u => u.IdMoto == id);
        }

        public async Task<ActionResult<MotoDisponible>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
            public async Task<ActionResult<MotoDisponible>> GetByIdAsync(int id, int? id2, int? id3)
            {
            throw new NotImplementedException();
        }

        //recherche par nom de moto
        public async Task<ActionResult<MotoDisponible>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(MotoDisponible entity)
        {
            await bmwDBContext.MotoDisponibles.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(MotoDisponible MotoDisponible, MotoDisponible entity)
        {
            bmwDBContext.Entry(MotoDisponible).State = EntityState.Modified;

            MotoDisponible.IdMoto = entity.IdMoto;
            


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(MotoDisponible MotoDisponible)
        {
            bmwDBContext.MotoDisponibles.Remove(MotoDisponible);
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
