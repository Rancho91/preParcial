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
        //public DataTable Consultar(string sp)
        // {
        //   SqlConnection conn = Connection.ObtenerInstancia().ObtenerConexion();

        //        SqlCommand cmd = new SqlCommand(sp, conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        DataTable dt = new DataTable();
        //        try
        //        {
        //            conn.Open();
        //            dt.Load(cmd.ExecuteReader());

        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show(e.Message);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //        return dt;

        //        throw new NotImplementedException();
        //}



        //    public int ConsultarEscalar()
        //{
        //    throw new NotImplementedException();
        //}

        //    public bool CrearOrden(Orden oOrden)
        //    {
        //        SqlConnection conn = Connection.ObtenerInstancia().ObtenerConexion();
        //        conn.Open();
        //        SqlTransaction t = conn.BeginTransaction();
        //        try
        //        {

        //            SqlCommand cmd = new SqlCommand("sp_create_orden", conn);
        //            cmd.Transaction = t;
        //            cmd.Parameters.Clear();
        //            SqlParameter parameter = new SqlParameter();
        //            parameter.ParameterName = "@nro";
        //            parameter.SqlDbType = SqlDbType.Int;
        //            parameter.Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add(parameter);
        //            cmd.Parameters.AddWithValue("@fecha", oOrden.Fecha.Date);
        //            cmd.Parameters.AddWithValue("@responsable", oOrden.Responsable);
        //            cmd.ExecuteNonQuery();
        //            int nroOrden = (int)parameter.SqlValue;


        //            foreach(DetalleOrden deto in oOrden.Detalles)
        //            {
        //                SqlCommand comand = new SqlCommand("sp_create_detalle", conn);
        //                comand.Parameters.Clear();
        //                comand.Transaction = t;
        //                comand.Parameters.AddWithValue("@Cantidad",deto.Cantidad);
        //                comand.Parameters.AddWithValue("@codigo", deto.Material.Codigo);
        //                comand.Parameters.AddWithValue("@nroOrden", nroOrden);
        //                comand.ExecuteNonQuery();
        //            }
        //            t.Commit();
        //            return true;

        //        }
        //        catch(Exception ex) 
        //        {
        //            t.Rollback();
        //            MessageBox.Show(ex.Message);
        //            return false;
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
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
                SqlCommand cmd = new SqlCommand("sp_create_orden", cnn);
                  cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@responsable", oOrden.Responsable.ToString());
                cmd.Parameters.AddWithValue("@fecha", oOrden.Fecha);

                SqlParameter parametroNroOrden = new SqlParameter("@nroOrden", SqlDbType.Int);
                parametroNroOrden.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parametroNroOrden); 
                cmd.Transaction = t;

                cmd.ExecuteNonQuery();
                int nro = (int)parametroNroOrden.Value;

                foreach(DetalleOrden detO in oOrden.Detalles)
                {
                    SqlCommand command = new SqlCommand("sp_create_detalle", cnn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = t;
                    command.Parameters.AddWithValue("@codigo", detO.Material.Codigo);
                    command.Parameters.AddWithValue("@nroorden", nro);
                    command.Parameters.AddWithValue("@cantidad", detO.Cantidad);
                    command.ExecuteNonQuery();

                }
                t.Commit();
                flagg = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                t.Rollback();
                flagg = false;
            }
            finally
            {
                cnn.Close();
                

            }
            return flagg;
        }
    

 
    }
}
