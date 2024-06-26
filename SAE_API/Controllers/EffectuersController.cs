﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class EffectuersController : ControllerBase
    {
        private readonly IDataRepository<Effectuer> _effectuers;

        public EffectuersController(IDataRepository<Effectuer> effectuersRepository)
        {
            this._effectuers = effectuersRepository;
        }

        [HttpGet]
        [ActionName("GetEffectuers")]
        public async Task<ActionResult<IEnumerable<Effectuer>>> GetEffectuers()
        {
            return await _effectuers.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IdCompteClient{id}/IdCommande{id2}")]
        [ActionName("GetEffectuerById")]
        public async Task<ActionResult<Effectuer>> GetEffectuerById(int id , int id2)
        {

            var effectuers = await _effectuers.GetByIdAsync(id , id2);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (effectuers == null)
            {
                return NotFound();
            }
            return effectuers;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("IdCompteClient{id}/IdCommande{id2}")]
        [ActionName("PutEffectuer")]
        public async Task<IActionResult> PutEffectuer(int id,int id2, Effectuer effectuers)
        {
            if (id != effectuers.IdCompteClient)
            {
                return BadRequest();
            }
            var userToUpdate = await _effectuers.GetByIdAsync(id, id2);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _effectuers.UpdateAsync(userToUpdate.Value, effectuers);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostEffectuer")]
        public async Task<ActionResult<Effectuer>> PostEffectuer(Effectuer effectuers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _effectuers.AddAsync(effectuers);
            return CreatedAtAction("GetEffectuerById", new { id = effectuers.IdCompteClient }, effectuers); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("IdCompteClient{id}/IdCommande{id2}")]
        [ActionName("DeleteEffectuer")]
        public async Task<IActionResult> DeleteEffectuer(int id, int id2)
        {
            var effectuers = await _effectuers.GetByIdAsync(id, id2);
            if (effectuers == null)
            {
                return NotFound();
            }
            await _effectuers.DeleteAsync(effectuers.Value);
            return NoContent();
        }

    }
}
