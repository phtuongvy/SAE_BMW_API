﻿using System;
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
    public class EnregistrerController : ControllerBase
    {
        private readonly IDataRepository<Enregistrer> _Enregistrer;

        public EnregistrerController(IDataRepository<Enregistrer> EnregistrerRepository)
        {
            this._Enregistrer = EnregistrerRepository;
        }

        [HttpGet]
        [ActionName("GetEnregistrers")]
        public async Task<ActionResult<IEnumerable<Enregistrer>>> GetEnregistrers()
        {
            return await _Enregistrer.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetEnregistrerById")]
        public async Task<ActionResult<Enregistrer>> GetEnregistrerById(int id)
        {

            var Enregistrer = await _Enregistrer.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (Enregistrer == null)
            {
                return NotFound();
            }
            return Enregistrer;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutEnregistrer")]
        public async Task<IActionResult> PutEnregistrer(int id, Enregistrer Enregistrer)
        {
            if (id != Enregistrer.IdCompteClient)
            {
                return BadRequest();
            }
            var userToUpdate = await _Enregistrer.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _Enregistrer.UpdateAsync(userToUpdate.Value, Enregistrer);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostEnregistrer")]
        public async Task<ActionResult<Enregistrer>> PostEnregistrer(Enregistrer Enregistrer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _Enregistrer.AddAsync(Enregistrer);
            return CreatedAtAction("GetEnregistrerById", new { id = Enregistrer.IdConfigurationMoto }, Enregistrer); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteEnregistrer")]
        public async Task<IActionResult> DeleteEnregistrer(int id)
        {
            var Enregistrer = await _Enregistrer.GetByIdAsync(id);
            if (Enregistrer == null)
            {
                return NotFound();
            }
            await _Enregistrer.DeleteAsync(Enregistrer.Value);
            return NoContent();
        }

    }
}
