using CapaDatos;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class ProductoRepository
    {
        ProductoDAL _productoDAL;

        public List<Producto> ObtenerTodos()
        {
            _productoDAL = new ProductoDAL();

            return _productoDAL.ObtenerTodos();
        }

        public Producto ObtenerPorID(int id)
        {
            _productoDAL = new ProductoDAL();

            return _productoDAL.ObtenerPorID(id);
        }

        public int GuardarProducto(Producto producto)
        {
            _productoDAL = new ProductoDAL();

            return _productoDAL.GuardarProducto(producto);
        }

        public int ActualizarProducto(Producto producto)
        {
            _productoDAL = new ProductoDAL();

            return _productoDAL.ActualizarProducto(producto);
        }

        public int EliminarProducto(int id)
        {
            _productoDAL = new ProductoDAL();

            return _productoDAL.EliminarProducto(id);
        }
        public List<Producto> FiltroNombre(string nombre, string opt)
        {
            _productoDAL = new ProductoDAL();

            return _productoDAL.FiltroNombre(nombre,opt);
        }
        

    }
}
