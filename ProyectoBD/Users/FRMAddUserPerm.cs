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
    public partial class FRMAddUserPerm : Form
    {
        private Conexion conexionSql;
        private int idUsuario;
        private int _idUsuario;
        const int ID_CREAR = 1;
        const int ID_EDITAR = 2;
        const int ID_ELIMINAR = 3;
        const int ID_VER = 4;
        public FRMAddUserPerm(int idUsuario, Conexion conexion,int _idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            this._idUsuario = _idUsuario;
            conexionSql = conexion;
            CargarPantallas();
            InicializarCheckPermisos();
        }

        private void CargarPantallas()
        {
            try
            {
                var pantallas = conexionSql.ObtenerPantallas();

                checkedListBoxPantallas.Items.Clear();
                foreach (var pantalla in pantallas)
                {
                    checkedListBoxPantallas.Items.Add(pantalla, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando pantallas: " + ex.Message);
            }
        }

        private void InicializarCheckPermisos()
        {
            chkInsertar.Checked = false;
            chkModificar.Checked = false;
            chkEliminar.Checked = false;
            chkConsultar.Checked = false;
        }

        private void checkedListBoxPantallas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxPantallas.SelectedItem is Pantalla pantallaSeleccionada)
            {
                CargarPermisosAsignados(idUsuario, pantallaSeleccionada.Id);
            }
        }

        private void CargarPermisosAsignados(int idUsuario, int idPantalla)
        {
            var permisosAsignados = conexionSql.ObtenerPermisosAsignadosUsuario(idUsuario, idPantalla);

            chkInsertar.Checked = permisosAsignados.Contains(ID_CREAR);
            chkModificar.Checked = permisosAsignados.Contains(ID_EDITAR);
            chkEliminar.Checked = permisosAsignados.Contains(ID_ELIMINAR);
            chkConsultar.Checked = permisosAsignados.Contains(ID_VER);
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (checkedListBoxPantallas.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecciona al menos una pantalla.");
                return;
            }

            List<int> permisosSeleccionados = new List<int>();

            if (chkInsertar.Checked)
                permisosSeleccionados.Add(conexionSql.ObtenerIdPermisoPorNombre("Insertar"));
            if (chkModificar.Checked)
                permisosSeleccionados.Add(conexionSql.ObtenerIdPermisoPorNombre("Modificar"));
            if (chkEliminar.Checked)
                permisosSeleccionados.Add(conexionSql.ObtenerIdPermisoPorNombre("Eliminar"));
            if (chkConsultar.Checked)
                permisosSeleccionados.Add(conexionSql.ObtenerIdPermisoPorNombre("Consultar"));

            if (permisosSeleccionados.Count == 0)
            {
                MessageBox.Show("Selecciona al menos un permiso.");
                return;
            }

            try
            {
                foreach (Pantalla pantalla in checkedListBoxPantallas.CheckedItems)
                {
                    // Obtener los permisos actuales asignados al usuario para esa pantalla
                    var permisosActuales = conexionSql.ObtenerPermisosAsignadosUsuario(idUsuario, pantalla.Id);

                    // Agregar permisos que no existan
                    foreach (int idPermiso in permisosSeleccionados)
                    {
                        if (!permisosActuales.Contains(idPermiso))
                            conexionSql.AsignarPermisoAUsuario(idUsuario, pantalla.Id, idPermiso, _idUsuario);
                    }

                    // Quitar permisos desmarcados
                    foreach (int idPermiso in permisosActuales)
                    {
                        if (!permisosSeleccionados.Contains(idPermiso))
                            conexionSql.EliminarPermisoAUsuario(idUsuario, pantalla.Id, idPermiso, _idUsuario);
                    }
                }

                MessageBox.Show("Permisos actualizados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar permisos: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMUsuario frmUsuarios = new FRMUsuario(_idUsuario);
            frmUsuarios.Show();
            this.Close();
        }
    }
}