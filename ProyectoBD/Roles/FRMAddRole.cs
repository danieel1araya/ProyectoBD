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

namespace ProyectoBD.Roles
{
    public partial class FRMAddRole : Form
    {
        private Conexion conexionSql;
        private int _idUsuario;
        public FRMAddRole(int idUsuario)
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
                MessageBox.Show("Error al cargar configuración: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMRoles frm_rol = new FRMRoles(_idUsuario);
            frm_rol.Show();
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombreRol = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(nombreRol))
            {
                MessageBox.Show("Debe ingresar el nombre del rol.");
                return;
            }

            if (conexionSql.RolExiste(nombreRol))
            {
                MessageBox.Show("El nombre del rol ya existe.");
                return;
            }

            Rol nuevoRol = new Rol
            {
                NombreRol = nombreRol,
                Descripcion = descripcion
            };

            bool agregado = conexionSql.AgregarRol(nuevoRol,_idUsuario);

            if (agregado)
            {
                MessageBox.Show("Rol agregado correctamente.");
                FRMRoles frm_rol = new FRMRoles(_idUsuario);
                frm_rol.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el rol.");
            }
        }
    }
}