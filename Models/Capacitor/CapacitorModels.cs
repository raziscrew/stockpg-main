using System;

namespace JPGStockServer.Models.Stocks
{
    public class CapacitorModels
    {
        public Int64 STOCK_ID { get; set; }

        public String PART_NUMBER { get; set; }

        public String CAPACITANCE { get; set; }

        public Double? VOLTAGE { get; set; }

        public Int64? QUANTITY { get; set; }

        public String LOCATION { get; set; }

        public Int32? IGNORE { get; set; }
    
       // public String CreateDate { get; set; }
       
        public DateTime UpdateDate { get; set; }

        public String User { get; set; }
    }
}
