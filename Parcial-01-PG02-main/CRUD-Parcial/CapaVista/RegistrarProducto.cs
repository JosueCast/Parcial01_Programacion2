using CapaDatos;
using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista
{
    public partial class RegistrarProducto : Form
    {
        ProductoRepository _productoRepository;
        int id;
        private VerProducto _form1;

        public RegistrarProducto(VerProducto form1,int _id = 0)
        {
            InitializeComponent();
            id = _id;

            if(id > 0)
            {
                CargarCampos(id);
            }
            else
            {
                productosBindingSource.MoveLast();
                productosBindingSource.AddNew();

                Producto producto = new Producto(); 
                productosBindingSource.DataSource = producto;
            }
        }

        //private void productosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        //{
        //    this.Validate();
        //    this.productosBindingSource.EndEdit();
        //    this.tableAdapterManager.UpdateAll(this.parcial01DataSet);

        //}

        private void RegistrarProducto_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'parcial01DataSet.Productos' Puede moverla o quitarla según sea necesario.
            this.productosTableAdapter.Fill(this.parcial01DataSet.Productos);

        }

        private void CargarCampos(int id)
        {
            _productoRepository = new ProductoRepository();
            productosBindingSource.DataSource = _productoRepository.ObtenerPorID(id);
        }

        private void GuardarProducto()
        {
            _productoRepository = new ProductoRepository();
            try
            {
                if (!ValidarCampos())
                {
                    return; // Si los campos no son válidos, salir del método
                }


                int resultado;
                //debemo indicar si es una actualizacion o es un nuevo producto
               
                if (id > 0)
                {
                    productosBindingSource.EndEdit();
                    Producto producto;
                    producto = (Producto)productosBindingSource.Current;
                    resultado = _productoRepository.ActualizarProducto(producto);
                    if (resultado > 0)
                    {
                        txtNombre.Clear();
                        txtDescripcion.Clear();
                        txtPrecio.Clear();
                        txtStock.Clear();
                        txtCategoria.Clear();
                        txtMarca.Clear();
                        MessageBox.Show("Producto actualizado con exito", "| Registro Producto",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se logro actualizar el producto", "| Registro Producto",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    productosBindingSource.EndEdit();

                    Producto producto;
                    producto = (Producto)productosBindingSource.Current;

                    resultado = _productoRepository.GuardarProducto(producto);

                    if (resultado > 0)
                    {
                        txtNombre.Clear();
                        txtDescripcion.Clear();
                        txtPrecio.Clear();
                        txtStock.Clear();
                        txtCategoria.Clear();
                        txtMarca.Clear();
                        MessageBox.Show("Producto agregado con exito", "| Registro Producto",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se logro guardar el Producto", "| Registro Producto",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                OnLlenarDataGridViewRequested();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrio un Error: {ex}", "| Registro Producto",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            GuardarProducto();
            
        }

        //Metodos para cargar el datagrid desde el segundo formulario simulando una ventana modal por que la pagina principal no se cierra
        public event EventHandler LlenarDataGridViewRequested;

        private void OnLlenarDataGridViewRequested()
        {
            LlenarDataGridViewRequested?.Invoke(this, EventArgs.Empty);
        }

        private bool ValidarCampos()
        {
            bool camposValidos = true;

            // Validación del nombre del producto
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                MessageBox.Show("Se requiere el nombre del producto. ¡Este campo es obligatorio!", "Tienda | Registro Producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                camposValidos = false;
                return camposValidos; 
            }

            // Validación de la descripción del producto
            if (string.IsNullOrEmpty(txtDescripcion.Text.Trim()))
            {
                MessageBox.Show("Se requiere la descripción del producto. ¡Este campo es obligatorio!", "Tienda | Registro Producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                camposValidos = false;
                return camposValidos; 
            }

            // Validación del precio del producto
            if (string.IsNullOrEmpty(txtPrecio.Text.Trim()) || !decimal.TryParse(txtPrecio.Text.Trim(), out _))
            {
                MessageBox.Show("Se requiere un precio válido para el producto. ¡Este campo es obligatorio!", "Tienda | Registro Producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                camposValidos = false;
                return camposValidos; 
            }

            // Validación del stock del producto
            if (string.IsNullOrEmpty(txtStock.Text.Trim()) || !int.TryParse(txtStock.Text.Trim(), out _))
            {
                MessageBox.Show("Se requiere una cantidad válida de stock para el producto. ¡Este campo es obligatorio!", "Tienda | Registro Producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                camposValidos = false;
                return camposValidos; 
            }

            // Validación de la marca del producto
            if (string.IsNullOrEmpty(txtMarca.Text.Trim()))
            {
                MessageBox.Show("Se requiere la marca del producto. ¡Este campo es obligatorio!", "Tienda | Registro Producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMarca.Focus();
                camposValidos = false;
                return camposValidos; 
            }

            // Validación de la categoría del producto
            if (string.IsNullOrEmpty(txtCategoria.Text.Trim()))
            {
                MessageBox.Show("Se requiere la categoría del producto. ¡Este campo es obligatorio!", "Tienda | Registro Producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoria.Focus();
                camposValidos = false;
                return camposValidos; 
            }

            return camposValidos; // Si todos los campos son válidos, se devuelve true.
        }


    }
}
