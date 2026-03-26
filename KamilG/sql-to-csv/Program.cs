using System;
using Microsoft.Data.SqlClient;

internal class Program
{
    static void Main()
    {
        Connect();
        Console.ReadKey();
    }

    static void Connect()
    {
        string constr;

        // for the connection to 
        // sql server database
        SqlConnection conn;

        // Data Source is the name of the 
        // server on which the database is stored.
        // The Initial Catalog is used to specify
        // the name of the database
        // The UserID and Password are the credentials
        // required to connect to the database.
        constr = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=""SQL Server Management Studio"";Command Timeout=0";

        conn = new SqlConnection(constr);

        // to open the connection
        conn.Open();

        Console.WriteLine("Connection Open!");