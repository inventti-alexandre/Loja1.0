using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Loja1._0.Control;
using Loja1._0.Model;

namespace Loja1._0
{
    public partial class VendaPagamento : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        public static DateTime dataInicio, dataFim;
        public static DataTable dtPagamentos = new DataTable();
        public static List<Pagamentos> listaPagamentos = new List<Pagamentos>();
        public static List<Pagamentos> listaSelecionada = new List<Pagamentos>();
        static string tipoPagamento = "";
        printDGV impresso = new printDGV();

        Email email = new Email();
        public string erro;

        public VendaPagamento(Model.Usuarios user)
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

                listaPagamentos = new List<Pagamentos>();
                listaPagamentos = controle.PesquisaPagamentoRecebidoPeriodo(dataInicio, dataFim);

                listaSelecionada = new List<Pagamentos>();

                foreach(Pagamentos value in listaPagamentos)
                {
                    if (rdbDinheiro.Checked)
                    {
                        if (value.formaPag.Equals("Dinheiro"))
                        {
                            listaSelecionada.Add(value);
                        }
                        tipoPagamento = value.formaPag;
                    }

                    else if (rdbDebito.Checked)
                    {
                        if (value.formaPag.Equals("Débito"))
                        {
                            listaSelecionada.Add(value);
                        }
                        tipoPagamento = value.formaPag;
                    }

                    else if (rdbPrePago.Checked)
                    {
                        if (value.formaPag.Equals("Pré-Pago"))
                        {
                            listaSelecionada.Add(value);
                        }
                        tipoPagamento = value.formaPag;
                    }

                    else if (rdbCredito.Checked)
                    {
                        if (value.formaPag.Equals("C.Crédito"))
                        {
                            listaSelecionada.Add(value);
                        }
                        tipoPagamento = value.formaPag;
                    }

                    else if (rdbCheque.Checked)
                    {
                        if (value.formaPag.Equals("Cheque"))
                        {
                            listaSelecionada.Add(value);
                        }
                        tipoPagamento = value.formaPag;
                    }
                }

                preencheDataGrindView(listaSelecionada);
            }
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);

            impresso = new printDGV();
            impresso.Print_DataGridView(dgvRelatorio, "Relatório de Vendas com " + tipoPagamento + " no Período - " + dataInicio.ToShortDateString() + " até " + dataFim.ToShortDateString());

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
                dtPagamentos = new DataTable();
                dtPagamentos.Columns.Add("Data Recebimento", typeof(string));
                dtPagamentos.Columns.Add("ID Venda", typeof(string));
                dtPagamentos.Columns.Add("Valor Parciais", typeof(string));
                dtPagamentos.Columns.Add("Valor Total", typeof(string));
                dtPagamentos.Columns.Add("Tipo Pagamento", typeof(string));
                dtPagamentos.Columns.Add("Forma Pagamento", typeof(string));
                dtPagamentos.Columns.Add("Status", typeof(string));

                foreach (Pagamentos value in listaPagamentos)
                {
                    dtPagamentos.Rows.Add(Convert.ToDateTime(value.dataPagamento).ToShortDateString(), 
                        controle.PesquisaPagVendaIdPagamento(value.id)[0].id_Venda.ToString(),
                        "R$" + value.valorParcela.ToString(),
                        "R$" + value.valorTotal.ToString(),
                        value.tipoPag.ToString(), 
                        value.formaPag.ToString(),
                        value.status.ToString()
                        );
                }

                dgvRelatorio.DataSource = dtPagamentos;

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
                erro = "VendaPagamento.cs, em instrução \"preencheDataGrindView\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
