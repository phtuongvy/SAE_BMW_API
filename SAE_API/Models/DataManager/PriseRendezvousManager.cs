using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class PriseRendezvousManager : IDataRepository<PriseRendezvous>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public PriseRendezvousManager() { }

        public PriseRendezvousManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<PriseRendezvous>>> GetAllAsync()
        {
            return await bmwDBContext.PriseRendezvouss.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<PriseRendezvous>> GetByIdAsync(int id)
        {
            return await bmwDBContext.PriseRendezvouss.FirstOrDefaultAsync(u => u.IdConcessionnaire == id);
        }
        public async Task<ActionResult<PriseRendezvous>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<PriseRendezvous>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<PriseRendezvous>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(PriseRendezvous entity)
        {
            await bmwDBContext.PriseRendezvouss.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(PriseRendezvous cartebancaire, PriseRendezvous entity)
        {
            bmwDBContext.Entry(cartebancaire).State = EntityState.Modified;

            cartebancaire.IdConcessionnaire = entity.IdConcessionnaire;
            cartebancaire.IdReservationOffre = entity.IdReservationOffre;
            cartebancaire.NomReservation = entity.NomReservation;
            cartebancaire.PrenomReservation = entity.PrenomReservation;
            cartebancaire.CiviliteReservation = entity.CiviliteReservation;
            cartebancaire.EmailReservation = entity.EmailReservation;
            cartebancaire.TelephoneReservation = entity.TelephoneReservation;
            cartebancaire.VilleReservation = entity.VilleReservation;
            cartebancaire.TypeDePermis = entity.TypeDePermis;

            

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(PriseRendezvous cartebancaire)
        {
            bmwDBContext.PriseRendezvouss.Remove(cartebancaire);
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

        public Task<ActionResult<IEnumerable<PriseRendezvous>>> GetByIdAsyncList(int id)
        {
            throw new NotImplementedException();
        }
    }
}
