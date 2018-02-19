namespace Loja1._0
{
    partial class VendaUser
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VendaUser));
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.dgvRelatorio = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.Label();
            this.btnPesquisaCliente = new System.Windows.Forms.Button();
            this.btnOkCliente = new System.Windows.Forms.Button();
            this.btnCancelCliente = new System.Windows.Forms.Button();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.txtBuscaCliente = new System.Windows.Forms.TextBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnImprime = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorio)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.AutoEllipsis = true;
            this.btnPesquisar.BackColor = System.Drawing.Color.White;
            this.btnPesquisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnPesquisar.Location = new System.Drawing.Point(484, 49);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(85, 45);
            this.btnPesquisar.TabIndex = 34;
            this.btnPesquisar.Text = "Buscar";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            // 
            // dgvRelatorio
            // 
            this.dgvRelatorio.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvRelatorio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRelatorio.Location = new System.Drawing.Point(4, 116);
            this.dgvRelatorio.Name = "dgvRelatorio";
            this.dgvRelatorio.Size = new System.Drawing.Size(793, 431);
            this.dgvRelatorio.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(1, 582);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 16);
            this.label3.TabIndex = 28;
            this.label3.Text = "© 2017 Guilherme Bernardelli";
            // 
            // lblNome
            // 
            this.lblNome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNome.AutoSize = true;
            this.lblNome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblNome.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblNome.Location = new System.Drawing.Point(519, 7);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(202, 27);
            this.lblNome.TabIndex = 27;
            this.lblNome.Text = "Vendas por usuário";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExit.Location = new System.Drawing.Point(765, 3);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 34);
            this.btnExit.TabIndex = 26;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLogo.BackgroundImage")));
            this.pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLogo.Location = new System.Drawing.Point(663, 41);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(136, 70);
            this.pnlLogo.TabIndex = 25;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblTitulo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblTitulo.Location = new System.Drawing.Point(6, 4);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(473, 33);
            this.lblTitulo.TabIndex = 24;
            this.lblTitulo.Text = "Sistema Alemão da Construção 1.0";
            // 
            // txtCliente
            // 
            this.txtCliente.AutoSize = true;
            this.txtCliente.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCliente.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtCliente.Location = new System.Drawing.Point(85, 54);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(62, 16);
            this.txtCliente.TabIndex = 86;
            this.txtCliente.Text = "Usuario";
            this.txtCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtCliente.Visible = false;
            // 
            // btnPesquisaCliente
            // 
            this.btnPesquisaCliente.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaCliente.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPesquisaCliente.BackgroundImage")));
            this.btnPesquisaCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPesquisaCliente.FlatAppearance.BorderSize = 0;
            this.btnPesquisaCliente.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPesquisaCliente.Location = new System.Drawing.Point(453, 49);
            this.btnPesquisaCliente.Margin = new System.Windows.Forms.Padding(0);
            this.btnPesquisaCliente.Name = "btnPesquisaCliente";
            this.btnPesquisaCliente.Size = new System.Drawing.Size(24, 24);
            this.btnPesquisaCliente.TabIndex = 85;
            this.btnPesquisaCliente.UseVisualStyleBackColor = false;
            // 
            // btnOkCliente
            // 
            this.btnOkCliente.BackColor = System.Drawing.Color.Transparent;
            this.btnOkCliente.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOkCliente.BackgroundImage")));
            this.btnOkCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOkCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOkCliente.FlatAppearance.BorderSize = 0;
            this.btnOkCliente.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOkCliente.ForeColor = System.Drawing.Color.Lime;
            this.btnOkCliente.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnOkCliente.Location = new System.Drawing.Point(427, 49);
            this.btnOkCliente.Margin = new System.Windows.Forms.Padding(0);
            this.btnOkCliente.Name = "btnOkCliente";
            this.btnOkCliente.Size = new System.Drawing.Size(24, 24);
            this.btnOkCliente.TabIndex = 84;
            this.btnOkCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOkCliente.UseVisualStyleBackColor = false;
            this.btnOkCliente.Visible = false;
            // 
            // btnCancelCliente
            // 
            this.btnCancelCliente.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelCliente.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancelCliente.BackgroundImage")));
            this.btnCancelCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelCliente.FlatAppearance.BorderSize = 0;
            this.btnCancelCliente.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelCliente.ForeColor = System.Drawing.Color.Red;
            this.btnCancelCliente.Location = new System.Drawing.Point(453, 49);
            this.btnCancelCliente.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancelCliente.Name = "btnCancelCliente";
            this.btnCancelCliente.Size = new System.Drawing.Size(24, 24);
            this.btnCancelCliente.TabIndex = 83;
            this.btnCancelCliente.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelCliente.UseVisualStyleBackColor = false;
            this.btnCancelCliente.Visible = false;
            // 
            // cmbCliente
            // 
            this.cmbCliente.FormattingEnabled = true;
            this.cmbCliente.Location = new System.Drawing.Point(85, 50);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(339, 21);
            this.cmbCliente.TabIndex = 82;
            this.cmbCliente.Visible = false;
            // 
            // txtBuscaCliente
            // 
            this.txtBuscaCliente.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscaCliente.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtBuscaCliente.Location = new System.Drawing.Point(85, 50);
            this.txtBuscaCliente.MaxLength = 30;
            this.txtBuscaCliente.Name = "txtBuscaCliente";
            this.txtBuscaCliente.Size = new System.Drawing.Size(354, 23);
            this.txtBuscaCliente.TabIndex = 81;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCliente.Location = new System.Drawing.Point(8, 53);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(74, 16);
            this.lblCliente.TabIndex = 80;
            this.lblCliente.Text = "Usuário : ";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.AutoEllipsis = true;
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Location = new System.Drawing.Point(572, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 45);
            this.button1.TabIndex = 87;
            this.button1.Text = "Limpar";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnImprime
            // 
            this.btnImprime.AutoEllipsis = true;
            this.btnImprime.BackColor = System.Drawing.Color.White;
            this.btnImprime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprime.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprime.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnImprime.Location = new System.Drawing.Point(355, 553);
            this.btnImprime.Name = "btnImprime";
            this.btnImprime.Size = new System.Drawing.Size(91, 45);
            this.btnImprime.TabIndex = 89;
            this.btnImprime.Text = "Imprimir";
            this.btnImprime.UseVisualStyleBackColor = false;
            // 
            // VendaUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnImprime);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.btnPesquisaCliente);
            this.Controls.Add(this.btnOkCliente);
            this.Controls.Add(this.btnCancelCliente);
            this.Controls.Add(this.cmbCliente);
            this.Controls.Add(this.txtBuscaCliente);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.dgvRelatorio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pnlLogo);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VendaUser";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Alemão da Construção 1.0";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.DataGridView dgvRelatorio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label txtCliente;
        private System.Windows.Forms.Button btnPesquisaCliente;
        private System.Windows.Forms.Button btnOkCliente;
        private System.Windows.Forms.Button btnCancelCliente;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.TextBox txtBuscaCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnImprime;
    }
}

