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

namespace ProyectoBD.Screens
{
    public partial class FRMEditPan : Form
    {
        private Pantalla pantallaActual;
        private Conexion conexionSql;
        private List<Sistema> sistemasDisponibles;
        private int _idUsuario;

        public FRMEditPan(Pantalla pantalla, Conexion conexion, int idUsuario)
        {
            InitializeComponent();

            pantallaActual = pantalla;
            conexionSql = conexion;


            CargarDatosPantalla();
            _idUsuario = idUsuario;
        }



        private void CargarDatosPantalla()
        {
            txtNombre.Text = pantallaActual.NombrePantalla;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            try
            {
                pantallaActual.NombrePantalla = txtNombre.Text.Trim();


                bool resultado = conexionSql.ActualizarPantalla(pantallaActual, _idUsuario);

                if (resultado)
                {
                    MessageBox.Show("Pantalla actualizada correctamente.");
                    FRMPantalla frmPantallas = new FRMPantalla(_idUsuario);
                    frmPantallas.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la pantalla.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar pantalla: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMPantalla frmPantallas = new FRMPantalla(_idUsuario);
            frmPantallas.Show();
            this.Close();
        }
    }
}