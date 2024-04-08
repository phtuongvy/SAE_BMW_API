using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class RepriseMotoManager : IDataRepository<RepriseMoto>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public RepriseMotoManager() { }

        public RepriseMotoManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<RepriseMoto>>> GetAllAsync()
        {
            return await bmwDBContext.RepriseMotos.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<RepriseMoto>> GetByIdAsync(int id)
        {
            return await bmwDBContext.RepriseMotos.FirstOrDefaultAsync(u => u.IdEstimationMoto == id);
        }
        public async Task<ActionResult<RepriseMoto>> GetByIdAsync(int id , int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<RepriseMoto>> GetByIdAsync(int id, int? id2, int? id3)
        {
            return await bmwDBContext.RepriseMotos.FirstOrDefaultAsync(u => u.IdCompteClient == id && u.IdCompteClient == id2 && u.IdCompteClient == id3);
        }
        //recherche par nom de moto
        public async Task<ActionResult<RepriseMoto>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(RepriseMoto entity)
        {
            await bmwDBContext.RepriseMotos.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(RepriseMoto RepriseMoto, RepriseMoto entity)
        {
            bmwDBContext.Entry(RepriseMoto).State = EntityState.Modified;

            RepriseMoto.IdDateLivraison = entity.IdDateLivraison;
            RepriseMoto.IdCompteClient = entity.IdCompteClient;
            RepriseMoto.IdEstimationMoto = entity.IdEstimationMoto;
            RepriseMoto.MarqueEstimationMoto = entity.MarqueEstimationMoto;
            RepriseMoto.ModeleEstimationMoto = entity.ModeleEstimationMoto;
            RepriseMoto.MoisImmatriculation = entity.MoisImmatriculation;
            RepriseMoto.AnneImmatriculation = entity.AnneImmatriculation;
            RepriseMoto.PrixEstimationMoto = entity.PrixEstimationMoto;
            RepriseMoto.KilometrageEstimationMoto = entity.KilometrageEstimationMoto;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(RepriseMoto RepriseMoto)
        {
            bmwDBContext.RepriseMotos.Remove(RepriseMoto);
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

        public Task<ActionResult<IEnumerable<RepriseMoto>>> GetByIdAsyncList(int id)
        {
            throw new NotImplementedException();
        }
    }
}
