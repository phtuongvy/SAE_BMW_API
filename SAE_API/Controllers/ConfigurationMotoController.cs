using Microsoft.AspNetCore.Mvc;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationMotoController : ControllerBase
    {
        private readonly IDataRepository<ConfigurationMoto> configurationManager;
        public ConfigurationMotoController(IDataRepository<ConfigurationMoto> configMotoManager)
        {
            configurationManager = configMotoManager;
        }
        // GET: api/Commandes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfigurationMoto>>> GetConfigMotos()
        {
            return await configurationManager.GetAllAsync();
        }
        // GET: api/ConfigurationMoto/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                return NoContent();
            }
        }
        // POST: api/ConfigurationMoto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ConfigurationMoto>> PostConfigMoto(ConfigurationMoto configurationMoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await configurationManager.AddAsync(configurationMoto);
            return CreatedAtAction("GetById", new { id = configurationMoto.IdConfigurationMoto }, configurationMoto); // GetById : nom de l’action
        }
        // DELETE: api/ConfigurationMoto/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCommande(int id)
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
