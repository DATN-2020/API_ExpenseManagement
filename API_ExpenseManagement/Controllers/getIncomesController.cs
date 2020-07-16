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
        public ResponseModel GetgetIncome([FromRoute] string id, [FromBody] Income_Outcome income_Outcome)
        {
            string id_cate = income_Outcome.CategoryId_Cate;
            string id_type = income_Outcome.Id_type;
            string id_bill = income_Outcome.Id_Bill;
            string id_budget = income_Outcome.Id_Budget;
            string id_per = income_Outcome.Id_Per;
            string date = income_Outcome.Date_come;
            //DateTime month = income_Outcome.Date_come.Month;
            income_Outcome.CategoryId_Cate = id_cate;
            income_Outcome.Id_type = id_type;
            income_Outcome.Id_Bill = id_bill;
            income_Outcome.Id_Budget = id_budget;
            income_Outcome.Id_Per = id_per;

            //filter theo tháng
                if (date != null)
                {
                    if (date.Length == 7)
                    {
                        var log = from a in _context.Income_Outcomes
                                  join b in _context.Categories
                                  on a.CategoryId_Cate equals b.Id_Cate.ToString()
                                  join c in _context.TypeCategories
                                  on a.Id_type equals c.Id_type.ToString()
                                  where (a.Date_come.Substring(5, 2) == date.Substring(5, 2))
                                  select new
                                  {
                                      idwallet = a.WalletId_Wallet,
                                      id_Income = a.Id_come,
                                      name = (a.Id_type != "1" ? c.Name_Type : a.CategoryId_Cate != "1" ? b.NameCate :
                                      a.Id_Bill != "1" ? "Thanh toán hóa đơn" : a.Id_Budget != "1" ? "Xử dụng ngân sách" :
                                      "Thanh toán định kỳ"),
                                      image = (a.Id_type != "1" ? c.Image_Type : a.CategoryId_Cate != "1" ? b.ImageCate :
                                      c.Image_Type),
                                      amount = a.Amount,
                                      date_s = a.Date_come
                                  };
                        var income = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
                        if (log == null)
                        {
                            ResponseModel res = new ResponseModel("Fail", null, "404");
                            return res;
                        }
                        else
                        {
                            ResponseModel res = new ResponseModel("Income", income, "200");
                            return res;
                        }
                    }

                    //filter theo ngày
                    if (income_Outcome.Date_come.Length > 7)
                    {
                        var log = from a in _context.Income_Outcomes
                                  join b in _context.Categories
                                  on a.CategoryId_Cate equals b.Id_Cate.ToString()
                                  join c in _context.TypeCategories
                                  on a.Id_type equals c.Id_type.ToString()
                                  where (a.Date_come.Substring(8, 2) == date.Substring(8, 2))
                                  select new
                                  {
                                      idwallet = a.WalletId_Wallet,
                                      id_Income = a.Id_come,
                                      name = (a.Id_type != null ? c.Name_Type : a.CategoryId_Cate != null ? b.NameCate :
                                      a.Id_Bill != null ? "Thanh toán hóa đơn" : a.Id_Budget != null ? "Xử dụng ngân sách" :
                                      "Thanh toán định kỳ"),
                                      image = (a.Id_type != null ? c.Image_Type : a.CategoryId_Cate != null ? b.ImageCate :
                                      c.Image_Type),
                                      amount = a.Amount,
                                      date_s = a.Date_come
                                  };
                        var income = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
                        if (log == null)
                        {
                            ResponseModel res = new ResponseModel("Fail", null, "404");
                            return res;
                        }
                        else
                        {
                            ResponseModel res = new ResponseModel("Income", income, "200");
                            return res;
                        }
                    }
                    else
                    {
                        var log = from a in _context.Income_Outcomes
                                  join b in _context.Categories
                                  on a.CategoryId_Cate equals b.Id_Cate.ToString()
                                  join c in _context.TypeCategories
                                  on a.Id_type equals c.Id_type.ToString()
                                  where (a.Date_come.Substring(1, 4) == date.Substring(1,4))
                                  select new
                                  {
                                      idwallet = a.WalletId_Wallet,
                                      id_Income = a.Id_come,
                                      name = (a.Id_type != null ? c.Name_Type : a.CategoryId_Cate != null ? b.NameCate :
                                      a.Id_Bill != null ? "Thanh toán hóa đơn" : a.Id_Budget != null ? "Xử dụng ngân sách" :
                                      "Thanh toán định kỳ"),
                                      image = (a.Id_type != null ? c.Image_Type : a.CategoryId_Cate != null ? b.ImageCate :
                                      c.Image_Type),
                                      amount = a.Amount,
                                      date_s = a.Date_come
                                  };
                        var income = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
                        if (log == null)
                        {
                            ResponseModel res = new ResponseModel("Fail", null, "404");
                            return res;
                        }
                        else
                        {
                            ResponseModel res = new ResponseModel("Income", income, "200");
                            return res;
                        }
                    }
                }
                else
                {
                    //Không có filter
                    var log = from a in _context.Income_Outcomes
                              join b in _context.Categories
                              on a.CategoryId_Cate equals b.Id_Cate.ToString()
                              join c in _context.TypeCategories
                              on a.Id_type equals c.Id_type.ToString()
                              where (a.Id_Bill != null || a.Id_Budget != null || a.CategoryId_Cate != null
                              || a.Id_type != null || a.Id_Per != null)
                              select new
                              {
                                  idwallet = a.WalletId_Wallet,
                                  id_Income = a.Id_come,
                                  name = (a.Id_type != null ? c.Name_Type : a.CategoryId_Cate != null ? b.NameCate :
                                  a.Id_Bill != null ? "Thanh toán hóa đơn" : a.Id_Budget != null ? "Xử dụng ngân sách" :
                                  "Thanh toán định kỳ"),
                                  image = (a.Id_type != null ? c.Image_Type : a.CategoryId_Cate != null ? b.ImageCate :
                                  c.Image_Type),
                                  amount = a.Amount,
                                  date_s = a.Date_come
                              };
                    var income = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
                    if (log == null)
                    {
                        ResponseModel res = new ResponseModel("Fail", null, "404");
                        return res;
                    }
                    else
                    {
                        ResponseModel res = new ResponseModel("Income", income, "200");
                        return res;
                    }
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