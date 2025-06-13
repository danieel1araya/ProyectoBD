namespace ProyectoBD.Screens
{
    partial class FRMPantalla
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
            label1 = new Label();
            dgvPantallas = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvPantallas).BeginInit();
            SuspendLayout();
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(525, 412);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 11;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(196, 412);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(75, 23);
            btnAgregar.TabIndex = 10;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(344, 9);
            label1.Name = "label1";
            label1.Size = new Size(116, 32);
            label1.TabIndex = 9;
            label1.Text = "Pantallas";
            // 
            // dgvPantallas
            // 
            dgvPantallas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPantallas.Location = new Point(-4, 60);
            dgvPantallas.Name = "dgvPantallas";
            dgvPantallas.Size = new Size(809, 335);
            dgvPantallas.TabIndex = 8;
            dgvPantallas.CellClick += dgvPantallas_CellClick;
            // 
            // FRMPantalla
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSalir);
            Controls.Add(btnAgregar);
            Controls.Add(label1);
            Controls.Add(dgvPantallas);
            Name = "FRMPantalla";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRMPantalla";
            ((System.ComponentModel.ISupportInitialize)dgvPantallas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSalir;
        private Button btnAgregar;
        private Label label1;
        private DataGridView dgvPantallas;
    }
}