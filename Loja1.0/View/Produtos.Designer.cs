namespace Loja1._0
{
    partial class Produtos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Produtos));
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblNome = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlImagem = new System.Windows.Forms.Panel();
            this.lblProduto = new System.Windows.Forms.Label();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblUnidade = new System.Windows.Forms.Label();
            this.cmbUnidade = new System.Windows.Forms.ComboBox();
            this.txtCusto = new System.Windows.Forms.TextBox();
            this.lblCusto = new System.Windows.Forms.Label();
            this.txtIcms = new System.Windows.Forms.TextBox();
            this.lblIcms = new System.Windows.Forms.Label();
            this.txtPreco = new System.Windows.Forms.TextBox();
            this.lblPreco = new System.Windows.Forms.Label();
            this.cmbFornecedor = new System.Windows.Forms.ComboBox();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.txtQntAtual = new System.Windows.Forms.TextBox();
            this.lblQntAtual = new System.Windows.Forms.Label();
            this.txtQntMinima = new System.Windows.Forms.TextBox();
            this.lblQntMinima = new System.Windows.Forms.Label();
            this.txtQntMaxima = new System.Windows.Forms.TextBox();
            this.lblQntMaxima = new System.Windows.Forms.Label();
            this.txtLocalRef = new System.Windows.Forms.TextBox();
            this.lblLocalRef = new System.Windows.Forms.Label();
            this.lblLocalSigla = new System.Windows.Forms.Label();
            this.txtLocalNum = new System.Windows.Forms.TextBox();
            this.lblLocalNum = new System.Windows.Forms.Label();
            this.txtLocalSigla = new System.Windows.Forms.TextBox();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.dgvProdutos = new System.Windows.Forms.DataGridView();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtNcm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLogo.BackgroundImage")));
            this.pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLogo.Location = new System.Drawing.Point(669, 528);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(114, 60);
            this.pnlLogo.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExit.Location = new System.Drawing.Point(763, 3);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 34);
            this.btnExit.TabIndex = 21;
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
            this.lblNome.Location = new System.Drawing.Point(493, 6);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(260, 33);
            this.lblNome.TabIndex = 3;
            this.lblNome.Text = "Cadastro Produtos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(0, 583);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "© 2017 Prezia Software House";
            // 
            // pnlImagem
            // 
            this.pnlImagem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlImagem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlImagem.BackgroundImage")));
            this.pnlImagem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlImagem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlImagem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlImagem.Enabled = false;
            this.pnlImagem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.pnlImagem.Location = new System.Drawing.Point(16, 48);
            this.pnlImagem.Name = "pnlImagem";
            this.pnlImagem.Size = new System.Drawing.Size(202, 195);
            this.pnlImagem.TabIndex = 20;
            this.pnlImagem.TabStop = true;
            this.pnlImagem.Click += new System.EventHandler(this.pnlImagem_Click);
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.BackColor = System.Drawing.Color.Transparent;
            this.lblProduto.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduto.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblProduto.Location = new System.Drawing.Point(241, 49);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(132, 16);
            this.lblProduto.TabIndex = 8;
            this.lblProduto.Text = "Descrição Produto :";
            // 
            // txtProduto
            // 
            this.txtProduto.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtProduto.Location = new System.Drawing.Point(379, 48);
            this.txtProduto.MaxLength = 30;
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.Size = new System.Drawing.Size(399, 20);
            this.txtProduto.TabIndex = 1;
            // 
            // txtCodigo
            // 
            this.txtCodigo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtCodigo.Location = new System.Drawing.Point(308, 81);
            this.txtCodigo.MaxLength = 13;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(235, 20);
            this.txtCodigo.TabIndex = 2;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigo.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCodigo.Location = new System.Drawing.Point(241, 82);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(61, 16);
            this.lblCodigo.TabIndex = 10;
            this.lblCodigo.Text = "Código :";
            // 
            // lblUnidade
            // 
            this.lblUnidade.AutoSize = true;
            this.lblUnidade.BackColor = System.Drawing.Color.Transparent;
            this.lblUnidade.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidade.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblUnidade.Location = new System.Drawing.Point(549, 82);
            this.lblUnidade.Name = "lblUnidade";
            this.lblUnidade.Size = new System.Drawing.Size(69, 16);
            this.lblUnidade.TabIndex = 12;
            this.lblUnidade.Text = "Unidade :";
            // 
            // cmbUnidade
            // 
            this.cmbUnidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnidade.Enabled = false;
            this.cmbUnidade.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cmbUnidade.FormattingEnabled = true;
            this.cmbUnidade.Items.AddRange(new object[] {
            "UNIDADE",
            "PACOTE",
            "CAIXA",
            "LITRO",
            "METRO"});
            this.cmbUnidade.Location = new System.Drawing.Point(625, 81);
            this.cmbUnidade.Name = "cmbUnidade";
            this.cmbUnidade.Size = new System.Drawing.Size(155, 21);
            this.cmbUnidade.TabIndex = 3;
            // 
            // txtCusto
            // 
            this.txtCusto.Enabled = false;
            this.txtCusto.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtCusto.Location = new System.Drawing.Point(298, 115);
            this.txtCusto.MaxLength = 10;
            this.txtCusto.Name = "txtCusto";
            this.txtCusto.Size = new System.Drawing.Size(119, 20);
            this.txtCusto.TabIndex = 4;
            // 
            // lblCusto
            // 
            this.lblCusto.AutoSize = true;
            this.lblCusto.BackColor = System.Drawing.Color.Transparent;
            this.lblCusto.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusto.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCusto.Location = new System.Drawing.Point(241, 116);
            this.lblCusto.Name = "lblCusto";
            this.lblCusto.Size = new System.Drawing.Size(51, 16);
            this.lblCusto.TabIndex = 14;
            this.lblCusto.Text = "Custo :";
            // 
            // txtIcms
            // 
            this.txtIcms.Enabled = false;
            this.txtIcms.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtIcms.Location = new System.Drawing.Point(476, 115);
            this.txtIcms.MaxLength = 10;
            this.txtIcms.Name = "txtIcms";
            this.txtIcms.Size = new System.Drawing.Size(119, 20);
            this.txtIcms.TabIndex = 5;
            // 
            // lblIcms
            // 
            this.lblIcms.AutoSize = true;
            this.lblIcms.BackColor = System.Drawing.Color.Transparent;
            this.lblIcms.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcms.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblIcms.Location = new System.Drawing.Point(423, 116);
            this.lblIcms.Name = "lblIcms";
            this.lblIcms.Size = new System.Drawing.Size(49, 16);
            this.lblIcms.TabIndex = 16;
            this.lblIcms.Text = "ICMS :";
            // 
            // txtPreco
            // 
            this.txtPreco.Enabled = false;
            this.txtPreco.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtPreco.Location = new System.Drawing.Point(659, 115);
            this.txtPreco.MaxLength = 10;
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.Size = new System.Drawing.Size(119, 20);
            this.txtPreco.TabIndex = 6;
            // 
            // lblPreco
            // 
            this.lblPreco.AutoSize = true;
            this.lblPreco.BackColor = System.Drawing.Color.Transparent;
            this.lblPreco.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreco.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblPreco.Location = new System.Drawing.Point(600, 116);
            this.lblPreco.Name = "lblPreco";
            this.lblPreco.Size = new System.Drawing.Size(53, 16);
            this.lblPreco.TabIndex = 18;
            this.lblPreco.Text = "Preço :";
            // 
            // cmbFornecedor
            // 
            this.cmbFornecedor.AutoCompleteCustomSource.AddRange(new string[] {
            "UNIDADE",
            "PACOTE",
            "CAIXA",
            "LITRO",
            "METRO"});
            this.cmbFornecedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFornecedor.Enabled = false;
            this.cmbFornecedor.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cmbFornecedor.FormattingEnabled = true;
            this.cmbFornecedor.Items.AddRange(new object[] {
            "UNIDADE",
            "PACOTE",
            "CAIXA",
            "LITRO",
            "METRO"});
            this.cmbFornecedor.Location = new System.Drawing.Point(338, 151);
            this.cmbFornecedor.Name = "cmbFornecedor";
            this.cmbFornecedor.Size = new System.Drawing.Size(256, 21);
            this.cmbFornecedor.TabIndex = 7;
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.BackColor = System.Drawing.Color.Transparent;
            this.lblFornecedor.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFornecedor.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblFornecedor.Location = new System.Drawing.Point(241, 152);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(89, 16);
            this.lblFornecedor.TabIndex = 20;
            this.lblFornecedor.Text = "Fornecedor :";
            this.lblFornecedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtQntAtual
            // 
            this.txtQntAtual.Enabled = false;
            this.txtQntAtual.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtQntAtual.Location = new System.Drawing.Point(327, 186);
            this.txtQntAtual.MaxLength = 6;
            this.txtQntAtual.Name = "txtQntAtual";
            this.txtQntAtual.Size = new System.Drawing.Size(92, 20);
            this.txtQntAtual.TabIndex = 9;
            // 
            // lblQntAtual
            // 
            this.lblQntAtual.AutoSize = true;
            this.lblQntAtual.BackColor = System.Drawing.Color.Transparent;
            this.lblQntAtual.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQntAtual.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblQntAtual.Location = new System.Drawing.Point(242, 187);
            this.lblQntAtual.Name = "lblQntAtual";
            this.lblQntAtual.Size = new System.Drawing.Size(79, 16);
            this.lblQntAtual.TabIndex = 22;
            this.lblQntAtual.Text = "Qnt. Atual :";
            // 
            // txtQntMinima
            // 
            this.txtQntMinima.Enabled = false;
            this.txtQntMinima.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtQntMinima.Location = new System.Drawing.Point(504, 187);
            this.txtQntMinima.MaxLength = 6;
            this.txtQntMinima.Name = "txtQntMinima";
            this.txtQntMinima.Size = new System.Drawing.Size(92, 20);
            this.txtQntMinima.TabIndex = 10;
            // 
            // lblQntMinima
            // 
            this.lblQntMinima.AutoSize = true;
            this.lblQntMinima.BackColor = System.Drawing.Color.Transparent;
            this.lblQntMinima.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQntMinima.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblQntMinima.Location = new System.Drawing.Point(425, 188);
            this.lblQntMinima.Name = "lblQntMinima";
            this.lblQntMinima.Size = new System.Drawing.Size(73, 16);
            this.lblQntMinima.TabIndex = 24;
            this.lblQntMinima.Text = "Qnt. Min. :";
            // 
            // txtQntMaxima
            // 
            this.txtQntMaxima.Enabled = false;
            this.txtQntMaxima.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtQntMaxima.Location = new System.Drawing.Point(686, 188);
            this.txtQntMaxima.MaxLength = 6;
            this.txtQntMaxima.Name = "txtQntMaxima";
            this.txtQntMaxima.Size = new System.Drawing.Size(92, 20);
            this.txtQntMaxima.TabIndex = 11;
            // 
            // lblQntMaxima
            // 
            this.lblQntMaxima.AutoSize = true;
            this.lblQntMaxima.BackColor = System.Drawing.Color.Transparent;
            this.lblQntMaxima.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQntMaxima.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblQntMaxima.Location = new System.Drawing.Point(603, 189);
            this.lblQntMaxima.Name = "lblQntMaxima";
            this.lblQntMaxima.Size = new System.Drawing.Size(77, 16);
            this.lblQntMaxima.TabIndex = 26;
            this.lblQntMaxima.Text = "Qnt. Max. :";
            // 
            // txtLocalRef
            // 
            this.txtLocalRef.Enabled = false;
            this.txtLocalRef.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtLocalRef.Location = new System.Drawing.Point(535, 221);
            this.txtLocalRef.MaxLength = 50;
            this.txtLocalRef.Name = "txtLocalRef";
            this.txtLocalRef.Size = new System.Drawing.Size(243, 20);
            this.txtLocalRef.TabIndex = 14;
            // 
            // lblLocalRef
            // 
            this.lblLocalRef.AutoSize = true;
            this.lblLocalRef.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalRef.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalRef.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblLocalRef.Location = new System.Drawing.Point(500, 224);
            this.lblLocalRef.Name = "lblLocalRef";
            this.lblLocalRef.Size = new System.Drawing.Size(33, 16);
            this.lblLocalRef.TabIndex = 32;
            this.lblLocalRef.Text = "Ref.";
            // 
            // lblLocalSigla
            // 
            this.lblLocalSigla.AutoSize = true;
            this.lblLocalSigla.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalSigla.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalSigla.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblLocalSigla.Location = new System.Drawing.Point(408, 223);
            this.lblLocalSigla.Name = "lblLocalSigla";
            this.lblLocalSigla.Size = new System.Drawing.Size(41, 16);
            this.lblLocalSigla.TabIndex = 30;
            this.lblLocalSigla.Text = "Sigla";
            // 
            // txtLocalNum
            // 
            this.txtLocalNum.Enabled = false;
            this.txtLocalNum.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtLocalNum.Location = new System.Drawing.Point(359, 221);
            this.txtLocalNum.MaxLength = 2;
            this.txtLocalNum.Name = "txtLocalNum";
            this.txtLocalNum.Size = new System.Drawing.Size(42, 20);
            this.txtLocalNum.TabIndex = 12;
            // 
            // lblLocalNum
            // 
            this.lblLocalNum.AutoSize = true;
            this.lblLocalNum.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalNum.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalNum.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblLocalNum.Location = new System.Drawing.Point(242, 222);
            this.lblLocalNum.Name = "lblLocalNum";
            this.lblLocalNum.Size = new System.Drawing.Size(115, 16);
            this.lblLocalNum.TabIndex = 28;
            this.lblLocalNum.Text = "Localização :  Nº";
            // 
            // txtLocalSigla
            // 
            this.txtLocalSigla.Enabled = false;
            this.txtLocalSigla.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtLocalSigla.Location = new System.Drawing.Point(451, 221);
            this.txtLocalSigla.MaxLength = 2;
            this.txtLocalSigla.Name = "txtLocalSigla";
            this.txtLocalSigla.Size = new System.Drawing.Size(42, 20);
            this.txtLocalSigla.TabIndex = 13;
            // 
            // btnLimpar
            // 
            this.btnLimpar.AutoEllipsis = true;
            this.btnLimpar.BackColor = System.Drawing.Color.White;
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnLimpar.Location = new System.Drawing.Point(669, 480);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(114, 35);
            this.btnLimpar.TabIndex = 14;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.AutoEllipsis = true;
            this.btnPesquisa.BackColor = System.Drawing.Color.White;
            this.btnPesquisa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisa.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisa.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnPesquisa.Location = new System.Drawing.Point(669, 275);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(114, 35);
            this.btnPesquisa.TabIndex = 15;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.UseVisualStyleBackColor = false;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.AutoEllipsis = true;
            this.btnNovo.BackColor = System.Drawing.Color.White;
            this.btnNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovo.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnNovo.Location = new System.Drawing.Point(669, 316);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(114, 35);
            this.btnNovo.TabIndex = 16;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.AutoEllipsis = true;
            this.btnSalvar.BackColor = System.Drawing.Color.White;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.Enabled = false;
            this.btnSalvar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSalvar.Location = new System.Drawing.Point(669, 439);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(114, 35);
            this.btnSalvar.TabIndex = 19;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.AutoEllipsis = true;
            this.btnExcluir.BackColor = System.Drawing.Color.White;
            this.btnExcluir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluir.Enabled = false;
            this.btnExcluir.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnExcluir.Location = new System.Drawing.Point(669, 398);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(114, 35);
            this.btnExcluir.TabIndex = 18;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.AutoEllipsis = true;
            this.btnAlterar.BackColor = System.Drawing.Color.White;
            this.btnAlterar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAlterar.Enabled = false;
            this.btnAlterar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnAlterar.Location = new System.Drawing.Point(669, 357);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(114, 35);
            this.btnAlterar.TabIndex = 17;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.UseVisualStyleBackColor = false;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.BackColor = System.Drawing.Color.Transparent;
            this.lblMensagem.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensagem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblMensagem.Location = new System.Drawing.Point(13, 256);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(140, 16);
            this.lblMensagem.TabIndex = 42;
            this.lblMensagem.Text = "Relação de Produtos";
            // 
            // dgvProdutos
            // 
            this.dgvProdutos.AllowUserToAddRows = false;
            this.dgvProdutos.AllowUserToDeleteRows = false;
            this.dgvProdutos.AllowUserToResizeColumns = false;
            this.dgvProdutos.AllowUserToResizeRows = false;
            this.dgvProdutos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvProdutos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvProdutos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvProdutos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvProdutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProdutos.Location = new System.Drawing.Point(16, 275);
            this.dgvProdutos.MultiSelect = false;
            this.dgvProdutos.Name = "dgvProdutos";
            this.dgvProdutos.ReadOnly = true;
            this.dgvProdutos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvProdutos.RowHeadersVisible = false;
            this.dgvProdutos.RowHeadersWidth = 25;
            this.dgvProdutos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvProdutos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProdutos.ShowEditingIcon = false;
            this.dgvProdutos.Size = new System.Drawing.Size(639, 304);
            this.dgvProdutos.TabIndex = 43;
            this.dgvProdutos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduto_Enter);
            this.dgvProdutos.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvProduto_Select);
            this.dgvProdutos.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvProduto_Select);
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
            this.lblTitulo.Location = new System.Drawing.Point(8, 6);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(473, 33);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Sistema Alemão da Construção 1.0";
            // 
            // txtNcm
            // 
            this.txtNcm.Enabled = false;
            this.txtNcm.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtNcm.Location = new System.Drawing.Point(651, 151);
            this.txtNcm.MaxLength = 10;
            this.txtNcm.Name = "txtNcm";
            this.txtNcm.Size = new System.Drawing.Size(127, 20);
            this.txtNcm.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(600, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 45;
            this.label1.Text = "NCM :";
            // 
            // Produtos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.txtNcm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvProdutos);
            this.Controls.Add(this.lblMensagem);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.txtLocalSigla);
            this.Controls.Add(this.txtLocalRef);
            this.Controls.Add(this.lblLocalRef);
            this.Controls.Add(this.lblLocalSigla);
            this.Controls.Add(this.txtLocalNum);
            this.Controls.Add(this.lblLocalNum);
            this.Controls.Add(this.txtQntMaxima);
            this.Controls.Add(this.lblQntMaxima);
            this.Controls.Add(this.txtQntMinima);
            this.Controls.Add(this.lblQntMinima);
            this.Controls.Add(this.txtQntAtual);
            this.Controls.Add(this.lblQntAtual);
            this.Controls.Add(this.cmbFornecedor);
            this.Controls.Add(this.lblFornecedor);
            this.Controls.Add(this.txtPreco);
            this.Controls.Add(this.lblPreco);
            this.Controls.Add(this.txtIcms);
            this.Controls.Add(this.lblIcms);
            this.Controls.Add(this.txtCusto);
            this.Controls.Add(this.lblCusto);
            this.Controls.Add(this.cmbUnidade);
            this.Controls.Add(this.lblUnidade);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.txtProduto);
            this.Controls.Add(this.lblProduto);
            this.Controls.Add(this.pnlImagem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pnlLogo);
            this.Controls.Add(this.lblTitulo);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Produtos";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Alemão da Construção 1.0";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlImagem;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.TextBox txtProduto;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblUnidade;
        private System.Windows.Forms.ComboBox cmbUnidade;
        private System.Windows.Forms.TextBox txtCusto;
        private System.Windows.Forms.Label lblCusto;
        private System.Windows.Forms.TextBox txtIcms;
        private System.Windows.Forms.Label lblIcms;
        private System.Windows.Forms.TextBox txtPreco;
        private System.Windows.Forms.Label lblPreco;
        private System.Windows.Forms.ComboBox cmbFornecedor;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.TextBox txtQntAtual;
        private System.Windows.Forms.Label lblQntAtual;
        private System.Windows.Forms.TextBox txtQntMinima;
        private System.Windows.Forms.Label lblQntMinima;
        private System.Windows.Forms.TextBox txtQntMaxima;
        private System.Windows.Forms.Label lblQntMaxima;
        private System.Windows.Forms.TextBox txtLocalRef;
        private System.Windows.Forms.Label lblLocalRef;
        private System.Windows.Forms.Label lblLocalSigla;
        private System.Windows.Forms.TextBox txtLocalNum;
        private System.Windows.Forms.Label lblLocalNum;
        private System.Windows.Forms.TextBox txtLocalSigla;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.DataGridView dgvProdutos;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtNcm;
        private System.Windows.Forms.Label label1;
    }
}

