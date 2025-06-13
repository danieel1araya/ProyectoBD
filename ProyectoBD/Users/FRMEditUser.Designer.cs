namespace ProyectoBD.Users
{
    partial class FRMEditUser
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
            btnSalir = new Button();
            btnEditar = new Button();
            txtContrasena = new TextBox();
            txtUsuario = new TextBox();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            cmbEstado = new ComboBox();
            SuspendLayout();
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(31, 394);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(108, 31);
            btnSalir.TabIndex = 16;
            btnSalir.Text = "Regresar";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(662, 394);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(108, 31);
            btnEditar.TabIndex = 15;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(446, 225);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(151, 23);
            txtContrasena.TabIndex = 14;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(446, 133);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(151, 23);
            txtUsuario.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(301, 216);
            label4.Name = "label4";
            label4.Size = new Size(123, 30);
            label4.TabIndex = 12;
            label4.Text = "Contraseña";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(301, 126);
            label2.Name = "label2";
            label2.Size = new Size(88, 30);
            label2.TabIndex = 11;
            label2.Text = "Usuario";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(288, 25);
            label1.Name = "label1";
            label1.Size = new Size(187, 32);
            label1.TabIndex = 10;
            label1.Text = "Editar Usuarios";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(301, 305);
            label3.Name = "label3";
            label3.Size = new Size(78, 30);
            label3.TabIndex = 17;
            label3.Text = "Estado";
            // 
            // cmbEstado
            // 
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cmbEstado.Location = new Point(446, 312);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(151, 23);
            cmbEstado.TabIndex = 18;
            // 
            // FRMEditUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cmbEstado);
            Controls.Add(label3);
            Controls.Add(btnSalir);
            Controls.Add(btnEditar);
            Controls.Add(txtContrasena);
            Controls.Add(txtUsuario);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FRMEditUser";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRMEditUser";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSalir;
        private Button btnEditar;
        private TextBox txtContrasena;
        private TextBox txtUsuario;
        private Label label4;
        private Label label2;
        private Label label1;
        private Label label3;
        private ComboBox cmbEstado;
    }
}