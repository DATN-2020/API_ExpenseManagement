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
    public class getPeriodicsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public getPeriodicsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/getPeriodics
        [HttpGet]
        public IEnumerable<getPeriodic> GetgetPeriodic()
        {
            return _context.getPeriodic;
        }

        // GET: api/getPeriodics/5
        [HttpGet("{id}")]
        public ResponseModel GetgetPeriodic([FromQuery] int id)
        {
            var log = from a in _context.Periodic
                      join b in _context.Categories
                      on a.Id_Cate equals b.Id_Cate
                      join c in _context.TypeCategories
                      on a.Id_Type equals c.Id_type
                      join d in _context.Time_Periodic
                      on a.id_Time equals d.id_Time
                      select new
                      {
                          idwallet = a.Id_Wallet,
                          idPeriodic = a.Id_Per,
                          name = (b.Id_Cate == 1 ? c.Name_Type : b.NameCate),
                          image = (b.Id_Cate == 1 ? c.Image_Type : b.ImageCate),
                          amount = a.Amount_Per,
                          date_s = a.date_s,
                          date_e = a.date_e,
                          time = d.desciption,
                          is_Comeback = a.date_e >= DateTime.Now ? false:true,
                          date_time_s = a.date_s,
                          date_time_e =
                          (a.id_Time == 1 ? a.date_s.AddDays(1) :
                          a.id_Time == 2 ? a.date_s.AddDays(7) :
                          a.id_Time == 3 ? DateTime.Today.AddDays(DateTime.DaysInMonth(2020, DateTime.Today.Month) - DateTime.Today.Day) :
                          DateTime.Today.AddDays(365 - (a.date_s.DayOfYear - 1)))
                      };
            var bill = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
            Periodic periodic = _context.Periodic.Where(m => m.Id_Wallet == id).FirstOrDefault();
            if (periodic.date_e <= DateTime.Today)
            {
                periodic.isFinnish = true;
                _context.Periodic.Update(periodic);
                _context.SaveChangesAsync();

            }
            if (log == null)
            {
                ResponseModel res = new ResponseModel("Fail", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Periodic", bill, "200");
                return res;
            }
        }

        // PUT: api/getPeriodics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutgetPeriodic([FromRoute] int id, [FromBody] getPeriodic getPeriodic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getPeriodic.id_getBudget)
            {
                return BadRequest();
            }

            _context.Entry(getPeriodic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!getPeriodicExists(id))
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

        // POST: api/getPeriodics
        [HttpPost]
        public async Task<IActionResult> PostgetPeriodic([FromBody] getPeriodic getPeriodic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.getPeriodic.Add(getPeriodic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetgetPeriodic", new { id = getPeriodic.id_getBudget }, getPeriodic);
        }

        // DELETE: api/getPeriodics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletegetPeriodic([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getPeriodic = await _context.getPeriodic.FindAsync(id);
            if (getPeriodic == null)
            {
                return NotFound();
            }

            _context.getPeriodic.Remove(getPeriodic);
            await _context.SaveChangesAsync();

            return Ok(getPeriodic);
        }

        private bool getPeriodicExists(int id)
        {
            return _context.getPeriodic.Any(e => e.id_getBudget == id);
        }
    }
}