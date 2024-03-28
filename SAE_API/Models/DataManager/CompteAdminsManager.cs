using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class CompteAdminManager : IDataRepository<CompteAdmin>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public CompteAdminManager() { }

        public CompteAdminManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<CompteAdmin>>> GetAllAsync()
        {
            return await bmwDBContext.CompteAdmins.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<CompteAdmin>> GetByIdAsync(int id)
        {
            return await bmwDBContext.CompteAdmins.FirstOrDefaultAsync(u => u.IdCompteClient == id);
        }

        public async Task<ActionResult<CompteAdmin>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<CompteAdmin>> GetByIdAsync(int id, int id2, int id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<CompteAdmin>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(CompteAdmin entity)
        {
            await bmwDBContext.CompteAdmins.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(CompteAdmin CompteAdmin, CompteAdmin entity)
        {
            bmwDBContext.Entry(CompteAdmin).State = EntityState.Modified;

            CompteAdmin.IdCompteClient = entity.IdCompteClient;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(CompteAdmin CompteAdmin)
        {
            bmwDBContext.CompteAdmins.Remove(CompteAdmin);
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
