using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace azuretestapp.Model
{
    public static class AppSetting
    {
        public static string Database_Server { get; private set; }
        public static string Database_Port { get; private set; }
        public static string Database_Name { get; private set; }
        public static string Database_Collection { get; set; }

        public static void LoadSetting()
        {
            Database_Server = Environment.GetEnvironmentVariable("DB_HOST");
            Database_Port = Environment.GetEnvironmentVariable("DB_PORT");
            Database_Name = Environment.GetEnvironmentVariable("DB_NAME");
            Database_Collection = Environment.GetEnvironmentVariable("DB_COLLECTION");
        }
    }
}
