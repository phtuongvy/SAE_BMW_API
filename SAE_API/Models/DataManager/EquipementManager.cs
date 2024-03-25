using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class EquipementManager : IDataRepository<Equipement>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public EquipementManager() { }

        public EquipementManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        public Task AddAsync(Equipement entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Equipement entity)
        {
            bmwDBContext.Equipements.Remove(entity);
            await bmwDBContext.SaveChangesAsync();
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Equipement>>> GetAllAsync()
        {
            return await bmwDBContext.Equipements.ToListAsync();
        }

        //recherche par ID
        public async Task<ActionResult<Equipement>> GetByIdAsync(Int32 id)
        {
            return await bmwDBContext.Equipements.FirstOrDefaultAsync(u => u.IdEquipement == id);
        }

        //recherche par nom 
        public async Task<ActionResult<Equipement>> GetByStringAsync(String str)
        {
            return await bmwDBContext.Equipements.FirstOrDefaultAsync(u => u.NomEquipement.ToUpper() == str.ToUpper());
        }

        //Mise à jour d'un équipement 
        public async Task UpdateAsync(Equipement equipement, Equipement entity)
        {
            bmwDBContext.Entry(equipement).State = EntityState.Modified;

            equipement.IdEquipement = entity.IdEquipement;
            equipement.IdSegment = entity.IdSegment;
            equipement.IdCollection = entity.IdCollection;
            equipement.IdTypeEquipement = entity.IdTypeEquipement;
            equipement.NomEquipement = entity.NomEquipement;
            equipement.DescriptionEquipement = entity.DescriptionEquipement;
            equipement.DetailEquipement = entity.DetailEquipement;
            equipement.DureeEquipement = entity.DureeEquipement;
            equipement.PrixEquipement = entity.PrixEquipement;
            equipement.Sexe = entity.Sexe;
            equipement.SegementEquipement = entity.SegementEquipement;
            equipement.CollectionEquipement = entity.CollectionEquipement;
            equipement.TypeEquipementEquipement = entity.TypeEquipementEquipement;
            equipement.APourTailleEquipement = entity.APourTailleEquipement;
            equipement.APourCouleurEquipement = entity.APourCouleurEquipement;
            equipement.CommanderEquipement = entity.CommanderEquipement;
            equipement.DetientEquipement = entity.DetientEquipement;
            equipement.DisposerEquipement = entity.DisposerEquipement;
            equipement.PresenteEquipement = entity.PresenteEquipement;

            await bmwDBContext.SaveChangesAsync();
        }
        
        
        //public async Task<ActionResult<Equipement>> GetByStringAsync(String str)
        //{
        //    return await bmwDBContext.Equipements.FirstOrDefaultAsync(u => u.NomEquipement.ToUpper() == str.ToUpper());
        //}
        public async Task<ActionResult<IEnumerable<Equipement>>> FiltreLesEquipemen(string nom ,string sexe , string taille , int couleur , int idsegament )
        {
            return await bmwDBContext.Equipements
                   .Include(e => e.APourTailleEquipement) // Inclure la relation de l'équipement avec APourTaille
                       .ThenInclude(apt => apt.TailleEquipementAPourTaille) // Inclure la relation avec TailleEquipement
                   .Include(e => e.SegementEquipement)
                   .Include(e => e.TypeEquipementEquipement)
                   .Include(e => e.CollectionEquipement)

                   .Where(e => e.NomEquipement.ToUpper() == nom.ToUpper())
                   .Where(e => e.Sexe == sexe)
                   .Where(e => e.APourTailleEquipement
                       .Any(apt => apt.TailleEquipementAPourTaille.NomTailleEquipement == taille))
                   .Where(e => e.SegementEquipement.IdSegement == idsegament )
                   .ToListAsync();
        }
    }

        
}
