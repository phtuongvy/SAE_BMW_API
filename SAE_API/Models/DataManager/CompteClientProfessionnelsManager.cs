using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class CompteClientProfessionnelManager : IDataRepository<CompteClientProfessionnel>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public CompteClientProfessionnelManager() { }

        public CompteClientProfessionnelManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<CompteClientProfessionnel>>> GetAllAsync()
        {
            return await bmwDBContext.CompteClientProfessionnels.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<CompteClientProfessionnel>> GetByIdAsync(int id)
        {
            return await bmwDBContext.CompteClientProfessionnels.FirstOrDefaultAsync(u => u.IdCompteClient == id);
        }

        public async Task<ActionResult<CompteClientProfessionnel>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<CompteClientProfessionnel>> GetByIdAsync(int id, int id2, int id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<CompteClientProfessionnel>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(CompteClientProfessionnel entity)
        {
            await bmwDBContext.CompteClientProfessionnels.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(CompteClientProfessionnel CompteClientProfessionnel, CompteClientProfessionnel entity)
        {
            bmwDBContext.Entry(CompteClientProfessionnel).State = EntityState.Modified;

            CompteClientProfessionnel.IdCompteClient = entity.IdCompteClient;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(CompteClientProfessionnel CompteClientProfessionnel)
        {
            bmwDBContext.CompteClientProfessionnels.Remove(CompteClientProfessionnel);
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
