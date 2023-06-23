using JPGStockServer.Entities;
using JPGStockServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JPGStockServer.Services
{
    public interface ICapacitorService
    {
        IEnumerable<Stock> GetAllCapacitors();


        IEnumerable<Stock> SearchByCapacitance(string CapacitanceValue);

        void UpdateQuantity(long id, Stock Quantity);
        //   Task<List<CapacitorUpdateQuantityModels>> UpdateQuantity(long id, Stock Quantity);
        void Update(long id, Stock update);

        void Add(Stock Add);

        void Delete(Stock Delete);

        Stock CapacitorExists(long id);
        Stock QuantitynotNull(long QuantityNotNull);


        bool SaveToDb();
    }


    public class CapacitorService : ICapacitorService
    {
        private DataContext _context;
        public CapacitorService(DataContext context)
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

        public Stock CapacitorExists(long id)
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

        public IEnumerable<Stock> GetAllCapacitors()
        {
            var result = _context.Stocks.Where(q => q.COMPONENTS_ID == "CAPACITOR").OrderBy(on => on.STOCK_ID);
            return result;
        }

        public Stock QuantitynotNull(long QuantityNotNull)
        {
            return _context.Stocks.FirstOrDefault(e => e.QUANTITY == QuantityNotNull);
        }

        public bool SaveToDb()
        {
            return (_context.SaveChanges() > 0);
        }

        public IEnumerable<Stock> SearchByCapacitance(string CapacitanceValue)
        {
            var capacitanceS = _context.Stocks.Where(x => x.CAPACITANCE.ToLower().Contains(CapacitanceValue.ToLower())).ToList();

            return capacitanceS;

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
