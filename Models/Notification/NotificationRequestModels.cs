using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Notification
{
    public class NotificationRequestModels
    {
        public Int64 NOTIFICATIONS_ID { get; set; }

        public Int64 READ { get; set; }

        public String PART_NUMBER { get; set; }

        public String LOCATION { get; set; }

        public Int64? QUANTITY { get; set; }

        public string COMPONENTS_ID { get; set; }

        public String User { get; set; }

        public Int64 STOCKID { get; set; }

        public Int64 Request_Quantitys { get; set; }


        public DateTime CreateDate { get; set; }
    }
}
