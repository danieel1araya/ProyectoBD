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

namespace ProyectoBD.Users
{
    public partial class FRMEditUser : Form
    {
        private User usuarioActual;
        private Conexion conexionSql;
        private int _idUsuario;

        public FRMEditUser(User user, Conexion conexion, int idUsuario  )
        {
            InitializeComponent();

            conexionSql = conexion;
            usuarioActual = user;

            // Cargar datos en controles
            txtUsuario.Text = usuarioActual.Usuario;
            txtContrasena.Text = usuarioActual.Contrasena;
            cmbEstado.SelectedIndex = usuarioActual.Activo == 1 ? 0 : 1;
            _idUsuario = idUsuario;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMUsuario frm_user = new FRMUsuario(_idUsuario);
            frm_user.Show();
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Validaciones (puedes agregar más)
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            // Actualizar el objeto usuarioActual (debería estar definido en tu clase)
            usuarioActual.Usuario = txtUsuario.Text.Trim();
            usuarioActual.Contrasena = txtContrasena.Text.Trim();
            usuarioActual.Activo = cmbEstado.SelectedItem.ToString() == "Activo" ? 1 : 0;

            try
            {
                bool resultado = conexionSql.ActualizarUsuario(usuarioActual, _idUsuario);

                if (resultado)
                {
                    MessageBox.Show("Usuario actualizado correctamente.");
                    FRMUsuario frm_user = new FRMUsuario(_idUsuario);
                    frm_user.Show();
                    this.Close();  
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el usuario.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

    }
}
