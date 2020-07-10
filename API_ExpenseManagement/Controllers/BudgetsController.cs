using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ExpenseManagement.Context;
using API_ExpenseManagement.Models;
using System.Security.Cryptography.X509Certificates;

namespace API_ExpenseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public BudgetsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Budgets
        [HttpGet]
        public IEnumerable<Budget> GetBudget()
        {
            return _context.Budget;
        }

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        public ResponseModel GetBudget([FromQuery] int id)
        {
            var log = _context.Budget.Where(m => m.Id_Wallet.Equals(id)).AsEnumerable();
            if (log == null)
            {
                ResponseModel res = new ResponseModel("Fail", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Budget", log, "200");
                return res;
            }
        }

        // PUT: api/Budgets/5
        [HttpPut("{id}")]
        public ResponseModel PutBudget([FromRoute] int id, [FromBody] Budget budget)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Fail", null, "200");
                return res;
            }

            if (id != budget.Id_Budget)
            {
                ResponseModel res = new ResponseModel("Fail", null, "200");
                return res;
            }
            if (budget.Id_Wallet == 0)
            {
                budget.Id_Wallet = 1;
            }
            if (budget.Id_Cate == 0)
            {
                budget.Id_Cate = 1;
            }
            if (budget.Id_type == 0)
            {
                budget.Id_type = 1;
            }
            budget.Remain = budget.Amount_Budget;
            budget.isFinnish = false;
            try
            {
                _context.Entry(budget).State = EntityState.Modified;
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Update success", null, "200");
                return res;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetExists(id))
                {
                    ResponseModel res = new ResponseModel("Not found", null, "200");
                    return res;
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Budgets
        [HttpPost]
        public ResponseModel PostBudget([FromBody] Budget budget)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Fail", null, "200");
                return res;
            }
            float amount = budget.Amount_Budget;
            float remain = budget.Remain;
            DateTime time_s = budget.time_s;
            DateTime time_e = budget.time_e;
            int id_cate = budget.Id_Cate;
            int id_wallet = budget.Id_Wallet;
            int id_custom = budget.Id_Custom;
            bool is_Finnish = false;
            if (budget.Id_Wallet == 0)
            {
                budget.Id_Wallet = 1;
            }
            if (budget.Id_Cate == 0)
            {
                budget.Id_Cate = 1;
            }
            if (budget.Id_type == 0)
            {
                budget.Id_type = 1;
            }
            budget.Remain = amount;
            budget.isFinnish = is_Finnish;
            try 
            {
                if(budget.Id_Budget ==0)
                {
                    _context.Budget.Add(budget);
                    _context.SaveChangesAsync();
                    ResponseModel res = new ResponseModel("Create success", null, "200");
                    return res;
                }
                else
                {
                    ResponseModel res = new ResponseModel("Fail", null, "200");
                    return res;
                }    
            }
            catch {
                ResponseModel res = new ResponseModel("Create fail", null, "200");
                return res;
            }
        }

        // DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        public ResponseModel DeleteBudget([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "200");
                return res;
            }

            var budget = _context.Budget.Find(id);
            if (budget == null)
            {
                ResponseModel res = new ResponseModel("Not found", null, "200");
                return res;
            }
            else
            {
                _context.Budget.Remove(budget);
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Delete success", null, "200");
                return res;
            }
        }

        private bool BudgetExists(int id)
        {
            return _context.Budget.Any(e => e.Id_Budget == id);
        }
    }
}