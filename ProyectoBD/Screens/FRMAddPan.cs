using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL;
using DAL;
using Microsoft.Extensions.Configuration;

namespace ProyectoBD.Screens
{
    public partial class FRMAddPan : Form
    {
        private Conexion conexionSql;
        private int _idUsuario;

        public FRMAddPan(int idUsuario)
        {
            InitializeComponent();
            CargarConfiguracion();
            _idUsuario = idUsuario;
        }

        private void CargarConfiguracion()
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string cadenaConexionSql = config.GetConnectionString("DefaultConnection");
                conexionSql = new Conexion(cadenaConexionSql);

                CargarComboSistemas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar configuración: " + ex.Message);
            }
        }

        private void CargarComboSistemas()
        {
            try
            {
                var sistemas = conexionSql.ObtenerSistemas();
                cmbSistemas.DataSource = sistemas;
                cmbSistemas.DisplayMember = "NombreSistema";
                cmbSistemas.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los sistemas: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombrePantalla = txtNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombrePantalla))
            {
                MessageBox.Show("Debe completar el nombre de la pantalla.");
                return;
            }

            if (cmbSistemas.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un sistema.");
                return;
            }

            int idSistema = (int)cmbSistemas.SelectedValue;

            // Verificar si ya existe una pantalla con ese nombre para ese sistema (opcional)
            if (conexionSql.PantallaExiste(idSistema, nombrePantalla))
            {
                MessageBox.Show("La pantalla ya existe para este sistema.");
                return;
            }

            Pantalla nuevaPantalla = new Pantalla
            {
                NombrePantalla = nombrePantalla,
                IdSistema = idSistema
            };

            bool agregado = conexionSql.AgregarPantalla(nuevaPantalla, _idUsuario);

            if (agregado)
            {
                MessageBox.Show("Pantalla agregada correctamente.");
                FRMPantalla frmPantallas = new FRMPantalla(_idUsuario);
                frmPantallas.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar la pantalla.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMPantalla frmPantallas = new FRMPantalla(_idUsuario);
            frmPantallas.Show();
            this.Hide();
        }
    }
}