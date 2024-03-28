using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class DonneesConstanteManager : IDataRepository<DonneesConstante>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public DonneesConstanteManager() { }

        public DonneesConstanteManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<DonneesConstante>>> GetAllAsync()
        {
            return await bmwDBContext.DonneesConstantes.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<DonneesConstante>> GetByIdAsync(int id)
        {
            return await bmwDBContext.DonneesConstantes.FirstOrDefaultAsync(u => u.IdDonnesConstante == id);
        }

        public async Task<ActionResult<DonneesConstante>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<DonneesConstante>> GetByIdAsync(int id, int id2, int id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<DonneesConstante>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(DonneesConstante entity)
        {
            await bmwDBContext.DonneesConstantes.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(DonneesConstante DonneesConstante, DonneesConstante entity)
        {
            bmwDBContext.Entry(DonneesConstante).State = EntityState.Modified;

            DonneesConstante.IdDonnesConstante = entity.IdDonnesConstante;
            DonneesConstante.FraisLivraisonCommande = entity.FraisLivraisonCommande;
            DonneesConstante.TVANormal = entity.TVANormal;
            DonneesConstante.TVAIntermediaire = entity.TVAIntermediaire;
            DonneesConstante.TVAReduit = entity.TVAReduit;
            DonneesConstante.TVAParticulier = entity.TVAParticulier;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(DonneesConstante DonneesConstante)
        {
            bmwDBContext.DonneesConstantes.Remove(DonneesConstante);
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
