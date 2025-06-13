using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAL;
using Microsoft.Extensions.Configuration;
using System.IO;
using BAL;

namespace ProyectoBD
{
    public partial class FRMLogin : Form
    {
        private Conexion conexionSql;
        private ConexionOracle conexionOracle;

        public FRMLogin()
        {
            InitializeComponent();
            CargarConfiguracion();
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
                string cadenaConexionOracle = config.GetConnectionString("OracleConnection");

                conexionSql = new Conexion(cadenaConexionSql);
                conexionOracle = new ConexionOracle(cadenaConexionOracle);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (!conexionSql.UsuarioExiste(usuario))
            {
                MessageBox.Show("El usuario no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (conexionSql.ValidarCredenciales(usuario, contrasena))
            {
                int idUsuario = conexionSql.ObtenerIdUsuario(usuario);

                // Registramos inicio de sesión en bitácora:
                conexionSql.InsertarBitacora(idUsuario, $"Usuario {usuario} inicio sesión.");

                FRMHome home = new FRMHome(idUsuario);
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
