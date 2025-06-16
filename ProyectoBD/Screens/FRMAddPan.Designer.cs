namespace ProyectoBD.Screens
{
    partial class FRMAddPan
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
            btnAgregar = new Button();
            txtNombre = new TextBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            cmbSistemas = new ComboBox();
            SuspendLayout();
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(31, 394);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(108, 31);
            btnSalir.TabIndex = 21;
            btnSalir.Text = "Regresar";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(662, 394);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(108, 31);
            btnAgregar.TabIndex = 20;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(473, 143);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(151, 23);
            txtNombre.TabIndex = 19;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(235, 136);
            label2.Name = "label2";
            label2.Size = new Size(232, 30);
            label2.TabIndex = 18;
            label2.Text = "Nombre de la pantalla";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(288, 25);
            label1.Name = "label1";
            label1.Size = new Size(205, 32);
            label1.TabIndex = 17;
            label1.Text = "Agregar Pantalla";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(312, 229);
            label3.Name = "label3";
            label3.Size = new Size(89, 30);
            label3.TabIndex = 22;
            label3.Text = "Sistema";
            // 
            // cmbSistemas
            // 
            cmbSistemas.FormattingEnabled = true;
            cmbSistemas.Location = new Point(473, 236);
            cmbSistemas.Name = "cmbSistemas";
            cmbSistemas.Size = new Size(121, 23);
            cmbSistemas.TabIndex = 23;
            cmbSistemas.SelectedIndexChanged += cmbSistemas_SelectedIndexChanged;
            // 
            // FRMAddPan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cmbSistemas);
            Controls.Add(label3);
            Controls.Add(btnSalir);
            Controls.Add(btnAgregar);
            Controls.Add(txtNombre);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FRMAddPan";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRMAddPan";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSalir;
        private Button btnAgregar;
        private TextBox txtNombre;
        private Label label2;
        private Label label1;
        private Label label3;
        private ComboBox cmbSistemas;
    }
}