using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class AdresseManager : IDataRepository<Adresse>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public AdresseManager() { }

        public AdresseManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAllAsync()
        {
            return await bmwDBContext.Adresses.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Adresse>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Adresses.FirstOrDefaultAsync(u => u.IdAdresse == id);
        }

        public Task<ActionResult<Adresse>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Adresse>> GetByIdAsync(int id, int id2, int id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<Adresse>> GetByStringAsync(string rue)
        {
            return await bmwDBContext.Adresses.FirstOrDefaultAsync(u => u.RueClient.ToUpper() == rue.ToUpper());
        }
        //ajoute une moto 
        public async Task AddAsync(Adresse entity)
        {
            await bmwDBContext.Adresses.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Adresse adresse, Adresse entity)
        {
            bmwDBContext.Entry(adresse).State = EntityState.Modified;

            adresse.IdAdresse = entity.IdAdresse;
            adresse.IdCompteClient = entity.IdCompteClient;
            adresse.IdConcessionnaire = entity.IdConcessionnaire;
            adresse.Numero = entity.Numero;
            adresse.RueClient = entity.RueClient;
            adresse.CodePostal = entity.CodePostal;
            adresse.Ville = entity.Ville;
            adresse.Pays = entity.Pays;
            adresse.TypeAdresse = entity.TypeAdresse;



            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Adresse adresse)
        {
            bmwDBContext.Adresses.Remove(adresse);
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
