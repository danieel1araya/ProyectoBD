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

namespace ProyectoBD.Screens
{
    public partial class FRMPantalla : Form
    {
        private Conexion conexionSql;
        private int _idUsuario;

        public FRMPantalla(int idUsuario)
        {
            InitializeComponent();
            dgvPantallas.ReadOnly = true;
            dgvPantallas.AllowUserToAddRows = false;
            dgvPantallas.AllowUserToDeleteRows = false;
            dgvPantallas.AllowUserToResizeRows = false;
            dgvPantallas.MultiSelect = false;

            CargarConfiguracion();
            CargarPantallas();
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

        public void CargarPantallas()
        {
            var pantallas = conexionSql.ObtenerPantallas(); // Tu método DAL que devuelve lista de pantallas
            dgvPantallas.DataSource = null;
            dgvPantallas.DataSource = pantallas;

            MejorarInterfazDataGridView();
        }

        private void MejorarInterfazDataGridView()
        {
            var grid = dgvPantallas;

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

            if (!grid.Columns.Contains("Eliminar"))
            {
                var btnEliminar = new DataGridViewButtonColumn
                {
                    Name = "Eliminar",
                    HeaderText = "",
                    Text = "Eliminar",
                    UseColumnTextForButtonValue = true,
                    Width = 75,
                };
                grid.Columns.Add(btnEliminar);
            }

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

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            grid.CellPainting -= dgvPantallas_CellPainting;
            grid.CellPainting += dgvPantallas_CellPainting;
        }

        private void dgvPantallas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvPantallas.Columns[e.ColumnIndex].Name;
            if (colName != "Editar" && colName != "Eliminar") return;

            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.ClipBounds);

            Color bgColor = colName == "Editar" ? Color.LightSteelBlue : Color.IndianRed;
            Color fgColor = colName == "Editar" ? Color.Black : Color.White;

            using (Brush brush = new SolidBrush(bgColor))
                e.Graphics.FillRectangle(brush, e.CellBounds);

            TextRenderer.DrawText(
                e.Graphics,
                colName,
                dgvPantallas.Font,
                e.CellBounds,
                fgColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );

            e.Handled = true;
        }

        private void dgvPantallas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columna = dgvPantallas.Columns[e.ColumnIndex].Name;

                var fila = dgvPantallas.Rows[e.RowIndex];
                int idPantalla = Convert.ToInt32(fila.Cells["Id"].Value);
                string nombrePantalla = fila.Cells["NombrePantalla"].Value.ToString();

                if (columna == "Editar")
                {
                    var pantalla = new Pantalla
                    {
                        Id = idPantalla,
                        NombrePantalla = nombrePantalla,
                        IdSistema = Convert.ToInt32(fila.Cells["IdSistema"].Value)
                    };

                    FRMEditPan frmEdit = new FRMEditPan(pantalla, conexionSql,_idUsuario);
                    frmEdit.Show();
                    this.Close();
                }
                else if (columna == "Eliminar")
                {
                    var confirm = MessageBox.Show($"¿Seguro que deseas eliminar la pantalla '{nombrePantalla}'?",
                                                  "Confirmar eliminación",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

                    if (confirm == DialogResult.Yes)
                    {
                        bool eliminado = conexionSql.EliminarPantalla(idPantalla, _idUsuario);

                        if (eliminado)
                        {
                            MessageBox.Show("Pantalla eliminada correctamente.");
                            this.Close();
                            FRMPantalla frm = new FRMPantalla(_idUsuario);
                            frm.Show();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar la pantalla.");
                        }
                    }
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FRMAddPan frmAdd = new FRMAddPan(_idUsuario);
            frmAdd.Show();
            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMHome frm_home = new FRMHome(_idUsuario);
            frm_home.Show();
            this.Hide();
        }
    }
}
