using JPGStockServer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPGStockServer.Models.Stocks
{
    public class StockModels
    {
        public Int64 STOCK_ID { get; set; }

        public String PART_NUMBER { get; set; }

        public Double? VOLTAGE { get; set; }

        public Double? AMPERE { get; set; }

        public Double? WATT { get; set; }

        public String TYPE { get; set; }

        public String PACKAGING { get; set; }

        public String LOCATION { get; set; }

        public Int64? QUANTITY { get; set; }

        public String SERIES { get; set; }

        public String HMI_MODEL { get; set; }

        public String BRAND { get; set; }

        public Double? Vds { get; set; }

        public Double? Ic { get; set; }

        public string? RESISTANCE { get; set; }

        public Double? Vce { get; set; }

        public string CAPACITANCE { get; set; }

        public string COMPONENTS_ID { get; set; }

        public Int64? IGNORE { get; set; }
        
        public DateTime? CreateDate { get; set; }

      
        public DateTime? UpdateDate { get; set; }

        public String User { get; set; }


    }
}
