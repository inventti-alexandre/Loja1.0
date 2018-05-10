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
    public partial class ComprasData : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        public static DateTime dataInicio, dataFim;
        public static DataTable dtCompra = new DataTable();
        public static List<Compras> listaCompras = new List<Compras>();
        printDGV impresso = new printDGV();

        Email email = new Email();
        public string erro;

        public ComprasData(Model.Usuarios user)
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

                listaCompras = new List<Compras>();
                listaCompras = controle.PesquisaComprasPeriodo(dataInicio, dataFim);

                preencheDataGrindView(listaCompras);
            }
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);

            impresso = new printDGV();
            impresso.Print_DataGridView(dgvRelatorio, "Relatório de Compras por Periodo");

            //preencheDataGrindView(listaCompras);

            AcceptButton = btnPesquisar;
            btnPesquisar.Enabled = true;
            btnImprime.Enabled = false;
            txtDataInicio.Enabled = true;
            txtDataFim.Enabled = true;
            //txtDataInicio.Text = "";
            //txtDataFim.Text = "";

            
            /*
            CompraDataImpresso impresso = new CompraDataImpresso(listaCompras);
            impresso.Show();
            */    
    }

        private void preencheDataGrindView(List<Compras> listaCompras)
        {
            try
            {
                DataTable dtCompra = new DataTable();
                dtCompra.Columns.Add("Data Compra", typeof(string));
                dtCompra.Columns.Add("Descrição", typeof(string));
                dtCompra.Columns.Add("Fornecedor", typeof(string));
                dtCompra.Columns.Add("Qnt Adquirida", typeof(string));
                dtCompra.Columns.Add("Custo (R$)", typeof(string));
                dtCompra.Columns.Add("ICMS (R$)", typeof(string));
                dtCompra.Columns.Add("Preço (R$)", typeof(string));                
                dtCompra.Columns.Add("Qnt Atual", typeof(string));
                dtCompra.Columns.Add("Qnt Min.", typeof(string));
                dtCompra.Columns.Add("Local Nº", typeof(string));
                dtCompra.Columns.Add("Local Sigla", typeof(string));
                dtCompra.Columns.Add("Local Referência", typeof(string));

                foreach (Compras value in listaCompras)
                {
                    dtCompra.Rows.Add(value.dt_compra.ToShortDateString(), value.Produtos.desc_produto, value.Fornecedores.nome, value.qnt_compra.ToString(), "R$" + value.preco_compra.ToString(), "R$" + value.icms_pago.ToString(), "R$" + value.preco_venda.ToString(), value.Produtos.Estoque.qnt_atual.ToString(), value.Produtos.Estoque.qnt_minima.ToString(), value.Produtos.Estoque.num_local.ToString(), value.Produtos.Estoque.letra_local.ToString(), value.Produtos.Estoque.ref_local.ToString());
                }

                dgvRelatorio.DataSource = dtCompra;

                dgvRelatorio.Columns[0].Width = 120;
                dgvRelatorio.Columns[1].Width = 250;
                dgvRelatorio.Columns[2].Width = 150;
                dgvRelatorio.Columns[3].Width = 110;
                dgvRelatorio.Columns[4].Width = 150;
                dgvRelatorio.Columns[5].Width = 150;
                dgvRelatorio.Columns[6].Width = 150;
                dgvRelatorio.Columns[7].Width = 65;
                dgvRelatorio.Columns[8].Width = 65;
                dgvRelatorio.Columns[9].Width = 60;
                dgvRelatorio.Columns[10].Width = 60;
                dgvRelatorio.Columns[11].Width = 250;
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
