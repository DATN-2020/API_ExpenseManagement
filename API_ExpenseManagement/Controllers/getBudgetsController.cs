using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ExpenseManagement.Context;
using API_ExpenseManagement.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API_ExpenseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getBudgetsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public getBudgetsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/getBudgets
        [HttpGet]
        public IEnumerable<getBudget> GetgetBudget()
        {
            return _context.getBudget;
        }

        // GET: api/getBudgets/5
        [HttpGet("{id}")]
        public ResponseModel GetgetBudget([FromQuery] int id)
        {
            var log = from a in _context.Budget
                      join b in _context.Categories
                      on a.Id_Cate equals b.Id_Cate
                      join c in _context.TypeCategories
                      on a.Id_type equals c.Id_type
                      select new
                      {
                          idwallet = a.Id_Wallet,
                          name = (b.Id_Cate == 1 ? c.Name_Type:b.NameCate),
                          amount = a.Amount_Budget,
                          remain = a.Remain
                      };
            var buget = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
            if (log == null)
            {
                ResponseModel res = new ResponseModel("Fail", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Budget", buget, "200");
                return res;
            }
        }

        // PUT: api/getBudgets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutgetBudget([FromRoute] int id, [FromBody] getBudget getBudget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getBudget.id_getBudget)
            {
                return BadRequest();
            }

            _context.Entry(getBudget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!getBudgetExists(id))
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

        // POST: api/getBudgets
        [HttpPost]
        public async Task<IActionResult> PostgetBudget([FromBody] getBudget getBudget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.getBudget.Add(getBudget);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetgetBudget", new { id = getBudget.id_getBudget }, getBudget);
        }

        // DELETE: api/getBudgets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletegetBudget([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getBudget = await _context.getBudget.FindAsync(id);
            if (getBudget == null)
            {
                return NotFound();
            }

            _context.getBudget.Remove(getBudget);
            await _context.SaveChangesAsync();

            return Ok(getBudget);
        }

        private bool getBudgetExists(int id)
        {
            return _context.getBudget.Any(e => e.id_getBudget == id);
        }
    }
}