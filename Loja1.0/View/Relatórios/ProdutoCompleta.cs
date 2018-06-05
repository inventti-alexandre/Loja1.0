using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Loja1._0.Control;
using Loja1._0.Model;

namespace Loja1._0
{
    public partial class ProdutoCompleta : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        string erro = "";
        Email email = new Email();
        
        public static List<Model.Produtos> listaProdutos = new List<Model.Produtos>();
        DataTable dtProdutos = new DataTable();
        printDGV impresso = new printDGV();


        public ProdutoCompleta(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();

            listaProdutos = new List<Model.Produtos>();
            listaProdutos = controle.PesquisaGeralProd();

            preencheDataGrindView(listaProdutos);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Relatorios form = new Relatorios(user);
            form.Show();
        }

        private void preencheDataGrindView(List<Model.Produtos> listaProdutos)
        {
            try
            {
                dtProdutos = new DataTable();
                dtProdutos.Columns.Add("Descrição", typeof(string));
                dtProdutos.Columns.Add("Fornecedor", typeof(string));
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
                        compra.Fornecedores.nome,
                        value.Estoque.qnt_atual.ToString(),
                        value.Estoque.qnt_minima.ToString(),
                        "R$" + compra.preco_compra.ToString(),
                        "R$" + compra.icms_pago.ToString(),
                        "R$" + compra.preco_venda.ToString(),
                        value.Estoque.num_local.ToString(),
                        value.Estoque.letra_local.ToString(),
                        value.Estoque.ref_local.ToString()
                        );
                }

                dgvRelatorio.DataSource = dtProdutos;

                dgvRelatorio.Columns[0].Width = 320;
                dgvRelatorio.Columns[1].Width = 220;
                dgvRelatorio.Columns[2].Width = 100;
                dgvRelatorio.Columns[3].Width = 100;
                dgvRelatorio.Columns[4].Width = 110;
                dgvRelatorio.Columns[5].Width = 110;
                dgvRelatorio.Columns[6].Width = 110;
                dgvRelatorio.Columns[7].Width = 65;
                dgvRelatorio.Columns[8].Width = 65;
                dgvRelatorio.Columns[9].Width = 230;
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "ProdutosCompleta.cs, na instrução \"preencheDataGrindView\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em " + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            impresso = new printDGV();
            impresso.Print_DataGridView(dgvRelatorio, "Relação Completa de Produtos");
        }
    }
}
