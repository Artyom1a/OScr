using ConsoleApp1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public interface ISQLFileService<T> where T : DbConnection
    {

        
        //1. старт программы

        void RunSQLScripts(T connection);

        //2. Достаём все файлы из папки
        List<string> NameFileOfDirectory(T connection);
      

        //3.вычитаем файлы из папки и те, что есть в бд
        void SearchScriptsUnrealizedStatus(T connection,List<string> files, List<DOC> docs);

        //4. метод считывания файла(скрипта), если его нет в базе данных
        void OpenFile(T connection, string pathfile);

    }
}
