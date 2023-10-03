using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parcial.entidades
{
    public class Material
    {

        public int Codigo { get; set; }
        public string Nombre { get; set; }

        public int Stock { get; set; }

        public Material(int codigo, string nombre, int stock)
        {
            Codigo = codigo;
            Nombre = nombre;
            Stock = stock;
        }














        //    public int Codigo { get; set; }
        //    public string Nombre { get; set;}
        //    public int Stock { get; set; }
        //    public Material()
        //    {
        //        Codigo = 0;
        //        Nombre = String.Empty;
        //        Stock = 0;
        //    }

        //    public Material(int codigo, string nombre, int stock)
        //    {
        //        Codigo = codigo;
        //        Nombre = nombre;
        //        Stock = stock;
        //    }
    }



}
