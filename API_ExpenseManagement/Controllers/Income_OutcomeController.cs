﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ExpenseManagement.Context;
using API_ExpenseManagement.Models;
using System.Collections;
using System.Net.Http.Headers;

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
        public ResponseModel GetIncome_Outcome([FromQuery] string id, string date)
        {
            //int t = DateTime.Parse(date).Month;
            var list = new ArrayList();
            var list_date = new ArrayList();
            var income = _context.Income_Outcomes.Where(m => m.WalletId_Wallet == id);
 
            foreach (Income_Outcome income_ in income)
            {
                if (income_.CategoryId_Cate == null)
                {
                    var log = from a in _context.Income_Outcomes
                              join c in _context.TypeCategories
                              on a.Id_type equals c.Id_type.ToString()
                              where (a.CategoryId_Cate == null && DateTime.Parse(a.Date_come).Month == DateTime.Parse(date).Month
                              && a.Id_come == income_.Id_come
                              && DateTime.Parse(a.Date_come).Year == DateTime.Parse(date).Year)
                              select new
                              {
                                  idwallet = a.WalletId_Wallet,
                                  id_come = a.Id_come,
                                  name = c.Name_Type,
                                  image = c.Image_Type,
                                  amount = a.Amount,
                                  date_come = a.Date_come,
                                  desciption = a.Description_come,
                                  is_come = a.Is_Come
                              };
                    var get = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
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
                              where (a.Id_type == null && DateTime.Parse(a.Date_come).Month == DateTime.Parse(date).Month
                              && a.Id_come == income_.Id_come && a.Date_come == income_.Date_come &&
                              DateTime.Parse(a.Date_come).Year == DateTime.Parse(date).Year)
                              select new
                              {
                                  idwallet = a.WalletId_Wallet,
                                  id_come = a.Id_come,
                                  name = c.NameCate,
                                  image = c.ImageCate,
                                  amount = a.Amount,
                                  date_come = a.Date_come,
                                  desciption = a.Description_come,
                                  is_come = a.Is_Come
                              };
                    var get = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
                    foreach (object l in get)
                    {
                        list.Add(l);
                    }
                    //foreach (object l in get)
                    //{
                    //    list_date.Add(list);
                    //}
                }
                list_date.Add(list);
            }
            ResponseModel res = new ResponseModel("Income", list, "200");
            return res;
        }

        // PUT: api/Income_Outcome/5
        [HttpPut("{id}")]
        public ResponseModel PutIncome_Outcome([FromRoute] int id, [FromBody] Income_Outcome income_Outcome)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }

            float amount = income_Outcome.Amount;
            string date = income_Outcome.Date_come;
            string desciption = income_Outcome.Description_come;
            bool is_come = income_Outcome.Is_Come;
            string id_cate = income_Outcome.CategoryId_Cate;
            string id_loan = income_Outcome.LoanId_Loan;
            string id_trip = income_Outcome.TripId_Trip;
            string id_wallet = income_Outcome.WalletId_Wallet;
            string id_type = income_Outcome.Id_type;
            string id_bill = income_Outcome.Id_Bill;
            string id_budget = income_Outcome.Id_Budget;
            string id_per = income_Outcome.Id_Per;
            income_Outcome.Amount = amount;
            income_Outcome.Date_come = date;
            income_Outcome.Description_come = desciption;
            income_Outcome.Is_Come = is_come;
            income_Outcome.CategoryId_Cate = id_cate.ToString();
            income_Outcome.LoanId_Loan = id_loan.ToString();
            income_Outcome.TripId_Trip = id_trip.ToString();
            income_Outcome.Id_Bill = id_bill.ToString();
            income_Outcome.Id_Budget = id_budget.ToString();
            income_Outcome.Id_Per = id_per.ToString();
            income_Outcome.Id_type = id_type.ToString();
            try
            {
                _context.Income_Outcomes.Update(income_Outcome);
                _context.Entry(income_Outcome).State = EntityState.Modified;
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Update success", null, "404");
                return res;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Income_OutcomeExists(id))
                {
                    ResponseModel res = new ResponseModel("Not found", null, "404");
                    return res;
                }
                else
                {
                    throw;
                }
            }
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
            string id_cate = income_Outcome.CategoryId_Cate;
            string id_loan = income_Outcome.LoanId_Loan;
            string id_trip = income_Outcome.TripId_Trip;
            string id_wallet = income_Outcome.WalletId_Wallet;
            string id_type = income_Outcome.Id_type;
            string id_bill = income_Outcome.Id_Bill;
            string id_budget = income_Outcome.Id_Budget;
            string id_per = income_Outcome.Id_Per;
            income.Amount = amount;
            income.Date_come = date;
            income.Description_come = desciption;
            income.Is_Come = false;
            income.CategoryId_Cate = id_cate;
            income.LoanId_Loan = id_loan;
            income.TripId_Trip = id_trip;
            income.WalletId_Wallet = id_wallet;
            income.Id_Bill = id_bill;
            income.Id_Budget = id_budget;
            income.Id_Per = id_per;
            income.Id_type = id_type;
            Wallet wallet = _context.Wallets.Where(m => m.Id_Wallet.ToString() == id_wallet).FirstOrDefault();


            if (wallet.Id_Type_Wallet != "1" && amount > wallet.Amount_now && income_Outcome.Id_type != "12" && income_Outcome.Id_type != "13" && income_Outcome.Id_type != "14" && income_Outcome.Id_type != "15" && income_Outcome.Id_type != "16" && income_Outcome.Id_type != "18") {
                ResponseModel res = new ResponseModel("Ví tiền hiện tại của bạn không đủ thực hiện thao tác này", null, "404");
                return res;
            }
   
            try
            {
                if(income.Date_come == null)
                {
                    income.Date_come = DateTime.Today.ToString();
                }    

                if(id_budget != null)
                {
                    Budget budget = _context.Budget.Where(m => m.Id_Budget.ToString() == id_budget).FirstOrDefault();
                    if (budget.time_s.Ticks <= DateTime.Parse(income.Date_come).Ticks && budget.time_e.Ticks >= DateTime.Parse(income.Date_come).Ticks) {
                        if (id_type != "12" || id_type != "13" || id_type != "14" || id_type != "15" || id_type != "16" || id_type != "18")
                        {
                            budget.Remain = budget.Remain + amount;
                        }
                        else
                        {
                            budget.Remain = budget.Remain - amount;
                        }
                        _context.Budget.Update(budget);
                    }
                }    
                if (id_bill != null)
                {
                    income.Is_Come = false;
                    Bill bill = _context.Bill.Where(m => m.Id_Bill.ToString() == id_bill).FirstOrDefault();
                    if(bill.date_s.Ticks == DateTime.Today.Ticks)
                    {
                        if (bill.id_Time == "1")
                        {
                            if (DateTime.Now.AddDays(1).Ticks > bill.date_e.Ticks)
                            {
                                bill.isPay = true;
                            }
                            else
                            {
                                bill.isPay = false;
                                bill.date_s = bill.date_s.AddDays(1);
                            }
                        }
                        if (bill.id_Time == "2") {
                            if (DateTime.Now.AddDays(7).Ticks > bill.date_e.Ticks)
                            {
                                bill.isPay = true;
                            }
                            else
                            {
                                bill.isPay = false;
                                bill.date_s = bill.date_s.AddDays(7);
                            }
                        }
                        if (bill.id_Time == "3")
                        {
                            if (DateTime.Now.AddDays(getDayOfMonth()).Ticks > bill.date_e.Ticks)
                            {
                                bill.isPay = true;
                            }
                            else
                            {
                                bill.isPay = false;
                                int month = DateTime.Now.Month;
                                switch (month)
                                {
                                    case 1:
                                    case 3:
                                    case 5:
                                    case 7:
                                    case 8:
                                    case 10:
                                    case 12:
                                        bill.date_s = bill.date_s.AddDays(31);
                                        break;

                                    case 2:
                                        int year = DateTime.Now.Year;
                                        if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))
                                        {
                                            bill.date_s = bill.date_s.AddDays(29);
                                        }
                                        else {
                                            bill.date_s = bill.date_s.AddDays(28);
                                        }
                                        break;
                                    case 4:
                                    case 6:
                                    case 9:
                                    case 11:
                                        bill.date_s = bill.date_s.AddDays(30);
                                        break;

                                }
                            }
                        }
                        if (bill.id_Time == "4")
                        {
                            if (DateTime.Now.AddYears(1).Ticks > bill.date_e.Ticks)
                            {
                                bill.isPay = true;
                            }
                            else
                            {
                                bill.isPay = false;
                                bill.date_s = bill.date_s.AddYears(1);
                            }
                        }
                        income.Description_come = "Thanh toán hóa đơn";
                        income.Date_come = DateTime.Today.ToString();
                        income.CategoryId_Cate = bill.Id_Category;
                        income.Id_type = bill.Id_Type;
                        wallet.Amount_now = wallet.Amount_now - bill.Amount_Bill;
                        income.Amount = bill.Amount_Bill;
                        _context.Bill.Update(bill);
                    }
                    else
                    {
                        ResponseModel result = new ResponseModel("Chưa tới hạn thanh toán", null, "404");
                        return result;
                    }
                    
                }

                if (id_type == "12" || id_type == "13" || id_type == "14" || id_type == "15" || id_type == "16" || id_type == "18")
                {
                    income.Is_Come = true;
                    wallet.Amount_now = wallet.Amount_now + amount;
                }
                else {
                    wallet.Amount_now = wallet.Amount_now - amount;
                }

                _context.Wallets.Update(wallet);
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

        public int getDayOfMonth()
        {
            int month = DateTime.Now.Month;
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 2:
                    int year = DateTime.Now.Year;
                    if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))
                    {
                        return 29;
                    }
                    else
                    {
                        return 28;
                    }
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;

            }
            return 30;
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