using JPGStockServer.Entities;
using JPGStockServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JPGStockServer.Services
{

    public interface IResistorService
    {
        IEnumerable<Stock> GetAllResistors();


        IEnumerable<Stock> SearchByResistance(string Resistances);

        void UpdateQuantity(long id, Stock Quantity);

        void Update(long id, Stock update);

        void Add(Stock Add);

        void Delete(Stock Delete);

        Stock ResistorExists(long id);


        bool SaveToDb();
    }


    public class ResistorService : IResistorService
    {
        private DataContext _context;
        public ResistorService(DataContext context)
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

        public Stock ResistorExists(long id)
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

        public IEnumerable<Stock> GetAllResistors()
        {
            var result = _context.Stocks.Where(q => q.COMPONENTS_ID == "RESISTOR").OrderBy(on => on.STOCK_ID);
            return result;
        }

        public bool SaveToDb()
        {
            return (_context.SaveChanges() > 0);
        }

        public IEnumerable<Stock> SearchByResistance(string Resistances)
        {
            var ResistanceS = _context.Stocks.Where(x => x.RESISTANCE.ToLower().Contains(Resistances.ToLower())).ToList();


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
