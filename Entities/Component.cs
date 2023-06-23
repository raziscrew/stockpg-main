using JPGStockServer.Models.Stocks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPGStockServer.Entities
{
    public class Component
    {[Key]
        public int ID { get; set; }
        public string Component_Name { get; set; }
        
    }
}
