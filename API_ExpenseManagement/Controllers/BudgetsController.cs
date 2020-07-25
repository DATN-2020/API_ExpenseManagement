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
using System.Collections;

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
        public ResponseModel GetBudget([FromQuery] string id_wallet, string id_budget, string date)
        {
            var list = new ArrayList();
            var income = _context.Income_Outcomes
                .Where(w => w.WalletId_Wallet == id_wallet).Where(t => t.Id_Budget == id_budget);
            foreach (Income_Outcome income_ in income)
            {
                if (income_.CategoryId_Cate == null)
                {
                    var log = from a in _context.Income_Outcomes
                              join c in _context.TypeCategories
                              on a.Id_type equals c.Id_type.ToString()
                              where (a.CategoryId_Cate == null 
                              && a.Id_come == income_.Id_come
                              && a.Id_Budget == id_budget)
                              select new
                              {
                                  idwallet = a.WalletId_Wallet,
                                  id_come = a.Id_come,
                                  id_budget = a.Id_Budget,
                                  name = c.Name_Type,
                                  image = c.Image_Type,
                                  amount = a.Amount,
                                  date_come = a.Date_come,
                                  desciption = a.Description_come,
                                  is_come = a.Is_Come
                              };
                    var get = log.Where(m => m.idwallet.Equals(id_wallet)).AsEnumerable();
                    foreach (object l in get)
                    {
                        list.Add(l);
                    }
                }
                if (income_.Id_type == null)
                {
                    var log = from a in _context.Income_Outcomes
                              join c in _context.Categories
                              on a.CategoryId_Cate equals c.Id_Cate.ToString()
                              where (a.Id_type == null
                              && a.Id_come == income_.Id_come
                              && a.Id_Budget == id_budget)
                              select new
                              {
                                  idwallet = a.WalletId_Wallet,
                                  id_come = a.Id_come,
                                  id_budget = a.Id_Budget,
                                  name = c.NameCate,
                                  image = c.ImageCate,
                                  amount = a.Amount,
                                  date_come = a.Date_come,
                                  desciption = a.Description_come,
                                  is_come = a.Is_Come
                              };
                    var get = log.Where(m => m.idwallet.Equals(id_wallet)).AsEnumerable();
                    foreach (object l in get)
                    {
                        list.Add(l);
                    }
                    //foreach (object l in get)
                    //{
                    //    list_date.Add(list);
                    //}
                }
            }
            ResponseModel res = new ResponseModel("Income", list, "200");
            return res;
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

            float amount = budget.Amount_Budget;
            float remain = budget.Remain;
            DateTime date_e = budget.time_e;
            DateTime date_s = budget.time_s;
            bool isfinnish = budget.isFinnish;
            string idtime = budget.id_Time;
            budget = _context.Budget.Where(m => m.Id_Budget == id).FirstOrDefault();
            budget.Amount_Budget = amount;
            budget.time_e = date_e;
            budget.time_s = date_s;
            budget.isFinnish = isfinnish;
            budget.id_Time = idtime;
            //if (budget.Id_Wallet == 0)
            //{
            //    budget.Id_Wallet = 1;
            //}
            //if (budget.Id_Cate == 0)
            //{
            //    budget.Id_Cate = 1;
            //}
            //if (budget.Id_type == 0)
            //{
            //    budget.Id_type = 1;
            //}
            budget.Remain = budget.Amount_Budget;
            budget.isFinnish = false;
            try
            {
                _context.Entry(budget).State = EntityState.Modified;
                _context.SaveChanges();
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
            string id_cate = budget.Id_Cate;
            string id_wallet = budget.Id_Wallet;
            bool is_Finnish = false;
            string idtime = budget.id_Time;
            budget.Remain = 0;
            budget.isFinnish = is_Finnish;
            try 
            {
                if(budget.Id_Budget ==0)
                {
                    _context.Budget.Add(budget);
                    _context.SaveChanges();
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
                _context.SaveChanges();
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