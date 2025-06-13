namespace ProyectoBD.Roles
{
    partial class FRMEditRole
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
            txtDescripcion = new TextBox();
            btnSalir = new Button();
            btnAgregar = new Button();
            txtNombre = new TextBox();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(446, 272);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(151, 23);
            txtDescripcion.TabIndex = 24;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(31, 394);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(108, 31);
            btnSalir.TabIndex = 23;
            btnSalir.Text = "Regresar";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(662, 394);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(108, 31);
            btnAgregar.TabIndex = 22;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnEditar_Click;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(446, 133);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(151, 23);
            txtNombre.TabIndex = 21;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(288, 263);
            label4.Name = "label4";
            label4.Size = new Size(127, 30);
            label4.TabIndex = 20;
            label4.Text = "Descripcion";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(272, 126);
            label2.Name = "label2";
            label2.Size = new Size(168, 30);
            label2.TabIndex = 19;
            label2.Text = "Nombre del Rol";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(288, 25);
            label1.Name = "label1";
            label1.Size = new Size(125, 32);
            label1.TabIndex = 18;
            label1.Text = "Editar Rol";
            // 
            // FRMEditRole
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtDescripcion);
            Controls.Add(btnSalir);
            Controls.Add(btnAgregar);
            Controls.Add(txtNombre);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FRMEditRole";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRMEditRole";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDescripcion;
        private Button btnSalir;
        private Button btnAgregar;
        private TextBox txtNombre;
        private Label label4;
        private Label label2;
        private Label label1;
    }
}