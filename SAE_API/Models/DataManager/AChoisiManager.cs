using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class AChoisiManager : IDataRepository<AChoisi>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public AChoisiManager() { }

        public AChoisiManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<AChoisi>>> GetAllAsync()
        {
            return await bmwDBContext.Achoisis.ToListAsync();
        }
        //recherche par ID 
        public async Task<ActionResult<AChoisi>> GetByIdAsync(int id)
        {
             throw new NotImplementedException();
        }
        //recherche par ID de Pack et de ID de 
        public async Task<ActionResult<AChoisi>> GetByIdAsync(int id, int id2)
        {
            return await bmwDBContext.Achoisis.FirstOrDefaultAsync(u => u.IDPack == id && u.IDConfigurationMoto == id2);
        }
        public async Task<ActionResult<AChoisi>> GetByIdAsync(int id, int id2,int id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<AChoisi>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(AChoisi entity)
        {
            await bmwDBContext.Achoisis.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(AChoisi AChoisi, AChoisi entity)
        {
            bmwDBContext.Entry(AChoisi).State = EntityState.Modified;

            AChoisi.IDConfigurationMoto = entity.IDConfigurationMoto;
            AChoisi.IDPack = entity.IDPack;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(AChoisi AChoisi)
        {
            bmwDBContext.Achoisis.Remove(AChoisi);
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
