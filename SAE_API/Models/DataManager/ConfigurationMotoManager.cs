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
            return await bmwDbContext.ConfigurationMotos.FirstOrDefaultAsync(c => c.IdConfigurationMoto == id);
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
    }

}
