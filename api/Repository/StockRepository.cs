using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.Include(s => s.Comments).ToListAsync();
        }

        async Task<Stock> IStockRepository.CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        async Task<Stock?> IStockRepository.DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        async Task<Stock?> IStockRepository.GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        async Task<Stock?> IStockRepository.UpdateAsync(int id, UpdateStockRequestDto stock)
        {
            var existingStock = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if(existingStock == null)
            {
                return null;
            }

            existingStock.CompanyName = stock.CompanyName;
            existingStock.Symbol = stock.Symbol;
            existingStock.Purchase = stock.Purchase;
            existingStock.Industry = stock.Industry;
            existingStock.MarketCap = stock.MarketCap;
            existingStock.LastDiv = stock.LastDiv;

            await _context.SaveChangesAsync();
            return existingStock;
        }
        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}