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
    public class MotosController : ControllerBase
    {
        private readonly IDataRepository<Moto> dataRepository;

        public MotosController(IDataRepository<Moto> dataRepo)
        {
            //_context = context;
            dataRepository = dataRepo;
        }

        [HttpGet]
        [ActionName("GetMotos")]
        public async Task<ActionResult<IEnumerable<Moto>>> GetMotos()
        {
            return await dataRepository.GetAllAsync();
        }


        [HttpGet]
        [ActionName("GetMotosCustom")]
        public async Task<ActionResult<object>> GetMotosCustom()
        {
            return await dataRepository.GetAllAsync1();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetMotosByIdCustom")]
        public async Task<ActionResult<object>> GetMotosByIdCustom(int id)
        {

            var moto = await dataRepository.GetByIdCustomAsync1(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (moto == null)
            {
                return NotFound();
            }
            return moto;
        }

        [HttpGet("{id}")]
        [ActionName("GetMotosById")]
        public async Task<ActionResult<Moto>> GetMotoById(int id)
        {

            var moto = await dataRepository.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (moto == null)
            {
                return NotFound();
            }
            return moto;
        }

        // GET : api/Utilisateurs/nom
        [HttpGet("{nom}")]
        [ActionName("GetMotoByName")]
        public async Task<ActionResult<Moto>> GetMotoByName(string nom)
        {
            var moto = await dataRepository.GetByStringAsync(nom);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (moto == null)
            {
                return NotFound();
            }
            return moto;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutMoto")]
        public async Task<IActionResult> PutMoto(int id, Moto moto)
        {
            if (id != moto.MotoId)
            {
                return BadRequest();
            }
            var userToUpdate = await dataRepository.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, moto);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostMoto")]
        public async Task<ActionResult<Moto>> PostMoto(Moto moto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(moto);
            return CreatedAtAction("GetMotosById", new { id = moto.MotoId }, moto); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteMoto")]
        public async Task<IActionResult> DeleteMoto(int id)
        {
            var moto = await dataRepository.GetByIdAsync(id);
            if (moto == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(moto.Value);
            return NoContent();
        }

    }
}
