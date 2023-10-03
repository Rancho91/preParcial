using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Data;
using System.Security.Principal;
using System.Windows.Forms;
using parcial.Data;

namespace parcial.Services
{
    public class Connection
    {
        private static Connection instancia;
        private SqlConnection cnn;
 
        private Connection()
        {
            cnn = new SqlConnection(Properties.Resources.dbString);
        }

        public static Connection ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new Connection();
            }
            return instancia;
        }
        public SqlConnection ObtenerConexion()
        {
            return this.cnn;
        }

        public DataTable Consultar(string sp)
        {
            SqlCommand cmd = new SqlCommand(sp, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                cnn.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return dt;

        }


    }
}
