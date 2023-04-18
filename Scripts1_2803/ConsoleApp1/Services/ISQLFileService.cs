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
        //2. Достаём все файлы из папки
        //3.вычитаем файлы из папки и те, что есть в бд
        //4. метод считывания файла(скрипта), если его нет в базе данных
        //5. 



        void RunSQLScripts(T connection);


        List<string> NameFileOfDirectory(T connection);
        void OpenFile(T connection, string pathfile);


        void SearchScriptsUnrealizedStatus(List<string> files, List<DOC> docs);

    }
}
