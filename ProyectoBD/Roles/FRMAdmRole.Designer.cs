namespace ProyectoBD.Roles
{
    partial class FRMAdmRole
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
            checkedListBoxPantallas = new CheckedListBox();
            chkInsertar = new CheckBox();
            chkModificar = new CheckBox();
            chkEliminar = new CheckBox();
            chkConsultar = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            btnAsignar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(219, 9);
            label1.Name = "label1";
            label1.Size = new Size(342, 32);
            label1.TabIndex = 10;
            label1.Text = "Administrar permisos del rol";
            // 
            // checkedListBoxPantallas
            // 
            checkedListBoxPantallas.FormattingEnabled = true;
            checkedListBoxPantallas.Location = new Point(48, 89);
            checkedListBoxPantallas.Name = "checkedListBoxPantallas";
            checkedListBoxPantallas.Size = new Size(293, 274);
            checkedListBoxPantallas.TabIndex = 11;
            checkedListBoxPantallas.SelectedIndexChanged += checkedListBoxPantallas_SelectedIndexChanged;
            // 
            // chkInsertar
            // 
            chkInsertar.AutoSize = true;
            chkInsertar.Location = new Point(548, 86);
            chkInsertar.Name = "chkInsertar";
            chkInsertar.Size = new Size(65, 19);
            chkInsertar.TabIndex = 12;
            chkInsertar.Text = "Insertar";
            chkInsertar.UseVisualStyleBackColor = true;
            // 
            // chkModificar
            // 
            chkModificar.AutoSize = true;
            chkModificar.Location = new Point(548, 170);
            chkModificar.Name = "chkModificar";
            chkModificar.Size = new Size(77, 19);
            chkModificar.TabIndex = 13;
            chkModificar.Text = "Modificar";
            chkModificar.UseVisualStyleBackColor = true;
            // 
            // chkEliminar
            // 
            chkEliminar.AutoSize = true;
            chkEliminar.Location = new Point(548, 263);
            chkEliminar.Name = "chkEliminar";
            chkEliminar.Size = new Size(69, 19);
            chkEliminar.TabIndex = 14;
            chkEliminar.Text = "Eliminar";
            chkEliminar.UseVisualStyleBackColor = true;
            // 
            // chkConsultar
            // 
            chkConsultar.AutoSize = true;
            chkConsultar.Location = new Point(548, 343);
            chkConsultar.Name = "chkConsultar";
            chkConsultar.Size = new Size(77, 19);
            chkConsultar.TabIndex = 15;
            chkConsultar.Text = "Consultar";
            chkConsultar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(452, 54);
            label2.Name = "label2";
            label2.Size = new Size(327, 25);
            label2.TabIndex = 16;
            label2.Text = "Permisos (Puede seleccionar varios)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(32, 54);
            label3.Name = "label3";
            label3.Size = new Size(325, 25);
            label3.TabIndex = 17;
            label3.Text = "Pantallas (Puede seleccionar varias)";
            // 
            // btnAsignar
            // 
            btnAsignar.Location = new Point(673, 402);
            btnAsignar.Name = "btnAsignar";
            btnAsignar.Size = new Size(88, 36);
            btnAsignar.TabIndex = 18;
            btnAsignar.Text = "Guardar";
            btnAsignar.UseVisualStyleBackColor = true;
            btnAsignar.Click += btnAsignar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(48, 402);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(88, 36);
            btnCancelar.TabIndex = 19;
            btnCancelar.Text = "Salir";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnSalir_Click;
            // 
            // FRMAdmRole
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
            Name = "FRMAdmRole";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRMAdmRole";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckedListBox checkedListBoxPantallas;
        private CheckBox chkInsertar;
        private CheckBox chkModificar;
        private CheckBox chkEliminar;
        private CheckBox chkConsultar;
        private Label label2;
        private Label label3;
        private Button btnAsignar;
        private Button btnCancelar;
    }
}