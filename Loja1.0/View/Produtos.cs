using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Loja1._0.Model;
using System.IO;
using Loja1._0.Control;
using System.Drawing.Imaging;

namespace Loja1._0
{
    public partial class Produtos : Form
    {
        public struct prod
        {
            public string ncm;
            public string desc;
            public string cod;
            public string custo;
            public string preco;
            public string qntAtual;
            public string qntMin;
            public string localNum;
            public string localSigla;
            public string localRef;
        };

        public static List<prod> listaProd = new List<prod>();
        Model.Usuarios user = new Model.Usuarios();

        Controle controle = new Controle();
        static List<Model.Produtos> listaProdutos = new List<Model.Produtos>();
        public static List<Model.Fornecedores> listaFornecedores = new List<Model.Fornecedores>();
        static Model.Produtos produto = new Model.Produtos();
        static Estoque estoque = new Estoque();
        static Gerenciamento gerencia = new Gerenciamento();
        static List<UnidMedidas> listaMedidas = new List<UnidMedidas>();
        Movimentos movimento = new Movimentos();
        public prod detalheProd = new prod();
        static Byte[] bytes;
        static Image foto;
        static Image branco;
        static bool flagNovo = true;
        static int qntTemp = 0;
        static Compras compra = new Compras();

        DataTable dtbProdutos;
        static Button selectProd = new Button();

        Email email = new Email();
        public string erro;

        public Produtos(Model.Usuarios user)
        {
            try
            {
                this.user = user;
                InitializeComponent();

                branco = pnlImagem.BackgroundImage;
                limpaForm();

                listaFornecedores = controle.PesquisaFornecedores("");                
                cmbFornecedor.DataSource = listaFornecedores;
                cmbFornecedor.ValueMember = "nome";

                listaMedidas = controle.PesquisaUnidades("");
                cmbUnidade.DataSource = listaMedidas;
                cmbUnidade.ValueMember = "medida";

                listaProdutos = controle.PesquisaGeralProd();
                listaCompleta(listaProdutos);                
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Produtos.cs, no construtor da classe";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        private void limpaImg()
        {
            pnlImagem.BackgroundImage = branco;
        }

        private void pnlImagem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = ("Image|*.png; *.jpeg; *.jpg; *.gif");
                dialog.Title = "Selecione Imagem";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pnlImagem.BackgroundImage = new Bitmap(dialog.OpenFile());
                }

                foto = pnlImagem.BackgroundImage;
                pnlImagem.BackgroundImageLayout = ImageLayout.Stretch;

                MemoryStream ms = new MemoryStream();
                foto.Save(ms, ImageFormat.Png);
                bytes = ms.ToArray();

            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Produtos.cs, na instrução \"pnlImagem_Click\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listaCompleta(List<Model.Produtos> listaProdutos)
        {
            try
            {
                listaProd = new List<prod>();

                foreach (Model.Produtos value in listaProdutos)
                {
                    compra = controle.PesquisaCompraAnterior(value.id);

                    detalheProd = new prod();
                    detalheProd.desc = value.desc_produto;
                    detalheProd.cod = value.cod_produto;
                    detalheProd.custo = compra.preco_compra.ToString();
                    detalheProd.preco = compra.preco_venda.ToString();
                    detalheProd.qntAtual = value.Estoque.qnt_atual.ToString();
                    detalheProd.qntMin = value.Estoque.qnt_minima.ToString();
                    detalheProd.localNum = value.Estoque.num_local.ToString();
                    detalheProd.localSigla = value.Estoque.letra_local;
                    detalheProd.localRef = value.Estoque.ref_local;

                    listaProd.Add(detalheProd);
                }

                dtbProdutos = new DataTable();
                dtbProdutos.Columns.Add("Produto", typeof(string));
                dtbProdutos.Columns.Add("Código", typeof(string));
                dtbProdutos.Columns.Add("Custo (R$)", typeof(string));
                dtbProdutos.Columns.Add("Preço (R$)", typeof(string));
                dtbProdutos.Columns.Add("Qnd Atual", typeof(string));
                dtbProdutos.Columns.Add("Qnd Min.", typeof(string));
                dtbProdutos.Columns.Add("Local Nº", typeof(string));
                dtbProdutos.Columns.Add("Local Sigla", typeof(string));
                dtbProdutos.Columns.Add("Local Referência", typeof(string));

                for (int i = 0; i < listaProd.Count; i++)
                {
                    dtbProdutos.Rows.Add(listaProd[i].desc, listaProd[i].cod, listaProd[i].custo, listaProd[i].preco, listaProd[i].qntAtual, listaProd[i].qntMin, listaProd[i].localNum, listaProd[i].localSigla, listaProd[i].localRef);
                }

                dgvProdutos.DataSource = dtbProdutos;
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Produtos.cs, na instrução \"listaCompleta\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            try
            {
                flagNovo = true;
                limpaForm();
                listaProdutos = controle.PesquisaGeralProd();
                listaCompleta(listaProdutos);
                lblMensagem.Text = "Relação de produtos";
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Produtos.cs, na instrução \"btnLimpar_Click\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limpaForm()
        {
            qntTemp = 0;
            dgvProdutos.Enabled = true;
            txtCodigo.Enabled = true;            
            txtProduto.Enabled = true;
            txtNcm.Enabled = false;
            txtCusto.Enabled = false;
            txtPreco.Enabled = false;
            txtIcms.Enabled = false;
            txtQntAtual.Enabled = false;
            txtQntMinima.Enabled = false;
            txtQntMaxima.Enabled = false;
            txtLocalNum.Enabled = false;
            txtLocalSigla.Enabled = false;
            txtLocalRef.Enabled = false;
            cmbFornecedor.Enabled = false;
            cmbUnidade.Enabled = false;
            btnPesquisa.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            pnlImagem.Enabled = false;
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            txtCodigo.Text = "";
            txtCusto.Text = "";
            txtNcm.Text = "";
            txtProduto.Text = "";
            txtIcms.Text = "";
            txtPreco.Text = "";
            txtQntMinima.Text = "";
            txtQntMaxima.Text = "";
            txtQntAtual.Text = "";
            txtLocalNum.Text = "";
            txtLocalSigla.Text = "";
            txtLocalRef.Text = "";
            cmbFornecedor.SelectedIndex = -1;
            cmbUnidade.SelectedIndex = -1;
            limpaImg();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProduto.Text.Length < 3 && txtCodigo.Text.Equals(""))
                {
                    MessageBox.Show("É necessário ao menos 3 caracteres para realizar uma busca por nome", "Ação inválida");
                    btnLimpar_Click(sender, e);
                }

                else if (txtCodigo.Text.Length < 13 && txtProduto.Text.Equals(""))
                {
                    MessageBox.Show("É necessário 13 caracteres para realizar uma busca por código", "Ação inválida");
                    btnLimpar_Click(sender, e);
                }

                else
                {
                    if (txtProduto.Text.Equals("") && !txtCodigo.Text.Equals(""))
                    {
                        if (controle.PesquisaProdutosNomeId(txtCodigo.Text.ToUpper()).Count > 0)
                        {
                            txtCodigo.Enabled = false;
                            txtProduto.Enabled = false;
                            lblMensagem.Text = "Produto com o código: " + txtCodigo.Text;
                            listaProdutos = controle.PesquisaProdutosNomeId(txtCodigo.Text.ToUpper().Trim());
                            listaCompleta(listaProdutos);
                        }
                        else
                        {
                            MessageBox.Show("Não existem produtos com o código \"" + txtCodigo.Text.ToUpper().Trim() + "\" ", "Ação inválida");
                            btnLimpar_Click(sender, e);
                        }
                    }
                    else if (txtCodigo.Text.Trim().Equals("") && !txtProduto.Text.Trim().Equals(""))
                    {
                        if (controle.PesquisaProdutosNomeId(txtProduto.Text.ToUpper().Trim()).Count > 0)
                        {
                            txtCodigo.Enabled = false;
                            txtProduto.Enabled = false;
                            lblMensagem.Text = "Relação de produtos que contém a expressão \"" + txtProduto.Text.ToUpper().Trim() + "\" ";
                            listaProdutos = controle.PesquisaProdutosNomeId(txtProduto.Text.ToUpper().Trim());
                            listaCompleta(listaProdutos);
                        }
                        else
                        {
                            MessageBox.Show("Não existem produtos com a expressão \"" + txtProduto.Text.ToUpper().Trim() + "\" ");
                            btnLimpar_Click(sender, e);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Para pesquisa de produtos, utilize somente, ou o código completo, ou parte do nome deste.", "Ação inválida");
                        btnLimpar_Click(sender, e);
                    }
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Produtos.cs, na instrução \"btnPesquisar_Click\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {           
            limpaForm();
            dgvProdutos.Enabled = false;
            flagNovo = true;
            txtCodigo.Enabled = true;
            txtCusto.Enabled = true;
            txtProduto.Enabled = true;
            txtNcm.Enabled = true;
            txtPreco.Enabled = true;
            txtIcms.Enabled = true;
            txtQntAtual.Enabled = true;
            txtQntMinima.Enabled = true;
            txtQntMaxima.Enabled = true;
            txtLocalNum.Enabled = true;
            txtLocalSigla.Enabled = true;
            txtLocalRef.Enabled = true;
            cmbFornecedor.Enabled = true;
            cmbUnidade.Enabled = true;
            btnPesquisa.Enabled = false;
            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnSalvar.Enabled = true;
            pnlImagem.Enabled = true;
            qntTemp = 0;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = true;
            flagNovo = false;
            dgvProdutos.Enabled = false;
            txtCusto.Enabled = true;
            txtProduto.Enabled = true;
            txtCodigo.Enabled = true;
            txtNcm.Enabled = true;
            txtPreco.Enabled = true;
            txtIcms.Enabled = true;
            txtQntMinima.Enabled = true;
            txtQntAtual.Enabled = true;
            txtQntMaxima.Enabled = true;
            txtLocalNum.Enabled = true;
            txtLocalSigla.Enabled = true;
            txtLocalRef.Enabled = true;
            cmbFornecedor.Enabled = true;
            cmbUnidade.Enabled = true;
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;            
            pnlImagem.Enabled = true;
            qntTemp = Convert.ToInt32(txtQntAtual.Text);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                produto.status = 0;
                controle.SalvaAtualiza();
                limpaForm();
                listaProdutos = controle.PesquisaGeralProd();
                listaCompleta(listaProdutos);
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Produtos.cs, na instrução \"btnExcluir_Click\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //Condicionais para validação do preenchimento
                //campos preenchidos
                if (!validaCampos())
                {
                    MessageBox.Show("Todos os campos devem ser preenchidos com valores válidos.");
                }
                //campo código numérico e com 13 digitos
                else if (txtCodigo.Text.All(char.IsLetter) || txtCodigo.Text.Length != 13)
                {
                    txtCodigo.Text = "";
                    MessageBox.Show("O campo \"Código\" deve ser exclusivamente numérico com 13 caracteres.");
                }
                //campo de custo, icms e preço correspondendo a valor monetário
                else if ((!txtCusto.Text.Any(char.IsNumber) || !txtCusto.Text.Any(char.IsPunctuation)) && Convert.ToInt32(txtCusto.Text) != 0)
                {
                    txtCusto.Text = "";
                    MessageBox.Show("O campo \"Custo\" deve ser preenchido com o formato \"XX.XX\".");
                }
                else if ((!txtIcms.Text.Any(char.IsNumber) || !txtIcms.Text.Any(char.IsPunctuation)) && Convert.ToInt32(txtIcms.Text) != 0)
                {
                    txtIcms.Text = "";
                    MessageBox.Show("O campo \"ICMS\" deve ser preenchido com o formato \"XX.XX\".");
                }
                else if ((!txtPreco.Text.Any(char.IsNumber) || !txtPreco.Text.Any(char.IsPunctuation)) && Convert.ToInt32(txtPreco.Text) != 0)
                {
                    txtPreco.Text = "";
                    MessageBox.Show("O campo \"Preço\" deve ser preenchido com o formato \"XX.XX\".");
                }
                //quantidade numérica
                else if (!txtQntAtual.Text.All(char.IsNumber))
                {
                    txtQntAtual.Text = "";
                    MessageBox.Show("O campo \"Qnt\" deve ser preenchido com valores numéricos.");
                }
                //preço superior ao custo
                else if (Convert.ToDouble(txtCusto.Text) >= Convert.ToDouble(txtPreco.Text))
                {
                    MessageBox.Show("O campo \"Preco\" obrigatóriamente deve ser maior que o \"Custo\".");
                }
                else if (validaCampos())
                {

                    //condicionais referente a regras de negócio
                    //Icms inferior a 25% do preço de custo
                    if (Convert.ToDouble(txtIcms.Text) > (Convert.ToDouble(txtCusto.Text) * 0.25))
                    {
                        MessageBox.Show("O campo \"ICMS\" corresponde a mais de 25% do custo de compra, Edite o produto caso necessário.");
                    }
                    //Lucro minimo de 30% sobre o custo
                    if (Convert.ToDecimal(txtPreco.Text) < (Convert.ToDecimal(txtCusto.Text) + (Convert.ToDecimal(txtCusto.Text) * (gerencia.lucroMinimo / 100))))
                    {
                        MessageBox.Show("O campo \"Preco\" está com uma taxa de lucro inferior ao determinado pela Administração.");
                    }                    

                    dgvProdutos.Enabled = true;

                    //se flag novo = true, novo elemento
                    if (flagNovo)
                    {
                        movimento = new Movimentos();
                        controle.SalvarMovimento(movimento);
                        movimento.data = DateTime.Today;
                        movimento.desc = "Aquisição de Estoque";
                        movimento.id_tipo = 12;
                        movimento.valor = Convert.ToDecimal(txtCusto.Text) * Convert.ToInt32(txtQntAtual.Text.Trim());

                        txtCusto.Text = Convert.ToDecimal(txtCusto.Text).ToString("0.00");
                        txtPreco.Text = Convert.ToDecimal(txtPreco.Text).ToString("0.00");
                        txtIcms.Text = Convert.ToDecimal(txtIcms.Text).ToString("0.00");
                        
                        produto = new Model.Produtos();
                        controle.SalvarProduto(produto);
                        produto.cod_produto = txtCodigo.Text.Trim();
                        produto.desc_produto = txtProduto.Text.Trim().ToUpper();
                        produto.ncm = txtNcm.Text.Trim().ToUpper();
                        produto.id_medida = controle.PesquisaMedidaByDesc(cmbUnidade.SelectedValue.ToString()).id;
                        produto.imagem = bytes;
                        produto.status = 1;
                        controle.SalvaAtualiza();

                        compra = new Compras();
                        controle.SalvarCompras(compra);
                        compra.id_produto = produto.id;
                        compra.preco_compra = Convert.ToDecimal(txtCusto.Text.Trim());
                        compra.preco_venda = Convert.ToDecimal(txtPreco.Text.Trim());
                        compra.icms_pago = Convert.ToDecimal(txtIcms.Text.Trim());
                        compra.id_fornecedor = controle.PesquisaFornecedorByNome(cmbFornecedor.SelectedValue.ToString()).id;
                        compra.qnt_compra = Convert.ToInt32(txtQntAtual.Text.Trim());
                        compra.status = 1;
                        compra.dt_compra = DateTime.Now;
                        controle.SalvaAtualiza();

                        estoque = new Estoque();
                        controle.SalvarEstoque(estoque);
                        estoque.id_produto = produto.id;
                        estoque.qnt_atual = Convert.ToInt32(txtQntAtual.Text.Trim());
                        estoque.qnt_minima = Convert.ToInt32(txtQntMinima.Text.Trim());
                        estoque.qnt_maxima = Convert.ToInt32(txtQntMaxima.Text.Trim());
                        estoque.num_local = Convert.ToInt32(txtLocalNum.Text.Trim());
                        estoque.letra_local = txtLocalSigla.Text.Trim().ToUpper();
                        estoque.ref_local = txtLocalRef.Text.Trim().ToUpper();
                        controle.SalvaAtualiza();

                        limpaForm();

                        listaProdutos = controle.PesquisaGeralProd();
                        listaCompleta(listaProdutos);
                    }

                    //alteração de elemento existente na base de dados
                    else if (!flagNovo)
                    {
                        if (qntTemp < Convert.ToInt32(txtQntAtual.Text))
                        {
                            movimento = new Movimentos();
                            controle.SalvarMovimento(movimento);
                            movimento.data = DateTime.Today;
                            movimento.desc = "Aquisição de Estoque";
                            movimento.id_tipo = 12;
                            movimento.valor = Convert.ToDecimal(txtCusto.Text) * (Convert.ToInt32(txtQntAtual.Text.Trim()) - qntTemp);
                        }
                        else if(qntTemp > Convert.ToInt32(txtQntAtual.Text))
                        {
                            txtQntAtual.Text = qntTemp.ToString();
                            MessageBox.Show("O campo \"Qnt Atual\" não aceita redução de quantidade de forma manual, a alteração será disfeita.");
                        }

                        txtCusto.Text = Convert.ToDecimal(txtCusto.Text).ToString("0.00");
                        txtPreco.Text = Convert.ToDecimal(txtPreco.Text).ToString("0.00");
                        txtIcms.Text = Convert.ToDecimal(txtIcms.Text).ToString("0.00");

                        int id = produto.id;
                        produto = new Model.Produtos();
                        produto = controle.PesquisaProdutoId(id);
                        produto.cod_produto = txtCodigo.Text.Trim();
                        produto.ncm = txtNcm.Text.Trim().ToUpper();
                        produto.desc_produto = txtProduto.Text.Trim().ToUpper();
                        produto.id_medida = controle.PesquisaMedidaByDesc(cmbUnidade.SelectedValue.ToString()).id;
                        produto.imagem = bytes;
                        produto.status = 1;
                        
                        controle.SalvaAtualiza();

                        if (qntTemp < Convert.ToInt32(txtQntAtual.Text))
                        {
                            compra = controle.PesquisaCompraAnterior(produto.id);
                            compra.status = 0;
                            controle.SalvaAtualiza();

                            compra = new Compras();
                            controle.SalvarCompras(compra);
                            compra.id_produto = produto.id;
                            compra.preco_compra = Convert.ToDecimal(txtCusto.Text.Trim());
                            compra.preco_venda = Convert.ToDecimal(txtPreco.Text.Trim());
                            compra.icms_pago = Convert.ToDecimal(txtIcms.Text.Trim());
                            compra.id_fornecedor = controle.PesquisaFornecedorByNome(cmbFornecedor.SelectedValue.ToString()).id;
                            compra.qnt_compra = Convert.ToInt32(txtQntAtual.Text.Trim()) - qntTemp;
                            compra.status = 1;
                            compra.dt_compra = DateTime.Now;
                            controle.SalvaAtualiza();
                        }
                        else
                        {
                            compra = controle.PesquisaCompraAnterior(produto.id);
                            compra.preco_compra = Convert.ToDecimal(txtCusto.Text.Trim());
                            compra.preco_venda = Convert.ToDecimal(txtPreco.Text.Trim());
                            compra.icms_pago = Convert.ToDecimal(txtIcms.Text.Trim());
                            compra.id_fornecedor = controle.PesquisaFornecedorByNome(cmbFornecedor.SelectedValue.ToString()).id;
                            controle.SalvaAtualiza();
                        }

                        estoque = new Estoque();
                        estoque = controle.PesquisaProdEstoqueId(id);
                        estoque.qnt_atual = Convert.ToInt32(txtQntAtual.Text);
                        estoque.qnt_minima = Convert.ToInt32(txtQntMinima.Text);
                        estoque.qnt_maxima = Convert.ToInt32(txtQntMaxima.Text);
                        estoque.num_local = Convert.ToInt32(txtLocalNum.Text);
                        estoque.letra_local = txtLocalSigla.Text;
                        estoque.ref_local = txtLocalRef.Text;
                        controle.SalvaAtualiza();

                        limpaForm();

                        listaProdutos = controle.PesquisaGeralProd();
                        listaCompleta(listaProdutos);
                    }
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Produtos.cs, na instrução \"btnSalvar_Click\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validaCampos()
        {
            if (!txtCodigo.Text.Equals("") && !txtCusto.Text.Equals("") && !txtProduto.Text.Equals("") && !txtPreco.Text.Equals("") && !txtIcms.Text.Equals("") && !txtQntAtual.Text.Equals("") && !txtQntMinima.Text.Equals(""))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private void dgvProduto_Select(object sender, DataGridViewCellMouseEventArgs e)
        {
            selecionaLinha();                
        }        

        private void dgvProduto_Enter(object sender, DataGridViewCellEventArgs e)
        {
            selecionaLinha();
        }

        private void selecionaLinha()
        {
            try
            {
                txtCodigo.Enabled = false;
                txtProduto.Enabled = false;
                produto = controle.PesquisaProdutoCod(dgvProdutos.SelectedCells[1].Value.ToString());
                if (produto != null)
                {
                    compra = controle.PesquisaCompraAnterior(produto.id);

                    btnNovo.Enabled = false;
                    btnPesquisa.Enabled = false;
                    btnAlterar.Enabled = true;
                    txtCodigo.Text = produto.cod_produto;
                    txtCusto.Text = compra.preco_compra.ToString();
                    txtProduto.Text = produto.desc_produto;
                    txtNcm.Text = produto.ncm;
                    txtIcms.Text = compra.icms_pago.ToString();
                    txtPreco.Text = compra.preco_venda.ToString();
                    txtQntMinima.Text = produto.Estoque.qnt_minima.ToString();
                    txtQntMaxima.Text = produto.Estoque.qnt_maxima.ToString();
                    txtQntAtual.Text = produto.Estoque.qnt_atual.ToString();
                    txtLocalNum.Text = produto.Estoque.num_local.ToString();
                    txtLocalSigla.Text = produto.Estoque.letra_local.ToString();
                    txtLocalRef.Text = produto.Estoque.ref_local.ToString();
                    cmbFornecedor.SelectedValue = controle.PesquisaFornecedorById(compra.id_fornecedor).nome;
                    cmbUnidade.SelectedValue = controle.PesquisaMedidaById(Convert.ToInt32(produto.id_medida)).medida;
                    if (produto.imagem == null)
                    {
                        pnlImagem.BackgroundImage = branco;
                    }
                    else
                    {
                        pnlImagem.BackgroundImage = Image.FromStream(new MemoryStream(produto.imagem));
                    }
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Produtos.cs, na instrução \"selecionaLinha\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
