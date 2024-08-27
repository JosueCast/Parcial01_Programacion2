using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista
{
    public partial class VerProducto : Form
    {
        ProductoRepository _productoRepository;

        public VerProducto()
        {
            InitializeComponent();
            CargarProductos();
            
            _productoRepository = new ProductoRepository();
        }

        private void productosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productosBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.parcial01DataSet);

        }

        private void VerProducto_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'parcial01DataSet.Productos' Puede moverla o quitarla según sea necesario.
            this.productosTableAdapter.Fill(this.parcial01DataSet.Productos);

        }

        private void CargarProductos()
        {
            _productoRepository = new ProductoRepository();

            productosDataGrid.DataSource = _productoRepository.ObtenerTodos();
        }







        private void btnAdd_Click(object sender, EventArgs e)
        {
            RegistrarProducto objRegistroProducto = new RegistrarProducto(this);
            objRegistroProducto.LlenarDataGridViewRequested += Form1_LlenarDataGridViewRequested;
            objRegistroProducto.Show();
        }
        private void Form1_LlenarDataGridViewRequested(object sender, EventArgs e)
        {
            // Actualizar el DataGridView
            CargarProductos();
        }
        private void productosDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (productosDataGrid.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                //Esta linea de abajo creo que no esta haciendo nada pero me da miedo borrarla XD
                int Id = Convert.ToInt32(productosDataGrid.CurrentRow.Cells["Id"].Value.ToString());

                 RegistrarProducto objRegistroProducto = new RegistrarProducto(this, Id);
            objRegistroProducto.LlenarDataGridViewRequested += Form1_LlenarDataGridViewRequested;
            objRegistroProducto.Show();
            }
            else if (productosDataGrid.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int Id = Convert.ToInt32(productosDataGrid.CurrentRow.Cells["Id"].Value.ToString());
                _productoRepository = new ProductoRepository();
                _productoRepository.EliminarProducto(Id);
                int resultado = _productoRepository.EliminarProducto(Id);

                if (resultado == 0)
                {
                    MessageBox.Show("Producto eliminado con exito", "| Registro Producto",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("No se logro eliminar el Producto", "| Registro Producto",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                CargarProductos();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void FiltroPorNombre()
        {
            _productoRepository = new ProductoRepository();
            string nombre = ttxBuscar.Text;
            string name = "Nombre";
            productosDataGrid.DataSource = _productoRepository.FiltroNombre(nombre, name);
        }

        private void FiltroPorMarca()
        {
            _productoRepository = new ProductoRepository();
            string nombre = txtMarca.Text;
            string marca = "Marca";
            productosDataGrid.DataSource = _productoRepository.FiltroNombre(nombre, marca);
        }

        private void FiltroPorCategoria()
        {
            _productoRepository = new ProductoRepository();
            string nombre = txtCategoria.Text;
            string categoria = "Categoria";
            productosDataGrid.DataSource = _productoRepository.FiltroNombre(nombre, categoria);
        }

        private void ttxBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltroPorNombre();
        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {
            FiltroPorMarca();
        }

        private void txtCategoria_TextChanged(object sender, EventArgs e)
        {
            FiltroPorCategoria();
        }

        private void ttxBuscar_Enter(object sender, EventArgs e)
        {
            txtCategoria.Clear();
            txtMarca.Clear();
        }

        private void txtMarca_Enter(object sender, EventArgs e)
        {
            txtCategoria.Clear();
            ttxBuscar.Clear();
        }

        private void txtCategoria_Enter(object sender, EventArgs e)
        {
            ttxBuscar.Clear();
            txtMarca.Clear();
        }
    }
}