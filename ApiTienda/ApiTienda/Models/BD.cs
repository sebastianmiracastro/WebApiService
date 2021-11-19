using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ApiTienda.Models
{
    public class BD
    {
        private MySqlConnection Connect;

        public BD()
        {
            Connect = new MySqlConnection("datasource=localhost;port=3306;userid=root;password=Sena1234;database=clientst");
        }

        public string ejecutarSQL(string sql)
        {
            string resultado = "";

            Connect.Open();

            MySqlCommand cmd = new MySqlCommand(sql, Connect);

            cmd.ExecuteNonQuery();

            Connect.Close();

            return resultado;
        }
        public DataTable GetElementInDataBase(string sql2)
        {
            DataTable TableData = new DataTable();

            MySqlCommand Exe = new MySqlCommand(sql2, Connect);
            try
            {
                Connect.Open();

                MySqlDataAdapter Adapter = new MySqlDataAdapter(Exe);

                Adapter.Fill(TableData);

                Connect.Close();

                Adapter.Dispose();
            }
            catch (Exception)
            {
                return null;
            }
            return TableData;

        }
    }
}
