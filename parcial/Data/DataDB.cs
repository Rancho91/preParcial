using parcial.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using parcial.entidades;
using System.Windows.Forms;

namespace parcial.Data {

    public class DataDB : IData
    {
   
        public DataTable Consultar(string sp)
        {
            SqlConnection cnn = Connection.ObtenerInstancia().ObtenerConexion();
            cnn.Open();
            SqlCommand cmd = new SqlCommand(sp, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            cnn.Close();
            return dt;
        }

        public int ConsultarEscalar()
        {
            throw new NotImplementedException();
        }

        public bool CrearOrden(Orden oOrden)
        {
            bool flagg = false;
            SqlConnection cnn = Connection.ObtenerInstancia().ObtenerConexion();
            cnn.Open();
            SqlTransaction t = cnn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand("SP_INSERTAR_ORDEN", cnn, t);
                  cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@responsable", oOrden.Responsable.ToString());

                SqlParameter parametroNroOrden = new SqlParameter("@nro", SqlDbType.Int);
                parametroNroOrden.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parametroNroOrden); 

                cmd.ExecuteNonQuery();
                int nro = (int)parametroNroOrden.Value;

                int nroDetalle = 1;
                foreach(DetalleOrden detO in oOrden.Detalles)
                {
                    SqlCommand command = new SqlCommand("SP_INSERTAR_DETALLES", cnn, t);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@codigo", detO.Material.Codigo);
                    command.Parameters.AddWithValue("@nro_orden", nro);
                    command.Parameters.AddWithValue("@cantidad", detO.Cantidad);
                    command.Parameters.AddWithValue("@detalle", nroDetalle);
                    command.ExecuteNonQuery();
                    nroDetalle++;

                }
                t.Commit();
                flagg = true;
                MessageBox.Show("se inserto todo de forma correcta");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                t.Rollback();
                flagg = false;
            }
            finally
            {
                if(cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();

                }


            }
            return flagg;
        }
    

 
    }
}
