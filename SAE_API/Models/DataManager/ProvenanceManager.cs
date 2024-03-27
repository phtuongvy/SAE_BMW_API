using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class ProvenanceManager : IDataRepository<Provenance>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public ProvenanceManager() { }

        public ProvenanceManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Provenance>>> GetAllAsync()
        {
            return await bmwDBContext.Provenances.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Provenance>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Provenances.FirstOrDefaultAsync(u => u.IdConcessionnaire == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Provenance>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Provenance entity)
        {
            await bmwDBContext.Provenances.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Provenance provenance, Provenance entity)
        {
            bmwDBContext.Entry(provenance).State = EntityState.Modified;

            provenance.IdCommande = entity.IdCommande;
            provenance.IdConcessionnaire = entity.IdConcessionnaire;
           

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Provenance provenance)
        {
            bmwDBContext.Provenances.Remove(provenance);
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
