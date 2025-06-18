namespace ProyectoBD.Users
{
    partial class FRMAddUserRol
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
            label3 = new Label();
            checkedListBoxRoles = new CheckedListBox();
            btnGuardar = new Button();
            flowLayoutPanelRoles = new FlowLayoutPanel();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(294, 9);
            label1.Name = "label1";
            label1.Size = new Size(213, 32);
            label1.TabIndex = 2;
            label1.Text = "Administrar roles";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 65);
            label2.Name = "label2";
            label2.Size = new Size(237, 32);
            label2.TabIndex = 3;
            label2.Text = "Seleccione los roles";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(541, 65);
            label3.Name = "label3";
            label3.Size = new Size(197, 32);
            label3.TabIndex = 4;
            label3.Text = "Roles asignados";
            // 
            // checkedListBoxRoles
            // 
            checkedListBoxRoles.FormattingEnabled = true;
            checkedListBoxRoles.Location = new Point(12, 114);
            checkedListBoxRoles.Name = "checkedListBoxRoles";
            checkedListBoxRoles.Size = new Size(237, 220);
            checkedListBoxRoles.TabIndex = 5;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(681, 394);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(97, 44);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // flowLayoutPanelRoles
            // 
            flowLayoutPanelRoles.Location = new Point(551, 113);
            flowLayoutPanelRoles.Name = "flowLayoutPanelRoles";
            flowLayoutPanelRoles.Size = new Size(227, 221);
            flowLayoutPanelRoles.TabIndex = 7;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(12, 394);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(97, 44);
            btnSalir.TabIndex = 8;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // FRMAddUserRol
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSalir);
            Controls.Add(flowLayoutPanelRoles);
            Controls.Add(btnGuardar);
            Controls.Add(checkedListBoxRoles);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FRMAddUserRol";
            Text = "FRMAddUserRol";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private CheckedListBox checkedListBoxRoles;
        private Button btnGuardar;
        private FlowLayoutPanel flowLayoutPanelRoles;
        private Button btnSalir;
    }
}