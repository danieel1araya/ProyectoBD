using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoBD.Roles;
using ProyectoBD.Screens;

namespace ProyectoBD
{
    public partial class FRMHome : Form
    {
        private int _idUsuario;
        public FRMHome(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
        }

        private void btnSistemas_Click(object sender, EventArgs e)
        {
            FRMSistema frm_system = new FRMSistema(_idUsuario);
            frm_system.Show();
            this.Hide();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FRMUsuario frm_user = new FRMUsuario(_idUsuario);
            frm_user.Show();
            this.Hide();
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            FRMRoles frm_roles = new FRMRoles(_idUsuario);
            frm_roles.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FRMPantalla frm_pantallas = new FRMPantalla(_idUsuario);
            frm_pantallas.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FRMLogin fRMLogin = new FRMLogin();
            fRMLogin.Show();
            this.Close();
        }
    }
}
