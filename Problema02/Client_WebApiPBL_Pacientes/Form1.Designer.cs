
namespace Client_WebApiPBL_Pacientes
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtURI = new System.Windows.Forms.TextBox();
            this.dgvDados = new System.Windows.Forms.DataGridView();
            this.btnObterPacientes = new System.Windows.Forms.Button();
            this.btnPacientesPorNome = new System.Windows.Forms.Button();
            this.btnIncluirPaciente = new System.Windows.Forms.Button();
            this.btnRemoverPaciente = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnNPacientes = new System.Windows.Forms.Button();
            this.txtNPacientes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "URI - WebAPI";
            // 
            // txtURI
            // 
            this.txtURI.Location = new System.Drawing.Point(96, 35);
            this.txtURI.Name = "txtURI";
            this.txtURI.Size = new System.Drawing.Size(238, 23);
            this.txtURI.TabIndex = 1;
            this.txtURI.Text = "http://localhost:44364/api/pacientes";
            // 
            // dgvDados
            // 
            this.dgvDados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDados.Location = new System.Drawing.Point(12, 115);
            this.dgvDados.Name = "dgvDados";
            this.dgvDados.RowTemplate.Height = 25;
            this.dgvDados.Size = new System.Drawing.Size(776, 323);
            this.dgvDados.TabIndex = 2;
            // 
            // btnObterPacientes
            // 
            this.btnObterPacientes.Location = new System.Drawing.Point(12, 74);
            this.btnObterPacientes.Name = "btnObterPacientes";
            this.btnObterPacientes.Size = new System.Drawing.Size(110, 23);
            this.btnObterPacientes.TabIndex = 3;
            this.btnObterPacientes.Text = "Todos Pacientes";
            this.btnObterPacientes.UseVisualStyleBackColor = true;
            this.btnObterPacientes.Click += new System.EventHandler(this.btnObterPacientes_Click);
            // 
            // btnPacientesPorNome
            // 
            this.btnPacientesPorNome.Location = new System.Drawing.Point(343, 74);
            this.btnPacientesPorNome.Name = "btnPacientesPorNome";
            this.btnPacientesPorNome.Size = new System.Drawing.Size(118, 23);
            this.btnPacientesPorNome.TabIndex = 4;
            this.btnPacientesPorNome.Text = "Um Paciente";
            this.btnPacientesPorNome.UseVisualStyleBackColor = true;
            this.btnPacientesPorNome.Click += new System.EventHandler(this.btnPacientesPorNome_Click);
            // 
            // btnIncluirPaciente
            // 
            this.btnIncluirPaciente.Location = new System.Drawing.Point(511, 74);
            this.btnIncluirPaciente.Name = "btnIncluirPaciente";
            this.btnIncluirPaciente.Size = new System.Drawing.Size(125, 23);
            this.btnIncluirPaciente.TabIndex = 5;
            this.btnIncluirPaciente.Text = "Adicionar Paciente";
            this.btnIncluirPaciente.UseVisualStyleBackColor = true;
            this.btnIncluirPaciente.Click += new System.EventHandler(this.btnIncluirPaciente_Click);
            // 
            // btnRemoverPaciente
            // 
            this.btnRemoverPaciente.Location = new System.Drawing.Point(676, 74);
            this.btnRemoverPaciente.Name = "btnRemoverPaciente";
            this.btnRemoverPaciente.Size = new System.Drawing.Size(112, 23);
            this.btnRemoverPaciente.TabIndex = 7;
            this.btnRemoverPaciente.Text = "Remover Paciente";
            this.btnRemoverPaciente.UseVisualStyleBackColor = true;
            this.btnRemoverPaciente.Click += new System.EventHandler(this.btnRemoverPaciente_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 5000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnNPacientes
            // 
            this.btnNPacientes.Location = new System.Drawing.Point(168, 74);
            this.btnNPacientes.Name = "btnNPacientes";
            this.btnNPacientes.Size = new System.Drawing.Size(121, 23);
            this.btnNPacientes.TabIndex = 8;
            this.btnNPacientes.Text = "N Pacientes";
            this.btnNPacientes.UseVisualStyleBackColor = true;
            this.btnNPacientes.Click += new System.EventHandler(this.btnNPacientes_Click);
            // 
            // txtNPacientes
            // 
            this.txtNPacientes.Location = new System.Drawing.Point(500, 35);
            this.txtNPacientes.Name = "txtNPacientes";
            this.txtNPacientes.Size = new System.Drawing.Size(288, 23);
            this.txtNPacientes.TabIndex = 9;
            this.txtNPacientes.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(422, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "N Pacientes ";
            // 
            // timer3
            // 
            this.timer3.Interval = 5000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNPacientes);
            this.Controls.Add(this.btnNPacientes);
            this.Controls.Add(this.btnRemoverPaciente);
            this.Controls.Add(this.btnIncluirPaciente);
            this.Controls.Add(this.btnPacientesPorNome);
            this.Controls.Add(this.btnObterPacientes);
            this.Controls.Add(this.dgvDados);
            this.Controls.Add(this.txtURI);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Client V1.1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtURI;
        private System.Windows.Forms.DataGridView dgvDados;
        private System.Windows.Forms.Button btnObterPacientes;
        private System.Windows.Forms.Button btnPacientesPorNome;
        private System.Windows.Forms.Button btnIncluirPaciente;
        private System.Windows.Forms.Button btnRemoverPaciente;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnNPacientes;
        private System.Windows.Forms.TextBox txtNPacientes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer3;
    }
}

