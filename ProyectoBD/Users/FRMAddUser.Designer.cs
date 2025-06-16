namespace ProyectoBD.Users
{
    partial class FRMAddUser
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
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            txtUsuario = new TextBox();
            txtContrasena = new TextBox();
            btnAgregar = new Button();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(279, 22);
            label1.Name = "label1";
            label1.Size = new Size(213, 32);
            label1.TabIndex = 2;
            label1.Text = "Agregar Usuarios";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(292, 123);
            label2.Name = "label2";
            label2.Size = new Size(88, 30);
            label2.TabIndex = 3;
            label2.Text = "Usuario";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(292, 260);
            label4.Name = "label4";
            label4.Size = new Size(123, 30);
            label4.TabIndex = 5;
            label4.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(437, 130);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(151, 23);
            txtUsuario.TabIndex = 6;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(437, 269);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(151, 23);
            txtContrasena.TabIndex = 7;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(653, 391);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(108, 31);
            btnAgregar.TabIndex = 8;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(22, 391);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(108, 31);
            btnSalir.TabIndex = 9;
            btnSalir.Text = "Regresar";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // FRMAddUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSalir);
            Controls.Add(btnAgregar);
            Controls.Add(txtContrasena);
            Controls.Add(txtUsuario);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FRMAddUser";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRMAddUser";
            Load += FRMAddUser_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label4;
        private TextBox txtUsuario;
        private TextBox txtContrasena;
        private Button btnAgregar;
        private Button btnSalir;
    }
}