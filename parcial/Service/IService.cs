using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using parcial.entidades;
namespace parcial.Dao
{
    internal interface Iservices
    {
        int TraerProximaOrden();
        List<Material> TraerMateriales();
        bool CrearOrden(Orden oOrden);
    }
}
