using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL;  // Tu capa de negocio
using DAL;  // Tu capa de acceso a datos

namespace ProyectoBD.Roles
{
    public partial class FRMAdmRole : Form
    {
        private Conexion conexionSql;
        private int _idUsuario;
        private int idRol;
        const int ID_CREAR = 1;
        const int ID_EDITAR = 2;
        const int ID_ELIMINAR = 3;
        const int ID_VER = 4;

        public FRMAdmRole(int idRol, Conexion conexion, int idUsuario)
        {
            InitializeComponent();
            this.idRol = idRol;
            conexionSql = conexion;
            CargarPantallas();
            InicializarCheckPermisos();
            _idUsuario = idUsuario;
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
                CargarPermisosAsignados(idRol, pantallaSeleccionada.Id);

            }
        }



        private void CargarPermisosAsignados(int idRol, int idPantalla)
        {
            var permisosAsignados = conexionSql.ObtenerPermisosAsignados(idRol, idPantalla);

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
                    // Obtener los permisos actualmente asignados en la BD para esa pantalla
                    var permisosActuales = conexionSql.ObtenerPermisosAsignados(idRol, pantalla.Id);

                    // Agregar nuevos permisos
                    foreach (int idPermiso in permisosSeleccionados)
                    {
                        if (!permisosActuales.Contains(idPermiso))
                            conexionSql.AsignarPermisoARol(idRol, pantalla.Id, idPermiso,_idUsuario);
                    }

                    // Eliminar permisos que se desmarcaron
                    foreach (int idPermiso in permisosActuales)
                    {
                        if (!permisosSeleccionados.Contains(idPermiso))
                            conexionSql.EliminarPermisoARol(idRol, pantalla.Id, idPermiso, _idUsuario);
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
            FRMRoles fRMRoles = new FRMRoles(_idUsuario);
            fRMRoles.Show();
            this.Close();
        }


    }
}

