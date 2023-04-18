using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using ConsoleApp1.Services;
using ConsoleApp1.Models;
using ConsoleApp1.Repositories;
using MySql.Data.MySqlClient;

namespace CsSqlSearchscripts
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*SQLFileRepository repository = new SQLFileRepository();
             List<DOC> docs = new List<DOC>()
         {
            new DOC("1","unrealized")
          };
         repository.AddRange(repository.Connection(), docs);
               repository.GetDocs(repository.Connection());*/

            SQLFileService service = new SQLFileService(new SQLFileRepository()); 
            service.RunSQLScripts(service.Connection());


        }


    }
}
