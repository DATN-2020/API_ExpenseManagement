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
    public class PeriodicsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public PeriodicsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Periodics
        [HttpGet]
        public IEnumerable<Periodic> GetPeriodic()
        {
            return _context.Periodic;
        }

        // GET: api/Periodics/5
        [HttpGet("{id}")]
        public ResponseModel GetPeriodic([FromQuery] string id_wallet, string id_per, string date)
        {
            var list = new ArrayList();
            var income = _context.Income_Outcomes
                .Where(w => w.WalletId_Wallet == id_wallet).Where(t => t.Id_Per == id_per);
            foreach (Income_Outcome income_ in income)
            {
                if (income_.CategoryId_Cate == null)
                {
                    var log = from a in _context.Income_Outcomes
                              join c in _context.TypeCategories
                              on a.Id_type equals c.Id_type.ToString()
                              where (a.CategoryId_Cate == null
                              && a.Id_come == income_.Id_come
                              && a.Id_Per == id_per)
                              select new
                              {
                                  idwallet = a.WalletId_Wallet,
                                  id_come = a.Id_come,
                                  id_per = a.Id_Per,
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
                              && a.Id_Per == id_per)
                              select new
                              {
                                  idwallet = a.WalletId_Wallet,
                                  id_come = a.Id_come,
                                  id_per = a.Id_Per,
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

        // PUT: api/Periodics/5
        [HttpPut("{id}")]
        public ResponseModel PutPeriodic([FromRoute] int id, [FromBody] Periodic periodic)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Fail", null, "200");
                return res;
            }

            float amount = periodic.Amount_Per;
            string disciption = periodic.Desciption;
            DateTime date_e = periodic.date_e;
            DateTime date_s = periodic.date_s;
            string id_time = periodic.id_Time;
            periodic = _context.Periodic.Where(m => m.Id_Per == id).FirstOrDefault();
            periodic.Amount_Per = amount;
            periodic.Desciption = disciption;
            periodic.date_e = date_e;
            periodic.date_s = date_s;
            periodic.id_Time = id_time;
            periodic.isComeback = false;
            //if (periodic.Id_Wallet == 0)
            //{
            //    periodic.Id_Wallet = 1;
            //}
            //if (periodic.Id_Cate == 0)
            //{
            //    periodic.Id_Cate = 1;
            //}
            //if (periodic.Id_Type == 0)
            //{
            //    periodic.Id_Type = 1;
            //}
            //if (periodic.id_Time == 0)
            //{
            //    periodic.id_Time = 1;
            //}
            try
            {
                _context.Entry(periodic).State = EntityState.Modified;
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Update success", null, "200");
                return res;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodicExists(id))
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

        // POST: api/Periodics
        [HttpPost]
        public ResponseModel PostPeriodic([FromBody] Periodic periodic)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Fail", null, "200");
                return res;
            }
         
            periodic.isComeback = false;
            if(periodic != null)
            {
                _context.Periodic.Add(periodic);
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Create success", null, "200");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Create fail", null, "200");
                return res;
            }
        }

        // DELETE: api/Periodics/5
        [HttpDelete("{id}")]
        public ResponseModel DeletePeriodic([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }

            var per = _context.Periodic.Find(id);
            if (per == null)
            {
                ResponseModel res = new ResponseModel("Not found", null, "404");
                return res;
            }
            else
            {
                _context.Periodic.Remove(per);
                var income = _context.Income_Outcomes
                .Where(w => w.Id_Per == id.ToString());
                foreach (Income_Outcome incomes in income)
                {
                    _context.Income_Outcomes.Remove(incomes);
                }
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Delete success", null, "200");
                return res;
            }
        }

        private bool PeriodicExists(int id)
        {
            return _context.Periodic.Any(e => e.Id_Per == id);
        }
    }
}