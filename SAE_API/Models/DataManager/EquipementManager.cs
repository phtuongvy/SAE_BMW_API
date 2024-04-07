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


        public async Task<ActionResult<IEnumerable<Equipement>>> GetAllAsync()
        {
            return await bmwDBContext.Equipements.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Equipement>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Equipements.FirstOrDefaultAsync(u => u.IdEquipement == id);
        }

        public async Task<ActionResult<Equipement>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Equipement>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
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
        
        public async Task<ActionResult<Object>> GetByIdCustomAsync1(Int32 id)
        {
            var equipement = await bmwDBContext.Equipements
            .Where(m => m.IdEquipement == id)
            .Select(m => new
            {
                equipementid = m.IdEquipement,
                equipementnom = m.NomEquipement,
                equipementprix = m.PrixEquipement,
                equipementcollectionnom = m.CollectionEquipement.NomCollection,
                equipementsexe = m.Sexe,

                equipementcouleurs = m.PresenteEquipement.Select(v => new
                {
                    couleurid = v.CouleurEquipementPresente.IdCouleurEquipement,
                    couleurnom = v.CouleurEquipementPresente.NomCouleurEquipement,
                    test = v.PhotoPresente.LienPhoto,

                }).ToList(),

               

                equipementtailles = m.APourTailleEquipement.Select(c => new
                {
                    tailleid = c.TailleEquipementAPourTaille.IdTailleEquipement,
                    taillenom = c.TailleEquipementAPourTaille.NomTailleEquipement,
                }).ToList()
            })
            .FirstOrDefaultAsync();


            return new ActionResult<object>(equipement);
        }
        public async Task<ActionResult<IEnumerable<Object>>> GetAllAsync1()
        {
            var equipements = await bmwDBContext.Equipements
                .Select(m => new
                {
                    equipementid = m.IdEquipement,
                    equipementnom = m.NomEquipement,
                    equipementprix = m.PrixEquipement,
                    equipementcollection = m.CollectionEquipement.NomCollection, // Assurez-vous que la relation est correctement configurée
                    equipementsexe = m.Sexe,
                    equipementphotos = m.PresenteEquipement.Select(i => i.PhotoPresente.LienPhoto).FirstOrDefault(),
                })
                .ToListAsync();

            return equipements; // ActionResult<IEnumerable<object>> automatiquement inféré
        }

        Task IDataRepository<Equipement>.AddAsync(Equipement equipement)
        {
            
            bmwDBContext.Equipements.AddAsync(equipement);
            bmwDBContext.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Equipement equipement)
        {
            var entity = await bmwDBContext.Equipements.FindAsync(equipement.IdEquipement);
            if (entity != null)
            {
                bmwDBContext.Equipements.Remove(entity);
                await bmwDBContext.SaveChangesAsync();
            }

        }
    }
    
}
