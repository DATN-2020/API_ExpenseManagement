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

namespace API_ExpenseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public WalletsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Wallets
        [HttpGet]
        public IEnumerable<Wallet> GetWallets([FromBody] WalletsForUser obj)
        {
            int userId = obj.User_Id;
            return _context.Wallets.Where(x => x.User_Id == userId.ToString());
        }

        // GET: api/Wallets/5
        [HttpGet("{id}")]
        public ResponseModel GetWallet([FromQuery] string id)
        {
            var log = _context.Wallets.
            Where(x => x.User_Id.Equals(id)).AsEnumerable();
            //var queryUrl = "/api/GetWallets/5?userId=" + id;  
            if (log == null)
            {
                ResponseModel res = new ResponseModel("Fail", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Wallets", log, "200");
                return res;
            }
        }

        // PUT: api/Wallets/5
        [HttpPut("{id}")]
        public ResponseModel PutWallet([FromRoute] int id, [FromBody] Wallet wallet)
        {
            string name = wallet.Name_Wallet;
            float amount = wallet.Amount_Wallet;
            string disciption = wallet.Description;
            Wallet walletFind = _context.Wallets.Where(m => m.Id_Wallet == id).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }

            if (walletFind == null)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }
            Income_Outcome income = new Income_Outcome();
            walletFind.Name_Wallet = name;
            walletFind.Description = disciption;
            if (amount < walletFind.Amount_Wallet)
            {
                income.Is_Come = false;
                walletFind.Amount_now = walletFind.Amount_Wallet - amount;
            }
            else {
                income.Is_Come = true;
                walletFind.Amount_now = amount - walletFind.Amount_Wallet;
            }
            walletFind.Amount_Wallet = amount;
            
            income.Amount = amount;
            income.Description_come = "Cập nhật ví " + name;
            income.Id_type = "16";
            income.Date_come = DateTime.Today.ToString();
            income.WalletId_Wallet = id.ToString();
            try
            {
                _context.Income_Outcomes.Add(income);
                _context.Wallets.Update(walletFind);
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Update success", null, "200");
                return res;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalletExists(id))
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

        // POST: api/wallet
        [HttpPost]
        public ResponseModel PostCreateWallet([FromBody] CreateWallet createWallet)
        {
            int user_Id = createWallet.User_Id;
            float amount = createWallet.Amount;
            string name = createWallet.Name_Wallet;
            string des = createWallet.Description;
            string typeWallet = createWallet.Id_Type_Wallet.ToString();
            Wallet insert = new Wallet();
            var check = false;
            insert.Amount_Wallet = amount;
            insert.User_Id = user_Id.ToString();
            insert.Amount_now = amount;
            if (name == null || name.Equals(""))
            {
                insert.Name_Wallet = "Ví tiền mặt";
            }
            else {
                insert.Name_Wallet = name;
            }

            if (des == null)
            {
                insert.Description = "";
            }
            else {
                insert.Description = des;
            }

            if (typeWallet == null)
            {
                insert.Id_Type_Wallet = "1";
            }
            else {
                insert.Id_Type_Wallet = typeWallet.ToString();
            }
           
            User user = _context.Users.Where(x => x.User_Id == user_Id).FirstOrDefault();
            user.Check_Wallet = true;
            try
            {
                _context.Wallets.Add(insert);
                _context.Users.Update(user);
                _context.SaveChanges();
                check = true;
            }
            catch { check = false; }
            if (check == false)
            {
                ResponseModel res = new ResponseModel("Create fail", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Create success", null, "200");
                return res;
            }
        }

        // DELETE: api/Wallets/5
        [HttpDelete("{id}")]
        public ResponseModel DeleteWallet([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }
            Wallet wallet = _context.Wallets.Find(id);
            var income = _context.Income_Outcomes
                .Where(w => w.WalletId_Wallet == id.ToString());
            foreach (Income_Outcome incomes in income)
            {
                _context.Income_Outcomes.Remove(incomes);
            }
            var budget = _context.Budget
                .Where(w => w.Id_Wallet == id.ToString());

            foreach (Budget budget1 in budget)
            {
                _context.Budget.Remove(budget1);
            }
            var per = _context.Periodic
                .Where(w => w.Id_Wallet == id.ToString());

            foreach (Periodic periodic in per)
            {
                _context.Periodic.Remove(periodic);
            }
            var bill = _context.Bill
                .Where(w => w.Id_Wallet == id.ToString());

            foreach (Bill bill1 in bill)
            {
                _context.Bill.Remove(bill1);
            }
            if (wallet != null)
            {
                _context.Wallets.Remove(wallet);
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

        private bool WalletExists(int id)
        {
            return _context.Wallets.Any(e => e.Id_Wallet == id);
        }
    }
}