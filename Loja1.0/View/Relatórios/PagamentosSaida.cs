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
    public partial class PagamentosSaida : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        public static DateTime dataInicio, dataFim;
        public static DataTable dtMovimento = new DataTable();
        public static List<Movimentos> listaMovimentos = new List<Movimentos>();
        printDGV impresso = new printDGV();

        Email email = new Email();
        public string erro;

        public PagamentosSaida(Model.Usuarios user)
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

                listaMovimentos = new List<Movimentos>();
                listaMovimentos = controle.PesquisaMovSaidaPeriodo(dataInicio, dataFim);

                preencheDataGrindView(listaMovimentos);
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

        private void preencheDataGrindView(List<Movimentos> listaMovimentos)
        {
            try
            {
                DataTable dtMovimento = new DataTable();
                dtMovimento.Columns.Add("Data Movimento", typeof(string));
                dtMovimento.Columns.Add("Descrição", typeof(string));
                dtMovimento.Columns.Add("Tipo", typeof(string));
                dtMovimento.Columns.Add("Tipo Detalhe", typeof(string));
                dtMovimento.Columns.Add("Valor", typeof(string));

                foreach (Movimentos value in listaMovimentos)
                {
                    dtMovimento.Rows.Add(value.data.ToShortDateString(), value.desc, value.Tipos_Movimentacao.descricao, value.Tipos_Movimentacao.sub_tipo, "R$" + value.valor.ToString());
                }

                dgvRelatorio.DataSource = dtMovimento;

                dgvRelatorio.Columns[0].Width = 120;
                dgvRelatorio.Columns[1].Width = 250;
                dgvRelatorio.Columns[2].Width = 250;
                dgvRelatorio.Columns[3].Width = 250;
                dgvRelatorio.Columns[4].Width = 120;
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "PagamentoSaida.cs, em instrução \"btnPreencheDataGrindView\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
