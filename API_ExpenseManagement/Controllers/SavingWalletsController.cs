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
            var saving = _context.SavingWallet.Where(w => w.id_saving.ToString() == id);
            var list = new ArrayList();
            foreach (SavingWallet savingWallet in saving)
            {
                if (DateTime.Parse(savingWallet.date_e) <= DateTime.Today)
                {
                    savingWallet.is_Finnish = true;
                    _context.SavingWallet.Update(savingWallet);
                }
            }
            _context.SaveChanges();
            foreach (SavingWallet savingWallet in saving)
            {
                var log = from a in _context.SavingWallet
                          join b in _context.Bank
                          on a.id_bank equals b.Id_Bank.ToString()
                          select new
                          {
                              id_saving = a.id_saving,
                              name = a.name_saving,
                              price = a.price,
                              date_s = a.date_s,
                              date_e = a.date_e,
                              is_finnish = a.is_Finnish,
                              name_bank = b.Name_Bank,
                              interest = b.Interest
                          };
                var get = log.Where(m => m.id_saving.Equals(id)).AsEnumerable();
                foreach (object l in get)
                {
                    list.Add(l);
                }
            }
            ResponseModel res1 = new ResponseModel("Saving Wallet", list, "200");
            return res1;
        }

        // PUT: api/SavingWallets/5
        [HttpPut("{id}")]
        public ResponseModel PutSavingWallet([FromRoute] string id, [FromBody] SavingWallet savingWallet)
        {
            string name = savingWallet.name_saving;
            string date_s = savingWallet.date_s;
            string date_e = savingWallet.date_e;
            float price = savingWallet.price;
            string id_bank = savingWallet.id_bank;
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }

            //if (id != savingWallet.id_saving.ToString())
            //{
            //    ResponseModel res = new ResponseModel("Update fail", null, "404");
            //    return res;
            //}
            savingWallet = _context.SavingWallet.Where(m => m.id_saving.ToString() == id).FirstOrDefault();
            if(savingWallet == null)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }    
            savingWallet.name_saving = name;
            savingWallet.date_e = date_e;
            savingWallet.date_s = date_s;
            savingWallet.price = price;
            savingWallet.id_bank = id_bank;
            try
            {
                _context.SavingWallet.Update(savingWallet);
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Update success", null, "404");
                return res;
            }
            catch (DbUpdateConcurrencyException)
            {
                ResponseModel res = new ResponseModel("Not found", null, "404");
                return res;
            }
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
            if(savingWallet == null)
            {
                ResponseModel res1 = new ResponseModel("Create fail", null, "404");
                return res1;
            }
            savingWallet.name_saving = name;
            savingWallet.date_e = date_e;
            savingWallet.date_s = date_s;
            savingWallet.price = price;
            savingWallet.price_end = 0;
            savingWallet.is_Finnish = false;
            savingWallet.id_bank = id_bank;
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