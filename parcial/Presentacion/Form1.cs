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
        //Funciones

        #region

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

        private void limpiar()
        {
            cbMateriales.SelectedIndex = -1;
            txtNombre.Text = String.Empty ;
            nupCantidad.Value = 0;


        }


        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
        

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if(cbMateriales.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un material", "abiso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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

            if (oOrden.buscarDetalle(codigo))
            {
                MessageBox.Show("ya existe ese producto en el detalle");
                return;
            }

            Material material = new Material (codigo, nombre, stock);
            detalleOrden.Material = material;         
          
            oOrden.AgregarDetalle(detalleOrden);
            limpiar();
            agregarDataGrid();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.crearOrden();
            
        }

        private void dgvDetalleOrden_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalleOrden.Columns[e.ColumnIndex].Name == "clAcciones")
            {       
                    int codigoMaterial = Convert.ToInt32(dgvDetalleOrden.Rows[e.RowIndex].Cells[0].Value);
                    dgvDetalleOrden.Rows.RemoveAt(e.RowIndex);
                    oOrden.QuitarDetalle(codigoMaterial);
                    agregarDataGrid();

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("desa salir", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if(resultado == DialogResult.Yes)
            {
                this.Close();

            }
        }
    }
}
