using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class CommandeManager : IDataRepository<Commande>
    {
        readonly BMWDBContext? bmwDbContext;
        public CommandeManager() { }
        public CommandeManager(BMWDBContext context)
        {
            bmwDbContext = context;
        }


        public async Task<ActionResult<IEnumerable<Commande>>> GetAllAsync()
        {
            return await bmwDbContext.Commandes.ToListAsync();
        }

        public async Task<ActionResult<Commande>> GetByIdAsync(int id)
        {
            return await bmwDbContext.Commandes.FirstOrDefaultAsync(c => c.IdCommande == id);
        }

        public async Task<ActionResult<Commande>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Commande entity)
        {
            await bmwDbContext.Commandes.AddAsync(entity);
            await bmwDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Commande commande, Commande entity)
        {
            bmwDbContext.Entry(commande).State = EntityState.Modified;
            commande.IdCommande = entity.IdCommande;
            commande.PrixFraisLivraison = entity.PrixFraisLivraison;
            commande.DateCommande = entity.DateCommande;
            commande.PrixTotal = entity.PrixTotal;
            commande.EffectuerCommande = entity.EffectuerCommande;
            commande.ProvenanceCommande = entity.ProvenanceCommande;
            await bmwDbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(Commande commande)
        {
            bmwDbContext.Commandes.Remove(commande);
            await bmwDbContext.SaveChangesAsync();

        }
    }
    
}
