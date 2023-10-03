using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using parcial.entidades;
using parcial.Service;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace parcial
{
    public partial class Form1 : Form
    {
        private ServiceDao serviceDao;
        private Orden oOrden;
        private List<Material> listMateriales;
        public Form1()
        {
            InitializeComponent();
            serviceDao = new ServiceDao();
            oOrden = new Orden();
            listMateriales = new List<Material>();
            cargarCombo();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        #region
        //Funciones

        private void cargarCombo()
        {
            List<Material> materials = serviceDao.TraerMateriales();
            listMateriales = materials;
            cbMateriales.DataSource = materials;
            cbMateriales.DisplayMember = "nombre";
            cbMateriales.ValueMember = "codigo";

        }

        private void crearOrden()
        {
            oOrden.Responsable = txtNombre.Text;
            oOrden.Fecha = Convert.ToDateTime(dtpFecha.Text);
            serviceDao.CrearOrden(oOrden);      
        }

        private void agregarDataGrid()
        {
            dgvDetalleOrden.Rows.Clear();

            foreach (DetalleOrden det in oOrden.Detalles)
            {
                dgvDetalleOrden.Rows.Add(det.Material.Codigo, det.Material.Nombre, det.Material.Stock, det.Cantidad, "Quitar");
            }

        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
        

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DetalleOrden detalleOrden = new DetalleOrden();

            detalleOrden.Cantidad = Convert.ToInt32(nupCantidad.Value);
            string nombre = cbMateriales.Text;
            int stock=0;
            foreach (Material mat in listMateriales)
            {
                if(mat.Codigo == Convert.ToInt32(cbMateriales.SelectedValue))
                {
                    stock = mat.Stock;
                    break;
                }
            }
            int codigo = Convert.ToInt32(cbMateriales.SelectedValue);
            Material material = new Material (codigo, nombre, stock);
            detalleOrden.Material = material;         
           
            oOrden.AgregarDetalle(detalleOrden);





            //DetalleOrden detalle = new DetalleOrden();
            //string nombre = cbMateriales.Text;
            //int stock = listMateriales[Convert.ToInt32(cbMateriales.SelectedValue)].Stock;
            //int codigo = Convert.ToInt32(cbMateriales.SelectedValue);
            //detalle.Material = new Material(codigo,nombre,stock);
            //detalle.Cantidad = (int)nupCantidad.Value;
    
            //oOrden.AgregarDetalle(detalle);
            agregarDataGrid();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.crearOrden();
            
        }

        private void dgvDetalleOrden_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
