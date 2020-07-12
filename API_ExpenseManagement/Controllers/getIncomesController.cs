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
    public class getIncomesController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public getIncomesController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/getIncomes
        [HttpGet]
        public IEnumerable<getIncome> GetgetIncome()
        {
            return _context.getIncome;
        }

        // GET: api/getIncomes/5
        [HttpGet("{id}")]
        public ResponseModel GetgetIncome([FromQuery] int id)
        {
            var log = from a in _context.Income_Outcomes
                      join b in _context.Categories
                      on a.CategoryId_Cate equals b.Id_Cate
                      join c in _context.TypeCategories
                      on a.Id_type equals c.Id_type
                      join d in _context.Bill
                      on a.Id_Bill equals d.Id_Bill
                      join e in _context.Budget
                      on a.Id_Budget equals e.Id_Budget
                      join f in _context.Periodic
                      on a.Id_Per equals f.Id_Per
                      select new
                      {
                          idwallet = a.WalletId_Wallet,
                          id_Income = a.Id_come,
                          name = (a.CategoryId_Cate != 1 ? b.NameCate : c.Name_Type),
                          image = (a.CategoryId_Cate == 1 ? c.Image_Type : b.ImageCate),
                          amount = a.Amount,
                          date_s = a.Date_come
                      };
            var bill = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
            if (log == null)
            {
                ResponseModel res = new ResponseModel("Fail", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Income", bill, "200");
                return res;
            }
        }

        // PUT: api/getIncomes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutgetIncome([FromRoute] int id, [FromBody] getIncome getIncome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getIncome.id_getIncome)
            {
                return BadRequest();
            }

            _context.Entry(getIncome).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!getIncomeExists(id))
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

        // POST: api/getIncomes
        [HttpPost]
        public async Task<IActionResult> PostgetIncome([FromBody] getIncome getIncome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.getIncome.Add(getIncome);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetgetIncome", new { id = getIncome.id_getIncome }, getIncome);
        }

        // DELETE: api/getIncomes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletegetIncome([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getIncome = await _context.getIncome.FindAsync(id);
            if (getIncome == null)
            {
                return NotFound();
            }

            _context.getIncome.Remove(getIncome);
            await _context.SaveChangesAsync();

            return Ok(getIncome);
        }

        private bool getIncomeExists(int id)
        {
            return _context.getIncome.Any(e => e.id_getIncome == id);
        }
    }
}