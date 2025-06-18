namespace ProyectoBD
{
    partial class FRMHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMHome));
            btnSistemas = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            btnUsuarios = new Button();
            pictureBox3 = new PictureBox();
            btnRoles = new Button();
            pictureBox4 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // btnSistemas
            // 
            btnSistemas.Location = new Point(68, 262);
            btnSistemas.Name = "btnSistemas";
            btnSistemas.Size = new Size(75, 23);
            btnSistemas.TabIndex = 1;
            btnSistemas.Text = "Sistemas";
            btnSistemas.UseVisualStyleBackColor = true;
            btnSistemas.Click += btnSistemas_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(42, 102);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(129, 135);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.AccessibleRole = AccessibleRole.None;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(239, 102);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(129, 135);
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // btnUsuarios
            // 
            btnUsuarios.Location = new Point(263, 262);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(75, 23);
            btnUsuarios.TabIndex = 4;
            btnUsuarios.Text = "Usuarios";
            btnUsuarios.UseVisualStyleBackColor = true;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(442, 102);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(122, 135);
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            // 
            // btnRoles
            // 
            btnRoles.Location = new Point(463, 262);
            btnRoles.Name = "btnRoles";
            btnRoles.Size = new Size(75, 23);
            btnRoles.TabIndex = 6;
            btnRoles.Text = "Roles";
            btnRoles.UseVisualStyleBackColor = true;
            btnRoles.Click += btnRoles_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(636, 102);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(122, 135);
            pictureBox4.TabIndex = 7;
            pictureBox4.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(661, 262);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 8;
            button1.Text = "Pantallas";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(2, 3);
            button2.Name = "button2";
            button2.Size = new Size(103, 23);
            button2.TabIndex = 9;
            button2.Text = "Cerrar Sesión";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // FRMHome
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox4);
            Controls.Add(btnRoles);
            Controls.Add(pictureBox3);
            Controls.Add(btnUsuarios);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(btnSistemas);
            Name = "FRMHome";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRMHome";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSistemas;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button btnUsuarios;
        private PictureBox pictureBox3;
        private Button btnRoles;
        private PictureBox pictureBox4;
        private Button button1;
        private Button button2;
    }
}