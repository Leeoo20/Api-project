using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP4_1.Models.DataManager;
using TP4_1.Models.EntityFramework;
using TP4_1.Models.Repository;
using TP4_1_Models_EntityFramework;

namespace TP4_1.Controllers
{
    /*AVANT pattern Repository*/
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly IDataRepository<Utilisateur> dataRepository;

        public UtilisateursController(IDataRepository<Utilisateur> dataRepo)
        {
            dataRepository = dataRepo;
        }


        // private readonly FilmRatingDBContext _context;
        /*
        public UtilisateursController(FilmRatingDBContext context)
        {
            _context = context;
        }*/



        // GET: api/Utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            //return await _context.Utilisateurs.ToListAsync();
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetUtilisateurById")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
        {
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            var utilisateur = await dataRepository.GetByIdAsync(id);

            if (utilisateur.Value == null) // Sinon ne marche pas avec la maj
            {
                return NotFound();
            }

            return utilisateur;
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{email}")]
        [ActionName("GetUtilisateurByEmail")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurByEmail(string email)
        {
            //var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(c => c.Mail == email);
            var utilisateur = await dataRepository.GetByStringAsync(email);


            if (utilisateur.Value == null)
            {
                return NotFound();
            }

            return utilisateur;
        }






        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            /* POUR LEVER ERREURS SI MARCHE PAS
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/



            if (id != utilisateur.UtilisateurId)
            {
                return BadRequest();
            }

            /*

            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/

            var userToUpdate = await dataRepository.GetByIdAsync(id);
            
            if (userToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, utilisateur);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*_context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();*/

            await dataRepository.AddAsync(utilisateur);

            return CreatedAtAction("GetUtilisateurById", new { id = utilisateur.UtilisateurId }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            var utilisateur = await dataRepository.GetByIdAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            /*_context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();*/
            await dataRepository.DeleteAsync(utilisateur.Value);


            return NoContent();
        }

        /*private bool UtilisateurExists(int id) NE SE
        {
            return _context.Utilisateurs.Any(e => e.UtilisateurId == id);
        }*/
    }
}
