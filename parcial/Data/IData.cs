using parcial.entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parcial.Data
{
    internal interface IData
    {
        bool CrearOrden(Orden oOrden);
        DataTable Consultar(string sp);
        int ConsultarEscalar();

    }
}
