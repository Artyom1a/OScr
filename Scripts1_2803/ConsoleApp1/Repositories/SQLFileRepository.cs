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
