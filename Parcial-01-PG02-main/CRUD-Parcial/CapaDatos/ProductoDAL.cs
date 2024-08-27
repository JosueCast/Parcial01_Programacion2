using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ProductoDAL
    {
        public List<Producto> ObtenerTodos()
        {
            using (var conexion = DBConectar.GetSqlConnection())
            {
                String selectFrom = "";

                selectFrom = selectFrom + "SELECT [Id] " + "\n";
                selectFrom = selectFrom + "      ,[Nombre] " + "\n";
                selectFrom = selectFrom + "      ,[Descripcion] " + "\n";
                selectFrom = selectFrom + "      ,[Precio] " + "\n";
                selectFrom = selectFrom + "      ,[Stock] " + "\n";
                selectFrom = selectFrom + "      ,[Marca] " + "\n";
                selectFrom = selectFrom + "      ,[Categoria] " + "\n";
                selectFrom = selectFrom + "  FROM [Productos]";

                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                {
                    SqlDataReader reader = comando.ExecuteReader();

                    List<Producto> Productos = new List<Producto>();

                    while (reader.Read())
                    {
                        var producto = LeerDelDataReader(reader);
                        Productos.Add(producto);
                    }

                    return Productos;
                }
            }
        }

        public Producto LeerDelDataReader(SqlDataReader reader)
        {
            Producto producto = new Producto();

            producto.Id = reader["Id"] == DBNull.Value ? 0 : (int)reader["Id"];
            producto.Nombre = reader["Nombre"] == DBNull.Value ? "" : (String)reader["Nombre"];
            producto.Descripcion = reader["Descripcion"] == DBNull.Value ? "" : (String)reader["Descripcion"];
            producto.Precio = reader["Precio"] == DBNull.Value ? 0 : (decimal)reader["Precio"];
            producto.Stock = reader["Stock"] == DBNull.Value ? 0 : (int)reader["Stock"];
            producto.Marca = reader["Marca"] == DBNull.Value ? "" : (String)reader["Marca"];
            producto.Categoria = reader["Categoria"] == DBNull.Value ? "" : (String)reader["Categoria"];

            return producto;
        }

        public Producto ObtenerPorID(int id)
        {
            using (var conexion = DBConectar.GetSqlConnection())
            {
                String selectForID = "";

                selectForID = selectForID + "SELECT [Id] " + "\n";
                selectForID = selectForID + "      ,[Nombre] " + "\n";
                selectForID = selectForID + "      ,[Descripcion] " + "\n";
                selectForID = selectForID + "      ,[Precio] " + "\n";
                selectForID = selectForID + "      ,[Stock] " + "\n";
                selectForID = selectForID + "      ,[Marca] " + "\n";
                selectForID = selectForID + "      ,[Categoria] " + "\n";
                selectForID = selectForID + "  FROM [Productos]";
                selectForID = selectForID + "  Where Id = @id";

                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {
                    comando.Parameters.AddWithValue("Id", id);

                    var reader = comando.ExecuteReader();

                    Producto producto = null;

                    if (reader.Read())
                    {
                        producto = LeerDelDataReader(reader);
                    }

                    return producto;
                }
            }
        }

        public int GuardarProducto(Producto producto)
        {
            using (var conexion = DBConectar.GetSqlConnection())
            {
                String insertInto = "";

                insertInto = insertInto + "INSERT INTO [dbo].[Productos] " + "\n";

                insertInto = insertInto + "           ([Nombre] " + "\n";
                insertInto = insertInto + "           ,[Descripcion] " + "\n";
                insertInto = insertInto + "           ,[Precio] " + "\n";
                insertInto = insertInto + "           ,[Stock] " + "\n";
                insertInto = insertInto + "           ,[Marca] " + "\n";
                insertInto = insertInto + "           ,[Categoria]) " + "\n"; // El paréntesis de cierre aquí

                insertInto = insertInto + "     VALUES " + "\n";
                insertInto = insertInto + "           (@Nombre " + "\n";
                insertInto = insertInto + "           ,@Descripcion " + "\n";
                insertInto = insertInto + "           ,@Precio " + "\n";
                insertInto = insertInto + "           ,@Stock " + "\n";
                insertInto = insertInto + "           ,@Marca " + "\n";
                insertInto = insertInto + "           ,@Categoria)"; // El paréntesis de cierre aquí

                using (var comando = new SqlCommand(insertInto, conexion))
                {
                    int insertados = parametrosProducto(producto, comando);
                    return insertados;
                }
            }
        }

        public int ActualizarProducto(Producto producto)
        {
            using (var conexion = DBConectar.GetSqlConnection())
            {
                String ActualizarProductoPorID = "";

                ActualizarProductoPorID = ActualizarProductoPorID + "UPDATE [dbo].[Productos] " + "\n";
                ActualizarProductoPorID = ActualizarProductoPorID + "   SET [Nombre] = @Nombre " + "\n";
                ActualizarProductoPorID = ActualizarProductoPorID + "      ,[Descripcion] = @Descripcion " + "\n";
                ActualizarProductoPorID = ActualizarProductoPorID + "      ,[Precio] = @Precio " + "\n";
                ActualizarProductoPorID = ActualizarProductoPorID + "      ,[Stock] = @Stock " + "\n";
                ActualizarProductoPorID = ActualizarProductoPorID + "      ,[Marca] = @Marca " + "\n";
                ActualizarProductoPorID = ActualizarProductoPorID + "      ,[Categoria] = @Categoria " + "\n";
                ActualizarProductoPorID = ActualizarProductoPorID + " WHERE Id= @Id";

                using (var comando = new SqlCommand(ActualizarProductoPorID, conexion))
                {
                    int actualizados = parametrosProducto(producto, comando);
                    return actualizados;
                }
            }
        }

        public int parametrosProducto(Producto producto, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("Id", producto.Id);
            comando.Parameters.AddWithValue("Nombre", producto.Nombre);
            comando.Parameters.AddWithValue("Descripcion", producto.Descripcion);
            comando.Parameters.AddWithValue("Precio", producto.Precio);
            comando.Parameters.AddWithValue("Stock", producto.Stock);
            comando.Parameters.AddWithValue("Marca", producto.Marca);
            comando.Parameters.AddWithValue("Categoria", producto.Categoria);

            var insertados = comando.ExecuteNonQuery();
            return insertados;
        }

        public int EliminarProducto(int id)
        {
            using (var conexion = DBConectar.GetSqlConnection())
            {
                String EliminarProducto = "";

                EliminarProducto = EliminarProducto + "DELETE FROM [dbo].[Productos] " + "\n";
                EliminarProducto = EliminarProducto + "      WHERE Id = @Id";

                using (SqlCommand comando = new SqlCommand(EliminarProducto, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);

                    int eliminados = comando.ExecuteNonQuery();

                    return eliminados;
                }
            }
        }


        public List<Producto> FiltroNombre(string nombre,string opt)
        {

            // Validación del nombre de columna
            if (opt != "Nombre" && opt != "Marca" && opt != "Categoria")
            {
                throw new ArgumentException("El valor de 'opt' debe ser 'Nombre', 'Marca', o 'Categoria'.");
            }

            using (var conexion = DBConectar.GetSqlConnection())
            {

                string queryBusqueda = $"SELECT Id, Nombre, Descripcion, Precio, Stock, Marca, Categoria " +
                                       "FROM [dbo].[Productos] " +
                                       $"WHERE {opt} LIKE @Nombre";

                using (SqlCommand comando = new SqlCommand(queryBusqueda, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        List<Producto> productosEncontrados = new List<Producto>();

                        while (reader.Read())
                        {
                            Producto producto = new Producto
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Precio = reader.GetDecimal(3),
                                Stock = reader.GetInt32(4),
                                Marca = reader.GetString(5),
                                Categoria = reader.GetString(6)
                            };

                            productosEncontrados.Add(producto);
                        }

                        return productosEncontrados;
                    }
                }
            }
        }



    }
}
