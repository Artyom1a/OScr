using ConsoleApp1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CsSqlSearchscripts
{
    public class DB
    {
        //MySqlConnection connection = new MySqlConnection("server=localhost;database=STUDIES;uid=root;password=reghjjh236H;");

        //public void OpenConnection()
        //{
        //    if (connection.State == System.Data.ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }

        //}

        //public void CloseConnection() 
        //{
        //    if (connection.State == System.Data.ConnectionState.Open)
        //    {
        //        connection.Close();
        //    }
        //}

        //public MySqlConnection GetConnection() 
        //{
        //    return connection;
        //}
        private readonly string ConnectionString = "server=localhost;database=SCRIPTSDB;uid=root;password=reghjjh236H;";
        private readonly string SQL_selectItemName = "select * from SCRIPTS where {0};";
        private readonly string SQL_insertItem = "insert into SCRIPTS(NAME, STATUS) values {0};";
        private readonly string SQL_selectItems = "select Id,NAME,STATUS from SCRIPTS;";
        string mypath = "C:\\Code Main\\Scripts1_2803\\ConsoleApp1\\Scripts";
        private readonly string SQL_UpdateItem = "update SCRIPTS set NAME = @NAME , STATUS = @STATUS where ID = @ID;";


        public void Service() //++ забрал себе в класс service 
        {
            SearchScriptsUnrealizedStatus(NameFileOfDirectory(), GetDocs());
        }
        //++ забрал себе в класс service 

        //++ забрал себе в класс репозиторий и интерфейс репозитория забираем все имена файлов из бд 
        public List<DOC> GetDocs()
        {
            MySqlConnection connection = Connection();
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
        //++ забрал себе в класс репозиторий и интерфейс репозитория забираем все имена файлов из бд 


        // ++ забрал себе в класс репозиторий,  метод  этот, добавляет запись в БД 
        public int Add(DOC doc)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            MySqlConnection connection = Connection();
            if (connection == null) throw new Exception("connection is null");
            try
            {
                MySqlCommand command = new MySqlCommand(string.Format(SQL_insertItem, "(@name0, @type0)"), connection);
                command.Parameters.AddWithValue("@name0", doc.NAME);
                command.Parameters.AddWithValue("@type0", doc.STATUS);
                command.ExecuteNonQuery();
                return (int)command.LastInsertedId;
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



        //++ забрал себе в класс репозиторий , метод забираем названия файлов, которые нам нужны



        //++ забрал себе в класс service 
        private List<string> NameFileOfDirectory()
        {

            List<string> listNameFileOfDirectory = new List<string>();
            string[] fileAll = Directory.GetFiles(mypath, "*.sql", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < fileAll.Length; i++)
            {
                listNameFileOfDirectory.Add(Path.GetFileName(fileAll[i]));
            }
            return listNameFileOfDirectory;
        }

        //++ забрал себе в класс service 


        //++ забрал себе в класс service  метод считывания файла(скрипта)
        private void OpenFile(string pathfile)
        {
            string[] AllCode = File.ReadAllText(mypath + "\\" + pathfile).Split(';');

            for (int i = 0; i < AllCode.Length; i++)
            {
                FileAllCodeRealized(AllCode[i]);
            }
        }

        //++ забрал себе в класс service  метод считывания файла(скрипта)

        //++ забрал себе в класс service метод выполнения allcode
        private void FileAllCodeRealized(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            MySqlConnection connection = Connection();
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
        //++ забрал себе в класс service метод выполнения allcode

        //++ забрал себе в класс service 
        private void SearchScriptsUnrealizedStatus(List<string> files, List<DOC> docs)
        {
            var untrackedFilesName = files.Except(docs.Select(x => x.NAME)).ToList();
            //  var untrackedFilesStatus = files.Except(docs.Select(x => x.STATUS)).ToList();

            for (int i = 0; i < untrackedFilesName.Count; i++)
            {
                if (!docs[i].NAME.Contains(untrackedFilesName[i]))
                {

                    OpenFile(untrackedFilesName[i]);
                    Console.WriteLine(untrackedFilesName[i]);
                    DOC docnew = new DOC(untrackedFilesName[i], "REALIZED");
                    Add(docnew);

                }

            }
            //++ забрал себе в класс service 

            //foreach (var file in files) //++
            //{
            //    for (int i = 0; i < docs.Count; i++)
            //    {

            //    }
            //} //++
        }

        // update name file
        public void UpdateItem(int id, string name, string status)
        {
            MySqlConnection connection = Connection();
            if (connection == null) throw new Exception("connection error");
            try
            {
                MySqlCommand command = new MySqlCommand(SQL_UpdateItem, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@NAME", name);
                command.Parameters.AddWithValue("@STATUS", status);
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



        //не нужен метод пока этот, забираем запись из БД как пишется 
        public DOC GetByName(string name)
        {
            MySqlConnection connection = Connection();
            if (connection == null) throw new Exception("connection error");

            try
            {
                MySqlCommand command = new MySqlCommand(string.Format(SQL_selectItemName, $"name = '{name}'"), connection);
                DOC doc = new DOC();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    doc.ID = reader.GetInt32(0);
                    doc.NAME = reader.GetString(1);
                    doc.STATUS = reader.GetString(2);
                }
                return doc;

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

        //метод этот, конекшн с БД создаёт
        private MySqlConnection Connection()
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
