using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TripAPI.Models;

namespace TripAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly TripContext _context;
        public TripsController(TripContext context)
        {
            _context = context;
        }

        // GET: api/trips
        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _context.Trips.ToListAsync(); 
            return Ok(trips); 
        }

        // GET: api/trips/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id); 
            if (trip == null)
                return NotFound(); 

            return Ok(trip); 
        }

        // POST: api/trips
        [HttpPost]
        public async Task<IActionResult> AddTrip([FromBody] Trip trip)
        {
            if (trip == null)
                return BadRequest("Trip data is required."); 

            _context.Trips.Add(trip); 
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrip), new { id = trip.Id }, trip); 
        }

        // PUT: api/trips/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrip(int id, [FromBody] Trip trip)
        {
            if (id != trip.Id)
                return BadRequest("Trip ID mismatch."); 

            _context.Entry(trip).State = EntityState.Modified; 
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
                    return NotFound();
                throw;
            }

            return NoContent(); 
        }

        // DELETE: api/trips/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
                return NotFound(); 

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync(); 

            return NoContent(); 
        }
        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}