using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class MoyenDePaiementManager : IDataRepository<MoyenDePaiement>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public MoyenDePaiementManager() { }

        public MoyenDePaiementManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<MoyenDePaiement>>> GetAllAsync()
        {
            return await bmwDBContext.MoyensDePaiement.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<MoyenDePaiement>> GetByIdAsync(int id)
        {
            return await bmwDBContext.MoyensDePaiement.FirstOrDefaultAsync(u => u.IdMoyenDePaiement == id);
        }

        public async Task<ActionResult<MoyenDePaiement>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<MoyenDePaiement>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<MoyenDePaiement>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.MoyensDePaiement.FirstOrDefaultAsync(u => u.LibelleMoyenDePaiement.ToUpper() == nom.ToUpper());
        }
        //ajoute une moto 
        public async Task AddAsync(MoyenDePaiement entity)
        {
            await bmwDBContext.MoyensDePaiement.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(MoyenDePaiement moyensDePaiement, MoyenDePaiement entity)
        {
            bmwDBContext.Entry(moyensDePaiement).State = EntityState.Modified;

            moyensDePaiement.IdMoyenDePaiement = entity.IdMoyenDePaiement;
            moyensDePaiement.LibelleMoyenDePaiement = entity.LibelleMoyenDePaiement;
            

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(MoyenDePaiement moyensDePaiement)
        {
            bmwDBContext.MoyensDePaiement.Remove(moyensDePaiement);
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
