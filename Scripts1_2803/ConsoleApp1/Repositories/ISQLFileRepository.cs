using ConsoleApp1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repositories
{
    public interface ISQLFileRepository<T> where T : DbConnection 
    {

        
       
        //4. создание конекшн - (можно тут, можно не тут)

        //1. Проверить таблицу в бд и создать, если нет
        void CheckAndCreateTable(T connection );

        //2. Вернуть все записи из таблицы
        List<DOC> GetDocs(T connection);
        //3. Добавление новой записи
        void Add(T connection,DOC doc);


    }
}
