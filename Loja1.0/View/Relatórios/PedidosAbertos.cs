using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Loja1._0.Control;
using Loja1._0.Model;

namespace Loja1._0
{
    public partial class PedidosAbertos : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        public static DateTime dataInicio, dataFim;
        public static DataTable dtPedidos = new DataTable();
        public static List<Vendas> listaVendas = new List<Vendas>();
        public static List<Vendas> listaPedidos = new List<Vendas>();
        printDGV impresso = new printDGV();

        Email email = new Email();
        public string erro;

        public PedidosAbertos(Model.Usuarios user)
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

                listaPedidos = new List<Vendas>();

                foreach(Vendas value in listaVendas)
                {
                    if (!controle.PesquisaPagamentoIdVenda(value.id.ToString()))
                    {
                        listaPedidos.Add(value);
                    }
                }

                preencheDataGrindView(listaPedidos);
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

        private void preencheDataGrindView(List<Vendas> listaPedidos)
        {
            try
            {
                dtPedidos = new DataTable();
                dtPedidos.Columns.Add("Data Pedido", typeof(string));
                dtPedidos.Columns.Add("Pedido Nº", typeof(string));
                dtPedidos.Columns.Add("Cliente", typeof(string));
                dtPedidos.Columns.Add("Impostos", typeof(string));
                dtPedidos.Columns.Add("Descontos", typeof(string));
                dtPedidos.Columns.Add("Valor Total", typeof(string));

                foreach (Vendas value in listaPedidos)
                {
                    dtPedidos.Rows.Add(value.data_Venda.ToShortDateString(), value.id.ToString(), value.Clientes.nome, value.icms.ToString() + " %", value.desconto.ToString() + " %", "R$ " + value.valor_Venda.ToString());
                }

                dgvRelatorio.DataSource = dtPedidos;

                dgvRelatorio.Columns[0].Width = 120;
                dgvRelatorio.Columns[1].Width = 120;
                dgvRelatorio.Columns[2].Width = 350;
                dgvRelatorio.Columns[3].Width = 120;
                dgvRelatorio.Columns[4].Width = 120;
                dgvRelatorio.Columns[5].Width = 120;
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "PedidoAberto.cs, em instrução \"btnPreencheDataGrindView\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro  +", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
