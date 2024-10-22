using StockMaster.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMaster
{
    internal class PortfolioManager
    {
        private readonly static StockContext _context = new StockContext(); // Контекст базы данных для работы с Entity Framework

      

      

        public static void AddOrUpdateStock(int portfolioId, int stockId, int quantity, decimal purchasePrice)
        {
            var portfolioStock = _context.PortfolioStocks
                .FirstOrDefault(ps => ps.PortfolioId == portfolioId && ps.StockId == stockId);

            if (portfolioStock != null)
            {
                portfolioStock.Quantity += quantity;
                portfolioStock.PurchasePrice = purchasePrice; // Обновляем цену (если нужно)
            }
            else
            {
                // Если акции нет, добавляем новую
                portfolioStock = new PortfolioStock
                {
                    PortfolioId = portfolioId,
                    StockId = stockId,
                    Quantity = quantity,
                    PurchasePrice = purchasePrice,
                    PurchaseDate = DateOnly.FromDateTime(DateTime.Now)
                };
                _context.PortfolioStocks.Add(portfolioStock);
            }

            // Обновляем общую стоимость портфолио
            UpdateTotalValue(portfolioId);

            _context.SaveChanges();
        }

        private static void UpdateTotalValue(int portfolioId)
        {
            var totalValue = _context.PortfolioStocks
                .Where(ps => ps.PortfolioId == portfolioId)
                .Sum(ps => (ps.Quantity * ps.PurchasePrice) ?? 0);

            var portfolio = _context.Portfolios.Find(portfolioId);
            if (portfolio != null)
            {
                portfolio.TotalValue = totalValue;
                _context.SaveChanges();
            }
        }
    }
}


