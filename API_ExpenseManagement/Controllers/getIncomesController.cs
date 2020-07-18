using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ExpenseManagement.Context;
using API_ExpenseManagement.Models;
using System.Collections;

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
        public ResponseModel GetgetIncome([FromQuery] string id, string date)
        {
            var income = _context.Income_Outcomes.Where(m => m.WalletId_Wallet == id);
            foreach (Income_Outcome income_ in income)
            {
                //if (DateTime.Parse(date).Month == DateTime.Parse(income_.Date_come).Month &&
                //    DateTime.Parse(date).Year == DateTime.Parse(income_.Date_come).Year)
                //{
                    var list = new ArrayList();
                    //date = income_.Date_come;
                    //ResponseModel res_date = new ResponseModel("Income", date, "200");
                    //foreach (Income_Outcome income_1 in income)
                    //{
                        if (income_.CategoryId_Cate == null)
                        {
                            var log = from a in _context.Income_Outcomes
                                      join c in _context.TypeCategories
                                      on a.Id_type equals c.Id_type.ToString()
                                      select new
                                      {
                                          idwallet = a.WalletId_Wallet,
                                          amount = a.Amount,
                                          date = a.Date_come,
                                          desciption = a.Description_come,
                                          is_come = a.Is_Come,
                                          name = c.Name_Type,
                                          image = c.Image_Type
                                      };
                            var get = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
                            list.Add(get);
                            //ResponseModel res1 = new ResponseModel("Income", get, "200");
                            //return res1;
                        }
                        if (income_.Id_type == null)
                        {
                            var log = from a in _context.Income_Outcomes
                                      join b in _context.Categories
                                      on a.CategoryId_Cate equals b.Id_Cate.ToString()
                                      select new
                                      {
                                          idwallet = a.WalletId_Wallet,
                                          amount = a.Amount,
                                          date = a.Date_come,
                                          desciption = a.Description_come,
                                          is_come = a.Is_Come,
                                          name = b.NameCate,
                                          image = b.ImageCate
                                      };
                            var get = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
                            //ResponseModel res1 = new ResponseModel("Income", get, "200");
                            //return res1;
                            list.Add(get);
                        }
                        ResponseModel res1 = new ResponseModel("Income", list, "200");
                        return res1;
                    //}
                //}
            }
            ResponseModel res = new ResponseModel("Income", null, "200");
            return res;
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