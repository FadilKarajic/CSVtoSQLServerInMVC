using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSVtoSQLServerInMVC.Models;
using System.Data.Entity;

namespace CSVtoSQLServerInMVC.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Name=ConnectionString")
        {

        }
        public DbSet<Order> Orders { get; set; }
    }
}