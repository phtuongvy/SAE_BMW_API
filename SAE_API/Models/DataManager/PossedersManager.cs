using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class PossederManager : IDataRepository<Posseder>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public PossederManager() { }

        public PossederManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Posseder>>> GetAllAsync()
        {
            return await bmwDBContext.Posseders.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Posseder>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Posseders.FirstOrDefaultAsync(u => u.IdMoto == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Posseder>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Posseder entity)
        {
            await bmwDBContext.Posseders.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Posseder Posseder, Posseder entity)
        {
            bmwDBContext.Entry(Posseder).State = EntityState.Modified;

            Posseder.IdMoto = entity.IdMoto;
            Posseder.IdEquipementMoto = entity.IdEquipementMoto;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Posseder Posseder)
        {
            bmwDBContext.Posseders.Remove(Posseder);
            await bmwDBContext.SaveChangesAsync();
        }

    }
}
