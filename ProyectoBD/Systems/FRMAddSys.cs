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

namespace ProyectoBD.Systems
{
    public partial class FRMAddSys : Form
    {
        private Conexion conexionSql;
        private int _idUsuario;

        public FRMAddSys(int idUsuario)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMSistema frmSistemas = new FRMSistema(_idUsuario);
            frmSistemas.Show();
            this.Hide();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombreSistema = txtNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombreSistema))
            {
                MessageBox.Show("Debe completar el nombre del sistema.");
                return;
            }

            // Verificar si el sistema ya existe (agrega este método a tu clase Conexion)
            if (conexionSql.SistemaExiste(nombreSistema))
            {
                MessageBox.Show("El nombre del sistema ya existe.");
                return;
            }

            // Crear objeto Sistema
            Sistema nuevoSistema = new Sistema
            {
                NombreSistema = nombreSistema
            };

            bool agregado = conexionSql.AgregarSistema(nuevoSistema,_idUsuario);

            if (agregado)
            {
                MessageBox.Show("Sistema agregado correctamente.");
                FRMSistema frmSistemas = new FRMSistema(_idUsuario);
                frmSistemas.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el sistema.");
            }
        }
    }
}
