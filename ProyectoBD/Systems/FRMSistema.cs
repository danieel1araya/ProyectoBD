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
using ProyectoBD.Systems;
using ProyectoBD.Users;

namespace ProyectoBD
{
    public partial class FRMSistema : Form
    {
        private int _idUsuario;
        private Conexion conexionSql;


        public FRMSistema(int idUsuario)
        {
            InitializeComponent();
            dgvSistemas.ReadOnly = true;
            dgvSistemas.AllowUserToAddRows = false;
            dgvSistemas.AllowUserToDeleteRows = false;
            dgvSistemas.AllowUserToResizeRows = false;
            dgvSistemas.MultiSelect = false;
            CargarConfiguracion();
            CargarSistemas();
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


        public void CargarSistemas()
        {
            var sistemas = conexionSql.ObtenerSistemasSistema(); 

            dgvSistemas.DataSource = null;
            dgvSistemas.DataSource = sistemas;

            MejorarInterfazDataGridView();
        }



        private void MejorarInterfazDataGridView()
        {
            var grid = dgvSistemas;

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
            grid.CellPainting -= dgvSistemas_CellPainting;
            grid.CellPainting += dgvSistemas_CellPainting;
        }

        private void dgvSistemas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string colName = dgvSistemas.Columns[e.ColumnIndex].Name;
            if (colName != "Editar" && colName != "dgvSistemas")
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
                dgvSistemas.Font,
                e.CellBounds,
                fgColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );

            e.Handled = true;
        }

        private void dgvSistemas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columna = dgvSistemas.Columns[e.ColumnIndex].Name;

                if (columna == "Editar")
                {
                    var fila = dgvSistemas.Rows[e.RowIndex];

                    var sistemaSeleccionado = new Sistema
                    {
                        Id = Convert.ToInt32(fila.Cells["Id"].Value),
                        NombreSistema = fila.Cells["NombreSistema"].Value.ToString()
                    };

                    FRMEditSys frmEdit = new FRMEditSys(sistemaSeleccionado, conexionSql, _idUsuario);
                    frmEdit.Show();
                    this.Close();
                }

                if (columna == "Eliminar")
                {
                    var fila = dgvSistemas.Rows[e.RowIndex];

                    int idSistema = Convert.ToInt32(fila.Cells["Id"].Value);
                    string nombreSistema = fila.Cells["NombreSistema"].Value.ToString();

                    var resultado = MessageBox.Show($"¿Seguro que deseas eliminar el sistema '{nombreSistema}'?",
                                                    "Confirmar eliminación",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);

                    if (resultado == DialogResult.Yes)
                    {
                        try
                        {
                            bool eliminado = conexionSql.EliminarSistema(idSistema, _idUsuario);

                            if (eliminado)
                            {
                                MessageBox.Show("Sistema eliminado correctamente.");
                                this.Close();
                                FRMSistema frm = new FRMSistema(_idUsuario);
                                frm.Show();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo eliminar el sistema.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FRMAddSys frm_sys = new FRMAddSys(_idUsuario);
            frm_sys.Show();
            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FRMHome frm_home = new FRMHome(_idUsuario);
            frm_home.Show();
            this.Hide();
        }

        private void dgvSistemas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
