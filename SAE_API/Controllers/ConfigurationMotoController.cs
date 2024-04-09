using Microsoft.AspNetCore.Mvc;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigurationMotoController : ControllerBase
    {
        private readonly IDataRepository<ConfigurationMoto> configurationManager;

        public ConfigurationMotoController(IDataRepository<ConfigurationMoto> configMotoManager)
        {
            configurationManager = configMotoManager;
        }


        [HttpGet]
        [ActionName("GetConfigMotos")]
        public async Task<ActionResult<IEnumerable<ConfigurationMoto>>> GetConfigMotos()
        {
            return await configurationManager.GetAllAsync();
        }

        // GET: api/ConfigurationMoto/5
        [HttpGet("{id}")]
        [ActionName("GetConfigMotoByIdCustom")]
        public async Task<ActionResult<object>> GetConfigMotoByIdCustom(int id)
        {
            var configurationMoto = configurationManager.GetByIdCustomAsync1(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (configurationMoto == null)
            {
                return NotFound();
            }
            return await configurationMoto;
        }

        // GET: api/ConfigurationMoto/5
        [HttpGet("{id}")]
        [ActionName("GetConfigMotoById")]
        public async Task<ActionResult<ConfigurationMoto>> GetConfigMotoById(int id)
        {
            var configurationMoto = configurationManager.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (configurationMoto == null)
            {
                return NotFound();
            }
            return await configurationMoto;
        }

        // PUT: api/ConfigurationMoto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutConfigMoto")]
        public async Task<IActionResult> PutConfigMoto(int id, ConfigurationMoto configurationMoto)
        {
            if (id != configurationMoto.IdConfigurationMoto)
            {
                return BadRequest();
            }
            var configToUpdate = await configurationManager.GetByIdAsync(id);
            if (configToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await configurationManager.UpdateAsync(configToUpdate.Value, configurationMoto);
                return CreatedAtAction("GetConcessionnaireById", new { id = configurationMoto.IdConfigurationMoto }, configurationMoto); // GetById : nom de l’action
            }
        }


        [HttpPost]
        [ActionName("PostConfigMoto")]
        public async Task<ActionResult<ConfigurationMoto>>PostConfigMoto(ConfigurationMoto configurationMoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await configurationManager.AddAsync(configurationMoto);
            return Ok(configurationMoto); // Retourne 200 avec l'objet
        }

        // DELETE: api/ConfigurationMoto/5
        [HttpDelete("{id}")]
        [ActionName("DeleteConfigMoto")]
        public async Task<IActionResult> DeleteConfigMoto(int id)
        {
            var configurationMoto = await configurationManager.GetByIdAsync(id);
            if (configurationMoto == null)
            {
                return NotFound();


            }

            await configurationManager.DeleteAsync(configurationMoto.Value);
            return NoContent();
        }

    }
}
