using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class GammeMotoManager : IDataRepository<GammeMoto>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public GammeMotoManager() { }

        public GammeMotoManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<GammeMoto>>> GetAllAsync()
        {
            return await bmwDBContext.GammeMotos.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<GammeMoto>> GetByIdAsync(int id)
        {
            return await bmwDBContext.GammeMotos.FirstOrDefaultAsync(u => u.IdGammeMoto == id);
        }

        public async Task<ActionResult<GammeMoto>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<GammeMoto>> GetByIdAsync(int id, int id2, int id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<GammeMoto>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.GammeMotos.FirstOrDefaultAsync(u => u.NomGammeMoto.ToUpper() == nom.ToUpper());
        }
        //ajoute une moto 
        public async Task AddAsync(GammeMoto entity)
        {
            await bmwDBContext.GammeMotos.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(GammeMoto gammeMoto, GammeMoto entity)
        {
            bmwDBContext.Entry(gammeMoto).State = EntityState.Modified;

            gammeMoto.IdGammeMoto = entity.IdGammeMoto;
            gammeMoto.NomGammeMoto = entity.NomGammeMoto;
            

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(GammeMoto gammeMoto)
        {
            bmwDBContext.GammeMotos.Remove(gammeMoto);
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
