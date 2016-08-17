using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeysApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var server = @"localhost\sqlexpress";
            var dataBase = "Aukcja";

            var connectionString = ConnectionStrings.SetConnectionWithIntegratedSecurity(server, dataBase);
            var queryString = Queries.FindForeignKeysWithoutIndexes();            
            var error = ConnectionErrors.NoForeignKeysError();

            var dataTable = Connections.CreateTextCommand(queryString, connectionString, error, true);
            CreateFile.CreateXLSXFile("", "", dataTable);

            Console.WriteLine("Job done. Press key to end.");
            Console.ReadKey();
        }
    }
}
