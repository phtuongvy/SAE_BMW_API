using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class DemanderContactManager : IDataRepository<DemanderContact>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public DemanderContactManager() { }

        public DemanderContactManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<DemanderContact>>> GetAllAsync()
        {
            return await bmwDBContext.DemanderContacts.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<DemanderContact>> GetByIdAsync(int id)
        {
            return await bmwDBContext.DemanderContacts.FirstOrDefaultAsync(u => u.IdReservationOffre == id);
        }

        public async Task<ActionResult<DemanderContact>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<DemanderContact>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<DemanderContact>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(DemanderContact entity)
        {
            await bmwDBContext.DemanderContacts.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(DemanderContact DemanderContact, DemanderContact entity)
        {
            bmwDBContext.Entry(DemanderContact).State = EntityState.Modified;

            DemanderContact.IdReservationOffre = entity.IdReservationOffre;
            DemanderContact.ObjetDeLaDemande = entity.ObjetDeLaDemande;
            DemanderContact.Objet = entity.Objet;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(DemanderContact DemanderContact)
        {
            bmwDBContext.DemanderContacts.Remove(DemanderContact);
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
