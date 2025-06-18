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
using ProyectoBD.Users;

namespace ProyectoBD
{
    public partial class FRMUsuario : Form
    {

        private Conexion conexionSql;
        private int _idUsuario;

        public FRMUsuario(int idUsuario)
        {
            InitializeComponent();
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AllowUserToResizeRows = false;
            dgvUsuarios.MultiSelect = false;
            CargarConfiguracion();
            CargarUsuarios();
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
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }


        public void CargarUsuarios()
        {
            var usuarios = conexionSql.ObtenerUsuarios(); // Usa tu clase de conexión

            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuarios;

            dgvUsuarios.Columns["Activo"].Visible = false;

            dgvUsuarios.Columns["EstadoTexto"].HeaderText = "Estado";

            MejorarInterfazDataGridView();
        }



        private void MejorarInterfazDataGridView()
        {
            var grid = dgvUsuarios;

            // Evitar duplicados si se llama varias veces
            if (!grid.Columns.Contains("Editar"))
            {
                var btnEditar = new DataGridViewButtonColumn
                {
                    Name = "Editar",
                    HeaderText = "",
                    Text = "Editar",
                    UseColumnTextForButtonValue = true,
                    Width = 70,
                };
                grid.Columns.Add(btnEditar);
            }

            if (!grid.Columns.Contains("Permisos"))
            {
                var btnPermisos = new DataGridViewButtonColumn
                {
                    Name = "Permisos",
                    HeaderText = "",
                    Text = "Permisos",
                    UseColumnTextForButtonValue = true,
                    Width = 85,
                };
                grid.Columns.Add(btnPermisos);
            }

            if (!grid.Columns.Contains("Roles"))
            {
                var btnRoles = new DataGridViewButtonColumn
                {
                    Name = "Roles",
                    HeaderText = "",
                    Text = "Roles",
                    UseColumnTextForButtonValue = true,
                    Width = 75,
                };
                grid.Columns.Add(btnRoles);
            }

            // Estilos generales
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.EnableHeadersVisualStyles = false;

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.ColumnHeadersHeight = 40;

            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            grid.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;

            grid.RowsDefaultCellStyle.BackColor = Color.White;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            grid.RowTemplate.Height = 35;

            // Ajuste de columnas
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Evento de pintado personalizado para que los colores de los botones se vean siempre
            grid.CellPainting -= dgvUsuarios_CellPainting;
            grid.CellPainting += dgvUsuarios_CellPainting;
        }

        private void dgvUsuarios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string colName = dgvUsuarios.Columns[e.ColumnIndex].Name;
            if (colName != "Editar" && colName != "Eliminar")
                return;

            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.ClipBounds);

            Color bgColor = colName == "Editar" ? Color.LightSteelBlue : Color.IndianRed;
            Color fgColor = colName == "Editar" ? Color.Black : Color.White;

            using (Brush brush = new SolidBrush(bgColor))
                e.Graphics.FillRectangle(brush, e.CellBounds);

            TextRenderer.DrawText(
                e.Graphics,
                colName,
                dgvUsuarios.Font,
                e.CellBounds,
                fgColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );

            e.Handled = true;
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columna = dgvUsuarios.Columns[e.ColumnIndex].Name;

                if (columna == "Editar")
                {
                    // Obtener el usuario seleccionado
                    var fila = dgvUsuarios.Rows[e.RowIndex];

                    User usuarioSeleccionado = new User
                    {
                        Id = Convert.ToInt32(fila.Cells["Id"].Value),
                        Usuario = fila.Cells["Usuario"].Value.ToString(),
                        Contrasena = fila.Cells["Contrasena"].Value.ToString(),
                        Activo = Convert.ToInt32(fila.Cells["Activo"].Value)
                    };


                    FRMEditUser frmEdit = new FRMEditUser(usuarioSeleccionado, conexionSql, _idUsuario);
                    frmEdit.Show();
                    this.Close();
                }

                if (columna == "Permisos")
                {
                    var fila = dgvUsuarios.Rows[e.RowIndex];
                    int idUsuario = Convert.ToInt32(fila.Cells["Id"].Value);


                    FRMAddUserPerm frmPermisos = new FRMAddUserPerm(idUsuario, conexionSql, _idUsuario);
                    frmPermisos.Show();
                }

                if (columna == "Roles")
                {
                    var fila = dgvUsuarios.Rows[e.RowIndex];
                    int idUsuario = Convert.ToInt32(fila.Cells["Id"].Value);


                    FRMAddUserRol frmRoles = new FRMAddUserRol(idUsuario, conexionSql, _idUsuario);
                    frmRoles.Show();
                }

            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FRMAddUser frm_user = new FRMAddUser(_idUsuario);
            frm_user.Show();
            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMHome frm_home = new FRMHome(_idUsuario);
            frm_home.Show();
            this.Hide();
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
