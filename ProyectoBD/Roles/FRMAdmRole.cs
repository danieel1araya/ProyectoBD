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
        private Conexion conexionOracle;
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
            conexionOracle = conexion;
            CargarPantallas();
            InicializarCheckPermisos();
            _idUsuario = idUsuario;
        }

        private void CargarPantallas()
        {
            try
            {
                var pantallas = conexionOracle.ObtenerPantallas();

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
            var permisosAsignados = conexionOracle.ObtenerPermisosAsignados(idRol, idPantalla);

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
                permisosSeleccionados.Add(conexionOracle.ObtenerIdPermisoPorNombre("Insertar"));
            if (chkModificar.Checked)
                permisosSeleccionados.Add(conexionOracle.ObtenerIdPermisoPorNombre("Modificar"));
            if (chkEliminar.Checked)
                permisosSeleccionados.Add(conexionOracle.ObtenerIdPermisoPorNombre("Eliminar"));
            if (chkConsultar.Checked)
                permisosSeleccionados.Add(conexionOracle.ObtenerIdPermisoPorNombre("Consultar"));

            try
            {
                foreach (Pantalla pantalla in checkedListBoxPantallas.CheckedItems)
                {
                    // Obtener los permisos actualmente en la base de datos
                    var permisosActuales = conexionOracle.ObtenerPermisosAsignados(idRol, pantalla.Id);

                    // Agregar nuevos que no están
                    foreach (int idPermiso in permisosSeleccionados)
                    {
                        if (!permisosActuales.Contains(idPermiso))
                            conexionOracle.AsignarPermisoARol(idRol, pantalla.Id, idPermiso, _idUsuario);
                    }

                    // Eliminar los que están pero ya no están en la selección
                    foreach (int idPermiso in permisosActuales)
                    {
                        if (!permisosSeleccionados.Contains(idPermiso))
                            conexionOracle.EliminarPermisoARol(idRol, pantalla.Id, idPermiso, _idUsuario);
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

        private void FRMAdmRole_Load(object sender, EventArgs e)
        {

        }
    }
}


