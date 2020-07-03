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
    public class Income_OutcomeController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public Income_OutcomeController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Income_Outcome
        [HttpGet]
        public IEnumerable<Income_Outcome> GetIncome_Outcomes()
        {
            return _context.Income_Outcomes;
        }

        // GET: api/Income_Outcome/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncome_Outcome([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var income_Outcome = await _context.Income_Outcomes.FindAsync(id);

            if (income_Outcome == null)
            {
                return NotFound();
            }

            return Ok(income_Outcome);
        }

        // PUT: api/Income_Outcome/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncome_Outcome([FromRoute] int id, [FromBody] Income_Outcome income_Outcome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != income_Outcome.Id_come)
            {
                return BadRequest();
            }

            _context.Entry(income_Outcome).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Income_OutcomeExists(id))
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

        // POST: api/Income_Outcome
        [HttpPost]
        public ResponseModel PostIncome_Outcome([FromBody] Income_Outcome income_Outcome)
        {
            Income_Outcome income = new Income_Outcome();
            float amount = income_Outcome.Amount;
            string date = income_Outcome.Date_come;
            string desciption = income_Outcome.Description_come;
            bool is_come = income_Outcome.Is_Come;
            int id_cate = income_Outcome.CategoryId_Cate;
            int id_loan = income_Outcome.LoanId_Loan;
            int id_trip = income_Outcome.TripId_Trip;
            int id_type = income_Outcome.TypeCategoryId;
            int id_wallet = income_Outcome.WalletId_Wallet;
            if(id_cate == 0)
            {
                id_cate = 1;
            }
            if (id_loan == 0)
            {
                id_loan = 1;
            }
            if (id_trip == 0)
            {
                id_trip = 1;
            }
            if (id_type == 0)
            {
                id_type = 1;
            }
            if (id_wallet == 0)
            {
                id_wallet = 1;
            }
            income.Amount = amount;
            income.Date_come = date;
            income.Description_come = desciption;
            income.Is_Come = is_come;
            income.CategoryId_Cate = id_cate;
            income.LoanId_Loan = id_loan;
            income.TripId_Trip = id_trip;
            income.TypeCategoryId = id_type;
            income.WalletId_Wallet = id_wallet;
            try
            {
                _context.Income_Outcomes.Add(income);
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Create success", null, "200");
                return res;
            }
            catch {
                ResponseModel res = new ResponseModel("Create fail", null, "404");
                return res;
            }
        }

        // DELETE: api/Income_Outcome/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome_Outcome([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var income_Outcome = await _context.Income_Outcomes.FindAsync(id);
            if (income_Outcome == null)
            {
                return NotFound();
            }

            _context.Income_Outcomes.Remove(income_Outcome);
            await _context.SaveChangesAsync();

            return Ok(income_Outcome);
        }

        private bool Income_OutcomeExists(int id)
        {
            return _context.Income_Outcomes.Any(e => e.Id_come == id);
        }
    }
}