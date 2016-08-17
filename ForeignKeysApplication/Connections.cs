using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeysApplication
{
    public class Connections
    {
        public static DataTable CreateTextCommand(string queryString, string connectionString, string errorMessage, bool writeToFile)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 15;
                command.CommandType = CommandType.Text;
                command.CommandText = queryString;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (writeToFile)
                        {
                            DataTable dt = new DataTable("FK");
                            dt.Load(reader);
                            return dt;
                        }
                        else
                        {
                            while(reader.Read())
                                Console.WriteLine(String.Format("{0}, {1}, {2}, {3}, {4}", reader[0], reader[1], reader[2], reader[3], reader[4]));

                            return null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0}", errorMessage);
                        return null;
                    }
                }
            }
        }
    }
}
