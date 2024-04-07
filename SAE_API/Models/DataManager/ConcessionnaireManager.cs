using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class ConcessionnaireManager : IDataRepository<Concessionnaire> 
    {

        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public ConcessionnaireManager() { }

        public ConcessionnaireManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Concessionnaire>>> GetAllAsync()
        {
            return await bmwDBContext.Concessionnaires.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Concessionnaire>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Concessionnaires.FirstOrDefaultAsync(u => u.IdConcessionnaire == id);
        }

        public async Task<ActionResult<Concessionnaire>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Concessionnaire>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<Concessionnaire>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.Concessionnaires.FirstOrDefaultAsync(u => u.NomConcessionnaire.ToUpper() == nom.ToUpper());
        }
        //ajoute une moto 
        public async Task AddAsync(Concessionnaire entity)
        {
            await bmwDBContext.Concessionnaires.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Concessionnaire concessionnaires, Concessionnaire entity)
        {
            bmwDBContext.Entry(concessionnaires).State = EntityState.Modified;

            concessionnaires.IdConcessionnaire = entity.IdConcessionnaire;
            concessionnaires.IdAdresse = entity.IdAdresse;
            concessionnaires.IdStock = entity.IdStock;
            concessionnaires.NomConcessionnaire = entity.NomConcessionnaire;
            
            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Concessionnaire concessionnaires)
        {
            bmwDBContext.Concessionnaires.Remove(concessionnaires);
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
