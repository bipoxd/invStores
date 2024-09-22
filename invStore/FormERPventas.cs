﻿using System;
using System.Windows.Forms;
using Npgsql; // Asegúrate de incluir esta librería para PostgreSQL

namespace invStore
{
    public partial class FormERPventas : Form
    {
        public FormERPventas()
        {
            InitializeComponent();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            // Verificar si ya se ha realizado una apertura hoy
            if (YaExisteAperturaHoy())
            {
                // Si ya existe una apertura hoy, abrir directamente el formulario de ventas
                FormVenta venta = new FormVenta();
                venta.ShowDialog();
            }
            else
            {
                // Si no existe apertura, mostrar el formulario de arqueo
                FormArqueoApertura apertura = new FormArqueoApertura();
                apertura.ShowDialog();
            }
        }

        private bool YaExisteAperturaHoy()
        {
            bool existeApertura = false;

            // Tu cadena de conexión para PostgreSQL
            string connectionString = "Host=autorack.proxy.rlwy.net;Port=21434;Username=postgres;Password=YeRYuiLMrmWePLNVhDrDLCUlCazYKmjO;Database=railway;Ssl Mode=Require;";

            // Consulta SQL para verificar si ya existe una apertura para el día actual
            string query = "SELECT COUNT(*) FROM apertura WHERE aper_fecha = @fechaActual";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fechaActual", DateTime.Now.Date); // Fecha actual sin la hora

                    try
                    {
                        connection.Open();
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count > 0)
                        {
                            existeApertura = true; // Ya existe una apertura para el día actual
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al verificar apertura: " + ex.Message);
                    }
                }
            }

            return existeApertura;
        }

        private void btnCerrarDia_Click(object sender, EventArgs e)
        {
            FormFinalizarDia finalizarDia = new FormFinalizarDia();
            finalizarDia.ShowDialog();
        }
    }
}
