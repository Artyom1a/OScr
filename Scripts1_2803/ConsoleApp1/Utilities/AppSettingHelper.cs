using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utilities
{
    public static class AppSettingHelper
    {
        private static string _connectionstring;
        public static string ConnectionString
        {
            get
            {
                if (_connectionstring == null)
                {
                    _connectionstring = "Text"; // из апсетинг джсон вызывать и сюда его выводить
                }
                return _connectionstring;
            }
        }



    }
}
