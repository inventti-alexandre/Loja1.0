using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        static List<Model.Fornecedores> listaFornecedores = new List<Model.Fornecedores>();
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

        DataTable dtbProdutos;
        static Button selectProd = new Button();

        public Produtos(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();

            branco = pnlImagem.BackgroundImage;
            limpaForm();

            listaFornecedores = controle.pesquisaFornecedores("");
            cmbFornecedor.DataSource = listaFornecedores;            
            cmbFornecedor.ValueMember = "nome";

            listaMedidas = controle.pesquisaUnidades("");
            cmbUnidade.DataSource = listaMedidas;
            cmbUnidade.ValueMember = "medida";

            listaProdutos = controle.pesquisaGeralProd();
            listaCompleta(listaProdutos);
            
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

            //byte[] para imagem
            //foto = Image.FromStream(new MemoryStream(byteArray);
        }

        private void listaCompleta(List<Model.Produtos> listaProdutos)
        {
            
            listaProd = new List<prod>();

            foreach (Model.Produtos value in listaProdutos)
            {
                detalheProd = new prod();
                detalheProd.desc = value.desc_produto;
                detalheProd.cod = value.cod_produto;
                detalheProd.custo = value.preco_compra.ToString();
                detalheProd.preco = value.preco_venda.ToString();
                detalheProd.qntAtual = value.Estoque.qnt_atual.ToString();
                detalheProd.qntMin = value.Estoque.qnt_minima.ToString();
                detalheProd.localNum = value.Estoque.num_local.ToString();
                detalheProd.localSigla = value.Estoque.letra_local;
                detalheProd.localRef = value.Estoque.ref_local;

                listaProd.Add(detalheProd);
            }

            dtbProdutos = new DataTable();
            dtbProdutos .Columns.Add("Produto", typeof(string));
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

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            flagNovo = true;
            limpaForm();
            listaProdutos = controle.pesquisaGeralProd();
            listaCompleta(listaProdutos);
            lblMensagem.Text = "Relação de produtos";
        }

        private void limpaForm()
        {
            qntTemp = 0;
            dgvProdutos.Enabled = true;
            txtCodigo.Enabled = true;
            txtProduto.Enabled = true;
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
                    if (controle.pesquisaProdutosNomeId(txtCodigo.Text.ToUpper()).Count > 0)
                    {
                        txtCodigo.Enabled = false;
                        txtProduto.Enabled = false;
                        lblMensagem.Text = "Produto com o código: " + txtCodigo.Text;
                        listaProdutos = controle.pesquisaProdutosNomeId(txtCodigo.Text.ToUpper());
                        listaCompleta(listaProdutos);
                    }
                    else
                    {
                        MessageBox.Show("Não existem produtos com o código \"" + txtCodigo.Text + "\" ", "Ação inválida");
                        btnLimpar_Click(sender, e);
                    }
                }
                else if (txtCodigo.Text.Equals("") && !txtProduto.Text.Equals(""))
                {
                    if (controle.pesquisaProdutosNomeId(txtProduto.Text.ToUpper()).Count > 0)
                    {
                        txtCodigo.Enabled = false;
                        txtProduto.Enabled = false;
                        lblMensagem.Text = "Relação de produtos que contém a expressão \"" + txtProduto.Text + "\" ";
                        listaProdutos = controle.pesquisaProdutosNomeId(txtProduto.Text.ToUpper());
                        listaCompleta(listaProdutos);
                    }
                    else
                    {
                        MessageBox.Show("Não existem produtos com a expressão \"" + txtProduto.Text + "\" ");
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limpaForm();
            dgvProdutos.Enabled = false;
            flagNovo = true;
            txtCodigo.Enabled = true;
            txtCusto.Enabled = true;
            txtProduto.Enabled = true;
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
            btnExcluir.Enabled = true;
            flagNovo = false;
            dgvProdutos.Enabled = false;
            txtCusto.Enabled = true;
            txtProduto.Enabled = true;
            txtCodigo.Enabled = true;
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
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            produto.status = 0;
            controle.salvaAtualiza();
            limpaForm();
            listaProdutos = controle.pesquisaGeralProd();
            listaCompleta(listaProdutos);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
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
                if (Convert.ToDouble(txtPreco.Text) < (Convert.ToDouble(txtCusto.Text) + (Convert.ToDouble(txtCusto.Text) * gerencia.lucroMinimo)))
                {
                    MessageBox.Show("O campo \"Preco\" está com uma taxa de lucro inferior ao determinado pela Administração.");
                }

                movimento = new Movimentos();
                controle.salvarMovimento(movimento);

                dgvProdutos.Enabled = true;

                //se flag novo = false, novo elemento
                if (flagNovo)
                {
                    movimento.data = DateTime.Today;
                    movimento.desc = "Aquisição de Estoque";
                    movimento.id_tipo = 12;
                    movimento.valor = Convert.ToDecimal(txtCusto.Text) * Convert.ToInt32(txtQntAtual.Text.Trim());

                    txtCusto.Text = Convert.ToDecimal(txtCusto.Text).ToString("0.00");
                    txtPreco.Text = Convert.ToDecimal(txtPreco.Text).ToString("0.00");
                    txtIcms.Text = Convert.ToDecimal(txtIcms.Text).ToString("0.00");

                    produto = new Model.Produtos();
                    controle.salvarProduto(produto);
                    produto.cod_produto = txtCodigo.Text.Trim();
                    produto.preco_compra = Convert.ToDecimal(txtCusto.Text.Trim());
                    produto.preco_venda = Convert.ToDecimal(txtPreco.Text.Trim());
                    produto.ICMS_pago = Convert.ToDecimal(txtIcms.Text.Trim());
                    produto.desc_produto = txtProduto.Text.Trim().ToUpper();
                    produto.id_fornecedor = controle.pesquisaFornecedorByNome(cmbFornecedor.SelectedValue.ToString()).id;
                    produto.id_medida = controle.pesquisaMedidaByDesc(cmbUnidade.SelectedValue.ToString()).id;
                    produto.imagem = bytes;
                    produto.status = 1;
                    controle.salvaAtualiza();

                    estoque = new Estoque();
                    controle.salvarEstoque(estoque);
                    estoque.id_produto = produto.id;
                    estoque.qnt_atual = Convert.ToInt32(txtQntAtual.Text.Trim());
                    estoque.qnt_minima = Convert.ToInt32(txtQntMinima.Text.Trim());
                    estoque.qnt_maxima = Convert.ToInt32(txtQntMaxima.Text.Trim());
                    estoque.num_local = Convert.ToInt32(txtLocalNum.Text.Trim());
                    estoque.letra_local = txtLocalSigla.Text.Trim().ToUpper();
                    estoque.ref_local = txtLocalRef.Text.Trim().ToUpper();
                    controle.salvaAtualiza();

                    limpaForm();

                    listaProdutos = controle.pesquisaGeralProd();
                    listaCompleta(listaProdutos);
                }

                //alteração de elemento existente na base de dados
                else if (!flagNovo)
                {
                    movimento.data = DateTime.Today;
                    movimento.desc = "Aquisição de Estoque";
                    movimento.id_tipo = 12;
                    movimento.valor = Convert.ToDecimal(txtCusto.Text) * (Convert.ToInt32(txtQntAtual.Text.Trim()) - qntTemp);

                    txtCusto.Text = Convert.ToDecimal(txtCusto.Text).ToString("0.00");
                    txtPreco.Text = Convert.ToDecimal(txtPreco.Text).ToString("0.00");
                    txtIcms.Text = Convert.ToDecimal(txtIcms.Text).ToString("0.00");

                    int id = produto.id;
                    produto = new Model.Produtos();
                    produto = controle.pesquisaProdutoId(id);
                    produto.cod_produto = txtCodigo.Text;
                    produto.preco_compra = Convert.ToDecimal(txtCusto.Text);
                    produto.preco_venda = Convert.ToDecimal(txtPreco.Text);
                    produto.ICMS_pago = Convert.ToDecimal(txtIcms.Text);
                    produto.desc_produto = txtProduto.Text;
                    produto.id_fornecedor = controle.pesquisaFornecedorByNome(cmbFornecedor.SelectedValue.ToString()).id;
                    produto.id_medida = controle.pesquisaMedidaByDesc(cmbUnidade.SelectedValue.ToString()).id;
                    produto.imagem = bytes;

                    estoque = new Estoque();
                    estoque = controle.pesquisaProdEstoqueId(id);
                    estoque.qnt_atual = Convert.ToInt32(txtQntAtual.Text);
                    estoque.qnt_minima = Convert.ToInt32(txtQntMinima.Text);
                    estoque.qnt_maxima = Convert.ToInt32(txtQntMaxima.Text);
                    estoque.num_local = Convert.ToInt32(txtLocalNum.Text);
                    estoque.letra_local = txtLocalSigla.Text;
                    estoque.ref_local = txtLocalRef.Text;
                    controle.salvaAtualiza();

                    limpaForm();

                    listaProdutos = controle.pesquisaGeralProd();
                    listaCompleta(listaProdutos);
                }
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
            txtCodigo.Enabled = false;
            txtProduto.Enabled = false;
            produto = controle.pesquisaProdutoCod(dgvProdutos.SelectedCells[1].Value.ToString());
            if (produto != null)
            {
                btnNovo.Enabled = false;
                btnPesquisa.Enabled = false;
                btnAlterar.Enabled = true;
                txtCodigo.Text = produto.cod_produto;
                txtCusto.Text = produto.preco_compra.ToString();
                txtProduto.Text = produto.desc_produto;
                txtIcms.Text = produto.ICMS_pago.ToString();
                txtPreco.Text = produto.preco_venda.ToString();
                txtQntMinima.Text = produto.Estoque.qnt_minima.ToString();
                txtQntMaxima.Text = produto.Estoque.qnt_maxima.ToString();
                txtQntAtual.Text = produto.Estoque.qnt_atual.ToString();
                txtLocalNum.Text = produto.Estoque.num_local.ToString();
                txtLocalSigla.Text = produto.Estoque.letra_local.ToString();
                txtLocalRef.Text = produto.Estoque.ref_local.ToString();
                cmbFornecedor.SelectedValue = controle.pesquisaFornecedorById(produto.id_fornecedor).nome;
                cmbUnidade.SelectedValue = controle.pesquisaMedidaById(produto.id_medida).medida;
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
    }
}
