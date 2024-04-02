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
        public async Task<ActionResult<CompteClient>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        //recherche par nom 
        public async Task<ActionResult<CompteClient>> GetByStringAsync(String str)
        {
            return await bmwDBContext.CompteClients.FirstOrDefaultAsync(u => u.Email.ToUpper() == str.ToUpper());
        }
        public async Task<ActionResult<CompteClient>> GetByIdAsync(int id, int id2, int id3)
        {
            throw new NotImplementedException();
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
        public async Task<ActionResult<Object>> GetByIdCustomAsync1(Int32 id)
        {
            var user = await bmwDBContext.CompteClients
            .Where(u => u.IdCompteClient == id)
            .Select(u => new
            {
                idcompteclient = u.IdCompteClient,
                nomclient = u.NomClient,
                prenomClient = u.PrenomClient,
                civiliteClient = u.CiviliteClient,
                numeroClient = u.NumeroClient,
                email  = u.Email,
                datenaissanceClient = u.DatenaissanceClient,
                password = u.Password,
                clientRole = u.ClientRole,

                idadresse = u.AdresseCompteClient.IdAdresse,
                numero = u.AdresseCompteClient.Numero,
                rueClient = u.AdresseCompteClient.RueClient,
                codePostal = u.AdresseCompteClient.CodePostal,
                ville = u.AdresseCompteClient.Ville,
                pays = u.AdresseCompteClient.Pays,
                typeAdresse = u.AdresseCompteClient.TypeAdresse,

                configmoto = u.EnregistrerCompteClient.Select(e => new
                {
                    e.IdConfigurationMoto,
                    e.NomConfiguration,
                    e.ConfigurationMotoEnregistrer.PrixTotalConfiguration,
                    e.ConfigurationMotoEnregistrer.DateConfiguration,
                }).ToList(),

                moto = u .EnregistrerCompteClient.Select(e=> new
                {
                    e.ConfigurationMotoEnregistrer.MotoConfigurationMoto.MotoId,
                    e.ConfigurationMotoEnregistrer.MotoConfigurationMoto.NomMoto,
                    e.ConfigurationMotoEnregistrer.MotoConfigurationMoto.DescriptionMoto,
                    e.ConfigurationMotoEnregistrer.MotoConfigurationMoto.GammeMotoMoto,
                }),

                pack = u.EnregistrerCompteClient.Select(e => new
                {
                    equipementoption = e.ConfigurationMotoEnregistrer.AChoisiConfigurationMoto.Select(m => new
                    {
                        m.PackChoisi.PackId,
                        m.PackChoisi.NomPack,
                        m.PackChoisi.DescriptionPack,
                        m.PackChoisi.PrixPack,
                    }),
                }).ToList(),

                option = u.EnregistrerCompteClient.Select(e => new
                {
                    equipementoption = e.ConfigurationMotoEnregistrer.AChoisiOptionsConfigurationMoto.Select(m => new
                    {
                        m.EquipementMotoChoisiOption.IdEquipementMoto,
                        m.EquipementMotoChoisiOption.NomEquipement,
                        m.EquipementMotoChoisiOption.PrixEquipementMoto,
                        m.EquipementMotoChoisiOption.DescriptionEquipementMoto,
                        m.EquipementMotoChoisiOption.EquipementOrigine,
                        m.EquipementMotoChoisiOption.PhotoEquipementMotoOption.LienPhoto,
                    
                    }),
                }).ToList(),

                colorie = u.EnregistrerCompteClient.Select(e => new
                {
                    e.ConfigurationMotoEnregistrer.ColorisConfigurationMoto.IdPhoto,
                    e.ConfigurationMotoEnregistrer.ColorisConfigurationMoto.NomColoris,
                    e.ConfigurationMotoEnregistrer.ColorisConfigurationMoto.PrixColoris,
                    e.ConfigurationMotoEnregistrer.ColorisConfigurationMoto.TypeColoris,
                    e.ConfigurationMotoEnregistrer.ColorisConfigurationMoto.PhotoColoris.LienPhoto,
                }).ToList(),
   
            })
            .FirstOrDefaultAsync();


            return new ActionResult<object>(user);
        }
        public Task<ActionResult<IEnumerable<Object>>> GetAllAsync1()
        {
            throw new NotImplementedException();
        }
    }
}
