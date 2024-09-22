using invStore.Dtos;
using Npgsql;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace invStore
{
    public partial class FormVenta : Form
    {
        private DataTable carrito;

        public FormVenta()
        {
            InitializeComponent();
            carrito = new DataTable();
            carrito.Columns.Add("Nombre Producto", typeof(string));
            carrito.Columns.Add("Cantidad Comprada", typeof(int)); // Renombrar a 'Cantidad Comprada'
            carrito.Columns.Add("Precio Total", typeof(decimal));
            dgvCarrito.DataSource = carrito;
            dgvCarrito.ReadOnly = true; // Solo lectura
            dgvCarrito.AllowUserToAddRows = false; // No permitir agregar filas
            dgvCarrito.AllowUserToDeleteRows = false; // No permitir eliminar filas
            dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cbxMedioPago.Items.Add("EFECTIVO");
            cbxMedioPago.Items.Add("DEBITO");
            cbxMedioPago.Items.Add("CREDITO");

        }

        private void txtCodigoProd_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodigoProd.Text))
            {
                var producto = buscarProducto(txtCodigoProd.Text);
                if (producto != null)
                {
                    agregarAlCarrito(producto);
                    txtCodigoProd.Clear();
                }
                else
                {
                    MessageBox.Show("Producto no encontrado.");
                    txtCodigoProd.Clear();
                }
            }
        }

        private Productos buscarProducto(string cod)
        {
            Productos producto = null;

            string connectionString = "Host=autorack.proxy.rlwy.net;Port=21434;Username=postgres;Password=YeRYuiLMrmWePLNVhDrDLCUlCazYKmjO;Database=railway;Ssl Mode=Require;";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT Id, prod_nombre, prod_precio, prod_cantidad, prod_codigo FROM Productos WHERE prod_codigo = @CodigoBarra";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CodigoBarra", cod);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producto = new Productos
                            {
                                Id = (int)reader["Id"],
                                NombreProducto = reader["prod_nombre"].ToString(),
                                precioProducto = (int)reader["prod_precio"],
                                stock = reader["prod_cantidad"].ToString(), // Este será el stock
                                codigo = cod
                            };
                        }
                    }
                }
            }
            return producto;
        }

        private void agregarAlCarrito(Productos producto)
        {
            // Variable para almacenar el total acumulado
            decimal totalCompra = 0;

            // Verifica si el producto ya existe en el carrito
            foreach (DataRow row in carrito.Rows)
            {
                if (row["Nombre Producto"].ToString() == producto.NombreProducto)
                {
                    // Si existe, aumenta la cantidad comprada
                    int cantidadActual = (int)row["Cantidad Comprada"];
                    int nuevaCantidad = cantidadActual + 1;
                    decimal precioPorUnidad = producto.precioProducto;

                    // Solo actualiza si hay stock disponible
                    if (nuevaCantidad <= Convert.ToInt32(producto.stock))
                    {
                        row["Cantidad Comprada"] = nuevaCantidad;
                        row["Precio Total"] = nuevaCantidad * precioPorUnidad; // Actualiza el precio total
                    }
                    else
                    {
                        MessageBox.Show("No hay suficiente stock disponible.");
                    }
                    // Salimos del método ya que hemos actualizado la fila existente
                    break;
                }
            }
            // Si no existe, agrega un nuevo producto
            if (!carrito.AsEnumerable().Any(r => r["Nombre Producto"].ToString() == producto.NombreProducto))
            {
                if (carrito.Rows.Count < Convert.ToInt32(producto.stock))
                {
                    carrito.Rows.Add(producto.NombreProducto, 1, producto.precioProducto);
                }
                else
                {
                    MessageBox.Show("No hay suficiente stock disponible.");
                }
            }

            // Recalcular el total después de agregar un producto
            foreach (DataRow row in carrito.Rows)
            {
                totalCompra += Convert.ToDecimal(row["Precio Total"]); // Suma el precio total de cada producto
            }

            // Mostrar el total en el Label
            lblTotal.Text = $"Total: {totalCompra:C}"; // Formato de moneda
        }

        private void btn_finalizarVenta_Click(object sender, EventArgs e)
        {
            //validar que haya productos en el carrito
            if (carrito.Rows.Count == 0)
            {
                MessageBox.Show("Complete los campos. No hay productos en el carrito.");
                return; // Salir del método si el carrito está vacío
            }

            if (cbxMedioPago.Text == "" || cbxMedioPago.Text == null || cbxMedioPago.Text == "---Selecciona Medio de Pago---")
            {
                MessageBox.Show("Complete los campos. No hay medio de pago seleccionado.");
                return;
            }
            // Crear conexión a la base de datos
            string connectionString = "Host=autorack.proxy.rlwy.net;Port=21434;Username=postgres;Password=YeRYuiLMrmWePLNVhDrDLCUlCazYKmjO;Database=railway;Ssl Mode=Require;";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Preparar la inserción
                foreach (DataRow row in carrito.Rows)
                {
                    string query = "INSERT INTO ventafinalizada (vent_producto, vent_cantidad, vent_total, vent_mediopago, vent_fecha) " +
                                   "VALUES (@Producto, @Cantidad, @Total, @MedioPago, @Fecha)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Producto", row["Nombre Producto"].ToString());
                        command.Parameters.AddWithValue("@Cantidad", (int)row["Cantidad Comprada"]);
                        command.Parameters.AddWithValue("@Total", Convert.ToDecimal(row["Precio Total"]));
                        command.Parameters.AddWithValue("@MedioPago", cbxMedioPago.Text.ToString());
                        command.Parameters.AddWithValue("@Fecha", DateTime.Now); // Fecha actual
                        //command.Parameters.AddWithValue("@CodigoVenta", codigoVenta);

                        command.ExecuteNonQuery(); // Ejecutar la inserción
                    }
                }

                MessageBox.Show("Venta finalizada exitosamente.");
                carrito.Clear();
            }

            // Limpiar el carrito y el total
            carrito.Clear();
            cbxMedioPago.Text = "---Selecciona Medio de Pago---";
            lblTotal.Text = "Total: $0.00"; // Reiniciar el total
        }

    }
}
