using Npgsql;
using System;
using System.Windows.Forms;

namespace invStore
{
    public partial class FormFinalizarDia : Form
    {
        private decimal montoInicialCaja;
        private decimal totalEfectivo;

        public FormFinalizarDia()
        {
            InitializeComponent();
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CalcularTotales();

            // Asignar el evento TextChanged a txtEfectivoRetirado
            txtEfectivoRetirado.TextChanged += TxtEfectivoRetirado_TextChanged;
        }

        private void CalcularTotales()
        {
            totalEfectivo = 0;
            decimal totalDebito = 0;
            decimal totalCredito = 0;

            string connectionString = "Host=autorack.proxy.rlwy.net;Port=21434;Username=postgres;Password=YeRYuiLMrmWePLNVhDrDLCUlCazYKmjO;Database=railway;Ssl Mode=Require;";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Obtener el monto inicial de la caja para la fecha actual
                    string queryMontoInicial = "SELECT aper_montoinicial " +
                                               "FROM apertura " +
                                               "WHERE aper_fecha = CURRENT_DATE";

                    using (var commandMontoInicial = new NpgsqlCommand(queryMontoInicial, connection))
                    {
                        var result = commandMontoInicial.ExecuteScalar();
                        if (result != null && decimal.TryParse(result.ToString(), out montoInicialCaja))
                        {
                            // El monto inicial de la caja se ha obtenido correctamente
                        }
                    }

                    // Obtener los totales de ventas
                    string queryVentas = "SELECT vent_mediopago, SUM(vent_total) AS TotalVentas " +
                                         "FROM ventafinalizada " +
                                         "WHERE vent_fecha::date = CURRENT_DATE " +
                                         "GROUP BY vent_mediopago";

                    using (var commandVentas = new NpgsqlCommand(queryVentas, connection))
                    {
                        using (var reader = commandVentas.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string medioPago = reader.GetString(0).ToLower();
                                decimal total = reader.GetDecimal(1);

                                switch (medioPago)
                                {
                                    case "efectivo":
                                        totalEfectivo = total;
                                        break;
                                    case "debito":
                                        totalDebito = total;
                                        break;
                                    case "credito":
                                        totalCredito = total;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular totales: {ex.Message}");
            }

            // Mostrar los totales en los TextBoxes con separador de miles y signo de peso
            lblEfectivo.Text = $"${totalEfectivo:N0}";
            lblDebito.Text = $"${totalDebito:N0}";
            lblCredito.Text = $"${totalCredito:N0}";

            // Mostrar el monto inicial de la caja
            lblMontoInicial.Text = $"${montoInicialCaja:N0}";

            // Calcular el total del efectivo en la caja
            decimal totalEfectivoCaja = totalEfectivo + montoInicialCaja;
            lblTotalEfectivoCaja.Text = $"${totalEfectivoCaja:N0}";

            // Calcular el total de ventas del día
            decimal totalVentasDelDia = totalEfectivo + totalDebito + totalCredito;
            lblTotalVentas.Text = $"${totalVentasDelDia:N0}";

            // Inicializar el campo de retiro y monto en caja
            txtEfectivoRetirado.Text = "0";
            lblMonto.Text = "$" + totalEfectivoCaja.ToString("N0");
        }

        private void TxtEfectivoRetirado_TextChanged(object sender, EventArgs e)
        {
            // Validar y actualizar el monto de la caja
            decimal retiroEfectivo;
            decimal montoEnCaja = totalEfectivo + montoInicialCaja;

            if (decimal.TryParse(txtEfectivoRetirado.Text, out retiroEfectivo))
            {
                if (retiroEfectivo > montoEnCaja)
                {
                    MessageBox.Show("El monto de retiro no puede ser mayor al total disponible.");
                    txtEfectivoRetirado.Text = montoEnCaja.ToString("N0");
                    return;
                }

                // Calcular el monto restante en caja
                decimal montoRestanteCaja = montoEnCaja - retiroEfectivo;
                lblMonto.Text = "$" + montoRestanteCaja.ToString("N0");
            }
            else
            {
                lblMonto.Text = "$" + montoEnCaja.ToString("N0");
            }
        }
    }
}
