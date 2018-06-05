using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Loja1._0.Control;
using Loja1._0.Model;

namespace Loja1._0
{
    public partial class VendaProduto : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        string erro = "";
        Email email = new Email();

        public static List<Model.Produtos> listaProdutos = new List<Model.Produtos>();
        public static List<Vendas_Produtos> listaVendas = new List<Vendas_Produtos>();
        DataTable dtVendas = new DataTable();
        printDGV impresso = new printDGV();


        public VendaProduto(Model.Usuarios user)
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

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtBuscaProduto.Text.ToUpper().Trim().Equals(""))
                {
                    MessageBox.Show("Para pesquisa de fornecedor insira parte do nome, ou cpf/cnpj completo", "Ação Inválida");
                }

                else
                {
                    listaProdutos = controle.PesquisaVendasProduto(txtBuscaProduto.Text.ToUpper().Trim());
                    cmbProduto.DataSource = listaProdutos;
                    cmbProduto.ValueMember = "desc_prod";
                    cmbProduto.DisplayMember = "desc_prod";
                    cmbProduto.Focus();
                    AcceptButton = btnPesquisaProduto;
                    CancelButton = btnCancel;
                    btnImprime.Enabled = true;

                    if (listaProdutos.Count == 0)
                    {
                        MessageBox.Show("Não foram encontrados fornecedores com o termo \"" + txtBuscaProduto.Text.ToUpper().Trim() + "\" em sua descrição, por favor, altere o termo e tente novamente.", "Pesquisa Inválida");
                        txtBuscaProduto.Text = "";
                    }

                    else if (listaProdutos.Count == 1)
                    {
                        btnOk.Visible = true;
                        cmbProduto.Visible = true;

                        btnOk.PerformClick();
                    }

                    else
                    {
                        txtBuscaProduto.Visible = false;
                        btnPesquisaProduto.Visible = false;
                        cmbProduto.Visible = true;
                        btnCancel.Visible = true;
                        btnOk.Visible = true;
                        txtBuscaProduto.Text = "";
                    }
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "VendaProduto.cs, em instrução \"btnPesquisaProduto_Click\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            txtProduto.Text = cmbProduto.SelectedValue.ToString();
            txtProduto.Visible = true;
            cmbProduto.Visible = false;
            txtBuscaProduto.Visible = false;
            btnOk.Visible = false;
            btnCancel.Visible = false;
            btnPesquisaProduto.Visible = false;

            listaVendas = new List<Vendas_Produtos>();
            listaVendas = controle.PesquisaVendasProdutoNome(cmbProduto.SelectedValue.ToString());           

            preencheDataGrindView(listaVendas);
        }

        private void preencheDataGrindView(List<Vendas_Produtos> listaVendas)
        {
            
            try
            {
                dtVendas = new DataTable();
                dtVendas.Columns.Add("Data Venda", typeof(string));
                dtVendas.Columns.Add("Qnt Vendida", typeof(string));
                dtVendas.Columns.Add("Nº Pedido", typeof(string));
                dtVendas.Columns.Add("Cliente", typeof(string));
                dtVendas.Columns.Add("Usuário", typeof(string));
                dtVendas.Columns.Add("Entrega", typeof(string));

                foreach (Vendas_Produtos value in listaVendas)
                {
                    dtVendas.Rows.Add(value.Vendas.data_Venda,
                        value.quantidade.ToString(),
                        value.Vendas.id,
                        value.Vendas.Clientes.nome,
                        value.Vendas.Usuarios.nome,
                        value.entregar.ToString()
                        );
                }

                dgvRelatorio.DataSource = dtVendas;

                dgvRelatorio.Columns[0].Width = 150;
                dgvRelatorio.Columns[1].Width = 100;
                dgvRelatorio.Columns[2].Width = 100;
                dgvRelatorio.Columns[3].Width = 300;
                dgvRelatorio.Columns[4].Width = 300;
                dgvRelatorio.Columns[5].Width = 300;
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "VendaProduto.cs, na instrução \"preencheDataGrindView\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em " + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtBuscaProduto.Visible = true;
            txtBuscaProduto.Text = "";
            txtProduto.Visible = false;
            txtProduto.Text = "";
            btnOk.Visible = false;
            btnCancel.Visible = false;
            btnPesquisaProduto.Visible = true;
            btnImprime.Enabled = false;
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            impresso = new printDGV();
            impresso.Print_DataGridView(dgvRelatorio, "Relatório de Vendas do Produto " + cmbProduto.SelectedValue.ToString());
            btnLimpar.PerformClick();
        }
    }
}
