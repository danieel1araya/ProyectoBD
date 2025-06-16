using System;
using System.Windows.Forms;
using DAL; // Aquí debe estar ConexionOracle
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ProyectoBD
{
    public partial class FRMLogin : Form
    {
        private Conexion conexionOracle;

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

                string cadenaConexionOracle = config.GetConnectionString("OracleConnection");

                conexionOracle = new Conexion(cadenaConexionOracle);
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

            try
            {
                if (!conexionOracle.UsuarioExiste(usuario))
                {
                    MessageBox.Show("El usuario no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (conexionOracle.ValidarCredenciales(usuario, contrasena))
                {
                    int idUsuario = conexionOracle.ObtenerIdUsuario(usuario);

                    // Registramos inicio de sesión en bitácora:
                    conexionOracle.InsertarBitacora(idUsuario, $"Usuario {usuario} inició sesión.");

                    FRMHome home = new FRMHome(idUsuario);
                    home.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el proceso de login: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
