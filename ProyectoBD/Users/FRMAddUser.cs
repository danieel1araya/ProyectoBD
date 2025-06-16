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

namespace ProyectoBD.Users
{
    public partial class FRMAddUser : Form
    {
        private Conexion conexionSql;
        private int _idUsuario;
        public FRMAddUser(int idUsuario)
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
            FRMUsuario frm_user = new FRMUsuario(_idUsuario);
            frm_user.Show();
            this.Hide();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Debe completar todos los campos.");
                return;
            }

            // Verificás si el usuario ya existe
            if (conexionSql.UsuarioExiste(nombreUsuario))
            {
                MessageBox.Show("El nombre de usuario ya existe.");
                return;
            }

            // Crear objeto User
            User nuevoUsuario = new User
            {
                Usuario = nombreUsuario,
                Contrasena = contrasena,
                Activo = 1 // Por defecto activo
            };

            bool agregado = conexionSql.AgregarUsuario(nuevoUsuario, _idUsuario);

            if (agregado)
            {
                MessageBox.Show("Usuario agregado correctamente.");
                FRMUsuario frm_user = new FRMUsuario(_idUsuario);
                frm_user.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el usuario.");
            }
        }

        private void FRMAddUser_Load(object sender, EventArgs e)
        {

        }
    }
}
