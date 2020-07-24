using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ExpenseManagement.Context;
using API_ExpenseManagement.Models;
using Microsoft.IdentityModel.Xml;

namespace API_ExpenseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummariesController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public SummariesController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Summaries
        [HttpGet]
        public IEnumerable<Summary> GetSummary()
        {
            return _context.Summary;
        }

        // GET: api/Summaries/5
        [HttpGet("{id}")]
        public ResponseModel GetSummary([FromQuery] string id,string date)
        {
            //string id_wallet = summary.id_wallet;
            Summary summary = new Summary();
            string date_set = date;
            //int month = int.Parse(date.Substring(5, 2));
            float total_income_old = 0;
            float total_income_new = 0;
            Wallet wallet = _context.Wallets.Where(m => m.Id_Wallet.ToString() == id).FirstOrDefault();
            var income = _context.Income_Outcomes
                .Where(w => w.WalletId_Wallet == id);
            if(wallet != null)
            {
                foreach (Income_Outcome incomes in income)
                {
                    //Tổng chi trong tháng
                    if (DateTime.Parse(incomes.Date_come).Month == DateTime.Parse(date).Month && incomes.Is_Come == false && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                    {
                        summary.totalOutcome = summary.totalOutcome + incomes.Amount;
                    }
                    //Tổng thu trong tháng
                    if (DateTime.Parse(incomes.Date_come).Month == DateTime.Parse(date).Month && incomes.Is_Come == true && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                    {
                        summary.totalIncome = summary.totalIncome + incomes.Amount;
                    }
                    //Tổng chi từ trước đến ngày đầu của tháng
                    if (DateTime.Parse(incomes.Date_come).Millisecond < DateTime.Parse(date).Millisecond && incomes.Is_Come == false)
                    {
                        summary.beginBalance = summary.beginBalance + incomes.Amount;
                    }
                    //Tổng Thu từ trước đến ngày đầu của tháng
                    if (DateTime.Parse(incomes.Date_come).Millisecond < DateTime.Parse(date).Millisecond && incomes.Is_Come == true)
                    {
                        total_income_old = total_income_old + incomes.Amount;
                    }
                    //Tổng thu từ trước đến ngày cuối của tháng
                    if (DateTime.Parse(incomes.Date_come).Millisecond <= DateTime.Parse(date).Millisecond && incomes.Is_Come == true)
                    {
                        total_income_new = total_income_new + incomes.Amount;
                    }
                    //Tổng chi từ trước đến ngày cuối của tháng
                    if (DateTime.Parse(incomes.Date_come).Millisecond <= DateTime.Parse(date).Millisecond && incomes.Is_Come == false)
                    {
                        summary.endBalance = summary.endBalance + incomes.Amount;
                    }
                    //Tổng thu, chi trong tháng đang set
                    if (DateTime.Parse(incomes.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                    {
                        summary.netBalance = summary.netBalance + incomes.Amount;
                    }
                    //Tổng đi vay
                    if (DateTime.Parse(incomes.Date_come).Month == DateTime.Parse(date).Month && incomes.Id_type == "18" && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                    {
                        summary.totalLoan = summary.totalLoan + incomes.Amount;
                    }
                    //Tổng cho vay
                    if (DateTime.Parse(incomes.Date_come).Month == DateTime.Parse(date).Month && incomes.Id_type == "17" && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                    {
                        summary.totalBorrow = summary.totalBorrow + incomes.Amount;
                    }
                    //Tổng khác
                    if (DateTime.Parse(incomes.Date_come).Month == DateTime.Parse(date).Month && incomes.Id_type == "16" && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                    {
                        summary.totalOther = summary.totalOther + incomes.Amount;
                    }

                    if (DateTime.Parse(incomes.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                    {
                        float t = 0;
                        float t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 1 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 1 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_1 = t2 - t;
                        t = 0;
                        t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 2 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 2 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_2 = t2 - t;
                        t = 0;
                        t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 3 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 3 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_3 = t2 - t;
                        t = 0;
                        t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 4 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 4 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_4 = t2 - t;
                        t = 0;
                        t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 5 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 5 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_5 = t2 - t;
                        t = 0;
                        t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 6 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 6 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_6 = t2 - t;
                        t = 0;
                        t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 7 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 7 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_7 = t2 - t;
                        t = 0;
                        t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 8 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 8 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_8 = t2 - t;
                        t = 0;
                        t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 9 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 9 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_9 = t2 - t;
                        t = 0;
                        t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 10 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 10 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_10 = t2 - t;
                        t = 0;
                        t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 11 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 11 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_11 = t2 - t;
                        t = 0;
                        t2 = 0;
                        foreach (Income_Outcome incomes_t in income)
                        {
                            if (incomes_t.Is_Come == false && DateTime.Parse(incomes_t.Date_come).Month == 12 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t = t + incomes_t.Amount;
                            }
                            if (incomes_t.Is_Come == true && DateTime.Parse(incomes_t.Date_come).Month == 12 && DateTime.Parse(incomes_t.Date_come).Month == DateTime.Parse(date).Month && DateTime.Parse(incomes.Date_come).Year == DateTime.Parse(date).Year)
                            {
                                t2 = t2 + incomes_t.Amount;
                            }
                        }
                        summary.totalIncome_Outcome_12 = t2 - t;
                        t = 0;
                        t2 = 0;
                    }
                }

                    summary.beginBalance = wallet.Amount_Wallet - summary.beginBalance + total_income_old;
                    summary.endBalance = wallet.Amount_Wallet - summary.endBalance + total_income_new;
                    summary.netBalance = summary.beginBalance - summary.netBalance;
                    ResponseModel res = new ResponseModel("Income", summary, "200");
                    return res;
                
            }    
            else
            {
                ResponseModel res = new ResponseModel("Income", null, "200");
                return res;
            }    
            
        }

        // PUT: api/Summaries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSummary([FromRoute] int id, [FromBody] Summary summary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != summary.id_Summary)
            {
                return BadRequest();
            }

            _context.Entry(summary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SummaryExists(id))
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

        // POST: api/Summaries
        [HttpPost]
        public async Task<IActionResult> PostSummary([FromBody] Summary summary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Summary.Add(summary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSummary", new { id = summary.id_Summary }, summary);
        }

        // DELETE: api/Summaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSummary([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var summary = await _context.Summary.FindAsync(id);
            if (summary == null)
            {
                return NotFound();
            }

            _context.Summary.Remove(summary);
            await _context.SaveChangesAsync();

            return Ok(summary);
        }

        private bool SummaryExists(int id)
        {
            return _context.Summary.Any(e => e.id_Summary == id);
        }
    }
}