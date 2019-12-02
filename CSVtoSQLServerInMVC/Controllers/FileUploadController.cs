using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data;

namespace CSVtoSQLServerInMVC.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: Home
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase readFile)
        {
            //Upload file to UploadedFiles folder, display message
            if (readFile !=null)
            {
                var fileName = Path.GetFileName(readFile.FileName);
                var filePath = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                readFile.SaveAs(filePath);
                ViewBag.Message = "File uploaded";


                //Create a DataTable and add column names and types
                DataTable table = new DataTable() ;
                            table.Columns.Add("Id", typeof(int));
                            table.Columns.Add("CustomerName", typeof(string));
                            table.Columns.Add("City", typeof(string));
                            table.Columns.Add("State", typeof(string));
                            table.Columns.Add("PostalCode", typeof(string));
                            table.Columns.Add("Country", typeof(string));
                            table.Columns.Add("Market", typeof(string));
                            table.Columns.Add("Region", typeof(string));
                

                //Read the file and loop until the end of file is reached
                StreamReader file = new StreamReader(filePath);
                while (!file.EndOfStream)
                {
                    //Split lines/rows
                    string row = file.ReadLine();
                    if (!string.IsNullOrEmpty(row))
                    {
                        string[] line = row.Split('\n');
                        table.Rows.Add();

                        //Split rows at commas
                        int i = 0;
                        foreach (string cell in row.Split(','))
                        {
                            table.Rows[table.Rows.Count - 1][i] = cell;
                            i++;
                          
                        }
                    }

                }
                

                //Get connection string
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    //Using SQLBulkCopy for performance
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connect))

                    {
                        //Destination table
                        sqlBulkCopy.DestinationTableName = "dbo.Customers";

                        //Map columns
                        sqlBulkCopy.ColumnMappings.Add("Id", "CustomerId");
                        sqlBulkCopy.ColumnMappings.Add("CustomerName", "CustomerName");
                        sqlBulkCopy.ColumnMappings.Add("City", "City");
                        sqlBulkCopy.ColumnMappings.Add("State", "State");
                        sqlBulkCopy.ColumnMappings.Add("PostalCode", "PostalCode");
                        sqlBulkCopy.ColumnMappings.Add("Country", "Country");
                        sqlBulkCopy.ColumnMappings.Add("Market", "Market");
                        sqlBulkCopy.ColumnMappings.Add("Region", "Region");

                        connect.Open();
                        sqlBulkCopy.WriteToServer(table);
                        connect.Close();
                    }
                }
              
            }

            return View();
        }
    }
}
