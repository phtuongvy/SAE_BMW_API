using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class TailleEquipementManager : IDataRepository<TailleEquipement>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public TailleEquipementManager() { }

        public TailleEquipementManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<TailleEquipement>>> GetAllAsync()
        {
            return await bmwDBContext.TailleEquipements.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<TailleEquipement>> GetByIdAsync(int id)
        {
            return await bmwDBContext.TailleEquipements.FirstOrDefaultAsync(u => u.IdTailleEquipement == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<TailleEquipement>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.TailleEquipements.FirstOrDefaultAsync(u => u.NomTailleEquipement.ToUpper() == nom.ToUpper());
        }
        //ajoute une moto 
        public async Task AddAsync(TailleEquipement entity)
        {
            await bmwDBContext.TailleEquipements.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(TailleEquipement tailleEquipements, TailleEquipement entity)
        {
            bmwDBContext.Entry(tailleEquipements).State = EntityState.Modified;

            tailleEquipements.IdTailleEquipement = entity.IdTailleEquipement;
            tailleEquipements.NomTailleEquipement = entity.NomTailleEquipement;
            tailleEquipements.APourTailleTailleEquipement = entity.APourTailleTailleEquipement;
           

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(TailleEquipement tailleEquipements)
        {
            bmwDBContext.TailleEquipements.Remove(tailleEquipements);
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
