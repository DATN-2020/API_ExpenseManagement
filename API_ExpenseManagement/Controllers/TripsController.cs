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
        public ResponseModel GetTrip([FromQuery] string id)
        {
            var log = _context.Trips.Where(m => m.id_user == id).AsEnumerable();
            ResponseModel res1 = new ResponseModel("Trip", log, "404");
            return res1;

        }

        // PUT: api/Trips/5
        [HttpPut("{id}")]
        public ResponseModel PutTrip([FromRoute] int id, [FromBody] Trip trip)
        {
            string name = trip.Name_Trip;
            if (!ModelState.IsValid)
            {
                ResponseModel res1 = new ResponseModel("Update fail", null, "404");
                return res1;
            }

            if (id != trip.Id_Trip)
            {
                ResponseModel res1 = new ResponseModel("Update fail", null, "404");
                return res1;
            }
            trip.Name_Trip = name;
            _context.Trips.Update(trip);
            _context.SaveChangesAsync();
            ResponseModel res = new ResponseModel("Update successs", null, "404");
            return res;

        }

        // POST: api/Trips
        [HttpPost]
        public ResponseModel PostTrip([FromBody] Trip trip)
        {
            string id_user = trip.id_user;
            string name = trip.Name_Trip;
            if(trip != null)
            {
                trip.id_user = id_user;
                trip.Name_Trip = name;
                _context.Trips.Add(trip);
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Create success", null, "404");
                return res;
            }    
            else
            {
                ResponseModel res = new ResponseModel("Create fail", null, "404");
                return res;
            }    
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