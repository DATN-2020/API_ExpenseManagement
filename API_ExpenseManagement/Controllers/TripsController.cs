using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ExpenseManagement.Context;
using API_ExpenseManagement.Models;

namespace API_ExpenseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public TripsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Trips
        [HttpGet]
        public IEnumerable<Trip> GetTrips()
        {
            return _context.Trips;
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trip = await _context.Trips.FindAsync(id);

            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        // PUT: api/Trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip([FromRoute] int id, [FromBody] Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trip.Id_Trip)
            {
                return BadRequest();
            }

            _context.Entry(trip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trips
        [HttpPost]
        public async Task<IActionResult> PostTrip([FromBody] Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrip", new { id = trip.Id_Trip }, trip);
        }

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public ResponseModel DeleteTrip([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }

            var trip = _context.Trips.Find(id);
            if (trip == null)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }
            else
            {
                _context.Trips.Remove(trip);
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Delete success", null, "404");
                return res;
            }
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.Id_Trip == id);
        }
    }
}