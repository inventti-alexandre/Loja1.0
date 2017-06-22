namespace Loja1._0
{
    partial class Fornecedores
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblNome = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlDados = new System.Windows.Forms.Panel();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.lblBairro = new System.Windows.Forms.Label();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.lblNum = new System.Windows.Forms.Label();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.pnlTelefone = new System.Windows.Forms.GroupBox();
            this.rdbClaro = new System.Windows.Forms.RadioButton();
            this.rdbVivo = new System.Windows.Forms.RadioButton();
            this.rdbOi = new System.Windows.Forms.RadioButton();
            this.rdbTim = new System.Windows.Forms.RadioButton();
            this.txtCelular = new System.Windows.Forms.TextBox();
            this.lblCelular = new System.Windows.Forms.Label();
            this.txtTel2 = new System.Windows.Forms.TextBox();
            this.lblTel2 = new System.Windows.Forms.Label();
            this.txtTel1 = new System.Windows.Forms.TextBox();
            this.lblTel1 = new System.Windows.Forms.Label();
            this.cmbCidade = new System.Windows.Forms.ComboBox();
            this.lblCidade = new System.Windows.Forms.Label();
            this.cmbUf = new System.Windows.Forms.ComboBox();
            this.lblUf = new System.Windows.Forms.Label();
            this.txtCnpj = new System.Windows.Forms.TextBox();
            this.lblCnpj = new System.Windows.Forms.Label();
            this.txtContato = new System.Windows.Forms.TextBox();
            this.lblContato = new System.Windows.Forms.Label();
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.lblDados = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.dgvFornecedores = new System.Windows.Forms.DataGridView();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.pnlDados.SuspendLayout();
            this.pnlTelefone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFornecedores)).BeginInit();
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
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblTitulo.Location = new System.Drawing.Point(7, 6);
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
            this.pnlLogo.Location = new System.Drawing.Point(674, 513);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(113, 62);
            this.pnlLogo.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
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
            this.lblNome.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblNome.Location = new System.Drawing.Point(490, 6);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(266, 33);
            this.lblNome.TabIndex = 3;
            this.lblNome.Text = "Cad. Fornecedores";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(0, 583);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "© 2017 Guilherme Bernardelli";
            // 
            // pnlDados
            // 
            this.pnlDados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDados.Controls.Add(this.chkAtivo);
            this.pnlDados.Controls.Add(this.txtBairro);
            this.pnlDados.Controls.Add(this.lblBairro);
            this.pnlDados.Controls.Add(this.txtNum);
            this.pnlDados.Controls.Add(this.lblNum);
            this.pnlDados.Controls.Add(this.txtEndereco);
            this.pnlDados.Controls.Add(this.lblEndereco);
            this.pnlDados.Controls.Add(this.pnlTelefone);
            this.pnlDados.Controls.Add(this.cmbCidade);
            this.pnlDados.Controls.Add(this.lblCidade);
            this.pnlDados.Controls.Add(this.cmbUf);
            this.pnlDados.Controls.Add(this.lblUf);
            this.pnlDados.Controls.Add(this.txtCnpj);
            this.pnlDados.Controls.Add(this.lblCnpj);
            this.pnlDados.Controls.Add(this.txtContato);
            this.pnlDados.Controls.Add(this.lblContato);
            this.pnlDados.Controls.Add(this.txtFornecedor);
            this.pnlDados.Controls.Add(this.lblFornecedor);
            this.pnlDados.Controls.Add(this.lblDados);
            this.pnlDados.Location = new System.Drawing.Point(13, 59);
            this.pnlDados.Name = "pnlDados";
            this.pnlDados.Size = new System.Drawing.Size(775, 193);
            this.pnlDados.TabIndex = 7;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Enabled = false;
            this.chkAtivo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAtivo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.chkAtivo.Location = new System.Drawing.Point(646, 4);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(122, 19);
            this.chkAtivo.TabIndex = 18;
            this.chkAtivo.Text = "Fornecedor Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // txtBairro
            // 
            this.txtBairro.Enabled = false;
            this.txtBairro.Location = new System.Drawing.Point(583, 98);
            this.txtBairro.MaxLength = 30;
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(185, 20);
            this.txtBairro.TabIndex = 17;
            // 
            // lblBairro
            // 
            this.lblBairro.AutoSize = true;
            this.lblBairro.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBairro.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblBairro.Location = new System.Drawing.Point(524, 99);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(63, 16);
            this.lblBairro.TabIndex = 16;
            this.lblBairro.Text = "Bairro : ";
            // 
            // txtNum
            // 
            this.txtNum.Enabled = false;
            this.txtNum.Location = new System.Drawing.Point(461, 98);
            this.txtNum.MaxLength = 6;
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(57, 20);
            this.txtNum.TabIndex = 15;
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNum.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblNum.Location = new System.Drawing.Point(429, 99);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(35, 16);
            this.lblNum.TabIndex = 14;
            this.lblNum.Text = "Nº : ";
            // 
            // txtEndereco
            // 
            this.txtEndereco.Enabled = false;
            this.txtEndereco.Location = new System.Drawing.Point(96, 98);
            this.txtEndereco.MaxLength = 50;
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(327, 20);
            this.txtEndereco.TabIndex = 13;
            // 
            // lblEndereco
            // 
            this.lblEndereco.AutoSize = true;
            this.lblEndereco.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndereco.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblEndereco.Location = new System.Drawing.Point(12, 99);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(88, 16);
            this.lblEndereco.TabIndex = 12;
            this.lblEndereco.Text = "Endereço : ";
            // 
            // pnlTelefone
            // 
            this.pnlTelefone.BackColor = System.Drawing.Color.Transparent;
            this.pnlTelefone.Controls.Add(this.rdbClaro);
            this.pnlTelefone.Controls.Add(this.rdbVivo);
            this.pnlTelefone.Controls.Add(this.rdbOi);
            this.pnlTelefone.Controls.Add(this.rdbTim);
            this.pnlTelefone.Controls.Add(this.txtCelular);
            this.pnlTelefone.Controls.Add(this.lblCelular);
            this.pnlTelefone.Controls.Add(this.txtTel2);
            this.pnlTelefone.Controls.Add(this.lblTel2);
            this.pnlTelefone.Controls.Add(this.txtTel1);
            this.pnlTelefone.Controls.Add(this.lblTel1);
            this.pnlTelefone.Enabled = false;
            this.pnlTelefone.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTelefone.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.pnlTelefone.Location = new System.Drawing.Point(15, 126);
            this.pnlTelefone.Name = "pnlTelefone";
            this.pnlTelefone.Size = new System.Drawing.Size(753, 54);
            this.pnlTelefone.TabIndex = 11;
            this.pnlTelefone.TabStop = false;
            this.pnlTelefone.Text = "Telefones";
            // 
            // rdbClaro
            // 
            this.rdbClaro.AutoSize = true;
            this.rdbClaro.BackColor = System.Drawing.Color.Khaki;
            this.rdbClaro.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdbClaro.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rdbClaro.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbClaro.Location = new System.Drawing.Point(692, 31);
            this.rdbClaro.Name = "rdbClaro";
            this.rdbClaro.Size = new System.Drawing.Size(55, 17);
            this.rdbClaro.TabIndex = 14;
            this.rdbClaro.TabStop = true;
            this.rdbClaro.Text = "Claro";
            this.rdbClaro.UseVisualStyleBackColor = false;
            // 
            // rdbVivo
            // 
            this.rdbVivo.AutoSize = true;
            this.rdbVivo.BackColor = System.Drawing.Color.Khaki;
            this.rdbVivo.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdbVivo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rdbVivo.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbVivo.Location = new System.Drawing.Point(692, 11);
            this.rdbVivo.Name = "rdbVivo";
            this.rdbVivo.Size = new System.Drawing.Size(52, 17);
            this.rdbVivo.TabIndex = 13;
            this.rdbVivo.TabStop = true;
            this.rdbVivo.Text = "Vivo";
            this.rdbVivo.UseVisualStyleBackColor = false;
            // 
            // rdbOi
            // 
            this.rdbOi.AutoSize = true;
            this.rdbOi.BackColor = System.Drawing.Color.Khaki;
            this.rdbOi.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdbOi.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rdbOi.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbOi.Location = new System.Drawing.Point(644, 31);
            this.rdbOi.Name = "rdbOi";
            this.rdbOi.Size = new System.Drawing.Size(40, 17);
            this.rdbOi.TabIndex = 12;
            this.rdbOi.TabStop = true;
            this.rdbOi.Text = "Oi";
            this.rdbOi.UseVisualStyleBackColor = false;
            // 
            // rdbTim
            // 
            this.rdbTim.AutoSize = true;
            this.rdbTim.BackColor = System.Drawing.Color.Khaki;
            this.rdbTim.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdbTim.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rdbTim.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbTim.Location = new System.Drawing.Point(644, 11);
            this.rdbTim.Name = "rdbTim";
            this.rdbTim.Size = new System.Drawing.Size(46, 17);
            this.rdbTim.TabIndex = 11;
            this.rdbTim.TabStop = true;
            this.rdbTim.Text = "Tim";
            this.rdbTim.UseVisualStyleBackColor = false;
            // 
            // txtCelular
            // 
            this.txtCelular.Location = new System.Drawing.Point(483, 18);
            this.txtCelular.MaxLength = 20;
            this.txtCelular.Name = "txtCelular";
            this.txtCelular.Size = new System.Drawing.Size(146, 20);
            this.txtCelular.TabIndex = 10;
            // 
            // lblCelular
            // 
            this.lblCelular.AutoSize = true;
            this.lblCelular.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCelular.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCelular.Location = new System.Drawing.Point(416, 19);
            this.lblCelular.Name = "lblCelular";
            this.lblCelular.Size = new System.Drawing.Size(66, 16);
            this.lblCelular.TabIndex = 9;
            this.lblCelular.Text = "Celular :";
            // 
            // txtTel2
            // 
            this.txtTel2.Location = new System.Drawing.Point(259, 18);
            this.txtTel2.MaxLength = 20;
            this.txtTel2.Name = "txtTel2";
            this.txtTel2.Size = new System.Drawing.Size(146, 20);
            this.txtTel2.TabIndex = 8;
            // 
            // lblTel2
            // 
            this.lblTel2.AutoSize = true;
            this.lblTel2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTel2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblTel2.Location = new System.Drawing.Point(211, 19);
            this.lblTel2.Name = "lblTel2";
            this.lblTel2.Size = new System.Drawing.Size(48, 16);
            this.lblTel2.TabIndex = 7;
            this.lblTel2.Text = "Tel 2 :";
            // 
            // txtTel1
            // 
            this.txtTel1.Location = new System.Drawing.Point(55, 18);
            this.txtTel1.MaxLength = 20;
            this.txtTel1.Name = "txtTel1";
            this.txtTel1.Size = new System.Drawing.Size(146, 20);
            this.txtTel1.TabIndex = 6;
            // 
            // lblTel1
            // 
            this.lblTel1.AutoSize = true;
            this.lblTel1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTel1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblTel1.Location = new System.Drawing.Point(7, 19);
            this.lblTel1.Name = "lblTel1";
            this.lblTel1.Size = new System.Drawing.Size(48, 16);
            this.lblTel1.TabIndex = 5;
            this.lblTel1.Text = "Tel 1 :";
            // 
            // cmbCidade
            // 
            this.cmbCidade.Enabled = false;
            this.cmbCidade.FormattingEnabled = true;
            this.cmbCidade.Location = new System.Drawing.Point(515, 63);
            this.cmbCidade.Name = "cmbCidade";
            this.cmbCidade.Size = new System.Drawing.Size(253, 21);
            this.cmbCidade.TabIndex = 10;
            // 
            // lblCidade
            // 
            this.lblCidade.AutoSize = true;
            this.lblCidade.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCidade.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCidade.Location = new System.Drawing.Point(449, 65);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(69, 16);
            this.lblCidade.TabIndex = 9;
            this.lblCidade.Text = "Cidade : ";
            // 
            // cmbUf
            // 
            this.cmbUf.Enabled = false;
            this.cmbUf.FormattingEnabled = true;
            this.cmbUf.Location = new System.Drawing.Point(388, 64);
            this.cmbUf.Name = "cmbUf";
            this.cmbUf.Size = new System.Drawing.Size(55, 21);
            this.cmbUf.TabIndex = 8;
            this.cmbUf.SelectedIndexChanged += new System.EventHandler(this.cmbUf_SelectedIndexChanged);
            // 
            // lblUf
            // 
            this.lblUf.AutoSize = true;
            this.lblUf.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUf.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblUf.Location = new System.Drawing.Point(352, 65);
            this.lblUf.Name = "lblUf";
            this.lblUf.Size = new System.Drawing.Size(39, 16);
            this.lblUf.TabIndex = 7;
            this.lblUf.Text = "UF : ";
            // 
            // txtCnpj
            // 
            this.txtCnpj.Location = new System.Drawing.Point(96, 64);
            this.txtCnpj.MaxLength = 14;
            this.txtCnpj.Name = "txtCnpj";
            this.txtCnpj.Size = new System.Drawing.Size(250, 20);
            this.txtCnpj.TabIndex = 6;
            // 
            // lblCnpj
            // 
            this.lblCnpj.AutoSize = true;
            this.lblCnpj.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCnpj.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCnpj.Location = new System.Drawing.Point(12, 65);
            this.lblCnpj.Name = "lblCnpj";
            this.lblCnpj.Size = new System.Drawing.Size(89, 16);
            this.lblCnpj.TabIndex = 5;
            this.lblCnpj.Text = "CNPJ/CPF : ";
            // 
            // txtContato
            // 
            this.txtContato.Enabled = false;
            this.txtContato.Location = new System.Drawing.Point(560, 29);
            this.txtContato.MaxLength = 30;
            this.txtContato.Name = "txtContato";
            this.txtContato.Size = new System.Drawing.Size(208, 20);
            this.txtContato.TabIndex = 4;
            // 
            // lblContato
            // 
            this.lblContato.AutoSize = true;
            this.lblContato.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContato.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblContato.Location = new System.Drawing.Point(490, 30);
            this.lblContato.Name = "lblContato";
            this.lblContato.Size = new System.Drawing.Size(71, 16);
            this.lblContato.TabIndex = 3;
            this.lblContato.Text = "Contato :";
            // 
            // txtFornecedor
            // 
            this.txtFornecedor.Location = new System.Drawing.Point(68, 29);
            this.txtFornecedor.MaxLength = 30;
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.Size = new System.Drawing.Size(416, 20);
            this.txtFornecedor.TabIndex = 2;
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFornecedor.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblFornecedor.Location = new System.Drawing.Point(12, 30);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(60, 16);
            this.lblFornecedor.TabIndex = 1;
            this.lblFornecedor.Text = "Nome : ";
            // 
            // lblDados
            // 
            this.lblDados.AutoSize = true;
            this.lblDados.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDados.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblDados.Location = new System.Drawing.Point(4, 4);
            this.lblDados.Name = "lblDados";
            this.lblDados.Size = new System.Drawing.Size(176, 19);
            this.lblDados.TabIndex = 0;
            this.lblDados.Text = "Dados do Fornecedor";
            // 
            // btnSalvar
            // 
            this.btnSalvar.AutoEllipsis = true;
            this.btnSalvar.BackColor = System.Drawing.Color.White;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.Enabled = false;
            this.btnSalvar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSalvar.Location = new System.Drawing.Point(673, 418);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(114, 35);
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
            this.btnAlterar.Location = new System.Drawing.Point(674, 369);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(114, 35);
            this.btnAlterar.TabIndex = 23;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.UseVisualStyleBackColor = false;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.AutoEllipsis = true;
            this.btnNovo.BackColor = System.Drawing.Color.White;
            this.btnNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovo.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnNovo.Location = new System.Drawing.Point(674, 321);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(114, 35);
            this.btnNovo.TabIndex = 22;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.AutoEllipsis = true;
            this.btnPesquisa.BackColor = System.Drawing.Color.White;
            this.btnPesquisa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisa.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisa.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnPesquisa.Location = new System.Drawing.Point(674, 275);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(114, 35);
            this.btnPesquisa.TabIndex = 21;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.UseVisualStyleBackColor = false;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.AutoEllipsis = true;
            this.btnLimpar.BackColor = System.Drawing.Color.White;
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnLimpar.Location = new System.Drawing.Point(674, 466);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(114, 35);
            this.btnLimpar.TabIndex = 20;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // dgvFornecedores
            // 
            this.dgvFornecedores.AllowUserToAddRows = false;
            this.dgvFornecedores.AllowUserToDeleteRows = false;
            this.dgvFornecedores.AllowUserToResizeColumns = false;
            this.dgvFornecedores.AllowUserToResizeRows = false;
            this.dgvFornecedores.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvFornecedores.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvFornecedores.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvFornecedores.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvFornecedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFornecedores.Location = new System.Drawing.Point(13, 275);
            this.dgvFornecedores.MultiSelect = false;
            this.dgvFornecedores.Name = "dgvFornecedores";
            this.dgvFornecedores.ReadOnly = true;
            this.dgvFornecedores.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvFornecedores.RowHeadersVisible = false;
            this.dgvFornecedores.RowHeadersWidth = 25;
            this.dgvFornecedores.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvFornecedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFornecedores.ShowEditingIcon = false;
            this.dgvFornecedores.Size = new System.Drawing.Size(650, 300);
            this.dgvFornecedores.TabIndex = 45;
            this.dgvFornecedores.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gdvFornecedores_Click);
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.BackColor = System.Drawing.Color.Transparent;
            this.lblMensagem.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensagem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblMensagem.Location = new System.Drawing.Point(10, 256);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(171, 16);
            this.lblMensagem.TabIndex = 44;
            this.lblMensagem.Text = "Relação de Fornecedores";
            // 
            // Fornecedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.dgvFornecedores);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblMensagem);
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.pnlDados);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pnlLogo);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Fornecedores";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Alemão da Construção 1.0";
            this.pnlDados.ResumeLayout(false);
            this.pnlDados.PerformLayout();
            this.pnlTelefone.ResumeLayout(false);
            this.pnlTelefone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFornecedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlDados;
        private System.Windows.Forms.GroupBox pnlTelefone;
        private System.Windows.Forms.RadioButton rdbClaro;
        private System.Windows.Forms.RadioButton rdbVivo;
        private System.Windows.Forms.RadioButton rdbOi;
        private System.Windows.Forms.RadioButton rdbTim;
        private System.Windows.Forms.TextBox txtCelular;
        private System.Windows.Forms.Label lblCelular;
        private System.Windows.Forms.TextBox txtTel2;
        private System.Windows.Forms.Label lblTel2;
        private System.Windows.Forms.TextBox txtTel1;
        private System.Windows.Forms.Label lblTel1;
        private System.Windows.Forms.ComboBox cmbCidade;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.ComboBox cmbUf;
        private System.Windows.Forms.Label lblUf;
        private System.Windows.Forms.TextBox txtCnpj;
        private System.Windows.Forms.Label lblCnpj;
        private System.Windows.Forms.TextBox txtContato;
        private System.Windows.Forms.Label lblContato;
        private System.Windows.Forms.TextBox txtFornecedor;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.Label lblDados;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.Label lblBairro;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.DataGridView dgvFornecedores;
        private System.Windows.Forms.Label lblMensagem;
    }
}

