using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class CompteClientManager : IDataRepository<CompteClient>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public CompteClientManager() { }

        public CompteClientManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        public Task AddAsync(CompteClient entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(CompteClient entity)
        {
            bmwDBContext.CompteClients.Remove(entity);
            await bmwDBContext.SaveChangesAsync();
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<CompteClient>>> GetAllAsync()
        {
            return await bmwDBContext.CompteClients.ToListAsync();
        }

        //recherche par ID
        public async Task<ActionResult<CompteClient>> GetByIdAsync(Int32 id)
        {
            return await bmwDBContext.CompteClients.FirstOrDefaultAsync(u => u.IdCompteClient == id);
        }

        //recherche par nom 
        public async Task<ActionResult<CompteClient>> GetByStringAsync(String str)
        {
            return await bmwDBContext.CompteClients.FirstOrDefaultAsync(u => u.Email.ToUpper() == str.ToUpper());
        }

        //Mise à jour d'un équipement 
        public async Task UpdateAsync(CompteClient compteClient, CompteClient entity)
        {
            bmwDBContext.Entry(compteClient).State = EntityState.Modified;

            compteClient.IdCompteClient = entity.IdCompteClient;
            compteClient.NomClient = entity.NomClient;
            compteClient.PrenomClient = entity.PrenomClient;
            compteClient.CiviliteClient = entity.CiviliteClient;
            compteClient.NumeroClient = entity.NumeroClient;
            compteClient.Email = entity.Email;
            compteClient.DatenaissanceClient = entity.DatenaissanceClient;
            compteClient.Password = entity.Password;
            compteClient.ClientRole = entity.ClientRole;
            compteClient.EnregistrerCompteClient = entity.EnregistrerCompteClient;
            compteClient.EffectuerCompteClient = entity.EffectuerCompteClient;
            compteClient.AcquisComptes = entity.AcquisComptes;
            compteClient.FavorisCompteClient = entity.FavorisCompteClient;
            compteClient.TransationCompteClient = entity.TransationCompteClient;
            compteClient.AdresseCompteClient = entity.AdresseCompteClient;
            compteClient.CompteAdminCompteClient = entity.CompteAdminCompteClient;
            //compteClient.CompteClientPriveCompteClient = entity.CompteClientPriveCompteClient;
            compteClient.CompteClientProfessionnelCompteClient = entity.CompteClientProfessionnelCompteClient;
            compteClient.EstimerCompteClient = entity.EstimerCompteClient;
            compteClient.RepriseMotoCompteClient = entity.RepriseMotoCompteClient;

            await bmwDBContext.SaveChangesAsync();
        }
    }
}
