using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Loja1._0.Control;
using Loja1._0.Model;

namespace Loja1._0
{
    public partial class ProdutosFornecedor : Form
    {
        private Model.Usuarios user;        
        Controle controle = new Controle();
        string erro = "";
        Email email = new Email();

        public static List<Model.Fornecedores> listaFornecedores = new List<Model.Fornecedores>();
        public static List<Model.Produtos> listaProdutos = new List<Model.Produtos>();
        DataTable dtProdutos = new DataTable();
        printDGV impresso = new printDGV();


        public ProdutosFornecedor(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Relatorios form = new Relatorios(user);
            form.Show();
        }

        private void btnPesquisaFornecedor_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtBuscaFornecedor.Text.ToUpper().Trim().Equals(""))
                {
                    MessageBox.Show("Para pesquisa de fornecedor insira parte do nome, ou cpf/cnpj completo", "Ação Inválida");
                }

                else
                {
                    listaFornecedores = controle.PesquisaFornecedores(txtBuscaFornecedor.Text.ToUpper().Trim());
                    cmbFornecedor.DataSource = listaFornecedores;
                    cmbFornecedor.ValueMember = "nome";
                    cmbFornecedor.DisplayMember = "nome";
                    cmbFornecedor.Focus();
                    AcceptButton = btnPesquisaFornecedor;
                    CancelButton = btnCancel;
                    btnImprime.Enabled = true;

                    if (listaFornecedores.Count == 0)
                    {
                        MessageBox.Show("Não foram encontrados fornecedores com o termo \"" + txtBuscaFornecedor.Text.ToUpper().Trim() + "\" em sua descrição, por favor, altere o termo e tente novamente.", "Pesquisa Inválida");
                        txtBuscaFornecedor.Text = "";
                    }
                    
                    else  if (listaFornecedores.Count == 1)
                    {
                        btnOk.Visible = true;
                        cmbFornecedor.Visible = true;

                        btnOk.PerformClick();
                    }

                    else
                    {
                        txtBuscaFornecedor.Visible = false;
                        btnPesquisaFornecedor.Visible = false;
                        cmbFornecedor.Visible = true;
                        btnCancel.Visible = true;
                        btnOk.Visible = true;
                        txtBuscaFornecedor.Text = "";
                    }
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "ProdutosFornecedor.cs, em instrução \"btnPreencheDataGrindView\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            txtFornecedor.Text = cmbFornecedor.SelectedValue.ToString();
            txtFornecedor.Visible = true;
            cmbFornecedor.Visible = false;
            txtBuscaFornecedor.Visible = false;
            btnOk.Visible = false;
            btnCancel.Visible = false;
            btnPesquisaFornecedor.Visible = false;

            listaProdutos = new List<Model.Produtos>();
            listaProdutos = controle.PesquisaProdutosFornecedor(cmbFornecedor.SelectedValue.ToString());

            preencheDataGrindView(listaProdutos);
        }

        private void preencheDataGrindView(List<Model.Produtos> listaProdutos)
        {
            try
            {
                dtProdutos = new DataTable();
                dtProdutos.Columns.Add("Descrição", typeof(string));
                dtProdutos.Columns.Add("Qnt Atual", typeof(string));
                dtProdutos.Columns.Add("Qnt Min.", typeof(string));
                dtProdutos.Columns.Add("Custo (R$)", typeof(string));
                dtProdutos.Columns.Add("ICMS (R$)", typeof(string));
                dtProdutos.Columns.Add("Preço (R$)", typeof(string));                
                dtProdutos.Columns.Add("Local Nº", typeof(string));
                dtProdutos.Columns.Add("Local Sigla", typeof(string));
                dtProdutos.Columns.Add("Local Referência", typeof(string));               

                foreach (Model.Produtos value in listaProdutos)
                {
                    Compras compra = new Compras();
                    compra = controle.pesquisaProdutoCompra(value.id);

                    dtProdutos.Rows.Add(value.desc_produto, 
                        value.Estoque.qnt_atual.ToString(), 
                        value.Estoque.qnt_minima.ToString(), 
                        "R$" + compra.preco_compra.ToString(), 
                        "R$" + compra.icms_pago.ToString(), 
                        "R$" + compra.preco_venda.ToString(), 
                        value.Estoque.num_local.ToString(), 
                        value.Estoque.letra_local.ToString(),
                        value.Estoque.ref_local.ToString());
                }

                dgvRelatorio.DataSource = dtProdutos;

                dgvRelatorio.Columns[0].Width = 320;
                dgvRelatorio.Columns[1].Width = 100;
                dgvRelatorio.Columns[2].Width = 100;
                dgvRelatorio.Columns[3].Width = 110;
                dgvRelatorio.Columns[4].Width = 110;
                dgvRelatorio.Columns[5].Width = 110;
                dgvRelatorio.Columns[6].Width = 65;
                dgvRelatorio.Columns[7].Width = 65;
                dgvRelatorio.Columns[8].Width = 230;
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "ProdutosFornecedor.cs, na instrução \"preencheDataGrindView\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em "+ erro +", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtBuscaFornecedor.Visible = true;
            txtBuscaFornecedor.Text = "";
            txtFornecedor.Visible = false;
            txtFornecedor.Text = "";
            btnOk.Visible = false;
            btnCancel.Visible = false;
            btnPesquisaFornecedor.Visible = true;
            btnImprime.Enabled = false;
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            impresso = new printDGV();
            impresso.Print_DataGridView(dgvRelatorio, "Relatório de Produtos - Fornecedor: " + cmbFornecedor.SelectedValue.ToString());
            btnLimpar.PerformClick();
        }
    }
}
