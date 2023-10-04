using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parcial.entidades
{
    public class Orden
    {
        public int NroOrden { get; set; }
        public DateTime Fecha { get; set; }
        public string Responsable { get; set; }

        public List<DetalleOrden> Detalles {  get; set; }

        public Orden()
        {

            Detalles = new List<DetalleOrden>();
        }


        public void AgregarDetalle(DetalleOrden detalleOrden)
        {
            Detalles.Add(detalleOrden);
        }

        public bool QuitarDetalle(int detalleOrden)
        {
            bool flagg = false;
            for (int i = 0; i < Detalles.Count; i++)
            {
                if (Detalles[i].Material.Codigo == detalleOrden)
                {
                    Detalles.RemoveAt(i);
                    flagg = true;
                    break;

                }
            }
            return flagg;
        }

        public bool buscarDetalle(int detalleOrden)
        {
            bool flagg= false;

            for (int i = 0; i < Detalles.Count; i++)
            {
                if (Detalles[i].Material.Codigo == detalleOrden)
                {
                    flagg = true;
                    break;

                }
            }
            return flagg;
        }

    }
}
