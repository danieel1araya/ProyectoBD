namespace ProyectoBD
{
    partial class FRMSistema
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
            dgvSistemas = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvSistemas).BeginInit();
            SuspendLayout();
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(523, 405);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(194, 405);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(75, 23);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(349, 9);
            label1.Name = "label1";
            label1.Size = new Size(113, 32);
            label1.TabIndex = 5;
            label1.Text = "Sistemas";
            // 
            // dgvSistemas
            // 
            dgvSistemas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSistemas.Location = new Point(-6, 53);
            dgvSistemas.Name = "dgvSistemas";
            dgvSistemas.Size = new Size(809, 335);
            dgvSistemas.TabIndex = 4;
            dgvSistemas.CellClick += dgvSistemas_CellClick;
            dgvSistemas.CellContentClick += dgvSistemas_CellContentClick;
            // 
            // FRMSistema
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSalir);
            Controls.Add(btnAgregar);
            Controls.Add(label1);
            Controls.Add(dgvSistemas);
            Name = "FRMSistema";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRMSistema";
            ((System.ComponentModel.ISupportInitialize)dgvSistemas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSalir;
        private Button btnAgregar;
        private Label label1;
        private DataGridView dgvSistemas;
    }
}