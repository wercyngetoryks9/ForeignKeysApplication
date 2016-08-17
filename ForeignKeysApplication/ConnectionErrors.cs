using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeysApplication
{
    public class ConnectionErrors
    {
        public static string NoForeignKeysError()
        {
            var message = "No foreign keys was found.";
            return message;
        }

        public static string NoLocationFound()
        {
            var message = "No location was found. Do you wish to create one?";
            return message;
        }
    }
}
