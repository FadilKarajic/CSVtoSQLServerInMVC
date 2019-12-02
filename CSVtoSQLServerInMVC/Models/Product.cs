using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSVtoSQLServerInMVC.Models
{
    public class Order
    {
        
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
        public string Market { get; set; }
        public string Regions { get; set; }
       
   

    }
}