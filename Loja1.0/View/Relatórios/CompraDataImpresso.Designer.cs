namespace Loja1._0.View.Relatórios
{
    partial class CompraDataImpresso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompraDataImpresso));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLojaCnpj = new System.Windows.Forms.Label();
            this.lblLoja = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.txtItemNome = new System.Windows.Forms.Label();
            this.panel55 = new System.Windows.Forms.Panel();
            this.txtItemUnd = new System.Windows.Forms.Label();
            this.panel77 = new System.Windows.Forms.Panel();
            this.txtItemQnt = new System.Windows.Forms.Label();
            this.panel99 = new System.Windows.Forms.Panel();
            this.txtVlUnd = new System.Windows.Forms.Label();
            this.panel121 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel225 = new System.Windows.Forms.Panel();
            this.label68 = new System.Windows.Forms.Label();
            this.panel285 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvRelatorio = new System.Windows.Forms.DataGridView();
            this.printablePanel1 = new Loja1._0.Control.PrintablePanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel55.SuspendLayout();
            this.panel77.SuspendLayout();
            this.panel99.SuspendLayout();
            this.panel121.SuspendLayout();
            this.panel225.SuspendLayout();
            this.panel285.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorio)).BeginInit();
            this.printablePanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnImprimir
            // 
            this.btnImprimir.AutoEllipsis = true;
            this.btnImprimir.BackColor = System.Drawing.Color.Gainsboro;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnImprimir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnImprimir.Location = new System.Drawing.Point(643, 749);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(72, 36);
            this.btnImprimir.TabIndex = 48;
            this.btnImprimir.Text = "OK";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.ImpressaoPagina);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblLojaCnpj);
            this.panel1.Controls.Add(this.lblLoja);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1344, 47);
            this.panel1.TabIndex = 2;
            // 
            // lblLojaCnpj
            // 
            this.lblLojaCnpj.AutoSize = true;
            this.lblLojaCnpj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblLojaCnpj.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLojaCnpj.Location = new System.Drawing.Point(554, 22);
            this.lblLojaCnpj.Name = "lblLojaCnpj";
            this.lblLojaCnpj.Size = new System.Drawing.Size(246, 20);
            this.lblLojaCnpj.TabIndex = 3;
            this.lblLojaCnpj.Text = "Relatório Compra por Período";
            // 
            // lblLoja
            // 
            this.lblLoja.AutoSize = true;
            this.lblLoja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblLoja.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLoja.Location = new System.Drawing.Point(508, 2);
            this.lblLoja.Name = "lblLoja";
            this.lblLoja.Size = new System.Drawing.Size(326, 20);
            this.lblLoja.TabIndex = 2;
            this.lblLoja.Text = "Loja 001 - Alemão da Construção LTDA";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Location = new System.Drawing.Point(11, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(99, 42);
            this.panel2.TabIndex = 0;
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.Gainsboro;
            this.panel20.Controls.Add(this.txtItemNome);
            this.panel20.Location = new System.Drawing.Point(80, 51);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(420, 17);
            this.panel20.TabIndex = 5;
            // 
            // txtItemNome
            // 
            this.txtItemNome.AutoSize = true;
            this.txtItemNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemNome.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtItemNome.Location = new System.Drawing.Point(125, 0);
            this.txtItemNome.Name = "txtItemNome";
            this.txtItemNome.Size = new System.Drawing.Size(158, 15);
            this.txtItemNome.TabIndex = 84;
            this.txtItemNome.Text = "Descrição dos produtos";
            // 
            // panel55
            // 
            this.panel55.BackColor = System.Drawing.Color.Gainsboro;
            this.panel55.Controls.Add(this.txtItemUnd);
            this.panel55.Location = new System.Drawing.Point(501, 51);
            this.panel55.Name = "panel55";
            this.panel55.Size = new System.Drawing.Size(49, 17);
            this.panel55.TabIndex = 22;
            // 
            // txtItemUnd
            // 
            this.txtItemUnd.AutoSize = true;
            this.txtItemUnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemUnd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtItemUnd.Location = new System.Drawing.Point(2, 0);
            this.txtItemUnd.Name = "txtItemUnd";
            this.txtItemUnd.Size = new System.Drawing.Size(37, 15);
            this.txtItemUnd.TabIndex = 85;
            this.txtItemUnd.Text = "Und.";
            // 
            // panel77
            // 
            this.panel77.BackColor = System.Drawing.Color.Gainsboro;
            this.panel77.Controls.Add(this.txtItemQnt);
            this.panel77.Location = new System.Drawing.Point(552, 51);
            this.panel77.Name = "panel77";
            this.panel77.Size = new System.Drawing.Size(87, 17);
            this.panel77.TabIndex = 40;
            // 
            // txtItemQnt
            // 
            this.txtItemQnt.AutoSize = true;
            this.txtItemQnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemQnt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtItemQnt.Location = new System.Drawing.Point(0, 0);
            this.txtItemQnt.Name = "txtItemQnt";
            this.txtItemQnt.Size = new System.Drawing.Size(87, 15);
            this.txtItemQnt.TabIndex = 86;
            this.txtItemQnt.Text = "Qnt. Compra";
            // 
            // panel99
            // 
            this.panel99.BackColor = System.Drawing.Color.Gainsboro;
            this.panel99.Controls.Add(this.txtVlUnd);
            this.panel99.Location = new System.Drawing.Point(641, 51);
            this.panel99.Name = "panel99";
            this.panel99.Size = new System.Drawing.Size(99, 17);
            this.panel99.TabIndex = 58;
            // 
            // txtVlUnd
            // 
            this.txtVlUnd.AutoSize = true;
            this.txtVlUnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVlUnd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtVlUnd.Location = new System.Drawing.Point(4, 0);
            this.txtVlUnd.Name = "txtVlUnd";
            this.txtVlUnd.Size = new System.Drawing.Size(79, 15);
            this.txtVlUnd.TabIndex = 87;
            this.txtVlUnd.Text = "R$ Compra";
            // 
            // panel121
            // 
            this.panel121.BackColor = System.Drawing.Color.Gainsboro;
            this.panel121.Controls.Add(this.label10);
            this.panel121.Location = new System.Drawing.Point(741, 51);
            this.panel121.Name = "panel121";
            this.panel121.Size = new System.Drawing.Size(99, 17);
            this.panel121.TabIndex = 76;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 15);
            this.label10.TabIndex = 88;
            this.label10.Text = "R$ Imposto";
            // 
            // panel225
            // 
            this.panel225.BackColor = System.Drawing.Color.Gainsboro;
            this.panel225.Controls.Add(this.label68);
            this.panel225.Location = new System.Drawing.Point(1128, 51);
            this.panel225.Name = "panel225";
            this.panel225.Size = new System.Drawing.Size(216, 17);
            this.panel225.TabIndex = 130;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label68.Location = new System.Drawing.Point(-2, 0);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(109, 15);
            this.label68.TabIndex = 88;
            this.label68.Text = "Nº   Sigla    Ref.";
            // 
            // panel285
            // 
            this.panel285.BackColor = System.Drawing.Color.Gainsboro;
            this.panel285.Controls.Add(this.label1);
            this.panel285.Location = new System.Drawing.Point(5, 51);
            this.panel285.Name = "panel285";
            this.panel285.Size = new System.Drawing.Size(73, 17);
            this.panel285.TabIndex = 166;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 86;
            this.label1.Text = "Data";
            // 
            // dgvRelatorio
            // 
            this.dgvRelatorio.AllowUserToAddRows = false;
            this.dgvRelatorio.AllowUserToDeleteRows = false;
            this.dgvRelatorio.AllowUserToResizeColumns = false;
            this.dgvRelatorio.AllowUserToResizeRows = false;
            this.dgvRelatorio.BackgroundColor = System.Drawing.Color.White;
            this.dgvRelatorio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRelatorio.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvRelatorio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRelatorio.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRelatorio.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRelatorio.Enabled = false;
            this.dgvRelatorio.GridColor = System.Drawing.SystemColors.ControlText;
            this.dgvRelatorio.Location = new System.Drawing.Point(9, 71);
            this.dgvRelatorio.Margin = new System.Windows.Forms.Padding(0);
            this.dgvRelatorio.Name = "dgvRelatorio";
            this.dgvRelatorio.ReadOnly = true;
            this.dgvRelatorio.RowHeadersVisible = false;
            this.dgvRelatorio.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvRelatorio.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRelatorio.RowTemplate.Height = 15;
            this.dgvRelatorio.RowTemplate.ReadOnly = true;
            this.dgvRelatorio.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRelatorio.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvRelatorio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRelatorio.ShowCellErrors = false;
            this.dgvRelatorio.ShowCellToolTips = false;
            this.dgvRelatorio.ShowEditingIcon = false;
            this.dgvRelatorio.ShowRowErrors = false;
            this.dgvRelatorio.Size = new System.Drawing.Size(1336, 664);
            this.dgvRelatorio.TabIndex = 167;
            // 
            // printablePanel1
            // 
            this.printablePanel1.BackColor = System.Drawing.Color.Transparent;
            this.printablePanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.printablePanel1.Controls.Add(this.panel6);
            this.printablePanel1.Controls.Add(this.panel4);
            this.printablePanel1.Controls.Add(this.panel3);
            this.printablePanel1.Controls.Add(this.panel99);
            this.printablePanel1.Controls.Add(this.panel121);
            this.printablePanel1.Controls.Add(this.dgvRelatorio);
            this.printablePanel1.Controls.Add(this.panel285);
            this.printablePanel1.Controls.Add(this.panel225);
            this.printablePanel1.Controls.Add(this.panel77);
            this.printablePanel1.Controls.Add(this.panel55);
            this.printablePanel1.Controls.Add(this.panel20);
            this.printablePanel1.Controls.Add(this.panel1);
            this.printablePanel1.Location = new System.Drawing.Point(0, 0);
            this.printablePanel1.Margin = new System.Windows.Forms.Padding(0);
            this.printablePanel1.Name = "printablePanel1";
            this.printablePanel1.Size = new System.Drawing.Size(1347, 746);
            this.printablePanel1.TabIndex = 0;
            this.printablePanel1.Text = "PrintablePanel";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Gainsboro;
            this.panel6.Controls.Add(this.label4);
            this.panel6.Location = new System.Drawing.Point(841, 51);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(99, 17);
            this.panel6.TabIndex = 88;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 15);
            this.label4.TabIndex = 87;
            this.label4.Text = "R$ Venda";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gainsboro;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(941, 51);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(92, 17);
            this.panel4.TabIndex = 88;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 86;
            this.label3.Text = "Qnt. Atual";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(1035, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(92, 17);
            this.panel3.TabIndex = 87;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 86;
            this.label2.Text = "Qnt. Minima";
            // 
            // CompraDataImpresso
            // 
            this.AcceptButton = this.btnImprimir;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1386, 788);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.printablePanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CompraDataImpresso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Pedido";
            this.Shown += new System.EventHandler(this.ImpressaoPagina);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel55.ResumeLayout(false);
            this.panel55.PerformLayout();
            this.panel77.ResumeLayout(false);
            this.panel77.PerformLayout();
            this.panel99.ResumeLayout(false);
            this.panel99.PerformLayout();
            this.panel121.ResumeLayout(false);
            this.panel121.PerformLayout();
            this.panel225.ResumeLayout(false);
            this.panel225.PerformLayout();
            this.panel285.ResumeLayout(false);
            this.panel285.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorio)).EndInit();
            this.printablePanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLojaCnpj;
        private System.Windows.Forms.Label lblLoja;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Label txtItemNome;
        private System.Windows.Forms.Panel panel55;
        private System.Windows.Forms.Label txtItemUnd;
        private System.Windows.Forms.Panel panel77;
        private System.Windows.Forms.Label txtItemQnt;
        private System.Windows.Forms.Panel panel99;
        private System.Windows.Forms.Label txtVlUnd;
        private System.Windows.Forms.Panel panel121;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel225;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Panel panel285;
        private System.Windows.Forms.DataGridView dgvRelatorio;
        private Control.PrintablePanel printablePanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
    }
}