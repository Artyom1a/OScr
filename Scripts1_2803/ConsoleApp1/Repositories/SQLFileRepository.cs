using ConsoleApp1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repositories
{
    public class SQLFileRepository : ISQLFileRepository<MySqlConnection>
    {

        private readonly string ConnectionString = "server=localhost;database=SCRIPTSDB;uid=root;password=reghjjh236H;";
        private readonly string SQL_selectItems = "select Id,NAME,STATUS from SCRIPTS;";
        private readonly string SQL_insertItem = "insert into SCRIPTS(NAME, STATUS) values {0};";
        private readonly string SQL_createItem = "CREATE TABLE IF NOT EXISTS SCRIPTS(\r\nID INT auto_increment PRIMARY KEY,\r\nNAME VARCHAR(20) NOT NULL,\r\nSTATUS varchar(20) DEFAULT 'UNREALIZED'\r\n); SCRIPTS(NAME, STATUS) values {0};";
        //  string mypath = "C:\\Code Main\\Scripts1_2803\\ConsoleApp1\\Scripts";

        // метод  этот, добавляет запись в БД 


        public void Add(MySqlConnection connection, DOC doc)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            connection = Connection();
            if (connection == null) throw new Exception("connection is null");
            try
            {
                MySqlCommand command = new MySqlCommand(string.Format(SQL_insertItem, "(@name0, @type0)"), connection);
                command.Parameters.AddWithValue("@name0", doc.NAME);
                command.Parameters.AddWithValue("@type0", doc.STATUS);
                command.ExecuteNonQuery();
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


        public void CheckAndCreateTable(MySqlConnection connection)
        {
            connection = Connection();
            if (connection == null) throw new Exception("connection is null");
            try
            {
                MySqlCommand command = new MySqlCommand(SQL_createItem, connection);

                command.ExecuteNonQuery();

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

        //++ забираем все имена файлов из бд 
        public List<DOC> GetDocs(MySqlConnection connection)
        {
            connection = Connection();
            if (connection == null) throw new Exception("connection is null");
            try
            {
                MySqlCommand command = new MySqlCommand(SQL_selectItems, connection);
                List<DOC> docs = new List<DOC>();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DOC doc = new DOC();
                    doc.ID = reader.GetInt32(0);
                    doc.NAME = reader.GetString(1);
                    doc.STATUS = reader.GetString(2);
                    docs.Add(doc);
                }
                return docs;
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









        //    // метод забираем названия файлов, которые нам нужны
        //    //++
        //    private List<string> NameFileOfDirectory()
        //    {

        //        List<string> listNameFileOfDirectory = new List<string>();
        //        string[] fileAll = Directory.GetFiles(mypath, "*.sql", SearchOption.TopDirectoryOnly);
        //        for (int i = 0; i < fileAll.Length; i++)
        //        {
        //            listNameFileOfDirectory.Add(Path.GetFileName(fileAll[i]));
        //        }
        //        return listNameFileOfDirectory;
        //    }

        //    //++


        //    // метод считывания файла(скрипта)
        //    private void OpenFile(string pathfile)
        //    {
        //        string[] AllCode = File.ReadAllText(mypath + "\\" + pathfile).Split(';');

        //        for (int i = 0; i < AllCode.Length; i++)
        //        {
        //            FileAllCodeRealized(AllCode[i]);
        //        }
        //    }


        //    // метод выполнения allcode
        //    private void FileAllCodeRealized(string text)
        //    {
        //        if (text == null) throw new ArgumentNullException(nameof(text));
        //        MySqlConnection connection = Connection();
        //        if (connection == null) throw new Exception("Connection Error");
        //        try
        //        {
        //            MySqlCommand command = new MySqlCommand(text, connection);
        //        }
        //        catch (MySqlException ex)
        //        {
        //            Console.WriteLine(ex);
        //            throw ex;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }



        //    private void SearchScriptsUnrealizedStatus(List<string> files, List<DOC> docs)
        //    {
        //        var untrackedFilesName = files.Except(docs.Select(x => x.NAME)).ToList();
        //        //  var untrackedFilesStatus = files.Except(docs.Select(x => x.STATUS)).ToList();

        //        for (int i = 0; i < untrackedFilesName.Count; i++)
        //        {
        //            if (!docs[i].NAME.Contains(untrackedFilesName[i]))
        //            {

        //                OpenFile(untrackedFilesName[i]);
        //                Console.WriteLine(untrackedFilesName[i]);
        //                DOC docnew = new DOC(untrackedFilesName[i], "REALIZED");
        //                Add(docnew);

        //            }

        //        }


        //        //foreach (var file in files) //++
        //        //{
        //        //    for (int i = 0; i < docs.Count; i++)
        //        //    {

        //        //    }
        //        //} //++
        //    }










        //метод этот, конекшн с БД создаёт
        public MySqlConnection Connection()
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
