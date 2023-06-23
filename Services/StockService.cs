using JPGStockServer.Entities;
using JPGStockServer.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPGStockServer.Services
{

    public interface IStockService
    {

        Task<IEnumerable<Stock>> GetAllStock();
        IEnumerable<Stock> LowStock();
        IEnumerable<Stock> IgnoreList();
        void IgnoredLowStock(long id, Stock Ignored);
        Stock getLowStockBy(int id);
        bool SaveToDb();




    }
    public class StockService : IStockService
    {
        private DataContext _context;

        public StockService(DataContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Stock>> GetAllStock()
        {
           
            return await _context.Stocks.ToListAsync();
        }


        public IEnumerable<Stock> LowStock()
        {
            var result = _context.Stocks.Where(q => q.QUANTITY <= 10 && q.IGNORE == null).OrderBy(on => on.QUANTITY);
            return result;
        }

        public bool SaveToDb()
        {
            return (_context.SaveChanges() > 0);
        }

        public void IgnoredLowStock(long id, Stock Ignored)
        {
            //Noting
        }

        public Stock getLowStockBy(int id)
        {
            return _context.Stocks.FirstOrDefault(p => p.STOCK_ID == id);

        }

        public IEnumerable<Stock> IgnoreList()
        {
            var result = _context.Stocks.Where(q => q.IGNORE == 1).OrderBy(on => on.QUANTITY);
            return result;
        }
    }
}


