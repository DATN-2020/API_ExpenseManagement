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
    public class BillsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public BillsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public IEnumerable<Bill> GetBill()
        {
            return _context.Bill;
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public ResponseModel GetBill([FromQuery] string id_wallet, string id_bill, string date)
        {
            var list = new ArrayList();
            var income = _context.Income_Outcomes
                .Where(w => w.WalletId_Wallet == id_wallet).Where(t => t.Id_Bill == id_bill);
            foreach (Income_Outcome income_ in income)
            {
                if (income_.CategoryId_Cate == null)
                {
                    var log = from a in _context.Income_Outcomes
                              join c in _context.TypeCategories
                              on a.Id_type equals c.Id_type.ToString()
                              where (a.CategoryId_Cate == null
                              && a.Id_come == income_.Id_come
                              && a.Id_Bill == id_bill)
                              select new
                              {
                                  idwallet = a.WalletId_Wallet,
                                  id_come = a.Id_come,
                                  id_bill = a.Id_Bill,
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
                              && a.Id_Bill == id_bill)
                              select new
                              {
                                  idwallet = a.WalletId_Wallet,
                                  id_come = a.Id_come,
                                  id_bill = a.Id_Bill,
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
                }
            }
            ResponseModel res = new ResponseModel("Income", list, "200");
            return res;
        }

        // PUT: api/Bills/5
        [HttpPut("{id}")]
        public ResponseModel PutBill([FromRoute] int id, [FromBody] Bill bill)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }
            float amount = bill.Amount_Bill;
            string disciption = bill.Desciption;
            DateTime date_e = bill.date_e;
            DateTime date_s = bill.date_s;
            bool isEdit = bill.isEdit;
            string id_time = bill.id_Time;
            bill = _context.Bill.Where(m => m.Id_Bill == id).FirstOrDefault();
            bill.Amount_Bill = amount;
            bill.Desciption = disciption;
            bill.date_e = date_e;
            bill.date_s = date_s;
            bill.isEdit = isEdit;
            bill.id_Time = id_time;
            try
            {
                if(bill.isEdit == false)
                {
                    bill.isPay = true;
                }
                _context.Bill.Update(bill);
                _context.Entry(bill).State = EntityState.Modified;
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Update success", null, "404");
                return res;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
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

        // POST: api/Bills
        [HttpPost]
        public ResponseModel PostBill([FromBody] Bill bill)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Fail", null, "200");
                return res;
            }
            bill.isPay = false;
            bill.isFinnish = false;
            try
            {
                _context.Bill.Add(bill);
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Create success", null, "200");
                return res;
            }
            catch
            {
                ResponseModel res = new ResponseModel("Create fail", null, "200");
                return res;
            }
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public ResponseModel DeleteBill([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }

            var bill = _context.Bill.Find(id);
            if (bill == null)
            {
                ResponseModel res = new ResponseModel("Not found", null, "404");
                return res;
            }
            else
            {
                _context.Bill.Remove(bill);
                var income = _context.Income_Outcomes
                .Where(w => w.Id_Bill == id.ToString());
                foreach (Income_Outcome incomes in income)
                {
                    _context.Income_Outcomes.Remove(incomes);
                }
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Delete success", null, "200");
                return res;
            }
        }

        private bool BillExists(int id)
        {
            return _context.Bill.Any(e => e.Id_Bill == id);
        }
    }
}