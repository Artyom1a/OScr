using ConsoleApp1.Models;
using ConsoleApp1.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public class SQLFileService : ISQLFileService<MySqlConnection>
    {
        private readonly string ConnectionString = "server=localhost;database=SCRIPTSDB;uid=root;password=reghjjh236H;";
        string mypath = "C:\\Code Main\\Scripts1_2803\\ConsoleApp1\\Scripts";
        private readonly ISQLFileRepository<MySqlConnection> m_Repository;
        public SQLFileService(ISQLFileRepository<MySqlConnection> repository)
        {
            m_Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        public void RunSQLScripts(MySqlConnection connection)
        {
            SearchScriptsUnrealizedStatus(connection, NameFileOfDirectory(connection), m_Repository.GetDocs(connection));
        }


        public List<string> NameFileOfDirectory(MySqlConnection connection)
        {

            List<string> listNameFileOfDirectory = new List<string>();
            string[] fileAll = Directory.GetFiles(mypath, "*.sql", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < fileAll.Length; i++)
            {
                listNameFileOfDirectory.Add(Path.GetFileName(fileAll[i]));
            }
            return listNameFileOfDirectory;
        }



        // метод выполнения allcode
        private void FileAllCodeRealized(MySqlConnection connection, string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            connection = Connection();
            if (connection == null) throw new Exception("Connection Error");
            try
            {
                MySqlCommand command = new MySqlCommand(text, connection);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        


        // метод считывания файла(скрипта)
        public void OpenFile(MySqlConnection connection, string pathfile)
        {
            string[] AllCode = File.ReadAllText(mypath + "\\" + pathfile).Split(';');

            for (int i = 0; i < AllCode.Length; i++)
            {
                FileAllCodeRealized(connection, AllCode[i]);
            }
        }



  
        //вычитаем файлы из папки и те, что есть в бд
        public void SearchScriptsUnrealizedStatus(MySqlConnection connection, List<string> files, List<DOC> docs)
        {
            var untrackedFilesName = files.Except(docs.Select(x => x.NAME)).ToList();
            //  var untrackedFilesStatus = files.Except(docs.Select(x => x.STATUS)).ToList();

            for (int i = 0; i < untrackedFilesName.Count; i++)
            {
                if (!docs[i].NAME.Contains(untrackedFilesName[i]))
                {

                    OpenFile(connection, untrackedFilesName[i]);
                    Console.WriteLine(untrackedFilesName[i]);
                    DOC docnew = new DOC(untrackedFilesName[i], "REALIZED");
                    m_Repository.Add(connection, docnew); //несколько файлов, нужно прописать логику

                }

            }
        }

            public MySqlConnection Connection()  // вынести этот метод в утилити в будущем
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(ConnectionString);
                    connection.Open();
                    return connection;
                }

                catch (MySqlException ex)
                {
                    return null;
                }

            }

        
    }
    }
    
