﻿using CustomerPurchases.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPurchases.Data
{
    public class PurchaseRepo : IPurchaseRepo
    {
        private readonly PurchaseDbContext _context;

        public PurchaseRepo(PurchaseDbContext context)
        {
            _context = context;
        }

        public void DeletePurchase(int id)
        {
            var purchase = _context.Purchase.FirstOrDefault(p => p.Id == id);
            _context.Purchase.Remove(purchase);
        }

        public async Task<List<Purchase>> GetAll()
        {
            return await _context.Purchase.Include(p => p.Product).ToListAsync();
        }

        public async Task<Purchase> GetPurchase(int id)
        {
            return await _context.Purchase.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Purchase>> GetPurchaseByAccount(int accId)
        {
            return await _context.Purchase.Where(p => p.AccountId == accId).ToListAsync();
        }

        public async void InsertPurchase(Purchase purchase)
        {
            await _context.Purchase.AddAsync(purchase);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdatePurchase(Purchase purchase)
        {
            _context.Entry(purchase).State = EntityState.Modified;
        }
    }
}