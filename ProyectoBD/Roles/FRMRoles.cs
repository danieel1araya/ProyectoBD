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
    public partial class FRMRoles : Form
    {
        private Conexion conexionSql;
        private int _idUsuario;
        public FRMRoles(int idUsuario)
        {
            InitializeComponent();
            dgvRoles.ReadOnly = true;
            dgvRoles.AllowUserToAddRows = false;
            dgvRoles.AllowUserToDeleteRows = false;
            dgvRoles.AllowUserToResizeRows = false;
            dgvRoles.MultiSelect = false;
            CargarConfiguracion();
            CargarRoles();
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

        private void CargarRoles()
        {
            var roles = conexionSql.ObtenerRoles();

            dgvRoles.DataSource = null;
            dgvRoles.DataSource = roles;
            dgvRoles.Columns["Id"].Visible = false;

            MejorarInterfazDataGridView();
        }

        private void MejorarInterfazDataGridView()
        {
            var grid = dgvRoles;

            if (!grid.Columns.Contains("Editar"))
                grid.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Editar",
                    HeaderText = "",
                    Text = "Editar",
                    UseColumnTextForButtonValue = true,
                    Width = 70,
                });

            if (!grid.Columns.Contains("Eliminar"))
                grid.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Eliminar",
                    HeaderText = "",
                    Text = "Eliminar",
                    UseColumnTextForButtonValue = true,
                    Width = 75,
                });

            if (!grid.Columns.Contains("Permisos"))
                grid.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Permisos",
                    HeaderText = "",
                    Text = "Administrar Permisos",
                    UseColumnTextForButtonValue = true,
                    Width = 150,
                });

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.DefaultCellStyle.SelectionBackColor = Color.LightBlue;

            grid.CellPainting -= dgvRoles_CellPainting;
            grid.CellPainting += dgvRoles_CellPainting;
        }

        private void dgvRoles_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string colName = dgvRoles.Columns[e.ColumnIndex].Name;
            if (colName != "Editar" && colName != "Eliminar" && colName != "Permisos")
                return;

            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.ClipBounds);

            Color bgColor = colName switch
            {
                "Editar" => Color.LightSteelBlue,
                "Eliminar" => Color.IndianRed,
                "Permisos" => Color.MediumPurple,
                _ => Color.White
            };

            Color fgColor = Color.White;

            using (Brush brush = new SolidBrush(bgColor))
                e.Graphics.FillRectangle(brush, e.CellBounds);

            string textoBoton = dgvRoles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
            TextRenderer.DrawText(
                e.Graphics,
                textoBoton,
                dgvRoles.Font,
                e.CellBounds,
                fgColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );

            e.Handled = true;
        }

        private void dgvRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columna = dgvRoles.Columns[e.ColumnIndex].Name;

                if (columna == "Editar")
                {
                    // Obtener el rol seleccionado
                    var fila = dgvRoles.Rows[e.RowIndex];

                    Rol rolSeleccionado = new Rol
                    {
                        Id = Convert.ToInt32(fila.Cells["Id"].Value),
                        NombreRol = fila.Cells["NombreRol"].Value.ToString(),
                        Descripcion = fila.Cells["Descripcion"].Value.ToString()
                    };

                    FRMEditRole frmEdit = new FRMEditRole(rolSeleccionado, conexionSql, _idUsuario);
                    frmEdit.Show();
                    this.Close();
                }
                else if (columna == "Eliminar")
                {
                    var fila = dgvRoles.Rows[e.RowIndex];

                    int idRol = Convert.ToInt32(fila.Cells["Id"].Value);
                    string nombreRol = fila.Cells["NombreRol"].Value.ToString();

                    var confirmacion = MessageBox.Show($"¿Seguro que deseas eliminar el rol '{nombreRol}'?",
                                                       "Confirmar eliminación",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Warning);

                    if (confirmacion == DialogResult.Yes)
                    {
                        string mensaje;
                        bool eliminado = conexionSql.EliminarRol(idRol, _idUsuario, out mensaje);

                        if (eliminado)
                        {
                            MessageBox.Show(mensaje);
                            this.Close();
                            FRMRoles frm = new FRMRoles(_idUsuario);
                            frm.Show();
                        }
                        else
                        {
                            MessageBox.Show(mensaje);
                        }
                    }
                }
                else if (columna == "Permisos")
                {
                    var fila = dgvRoles.Rows[e.RowIndex];
                    int idRol = Convert.ToInt32(fila.Cells["Id"].Value);
                    // Abrir ventana para administrar permisos
                    FRMAdmRole frmPermisos = new FRMAdmRole(idRol, conexionSql, _idUsuario);
                    frmPermisos.Show();
                    this.Close();
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FRMAddRole frm = new FRMAddRole(_idUsuario);
            frm.Show();
            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMHome frm = new FRMHome(_idUsuario);
            frm.Show();
            this.Hide();
        }

        private void dgvRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}