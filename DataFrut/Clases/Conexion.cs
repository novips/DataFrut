using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataFrut.Clases
{

    public static class Conexion
    {
        public static MySqlConnection con;
        public static bool conectar(string usuario, string password)
        {
            bool conexionExitosa = false;
            string stringConexion;
            stringConexion = "Database=datafrut;Password=" + password + ";Server=192.168.42.128;User=" + usuario + ";";
            //stringConexion = "server=192.168.42.128;port=3306;Database=datafrut;uid=" + usuario + ";pwd=" + password + "; ";
            con = new MySql.Data.MySqlClient.MySqlConnection();
            con.ConnectionString = stringConexion;
            abrirConexion();
            if(con.State == System.Data.ConnectionState.Open)
            {
                conexionExitosa = true;
            }
            cerrarConexion();
            return conexionExitosa;
        }

        public static void abrirConexion()
        {
            try
            {
                con.Open();
            }
            catch (Exception)
            {

            }
        }

        public static void cerrarConexion()
        {
            try
            {
                con.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
