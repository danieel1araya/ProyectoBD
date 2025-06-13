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

namespace ProyectoBD.Roles
{
    public partial class FRMEditRole : Form
    {
        private Rol rolActual;
        private Conexion conexionSql;
        private int _idUsuario;

        public FRMEditRole(Rol rolSeleccionado, Conexion conexion, int idUsuario)
        {
            InitializeComponent();
            conexionSql = conexion;

            // Obtener rol por ID
            rolActual = rolSeleccionado;

            if (rolActual == null)
            {
                MessageBox.Show("No se pudo cargar la información del rol.");
                this.Close();
                return;
            }

            // Cargar datos en controles
            txtNombre.Text = rolActual.NombreRol;
            txtDescripcion.Text = rolActual.Descripcion;
            _idUsuario = idUsuario;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMRoles frmRol = new FRMRoles(_idUsuario);
            frmRol.Show();
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del rol no puede estar vacío.");
                return;
            }

            rolActual.NombreRol = txtNombre.Text.Trim();
            rolActual.Descripcion = txtDescripcion.Text.Trim();

            try
            {
                bool actualizado = conexionSql.ActualizarRol(rolActual, _idUsuario);

                if (actualizado)
                {
                    MessageBox.Show("Rol actualizado correctamente.");
                    FRMRoles frmRol = new FRMRoles(_idUsuario);
                    frmRol.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el rol.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el rol: " + ex.Message);
            }
        }
    }
}
