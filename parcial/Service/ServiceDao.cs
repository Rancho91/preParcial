using parcial.Dao;
using parcial.entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using parcial.Data;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace parcial.Service
{
    public class ServiceDao : Iservices
    {
         private DataDB data;

        public ServiceDao()
        {
            data = new DataDB();
        }
     
        public bool CrearOrden(Orden oOrden)
        {

            return data.CrearOrden(oOrden);

        }

        public List<Material> TraerMateriales()
        {
          DataTable dataTable =  data.Consultar("SP_CONSULTAR_MATERIALES");

            List<Material> list = new List<Material>();
            foreach (DataRow mat in dataTable.Rows)
            {
                int codigo = Convert.ToInt32(mat["codigo"]);
                string nombre = mat["nombre"].ToString();
                int stock = Convert.ToInt32(mat["stock"]);
                list.Add(new Material(codigo, nombre, stock));
            }
            return list;
        }

        public int TraerProximaOrden()
        {
            throw new NotImplementedException();
        }
    }
}
