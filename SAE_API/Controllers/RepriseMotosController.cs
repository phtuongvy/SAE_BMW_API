using Microsoft.AspNetCore.Mvc;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RepriseMotosController : ControllerBase
    {
        private readonly IDataRepository<RepriseMoto> repriseMotoManager;

        public RepriseMotosController(IDataRepository<RepriseMoto> repriseMotoRepository)
        {
            this.repriseMotoManager = repriseMotoRepository;
        }



        // GET: api/RepriseMotos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepriseMoto>>> GetRepriseMotos()
        {
            return await repriseMotoManager.GetAllAsync();
        }

        // GET: api/RepriseMotos/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RepriseMoto>> GetRepriseMotoById(int id)
        {
            var repriseMoto = repriseMotoManager.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (repriseMoto == null)
            {
                return NotFound();
            }
            return await repriseMoto;
        }

        // PUT: api/RepriseMotos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRepriseMoto(int id, RepriseMoto repriseMoto)
        {
            if (id != repriseMoto.IdCompteClient)
            {
                return BadRequest();
            }
            var repriseMotoToUpdate = await repriseMotoManager.GetByIdAsync(id);
            if (repriseMotoToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await repriseMotoManager.UpdateAsync(repriseMotoToUpdate.Value, repriseMoto);
                return NoContent();
            }
        }
        // POST: api/RepriseMotos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RepriseMoto>> PostRepriseMoto(RepriseMoto repriseMoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await repriseMotoManager.AddAsync(repriseMoto);
            return CreatedAtAction("GetById", new { id = repriseMoto.IdCompteClient }, repriseMoto); // GetById : nom de l’action
        }
        // DELETE: api/RepriseMotos/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRepriseMoto(int id)
        {
            var repriseMoto = await repriseMotoManager.GetByIdAsync(id);
            if (repriseMoto == null)
            {
                return NotFound();


            }

            await repriseMotoManager.DeleteAsync(repriseMoto.Value);
            return NoContent();
        }

    }
}
