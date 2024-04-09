using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class ConfigurationMotoManager : IDataRepository<ConfigurationMoto>
    {
        readonly BMWDBContext? bmwDbContext;
        public ConfigurationMotoManager()
        {
            
        }
        public ConfigurationMotoManager(BMWDBContext context)
        {
            bmwDbContext = context;
        }


        public async Task<ActionResult<IEnumerable<ConfigurationMoto>>> GetAllAsync()
        {
            return await bmwDbContext.ConfigurationMotos.ToListAsync();
        }

        public async Task<ActionResult<ConfigurationMoto>> GetByIdAsync(int id)
        {
            return await bmwDbContext.ConfigurationMotos.FirstOrDefaultAsync(u => u.IdConfigurationMoto == id);
        }
        public async Task<ActionResult<ConfigurationMoto>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<ConfigurationMoto>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<ConfigurationMoto>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(ConfigurationMoto entity)
        {
            await bmwDbContext.ConfigurationMotos.AddAsync(entity);
            await bmwDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ConfigurationMoto configurationMoto, ConfigurationMoto entity)
        {
            bmwDbContext.Entry(configurationMoto).State = EntityState.Modified;
            configurationMoto.IdConfigurationMoto = entity.IdConfigurationMoto;
            configurationMoto.IdReservationOffre = entity.IdReservationOffre;
            configurationMoto.IdMoto = entity.IdMoto;
            configurationMoto.IdColoris = entity.IdColoris;
            configurationMoto.PrixTotalConfiguration = entity.PrixTotalConfiguration;
            configurationMoto.DateConfiguration = entity.DateConfiguration;
            configurationMoto.MotoConfigurationMoto = entity.MotoConfigurationMoto;
            configurationMoto.ColorisConfigurationMoto = entity.ColorisConfigurationMoto;
            configurationMoto.EnregistrerConfigurationMoto = entity.EnregistrerConfigurationMoto;
            configurationMoto.AChoisiConfigurationMoto = entity.AChoisiConfigurationMoto;
            configurationMoto.AChoisiOptionsConfigurationMoto = entity.AChoisiOptionsConfigurationMoto;
            configurationMoto.CommanderConfigurationMoto = entity.CommanderConfigurationMoto;
            await bmwDbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(ConfigurationMoto configurationMoto)
        {
            bmwDbContext.ConfigurationMotos.Remove(configurationMoto);
            await bmwDbContext.SaveChangesAsync();

        }
        public async Task<ActionResult<Object>> GetByIdCustomAsync1(Int32 id)
        {
            var moto = await bmwDbContext.ConfigurationMotos
            .Where(m => m.IdConfigurationMoto == id)
            .Select(m => new
            {
                configmotoid = m.IdConfigurationMoto,
                configmotonom = m.MotoConfigurationMoto.NomMoto,
                configmotodescription = m.MotoConfigurationMoto.DescriptionMoto,
                configmotoidmoto = m.IdMoto,
                configmotocolorisnom = m.ColorisConfigurationMoto.NomColoris,
                configmotoprix = m.PrixTotalConfiguration,

                configmotopacks = m.AChoisiConfigurationMoto.Select(p => new
                {
                    packid = p.PackChoisi.PackId,
                    packnom = p.PackChoisi.NomPack,
                    
                }).ToList(),

                configmotooptions = m.AChoisiOptionsConfigurationMoto.Select(c => new
                {
                    optionid =  c.EquipementMotoChoisiOption.IdEquipementMoto,
                    optionnom = c.EquipementMotoChoisiOption.NomEquipement,
                }).ToList(),

               
               
            })
            .FirstOrDefaultAsync();


            return new ActionResult<object>(moto);
        }
        public Task<ActionResult<IEnumerable<Object>>> GetAllAsync1()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<ConfigurationMoto>>> GetByIdAsyncList(int id)
        {
            throw new NotImplementedException();
        }
    }

}
