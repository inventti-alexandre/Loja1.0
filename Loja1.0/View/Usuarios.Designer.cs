namespace Loja1._0
{
    partial class Usuarios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblNome = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.pnlDetalhe = new System.Windows.Forms.GroupBox();
            this.txtRegistro = new System.Windows.Forms.TextBox();
            this.lblRegistro = new System.Windows.Forms.Label();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.pnlPerfil = new System.Windows.Forms.Panel();
            this.rdbOperador = new System.Windows.Forms.RadioButton();
            this.rdbCaixa = new System.Windows.Forms.RadioButton();
            this.rdbGerente = new System.Windows.Forms.RadioButton();
            this.rdbAdministrador = new System.Windows.Forms.RadioButton();
            this.lblSelecione = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.pnlDetalhe.SuspendLayout();
            this.pnlPerfil.SuspendLayout();
            this.SuspendLayout();
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
            this.lblTitulo.Location = new System.Drawing.Point(5, 4);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(473, 33);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Sistema Alemão da Construção 1.0";
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogo.BackgroundImage = global::Loja1._0.Properties.Resources.LgAlemão;
            this.pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLogo.Location = new System.Drawing.Point(612, 489);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(176, 86);
            this.pnlLogo.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Khaki;
            this.btnExit.BackgroundImage = global::Loja1._0.Properties.Resources.voltar1;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExit.Location = new System.Drawing.Point(764, 3);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 34);
            this.btnExit.TabIndex = 2;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblNome
            // 
            this.lblNome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNome.AutoSize = true;
            this.lblNome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblNome.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblNome.Location = new System.Drawing.Point(493, 4);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(259, 33);
            this.lblNome.TabIndex = 3;
            this.lblNome.Text = "Cadastro Usuários";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(0, 582);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "© 2017 Guilherme Bernardelli";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvUsuarios);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.Location = new System.Drawing.Point(5, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 416);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Usuários";
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.AllowUserToResizeColumns = false;
            this.dgvUsuarios.AllowUserToResizeRows = false;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvUsuarios.GridColor = System.Drawing.Color.Khaki;
            this.dgvUsuarios.Location = new System.Drawing.Point(6, 22);
            this.dgvUsuarios.MultiSelect = false;
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.Size = new System.Drawing.Size(589, 388);
            this.dgvUsuarios.TabIndex = 0;
            this.dgvUsuarios.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUsuarios_CellMouseClick);
            // 
            // pnlDetalhe
            // 
            this.pnlDetalhe.Controls.Add(this.txtRegistro);
            this.pnlDetalhe.Controls.Add(this.lblRegistro);
            this.pnlDetalhe.Controls.Add(this.chkStatus);
            this.pnlDetalhe.Controls.Add(this.pnlPerfil);
            this.pnlDetalhe.Controls.Add(this.txtLogin);
            this.pnlDetalhe.Controls.Add(this.lblUser);
            this.pnlDetalhe.Enabled = false;
            this.pnlDetalhe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDetalhe.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.pnlDetalhe.Location = new System.Drawing.Point(5, 55);
            this.pnlDetalhe.Name = "pnlDetalhe";
            this.pnlDetalhe.Size = new System.Drawing.Size(783, 102);
            this.pnlDetalhe.TabIndex = 8;
            this.pnlDetalhe.TabStop = false;
            this.pnlDetalhe.Text = "Detalhes do Usuário";
            // 
            // txtRegistro
            // 
            this.txtRegistro.Location = new System.Drawing.Point(457, 23);
            this.txtRegistro.MaxLength = 30;
            this.txtRegistro.Name = "txtRegistro";
            this.txtRegistro.Size = new System.Drawing.Size(161, 23);
            this.txtRegistro.TabIndex = 23;
            // 
            // lblRegistro
            // 
            this.lblRegistro.AutoSize = true;
            this.lblRegistro.Location = new System.Drawing.Point(367, 26);
            this.lblRegistro.Name = "lblRegistro";
            this.lblRegistro.Size = new System.Drawing.Size(84, 17);
            this.lblRegistro.TabIndex = 22;
            this.lblRegistro.Text = "Registro : ";
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Location = new System.Drawing.Point(704, 25);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkStatus.Size = new System.Drawing.Size(63, 21);
            this.chkStatus.TabIndex = 21;
            this.chkStatus.Text = "Ativo";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // pnlPerfil
            // 
            this.pnlPerfil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlPerfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPerfil.Controls.Add(this.rdbOperador);
            this.pnlPerfil.Controls.Add(this.rdbCaixa);
            this.pnlPerfil.Controls.Add(this.rdbGerente);
            this.pnlPerfil.Controls.Add(this.rdbAdministrador);
            this.pnlPerfil.Controls.Add(this.lblSelecione);
            this.pnlPerfil.Location = new System.Drawing.Point(106, 52);
            this.pnlPerfil.Name = "pnlPerfil";
            this.pnlPerfil.Size = new System.Drawing.Size(551, 43);
            this.pnlPerfil.TabIndex = 20;
            // 
            // rdbOperador
            // 
            this.rdbOperador.AutoSize = true;
            this.rdbOperador.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbOperador.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rdbOperador.Location = new System.Drawing.Point(462, 19);
            this.rdbOperador.Name = "rdbOperador";
            this.rdbOperador.Size = new System.Drawing.Size(79, 19);
            this.rdbOperador.TabIndex = 6;
            this.rdbOperador.TabStop = true;
            this.rdbOperador.Text = "Operador";
            this.rdbOperador.UseVisualStyleBackColor = true;
            // 
            // rdbCaixa
            // 
            this.rdbCaixa.AutoSize = true;
            this.rdbCaixa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbCaixa.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rdbCaixa.Location = new System.Drawing.Point(314, 19);
            this.rdbCaixa.Name = "rdbCaixa";
            this.rdbCaixa.Size = new System.Drawing.Size(57, 19);
            this.rdbCaixa.TabIndex = 5;
            this.rdbCaixa.TabStop = true;
            this.rdbCaixa.Text = "Caixa";
            this.rdbCaixa.UseVisualStyleBackColor = true;
            // 
            // rdbGerente
            // 
            this.rdbGerente.AutoSize = true;
            this.rdbGerente.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbGerente.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rdbGerente.Location = new System.Drawing.Point(166, 19);
            this.rdbGerente.Name = "rdbGerente";
            this.rdbGerente.Size = new System.Drawing.Size(70, 19);
            this.rdbGerente.TabIndex = 4;
            this.rdbGerente.TabStop = true;
            this.rdbGerente.Text = "Gerente";
            this.rdbGerente.UseVisualStyleBackColor = true;
            // 
            // rdbAdministrador
            // 
            this.rdbAdministrador.AutoSize = true;
            this.rdbAdministrador.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbAdministrador.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rdbAdministrador.Location = new System.Drawing.Point(8, 19);
            this.rdbAdministrador.Name = "rdbAdministrador";
            this.rdbAdministrador.Size = new System.Drawing.Size(106, 19);
            this.rdbAdministrador.TabIndex = 3;
            this.rdbAdministrador.TabStop = true;
            this.rdbAdministrador.Text = "Administrador";
            this.rdbAdministrador.UseVisualStyleBackColor = true;
            // 
            // lblSelecione
            // 
            this.lblSelecione.AutoSize = true;
            this.lblSelecione.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelecione.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblSelecione.Location = new System.Drawing.Point(3, 2);
            this.lblSelecione.Name = "lblSelecione";
            this.lblSelecione.Size = new System.Drawing.Size(37, 15);
            this.lblSelecione.TabIndex = 0;
            this.lblSelecione.Text = "Perfil";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(76, 23);
            this.txtLogin.MaxLength = 30;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(257, 23);
            this.txtLogin.TabIndex = 1;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(6, 26);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(63, 17);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Login : ";
            // 
            // btnSalvar
            // 
            this.btnSalvar.AutoEllipsis = true;
            this.btnSalvar.BackColor = System.Drawing.Color.White;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.Enabled = false;
            this.btnSalvar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSalvar.Location = new System.Drawing.Point(612, 286);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(176, 73);
            this.btnSalvar.TabIndex = 25;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.AutoEllipsis = true;
            this.btnAlterar.BackColor = System.Drawing.Color.White;
            this.btnAlterar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAlterar.Enabled = false;
            this.btnAlterar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnAlterar.Location = new System.Drawing.Point(612, 185);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(176, 73);
            this.btnAlterar.TabIndex = 23;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.UseVisualStyleBackColor = false;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoEllipsis = true;
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancelar.Location = new System.Drawing.Point(612, 387);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(176, 73);
            this.btnCancelar.TabIndex = 26;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // Usuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.pnlDetalhe);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pnlLogo);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Usuarios";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Alemão da Construção 1.0";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.pnlDetalhe.ResumeLayout(false);
            this.pnlDetalhe.PerformLayout();
            this.pnlPerfil.ResumeLayout(false);
            this.pnlPerfil.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.GroupBox pnlDetalhe;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel pnlPerfil;
        private System.Windows.Forms.RadioButton rdbOperador;
        private System.Windows.Forms.RadioButton rdbCaixa;
        private System.Windows.Forms.RadioButton rdbGerente;
        public System.Windows.Forms.RadioButton rdbAdministrador;
        private System.Windows.Forms.Label lblSelecione;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.TextBox txtRegistro;
        private System.Windows.Forms.Label lblRegistro;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnCancelar;
    }
}

