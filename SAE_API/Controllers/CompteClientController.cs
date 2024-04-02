using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAE_API.Models;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;
using System.Security.Cryptography;
using System.Text;

namespace SAE_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompteClientController : ControllerBase
    {
        private readonly IDataRepository<CompteClient> dataRepository;

        public CompteClientController(IDataRepository<CompteClient> dataRepo)
        {
            this.dataRepository = dataRepo;
        }

        [HttpGet]
        [ActionName("GetUtilisateurs")]
        public async Task<ActionResult<IEnumerable<CompteClient>>> GetUtilisateurs()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetUtilisateurById")]
        public async Task<ActionResult<CompteClient>> GetUtilisateurById(int id)
        {

            var compteClient = await dataRepository.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (compteClient == null)
            {
                return NotFound();
            }
            return compteClient;
        }

        // GET : api/Utilisateurs/nom
        [HttpGet("{nom}")]
        [ActionName("GetUtilisateurByName")]
        public async Task<ActionResult<CompteClient>> GetUtilisateurByName(string email)
        {
            var compteClient = await dataRepository.GetByStringAsync(email);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (compteClient == null)
            {
                return NotFound();
            }
            return compteClient;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutUtilisateur")]
        public async Task<IActionResult> PutUtilisateur(int id, CompteClient compteClient)
        {
            if (id != compteClient.IdCompteClient)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, compteClient);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostUtilisateur")]
        public async Task<ActionResult<CompteClient>> PostUtilisateur(CompteClient compteClient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hashedPassword = ComputeSha256Hash(compteClient.Password);
            compteClient.Password = hashedPassword;
            await dataRepository.AddAsync(compteClient);
            return CreatedAtAction("GetUtilisateurById", new { id = compteClient.IdCompteClient }, compteClient); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteUtilisateur")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var compteClient = await dataRepository.GetByIdAsync(id);
            if (compteClient == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(compteClient.Value);
            return NoContent();
        }

        [HttpGet]
        [ActionName("GetUserData")]
        [Authorize(Policy = Policies.User)]
        public IActionResult GetUserData()
        {
            return Ok("This is a response from user method");
        }
        [HttpGet]
        [ActionName("GetAdminData")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetAdminData()
        {
            return Ok("This is a response from admin method");
        }

        // to encrypt password data
        private string ComputeSha256Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }





    }
}
