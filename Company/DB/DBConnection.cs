using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company.DB
{
    class DBConnection
    {
        private string server;
        private string user;
        private string db;
        private string password;
        private string charSet;
        private MySqlConnection mySqlConnection;
        private MySqlCommand command;
        MySqlDataAdapter adapter;

        public MySqlConnection MySqlConnection { get => mySqlConnection; }
        public MySqlDataAdapter Adapter { get => adapter;}

        public DBConnection(string server, string user, string db, string password, string charSet)
        {
            this.server = server;
            this.user = user;
            this.db = db;
            this.password = password;
            this.charSet = charSet;
            string connectSring = "server=" + server + ";user=" + user + ";database=" +
                db + ";password=" + password + ";CharSet=" + charSet + ";";
            mySqlConnection = new MySqlConnection(connectSring);
            adapter = new MySqlDataAdapter();
        }

        public void OpenConnection()
        {
            if (mySqlConnection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    mySqlConnection.Open();
                }
                catch(MySqlException)
                {
                    MessageBox.Show("Ошибка при подключении к серверу!");
                    Environment.Exit(0);
                }
                MessageBox.Show("Подключение к серверу успешно выполнено!");
            }
            else
            {
                MessageBox.Show("Соединение сервером уже установлено!");
            }
        }

        public void CloseConnetion()
        {
            mySqlConnection.Close();
        }
    }
}
