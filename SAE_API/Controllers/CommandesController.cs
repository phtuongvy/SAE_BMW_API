using Microsoft.AspNetCore.Mvc;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandesController : ControllerBase
    {
        private readonly IDataRepository<Commande> commandeManager;

        public CommandesController(IDataRepository<Commande> cartebancaireRepository)
        {
            this.commandeManager = cartebancaireRepository;
        }



        // GET: api/Commandes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommandes()
        {
            return await commandeManager.GetAllAsync();
        }

        // GET: api/Commandes/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Commande>> GetCommandeById(int id)
        {
            var commande = commandeManager.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (commande == null)
            {
                return NotFound();
            }
            return await commande;
        }
        
        // PUT: api/Commandes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCommande(int id, Commande commande)
        {
            if (id != commande.IdCommande)
            {
                return BadRequest();
            }
            var commandeToUpdate =  await commandeManager.GetByIdAsync(id);
            if (commandeToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await commandeManager.UpdateAsync(commandeToUpdate.Value, commande);
                return NoContent();
            }
        }
        // POST: api/Commandes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Commande>> PostCommande(Commande commande)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await commandeManager.AddAsync(commande);
            return CreatedAtAction("GetById", new { id = commande.IdCommande }, commande); // GetById : nom de l’action
        }
        // DELETE: api/Commandes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCommande(int id)
        {
            var commande = await commandeManager.GetByIdAsync(id);
            if (commande == null)
            {
                return NotFound();


            }

            await commandeManager.DeleteAsync(commande.Value);
            return NoContent();
        }
        
    }
}
