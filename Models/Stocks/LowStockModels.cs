using System;

namespace JPGStockServer.Models.Stocks
{
    public class LowStockModels
    {

        public Int32 STOCK_ID { get; set; }

        public String PART_NUMBER { get; set; }

        public String LOCATION { get; set; }

        public Int64? QUANTITY { get; set; }

        public Int32? IGNORE { get; set; }

    }
}
