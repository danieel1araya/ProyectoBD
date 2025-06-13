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

namespace ProyectoBD.Systems
{
    public partial class FRMEditSys : Form
    {
        private Sistema sistemaActual;
        private Conexion conexionSql;
        private int _idUsuario;

        public FRMEditSys(Sistema sistema, Conexion conexion, int idUsuario)
        {
            InitializeComponent();

            conexionSql = conexion;
            sistemaActual = sistema;

            // Cargar datos en controles
            txtNombre.Text = sistemaActual.NombreSistema;
            _idUsuario = idUsuario;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMSistema frmSistemas = new FRMSistema(_idUsuario);
            frmSistemas.Show();
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del sistema no puede estar vacío.");
                return;
            }

            sistemaActual.NombreSistema = txtNombre.Text.Trim();

            try
            {
                bool resultado = conexionSql.ActualizarSistema(sistemaActual, _idUsuario);

                if (resultado)
                {
                    MessageBox.Show("Sistema actualizado correctamente.");
                    FRMSistema frmSistemas = new FRMSistema(_idUsuario);
                    frmSistemas.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el sistema.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }
    }
}
