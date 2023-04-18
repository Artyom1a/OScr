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
            //DB query = new DB();
            //DOC doc = new DOC() { NAME = "Doc2", STATUS = "UNREALIZED" };
            //Console.WriteLine(query.Add(doc));
            //Console.ReadKey();



            //List<DOC> docs = query.GetDocs();
            //for (int i = 0; i < docs.Count; i++)
            //{
            //    doc = docs[i];
            //    Console.WriteLine(doc);
            //}

            //DB db = new DB();
            //db.Service();




            //SQLFileRepository repository = new SQLFileRepository();
            //List<DOC> docs = new List<DOC>()
            //{
            //    new DOC("1","unrealized")
            //    };
            //repository.AddRange(repository.Connection(), docs);


            //repository.GetDocs(repository.Connection());


            SQLFileService service = new SQLFileService(); //должно это остаться 
            service.RunSQLScripts(service.Connection());
        }


    }
}
