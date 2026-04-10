using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using Microsoft.Data.SqlClient;

namespace sql_to_csv
{
    
    public class xml
    {
        public static void xmlE( string conn, string que, string path)
        {
            var dataAdapter = new SqlDataAdapter(que,conn);
            {
                var dataset = new DataSet("data");
                dataAdapter.Fill(dataset, "tabela");
                dataset.WriteXml(path, XmlWriteMode.WriteSchema);
            }
            Console.WriteLine("Zapisane XML");
        }
        
    }
}
