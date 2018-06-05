using Loja1._0.Control;
using Loja1._0.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Loja1._0
{
    public partial class PagamentosRecebidos : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        public static DateTime dataInicio, dataFim;
        public static DataTable dtVenda = new DataTable();
        public static List<Pagamentos> listaPagamento = new List<Pagamentos>();
        printDGV impresso = new printDGV();

        Email email = new Email();
        public string erro;

        public PagamentosRecebidos(Model.Usuarios user)
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

                listaPagamento = new List<Pagamentos>();
                listaPagamento = controle.PesquisaPagamentoRecebidoPeriodo(dataInicio, dataFim);

                preencheDataGrindView(listaPagamento);
            }
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);

            impresso = new printDGV();
            impresso.Print_DataGridView(dgvRelatorio, "Relatório de Recebidos");

            AcceptButton = btnPesquisar;
            btnPesquisar.Enabled = true;
            btnImprime.Enabled = false;
            txtDataInicio.Enabled = true;
            txtDataFim.Enabled = true;

        }

        private void preencheDataGrindView(List<Pagamentos> listaPagamentos)
        {
            try
            {
                DataTable dtVenda = new DataTable();
                dtVenda.Columns.Add("Data Recebimento", typeof(string));
                dtVenda.Columns.Add("ID Venda", typeof(string));
                dtVenda.Columns.Add("Valor Parciais", typeof(string));
                dtVenda.Columns.Add("Valor Total", typeof(string));
                dtVenda.Columns.Add("Tipo Pagamento", typeof(string));
                dtVenda.Columns.Add("Forma Pagamento", typeof(string));
                dtVenda.Columns.Add("Status", typeof(string));

                foreach (Pagamentos value in listaPagamentos)
                {
                    dtVenda.Rows.Add(Convert.ToDateTime(value.dataPagamento).ToShortDateString(), controle.PesquisaPagVendaIdPagamento(value.id)[0].id_Venda.ToString(), "R$" + value.valorParcela.ToString(), "R$" + value.valorTotal.ToString(), value.tipoPag.ToString(), value.formaPag.ToString(), value.status.ToString());
                }

                dgvRelatorio.DataSource = dtVenda;

                dgvRelatorio.Columns[0].Width = 120;
                dgvRelatorio.Columns[1].Width = 100;
                dgvRelatorio.Columns[2].Width = 250;
                dgvRelatorio.Columns[3].Width = 250;
                dgvRelatorio.Columns[4].Width = 200;
                dgvRelatorio.Columns[5].Width = 200;
                dgvRelatorio.Columns[6].Width = 100;
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "PagamentoRecebidos.cs, instrução \"preencheDataGrindView\"";                
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}