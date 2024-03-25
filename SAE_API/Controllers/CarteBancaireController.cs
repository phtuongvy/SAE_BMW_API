using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarteBancaireController : ControllerBase
    {
        private readonly IDataRepository<CarteBancaire> _cartebancaire;

        public CarteBancaireController(IDataRepository<CarteBancaire> cartebancaireRepository)
        {
            this._cartebancaire = cartebancaireRepository;
        }

        [HttpGet]
        [ActionName("GetCarteBancaires")]
        public async Task<ActionResult<IEnumerable<CarteBancaire>>> GetCarteBancaires()
        {
            return await _cartebancaire.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetCarteBancaireById")]
        public async Task<ActionResult<CarteBancaire>> GetCarteBancaireById(int id)
        {

            var cartebancaire = await _cartebancaire.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (cartebancaire == null)
            {
                return NotFound();
            }
            return cartebancaire;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutCarteBancaire")]
        public async Task<IActionResult> PutCarteBancaire(int id, CarteBancaire cartebancaire)
        {
            if (id != cartebancaire.IdCb)
            {
                return BadRequest();
            }
            var userToUpdate = await _cartebancaire.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _cartebancaire.UpdateAsync(userToUpdate.Value, cartebancaire);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCarteBancaire")]
        public async Task<ActionResult<CarteBancaire>> PostCarteBancaire(CarteBancaire cartebancaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _cartebancaire.AddAsync(cartebancaire);
            return CreatedAtAction("GetCarteBancaireById", new { id = cartebancaire.IdCb }, cartebancaire); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCarteBancaire")]
        public async Task<IActionResult> DeleteCarteBancaire(int id)
        {
            var cartebancaire = await _cartebancaire.GetByIdAsync(id);
            if (cartebancaire == null)
            {
                return NotFound();
            }
            await _cartebancaire.DeleteAsync(cartebancaire.Value);
            return NoContent();
        }

    }
}
