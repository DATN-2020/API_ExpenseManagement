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
    public class SavingWalletsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public SavingWalletsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/SavingWallets
        [HttpGet]
        public IEnumerable<SavingWallet> GetSavingWallet()
        {
            return _context.SavingWallet;
        }

        // GET: api/SavingWallets/5
        [HttpGet("{id}")]
        public ResponseModel GetSavingWallet([FromQuery] string id)
        {
            var saving = _context.SavingWallet.Where(w => w.id_user.ToString() == id);
            var list = new ArrayList();
            if(saving == null)
            {
                ResponseModel res1 = new ResponseModel("Saving wallets", null, "200");
                return res1;
            }
            foreach (SavingWallet saving1 in saving)
            {
                if (DateTime.Parse(saving1.date_e) <= DateTime.Today)
                {
                    saving1.is_Finnish = true;
                    _context.SavingWallet.Update(saving1);
                }
            }
            _context.SaveChanges();
            var saving_ = _context.SavingWallet.Where(w => w.id_user.ToString() == id);
            foreach (SavingWallet savingWallet in saving_)
                {
                var log = from a in _context.SavingWallet
                              join b in _context.Bank
                              on a.id_bank equals b.Id_Bank.ToString()
                              where(a.id_saving == savingWallet.id_saving)
                              select new
                              {
                                  id_saving = a.id_saving,
                                  name = a.name_saving,
                                  date_e = a.date_e,
                                  date_s = a.date_s,
                                  price = a.price,
                                  price_end = a.price_end,
                                  is_Finish = a.is_Finnish,
                                  name_bank = b.Name_Bank,
                                  interest = b.Interest,
                                  id_user = a.id_user
                              };
                    var get = log.Where(m => m.id_user.Equals(id)).AsEnumerable();
                    foreach (object l in get)
                    {
                        list.Add(l);
                    }
                }
                
                ResponseModel res = new ResponseModel("Saving wallets", list, "200");
                return res;
        }

        // PUT: api/SavingWallets/5
        [HttpPut("{id}")]
        public ResponseModel PutSavingWallet(/*[FromRoute] string id, */[FromBody] SavingWallet savingWallet)
        {
            string id_saving = savingWallet.id_saving.ToString();
            string date = savingWallet.date_s;
            savingWallet.is_Finnish = true;
            var log = from a in _context.SavingWallet
                      join b in _context.Bank
                      on a.id_bank equals b.Id_Bank.ToString()
                      where (a.id_saving.ToString() == id_saving)
                      select new SavingWallet
                      {
                          id_saving = a.id_saving,
                          is_Finnish = true,
                          price_end = (DateTime.Parse(a.date_s).Year == DateTime.Parse(date).Year) ? a.price :
                          ((365 - DateTime.Parse(a.date_s).DayOfYear) + DateTime.Parse(date).DayOfYear) < 365 ? a.price :
                          ((DateTime.Parse(date).Year) - (DateTime.Parse(a.date_s).Year)) *
                          ((float)b.Interest) * (a.price) + a.price
                      };   
            SavingWallet saving = log.Where(m => m.id_saving.ToString() == id_saving).FirstOrDefault();
            savingWallet = _context.SavingWallet.Where(m => m.id_saving.ToString() == id_saving).FirstOrDefault();
            if (savingWallet == null)
            {
                ResponseModel res = new ResponseModel("Fail", null, "404");
                return res;
            }
            savingWallet.price_end = saving.price_end;
            savingWallet.is_Finnish = saving.is_Finnish;
            _context.SavingWallet.Update(savingWallet);
            _context.SaveChanges();
            ResponseModel res1 = new ResponseModel("Success", null, "404");
            return res1;
        }

        // POST: api/SavingWallets
        [HttpPost]
        public ResponseModel PostSavingWallet([FromBody] SavingWallet savingWallet)
        {
            string name = savingWallet.name_saving;
            string date_s = savingWallet.date_s;
            string date_e = savingWallet.date_e;
            float price = savingWallet.price;
            string id_bank = savingWallet.id_bank;
            string id_user = savingWallet.id_user;
            if(savingWallet == null)
            {
                ResponseModel res1 = new ResponseModel("Create fail", null, "404");
                return res1;
            }
            if(id_user == null)
            {
                savingWallet.id_user = "1";
            }    
            savingWallet.name_saving = name;
            savingWallet.date_e = date_e;
            savingWallet.date_s = date_s;
            savingWallet.price = price;
            savingWallet.price_end = 0;
            savingWallet.is_Finnish = false;
            savingWallet.id_bank = id_bank;
            savingWallet.id_user = id_user;
            _context.SavingWallet.Add(savingWallet);
            _context.SaveChanges();
            ResponseModel res = new ResponseModel("Create success", null, "404");
            return res;
        }

        // DELETE: api/SavingWallets/5
        [HttpDelete("{id}")]
        public ResponseModel DeleteSavingWallet([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }

            SavingWallet savingWallet = _context.SavingWallet.Find(id);
            var trans = _context.Transactions
                .Where(w => w.id_saving == id.ToString());
            foreach (Transactions transactions in trans)
            {
                _context.Transactions.Remove(transactions);
            }
            if (savingWallet != null)
            {
                _context.SavingWallet.Remove(savingWallet);
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Delete success", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }
        }

        private bool SavingWalletExists(int id)
        {
            return _context.SavingWallet.Any(e => e.id_saving == id);
        }
    }
}