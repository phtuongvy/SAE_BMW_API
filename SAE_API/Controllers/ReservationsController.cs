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
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IDataRepository<Reservation> _reservation;

        public ReservationsController(IDataRepository<Reservation> reservationRepository)
        {
            this._reservation = reservationRepository;
        }

        [HttpGet]
        [ActionName("GetReservations")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _reservation.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetReservationById")]
        public async Task<ActionResult<Reservation>> GetReservationById(int id)
        {

            var reservation = await _reservation.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return reservation;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutReservation")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.IdConcessionnaire)
            {
                return BadRequest();
            }
            var userToUpdate = await _reservation.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _reservation.UpdateAsync(userToUpdate.Value, reservation);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostReservation")]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _reservation.AddAsync(reservation);
            return CreatedAtAction("GetReservationById", new { id = reservation.IdConcessionnaire }, reservation); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteReservation")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservation.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            await _reservation.DeleteAsync(reservation.Value);
            return NoContent();
        }
    }
}
