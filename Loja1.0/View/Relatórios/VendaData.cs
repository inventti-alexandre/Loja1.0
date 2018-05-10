using Loja1._0.Control;
using Loja1._0.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Loja1._0
{
    public partial class VendaData : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        public static DateTime dataInicio, dataFim;
        public static DataTable dtVenda = new DataTable();
        public static List<Vendas> listaVendas = new List<Vendas>();
        printDGV impresso = new printDGV();

        Email email = new Email();
        public string erro;

        public VendaData(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();

            AcceptButton = btnPesquisar;
            btnPesquisar.Enabled = true;
            btnImprime.Enabled = false;
            txtDataFim.Enabled = true;
            txtDataFim.Enabled = true;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Relatorios form = new Relatorios(user);
            form.Show();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtDataInicio.Text.Equals("") || txtDataFim.Text.Equals(""))
            {
                MessageBox.Show("É necessário o preenchimento dos campos de data inicio e fim, corrija e tente novamente", "Ação invalida");
            }
            else if (!DateTime.TryParse(txtDataInicio.Text, out DateTime resultInicio))
            {
                MessageBox.Show("O campo Data Incio está preenchido com um valor de data invalido, corrija e tente novamente", "Ação invalida");
            }
            else if (!DateTime.TryParse(txtDataFim.Text, out DateTime resultFim))
            {
                MessageBox.Show("O campo Data Fim está preenchido com um valor de data invalido, corrija e tente novamente", "Ação invalida");
            }
            else
            {
                AcceptButton = btnImprime;
                btnPesquisar.Enabled = false;
                btnImprime.Enabled = true;
                txtDataInicio.Enabled = false;
                txtDataFim.Enabled = false;

                dataInicio = Convert.ToDateTime(txtDataInicio.Text);
                dataFim = Convert.ToDateTime(txtDataFim.Text);

                listaVendas = new List<Vendas>();
                listaVendas = controle.pesquisaVendasPeriodo(dataInicio, dataFim);

                preencheDataGrindView(listaVendas);
            }
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);

            impresso = new printDGV();
            impresso.Print_DataGridView(dgvRelatorio, "Relatório de Movimentação Financeira por Periodo");

            AcceptButton = btnPesquisar;
            btnPesquisar.Enabled = true;
            btnImprime.Enabled = false;
            txtDataInicio.Enabled = true;
            txtDataFim.Enabled = true;

        }

        private void preencheDataGrindView(List<Vendas> listaVendas)
        {
            try
            {
                DataTable dtVenda = new DataTable();
                dtVenda.Columns.Add("Data Venda", typeof(string));
                dtVenda.Columns.Add("Valor", typeof(string));
                dtVenda.Columns.Add("Cliente", typeof(string));
                dtVenda.Columns.Add("Usuário", typeof(string));
                
                foreach (Vendas value in listaVendas)
                {
                    string cliente = "";
                    if(value.Clientes != null)
                    {
                        cliente = value.Clientes.nome;
                    }
                    dtVenda.Rows.Add(value.data_Venda.ToShortDateString(), "R$" + value.valor_Venda.ToString(), cliente, value.Usuarios.nome.ToString());
                }

                dgvRelatorio.DataSource = dtVenda;

                dgvRelatorio.Columns[0].Width = 120;
                dgvRelatorio.Columns[1].Width = 150;
                dgvRelatorio.Columns[2].Width = 350;
                dgvRelatorio.Columns[3].Width = 350;
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "CompraData.cs, instrução \"btnPreencheDataGrindView\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em CompraData.cs, instrução \"btnPreencheDataGrindView\", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}