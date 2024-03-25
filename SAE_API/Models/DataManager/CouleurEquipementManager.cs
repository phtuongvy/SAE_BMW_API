using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;

namespace SAE_API.Models.DataManager
{
    public class CouleurEquipementManager
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public CouleurEquipementManager() { }

        public CouleurEquipementManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<CouleurEquipement>>> GetAllAsync()
        {
            return await bmwDBContext.CouleurEquipements.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<CouleurEquipement>> GetByIdAsync(int id)
        {
            return await bmwDBContext.CouleurEquipements.FirstOrDefaultAsync(u => u.IdCouleurEquipement == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<CouleurEquipement>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.CouleurEquipements.FirstOrDefaultAsync(u => u.NomCouleurEquipement.ToUpper() == nom.ToUpper());
        }
        //ajoute une moto 
        public async Task AddAsync(CouleurEquipement entity)
        {
            await bmwDBContext.CouleurEquipements.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(CouleurEquipement couleurEquipement, CouleurEquipement entity)
        {
            bmwDBContext.Entry(couleurEquipement).State = EntityState.Modified;

            couleurEquipement.IdCouleurEquipement = entity.IdCouleurEquipement;
            couleurEquipement.NomCouleurEquipement = entity.NomCouleurEquipement;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(CouleurEquipement couleurEquipement)
        {
            bmwDBContext.CouleurEquipements.Remove(couleurEquipement);
            await bmwDBContext.SaveChangesAsync();
        }

    }
}
