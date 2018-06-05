using Loja1._0.Control;
using Loja1._0.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Loja1._0
{
    public partial class PagamentosPendentes : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        public static DateTime dataInicio, dataFim;
        public static DataTable dtVenda = new DataTable();
        public static List<Pagamentos> listaPagamento = new List<Pagamentos>();
        printDGV impresso = new printDGV();

        Email email = new Email();
        public string erro;

        public PagamentosPendentes(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();

            PesquisarPagamentosPendentes();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Relatorios form = new Relatorios(user);
            form.Show();
        }

        private void PesquisarPagamentosPendentes()
        {
            listaPagamento = new List<Pagamentos>();
            listaPagamento = controle.PesquisaPagamentosGeral();

            preencheDataGrindView(listaPagamento);

        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            impresso = new printDGV();
            impresso.Print_DataGridView(dgvRelatorio, "Relatório de Pagamentos Pendentes");
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

                foreach (Pagamentos value in listaPagamentos)
                {
                    if (value.status == 0)
                    {
                        dtVenda.Rows.Add(Convert.ToDateTime(value.dataPagamento).ToShortDateString(),
                            controle.PesquisaPagVendaIdPagamento(value.id)[0].id_Venda.ToString(), 
                            "R$" + value.valorParcela.ToString(), "R$" + value.valorTotal.ToString(),
                            value.tipoPag.ToString(),
                            value.formaPag.ToString()
                            );
                    }
                }

                dgvRelatorio.DataSource = dtVenda;

                dgvRelatorio.Columns[0].Width = 120;
                dgvRelatorio.Columns[1].Width = 100;
                dgvRelatorio.Columns[2].Width = 250;
                dgvRelatorio.Columns[3].Width = 250;
                dgvRelatorio.Columns[4].Width = 200;
                dgvRelatorio.Columns[5].Width = 200;
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "PagamentosPendentes.cs, instrução \"preencheDataGrindView\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em CompraData.cs, instrução \"btnPreencheDataGrindView\", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}