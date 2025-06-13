using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DAL; // Asegúrate de tener acceso a tu clase Conexion
using BAL; // Suponiendo que tenés la clase Rol


namespace ProyectoBD.Users
{
    public partial class FRMAddUserRol : Form
    {
        private int idUsuario;
        private int _idUsuario;
        private Conexion conexionSql;
        private List<Rol> rolesDisponibles;
        private List<Rol> rolesAsignados;

        public FRMAddUserRol(int idUsuario, Conexion conexion, int _idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            this._idUsuario = _idUsuario;
            this.conexionSql = conexion;

            CargarRolesDisponibles();
            CargarRolesAsignados();
        }

        private void CargarRolesDisponibles()
        {
            rolesDisponibles = conexionSql.ObtenerRoles();

            checkedListBoxRoles.Items.Clear();

            foreach (var rol in rolesDisponibles)
            {
                checkedListBoxRoles.Items.Add(rol, false);
            }
        }

        private void CargarRolesAsignados()
        {
            rolesAsignados = conexionSql.ObtenerRolesPorUsuario(idUsuario);

            flowLayoutPanelRoles.Controls.Clear();

            foreach (var rol in rolesAsignados)
            {
                var panel = new Panel
                {
                    Height = 30,
                    Width = 250,
                    BackColor = Color.LightGray,
                    Margin = new Padding(5)
                };

                var lbl = new Label
                {
                    Text = rol.NombreRol,
                    AutoSize = true,
                    Location = new Point(5, 7)
                };

                var btnEliminar = new Button
                {
                    Text = "X",
                    Width = 30,
                    Height = 25,
                    Location = new Point(180, 2),
                    Tag = rol // Guardamos el rol en el botón para usarlo después
                };

                btnEliminar.Click += (s, e) =>
                {
                    var boton = s as Button;
                    var rolAEliminar = (Rol)boton.Tag;

                    var confirmar = MessageBox.Show($"¿Eliminar rol '{rolAEliminar.NombreRol}'?", "Confirmar", MessageBoxButtons.YesNo);
                    if (confirmar == DialogResult.Yes)
                    {
                        conexionSql.EliminarRolDeUsuario(idUsuario, rolAEliminar.Id, _idUsuario);
                        CargarRolesAsignados();
                    }
                };

                panel.Controls.Add(lbl);
                panel.Controls.Add(btnEliminar);
                flowLayoutPanelRoles.Controls.Add(panel);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var rolesSeleccionados = new List<int>();

            foreach (var item in checkedListBoxRoles.CheckedItems)
            {
                if (item is Rol rol)
                {
                    rolesSeleccionados.Add(rol.Id);
                }
            }

            if (rolesSeleccionados.Count == 0)
            {
                MessageBox.Show("Selecciona al menos un rol para asignar.");
                return;
            }

            foreach (int idRol in rolesSeleccionados)
            {
                conexionSql.AsignarRolAUsuario(idUsuario, idRol,_idUsuario);
            }

            MessageBox.Show("Roles asignados correctamente.");
            CargarRolesAsignados(); // Refresca la lista de roles
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMUsuario frm_home = new FRMUsuario(_idUsuario);
            frm_home.Show();
            this.Hide();
        }
    }
}
