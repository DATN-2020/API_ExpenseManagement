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
        public ResponseModel GetSummary([FromRoute] string id, [FromBody] Summary summary)
        {
            //string id_wallet = summary.id_wallet;
            string date = summary.date;
            int month = int.Parse(date.Substring(5, 2));
            float total_income_old = summary.beginBalance;
            float total_income_new = summary.beginBalance;
            //var log = from a in _context.Summary
            //          join b in _context.Income_Outcomes
            //          on a.id_Come equals b.Id_come.ToString()
            //          where (a.id_wallet == b.WalletId_Wallet && a.date.Substring(5, 2) == b.Date_come.Substring(5, 2))
            //          select new
            //          {
            //              beginBalance = 
            //          }
            Wallet wallet = _context.Wallets.Where(m => m.Id_Wallet.ToString() == id).FirstOrDefault();
            var income = _context.Income_Outcomes
                .Where(w => w.WalletId_Wallet == id);
            foreach (Income_Outcome incomes in income)
            {
                //Tổng chi trong tháng
                if (DateTime.Parse(incomes.Date_come).Month == DateTime.Today.Month && incomes.Is_Come == false)
                {
                    summary.totalOutcome = summary.totalOutcome + incomes.Amount;
                }
                //Tổng thu trong tháng
                if (DateTime.Parse(incomes.Date_come).Month == DateTime.Today.Month && incomes.Is_Come == true)
                {
                    summary.totalIncome = summary.totalIncome + incomes.Amount;
                }
                //Tổng chi từ trước đến ngày đầu của tháng
                if (DateTime.Parse(incomes.Date_come).Month < DateTime.Today.Month && incomes.Is_Come == false)
                {
                    summary.beginBalance = summary.beginBalance + incomes.Amount;
                }
                //Tổng Thu từ trước đến ngày đầu của tháng
                if (DateTime.Parse(incomes.Date_come).Month < DateTime.Today.Month && incomes.Is_Come == true)
                {
                    total_income_old = total_income_old + incomes.Amount;
                }
                //Tổng thu từ trước đến ngày cuối của tháng
                if (DateTime.Parse(incomes.Date_come).Month <= DateTime.Today.Month && incomes.Is_Come == true)
                {
                    total_income_new = total_income_new + incomes.Amount;
                }
                //Tổng chi từ trước đến ngày cuối của tháng
                if (DateTime.Parse(incomes.Date_come).Month <= DateTime.Today.Month && incomes.Is_Come == false)
                {
                    summary.endBalance = summary.endBalance + incomes.Amount;
                }
                //Tổng thu, chi trong tháng đang set
                if (DateTime.Parse(incomes.Date_come).Month == DateTime.Today.Month)
                {
                    summary.netBalance = summary.netBalance + incomes.Amount;
                }
                //Tổng đi vay
                if (DateTime.Parse(incomes.Date_come).Month == DateTime.Today.Month && incomes.Id_type == "18")
                {
                    summary.totalLoan = summary.totalLoan + incomes.Amount;
                }
                //Tổng cho vay
                if (DateTime.Parse(incomes.Date_come).Month == DateTime.Today.Month && incomes.Id_type == "17")
                {
                    summary.totalBorrow = summary.totalBorrow + incomes.Amount;
                }
                //Tổng khác
                if (DateTime.Parse(incomes.Date_come).Month == DateTime.Today.Month && incomes.Id_type == "16")
                {
                    summary.totalOther = summary.totalOther + incomes.Amount;
                }
            }
            summary.beginBalance = wallet.Amount_Wallet - summary.beginBalance + total_income_old;
            summary.endBalance = wallet.Amount_Wallet - summary.endBalance + total_income_new;
            summary.netBalance = summary.beginBalance - summary.netBalance;
            ResponseModel res = new ResponseModel("Income", summary, "200");
            return res;
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