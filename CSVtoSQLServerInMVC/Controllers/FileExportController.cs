using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace CSVExport.Controllers
{
    public class FileExportController : Controller
    {
        public ActionResult Export()
        {
            return View();
        }

        [HttpGet]
        public void FileExport()
        {
            //Get connection string
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                //Select data from Customers table
                using (SqlCommand command = new SqlCommand("SELECT * FROM Customers"))
                {
                    //SQLAdapater used for mapping Fill, also enables used of commands
                    using (SqlDataAdapter adapater = new SqlDataAdapter())
                    {
                        command.Connection = connect;
                        adapater.SelectCommand = command;
                        using (DataTable table = new DataTable())
                        {
                            adapater.Fill(table);
                            //StringBuilder used to improve performance
                            StringBuilder outputFile = new StringBuilder();

                            //Loop through rows and columns, append to file
                            foreach (DataRow row in table.Rows)
                            {

                                foreach (DataColumn column in table.Columns)
                                {
                                    outputFile.Append(row[column.ColumnName].ToString().Replace(",", ";") + ',');
                                }
                                outputFile.AppendLine();

                            }

                            //Write data to CSV format
                            Response.Clear();
                            //Set to true so the output is not sent to client until writing to file is complete
                            Response.Buffer = true;
                            Response.AppendHeader("content-disposition", "attachment;filename=ExportedCSVFile.csv");
                            Response.Output.Write(outputFile);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
            
        }

        
    }
}
