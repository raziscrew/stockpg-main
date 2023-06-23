using JPGStockServer.Entities;
using JPGStockServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JPGStockServer.Services
{

    public interface IModuleService
    {
        IEnumerable<Stock> GetAllModules();


        IEnumerable<Stock> SearchByPartNumber(string Partnumbers);

        void UpdateQuantity(long id, Stock Quantity);

        void Update(long id, Stock update);

        void Add(Stock Add);

        void Delete(Stock Delete);

        Stock ModuleExists(long id);


        bool SaveToDb();
    }


    public class ModuleService : IModuleService
    {
        private DataContext _context;
        public ModuleService(DataContext context)
        {
            _context = context;
        }

        public void Add(Stock Add)
        {

            if (Add == null)

            {
                throw new ArgumentNullException(nameof(Add));
            }
            _context.Stocks.Add(Add);



        }

        public Stock ModuleExists(long id)
        {
            return _context.Stocks.FirstOrDefault(e => e.STOCK_ID == id);
        }


        public void Delete(Stock Delete)
        {
            if (Delete == null)

            {
                throw new ArgumentNullException(nameof(Add));
            }
            _context.Stocks.Remove(Delete);
        }

        public IEnumerable<Stock> GetAllModules()
        {
            var result = _context.Stocks.Where(q => q.COMPONENTS_ID == "MODULE").OrderBy(on => on.STOCK_ID);
            return result;
        }

        public bool SaveToDb()
        {
            return (_context.SaveChanges() > 0);
        }

        public IEnumerable<Stock> SearchByPartNumber(string Partnumbers)
        {
            var PartnumberS = _context.Stocks.Where(x => x.PART_NUMBER.ToLower().Contains(Partnumbers.ToLower()) && x.COMPONENTS_ID == "MODULE").ToList();


            return PartnumberS;

        }

        public void Update(long id, Stock update)
        {
            //Noting
        }

        public void UpdateQuantity(long id, Stock Quantity)
        {
            //Noting
        }
    }
}
