using JPGStockServer.Entities;
using JPGStockServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JPGStockServer.Services
{

    public interface ITransistorService
    {
        IEnumerable<Stock> GetAllTransistors();


        IEnumerable<Stock> SearchByPartNumber(string Partnumbers);

        void UpdateQuantity(long id, Stock Quantity);

        void Update(long id, Stock update);

        void Add(Stock Add);

        void Delete(Stock Delete);

        Stock TransistorExists(long id);


        bool SaveToDb();
    }


    public class TransistorService : ITransistorService
    {
        private DataContext _context;
        public TransistorService(DataContext context)
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

        public Stock TransistorExists(long id)
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

        public IEnumerable<Stock> GetAllTransistors()
        {
            var result = _context.Stocks.Where(q => q.COMPONENTS_ID == "TRANSISTOR").OrderBy(on => on.STOCK_ID);
            return result;
        }

        public bool SaveToDb()
        {
            return (_context.SaveChanges() > 0);
        }

        public IEnumerable<Stock> SearchByPartNumber(string Partnumbers)
        {
            var ResistanceS = _context.Stocks.Where(x => x.PART_NUMBER.ToLower().Contains(Partnumbers.ToLower()) && x.COMPONENTS_ID == "TRANSISTOR").ToList();


            return ResistanceS;

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
