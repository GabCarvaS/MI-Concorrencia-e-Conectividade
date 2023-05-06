
namespace ClientePBL03
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
            this.Origem = new System.Windows.Forms.Label();
            this.textOrigem = new System.Windows.Forms.TextBox();
            this.textDestino = new System.Windows.Forms.TextBox();
            this.dgvDados = new System.Windows.Forms.DataGridView();
            this.btnObterRota = new System.Windows.Forms.Button();
            this.Destino = new System.Windows.Forms.Label();
            this.confirmCompra = new System.Windows.Forms.Button();
            this.btnEmpresa01 = new System.Windows.Forms.Button();
            this.btnEmpresa02 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).BeginInit();
            this.SuspendLayout();
            // 
            // Origem
            // 
            this.Origem.AutoSize = true;
            this.Origem.Location = new System.Drawing.Point(12, 396);
            this.Origem.Name = "Origem";
            this.Origem.Size = new System.Drawing.Size(47, 15);
            this.Origem.TabIndex = 0;
            this.Origem.Text = "Origem";
            // 
            // textOrigem
            // 
            this.textOrigem.Location = new System.Drawing.Point(65, 393);
            this.textOrigem.Name = "textOrigem";
            this.textOrigem.Size = new System.Drawing.Size(254, 23);
            this.textOrigem.TabIndex = 1;
            // 
            // textDestino
            // 
            this.textDestino.Location = new System.Drawing.Point(496, 393);
            this.textDestino.Name = "textDestino";
            this.textDestino.Size = new System.Drawing.Size(292, 23);
            this.textDestino.TabIndex = 2;
            // 
            // dgvDados
            // 
            this.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDados.Location = new System.Drawing.Point(12, 35);
            this.dgvDados.Name = "dgvDados";
            this.dgvDados.RowTemplate.Height = 25;
            this.dgvDados.Size = new System.Drawing.Size(776, 355);
            this.dgvDados.TabIndex = 3;
            // 
            // btnObterRota
            // 
            this.btnObterRota.Location = new System.Drawing.Point(713, 6);
            this.btnObterRota.Name = "btnObterRota";
            this.btnObterRota.Size = new System.Drawing.Size(75, 23);
            this.btnObterRota.TabIndex = 4;
            this.btnObterRota.Text = "Listar";
            this.btnObterRota.UseVisualStyleBackColor = true;
            this.btnObterRota.Click += new System.EventHandler(this.btnObterRota_Click);
            // 
            // Destino
            // 
            this.Destino.AutoSize = true;
            this.Destino.Location = new System.Drawing.Point(443, 396);
            this.Destino.Name = "Destino";
            this.Destino.Size = new System.Drawing.Size(47, 15);
            this.Destino.TabIndex = 5;
            this.Destino.Text = "Destino";
            // 
            // confirmCompra
            // 
            this.confirmCompra.Location = new System.Drawing.Point(713, 422);
            this.confirmCompra.Name = "confirmCompra";
            this.confirmCompra.Size = new System.Drawing.Size(75, 23);
            this.confirmCompra.TabIndex = 6;
            this.confirmCompra.Text = "Confirmar";
            this.confirmCompra.UseVisualStyleBackColor = true;
            this.confirmCompra.Click += new System.EventHandler(this.confirmCompra_Click);
            // 
            // btnEmpresa01
            // 
            this.btnEmpresa01.Location = new System.Drawing.Point(12, 6);
            this.btnEmpresa01.Name = "btnEmpresa01";
            this.btnEmpresa01.Size = new System.Drawing.Size(93, 23);
            this.btnEmpresa01.TabIndex = 7;
            this.btnEmpresa01.Text = "Empresa 01";
            this.btnEmpresa01.UseVisualStyleBackColor = true;
            this.btnEmpresa01.Click += new System.EventHandler(this.btnEmpresa01_Click);
            // 
            // btnEmpresa02
            // 
            this.btnEmpresa02.Location = new System.Drawing.Point(120, 6);
            this.btnEmpresa02.Name = "btnEmpresa02";
            this.btnEmpresa02.Size = new System.Drawing.Size(90, 23);
            this.btnEmpresa02.TabIndex = 8;
            this.btnEmpresa02.Text = "Empresa 02";
            this.btnEmpresa02.UseVisualStyleBackColor = true;
            this.btnEmpresa02.Click += new System.EventHandler(this.btnEmpresa02_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnEmpresa02);
            this.Controls.Add(this.btnEmpresa01);
            this.Controls.Add(this.confirmCompra);
            this.Controls.Add(this.Destino);
            this.Controls.Add(this.btnObterRota);
            this.Controls.Add(this.dgvDados);
            this.Controls.Add(this.textDestino);
            this.Controls.Add(this.textOrigem);
            this.Controls.Add(this.Origem);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Origem;
        private System.Windows.Forms.TextBox textOrigem;
        private System.Windows.Forms.TextBox textDestino;
        private System.Windows.Forms.DataGridView dgvDados;
        private System.Windows.Forms.Button btnObterRota;
        private System.Windows.Forms.Label Destino;
        private System.Windows.Forms.Button confirmCompra;
        private System.Windows.Forms.Button btnEmpresa01;
        private System.Windows.Forms.Button btnEmpresa02;
    }
}

