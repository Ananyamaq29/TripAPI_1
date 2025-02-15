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

        // Constructor to inject TripContext
        public TripsController(TripContext context)
        {
            _context = context;
        }

        // GET: api/trips
        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _context.Trips.ToListAsync(); // Get all trips from the database
            return Ok(trips); // Return the list of trips
        }

        // GET: api/trips/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id); // Find trip by ID
            if (trip == null)
                return NotFound(); // Return 404 if trip not found

            return Ok(trip); // Return the trip data
        }

        // POST: api/trips
        [HttpPost]
        public async Task<IActionResult> AddTrip([FromBody] Trip trip)
        {
            if (trip == null)
                return BadRequest("Trip data is required."); // Return 400 if trip data is missing

            _context.Trips.Add(trip); // Add the new trip to the DbSet
            await _context.SaveChangesAsync(); // Save changes to the database

            return CreatedAtAction(nameof(GetTrip), new { id = trip.Id }, trip); // Return the created trip with status code 201
        }

        // PUT: api/trips/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrip(int id, [FromBody] Trip trip)
        {
            if (id != trip.Id)
                return BadRequest("Trip ID mismatch."); // Check if the ID in the URL matches the ID in the body

            _context.Entry(trip).State = EntityState.Modified; // Mark the trip as modified
            try
            {
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id)) // Check if the trip exists
                    return NotFound();
                throw;
            }

            return NoContent(); // Return 204 No Content if the update is successful
        }

        // DELETE: api/trips/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id); // Find the trip by ID
            if (trip == null)
                return NotFound(); // Return 404 if trip not found

            _context.Trips.Remove(trip); // Remove the trip from the DbSet
            await _context.SaveChangesAsync(); // Save changes to the database

            return NoContent(); // Return 204 No Content if the deletion is successful
        }

        // Helper method to check if a trip exists
        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id); // Check if the trip with the given ID exists
        }
    }
}