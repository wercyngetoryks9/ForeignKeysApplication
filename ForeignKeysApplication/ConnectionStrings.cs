using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeysApplication
{
    public class ConnectionStrings
    {
        public static string SetConnectionWithPassword(string server, string dataBase, string user, string password)
        {
            string connectionString = "Data Source=" + server + ";";
                   connectionString += "Initial Catalog=" + dataBase + ";";
                   connectionString += "User id=" + user + ";";
                   connectionString += "Password=" + password + ";";

            return connectionString;
        }

        public static string SetConnectionWithIntegratedSecurity(string server, string dataBase)
        {
            string connectionString = "Data Source=" + server + ";";
                   connectionString += "Initial Catalog=" + dataBase + ";";
                   connectionString += "Integrated Security= SSPI;";

            return connectionString;
        }
    }
}
