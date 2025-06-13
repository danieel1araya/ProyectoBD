namespace ProyectoBD.Users
{
    partial class FRMAddUserPerm
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
            btnCancelar = new Button();
            btnAsignar = new Button();
            label3 = new Label();
            label2 = new Label();
            chkConsultar = new CheckBox();
            chkEliminar = new CheckBox();
            chkModificar = new CheckBox();
            chkInsertar = new CheckBox();
            checkedListBoxPantallas = new CheckedListBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(43, 404);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(88, 36);
            btnCancelar.TabIndex = 29;
            btnCancelar.Text = "Salir";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnSalir_Click;
            // 
            // btnAsignar
            // 
            btnAsignar.Location = new Point(668, 404);
            btnAsignar.Name = "btnAsignar";
            btnAsignar.Size = new Size(88, 36);
            btnAsignar.TabIndex = 28;
            btnAsignar.Text = "Guardar";
            btnAsignar.UseVisualStyleBackColor = true;
            btnAsignar.Click += btnAsignar_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(27, 56);
            label3.Name = "label3";
            label3.Size = new Size(325, 25);
            label3.TabIndex = 27;
            label3.Text = "Pantallas (Puede seleccionar varias)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(447, 56);
            label2.Name = "label2";
            label2.Size = new Size(327, 25);
            label2.TabIndex = 26;
            label2.Text = "Permisos (Puede seleccionar varios)";
            // 
            // chkConsultar
            // 
            chkConsultar.AutoSize = true;
            chkConsultar.Location = new Point(543, 345);
            chkConsultar.Name = "chkConsultar";
            chkConsultar.Size = new Size(77, 19);
            chkConsultar.TabIndex = 25;
            chkConsultar.Text = "Consultar";
            chkConsultar.UseVisualStyleBackColor = true;
            // 
            // chkEliminar
            // 
            chkEliminar.AutoSize = true;
            chkEliminar.Location = new Point(543, 265);
            chkEliminar.Name = "chkEliminar";
            chkEliminar.Size = new Size(69, 19);
            chkEliminar.TabIndex = 24;
            chkEliminar.Text = "Eliminar";
            chkEliminar.UseVisualStyleBackColor = true;
            // 
            // chkModificar
            // 
            chkModificar.AutoSize = true;
            chkModificar.Location = new Point(543, 172);
            chkModificar.Name = "chkModificar";
            chkModificar.Size = new Size(77, 19);
            chkModificar.TabIndex = 23;
            chkModificar.Text = "Modificar";
            chkModificar.UseVisualStyleBackColor = true;
            // 
            // chkInsertar
            // 
            chkInsertar.AutoSize = true;
            chkInsertar.Location = new Point(543, 88);
            chkInsertar.Name = "chkInsertar";
            chkInsertar.Size = new Size(65, 19);
            chkInsertar.TabIndex = 22;
            chkInsertar.Text = "Insertar";
            chkInsertar.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxPantallas
            // 
            checkedListBoxPantallas.FormattingEnabled = true;
            checkedListBoxPantallas.Location = new Point(43, 91);
            checkedListBoxPantallas.Name = "checkedListBoxPantallas";
            checkedListBoxPantallas.Size = new Size(293, 274);
            checkedListBoxPantallas.TabIndex = 21;
            checkedListBoxPantallas.SelectedIndexChanged += checkedListBoxPantallas_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(214, 11);
            label1.Name = "label1";
            label1.Size = new Size(396, 32);
            label1.TabIndex = 20;
            label1.Text = "Administrar permisos del usuario";
            // 
            // FRMAddUserPerm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancelar);
            Controls.Add(btnAsignar);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(chkConsultar);
            Controls.Add(chkEliminar);
            Controls.Add(chkModificar);
            Controls.Add(chkInsertar);
            Controls.Add(checkedListBoxPantallas);
            Controls.Add(label1);
            Name = "FRMAddUserPerm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRMAddUserPerm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancelar;
        private Button btnAsignar;
        private Label label3;
        private Label label2;
        private CheckBox chkConsultar;
        private CheckBox chkEliminar;
        private CheckBox chkModificar;
        private CheckBox chkInsertar;
        private CheckedListBox checkedListBoxPantallas;
        private Label label1;
    }
}