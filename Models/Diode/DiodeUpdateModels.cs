using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Diode
{
    public class DiodeUpdateModels
    {
        [Required]
        public Int64 STOCK_ID { get; set; }

        public String PART_NUMBER { get; set; }

        public Double? VOLTAGE { get; set; }

        public Double? AMPERE { get; set; }

        public Double? WATT { get; set; }

        public String TYPE { get; set; }

        public String PACKAGING { get; set; }

        public String LOCATION { get; set; }

        public Int32? IGNORE { get; set; }

        public Int64? QUANTITY { get; set; }

        public DateTime UpdateDate { get; set; }

        public String User { get; set; }
    }
}
