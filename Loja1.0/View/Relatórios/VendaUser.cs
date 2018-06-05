using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Loja1._0.Control;
using Loja1._0.Model;

namespace Loja1._0
{
    public partial class VendaUser : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        string erro = "";
        Email email = new Email();

        public static List<Model.Usuarios> listaUsuarios = new List<Model.Usuarios>();
        public static List<Vendas> listaVendas = new List<Vendas>();
        DataTable dtVendas = new DataTable();
        printDGV impresso = new printDGV();


        public VendaUser(Model.Usuarios user)
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

        private void btnPesquisaUser_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtBuscaUser.Text.ToUpper().Trim().Equals(""))
                {
                    MessageBox.Show("Para pesquisa de usuário insira parte do nome, ou cpf/cnpj completo", "Ação Inválida");
                }

                else
                {
                    listaUsuarios = controle.PesquisaUserNomeRelação(txtBuscaUser.Text.ToUpper().Trim());
                    cmbUser.DataSource = listaUsuarios;
                    cmbUser.ValueMember = "nome";
                    cmbUser.DisplayMember = "nome";
                    cmbUser.Focus();
                    AcceptButton = btnPesquisa;
                    CancelButton = btnCancel;
                    btnImprime.Enabled = true;

                    if (listaUsuarios.Count == 0)
                    {
                        MessageBox.Show("Não foram encontrados usuários com o termo \"" + txtBuscaUser.Text.ToUpper().Trim() + "\" em sua descrição, por favor, altere o termo e tente novamente.", "Pesquisa Inválida");
                        txtBuscaUser.Text = "";
                    }

                    else if (listaUsuarios.Count == 1)
                    {
                        btnOk.Visible = true;
                        cmbUser.Visible = true;

                        btnOk.PerformClick();
                    }

                    else
                    {
                        txtBuscaUser.Visible = false;
                        btnPesquisa.Visible = false;
                        cmbUser.Visible = true;
                        btnCancel.Visible = true;
                        btnOk.Visible = true;
                        txtBuscaUser.Text = "";
                    }
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "VendaUser.cs, em instrução \"btnPesquisaUser_Click\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em" + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = cmbUser.SelectedValue.ToString();
            txtUsuario.Visible = true;
            cmbUser.Visible = false;
            txtBuscaUser.Visible = false;
            btnOk.Visible = false;
            btnCancel.Visible = false;
            btnPesquisa.Visible = false;

            listaVendas = new List<Vendas>();
            listaVendas = controle.PesquisaVendasUser(cmbUser.SelectedValue.ToString());

            preencheDataGrindView(listaVendas);
        }

        private void preencheDataGrindView(List<Vendas> listaVendas)
        {
            try
            {
                dtVendas = new DataTable();
                dtVendas.Columns.Add("Data Venda", typeof(string));
                dtVendas.Columns.Add("Cliente", typeof(string));
                dtVendas.Columns.Add("Preço (R$)", typeof(string));
                dtVendas.Columns.Add("Comissão", typeof(string));                
                dtVendas.Columns.Add("ICMS (R$)", typeof(string));
                dtVendas.Columns.Add("Desconto (R$)", typeof(string));
                dtVendas.Columns.Add("Local Nº", typeof(string));
                dtVendas.Columns.Add("Local Sigla", typeof(string));
                dtVendas.Columns.Add("Local Referência", typeof(string));

                foreach (Vendas value in listaVendas)
                {
                    //Compras compra = new Compras();
                    //compra = controle.pesquisaProdutoCompra(value.id);

                    dtVendas.Rows.Add(value.data_Venda.ToShortDateString(),
                        value.Clientes.nome,
                        "R$" + value.valor_Venda.ToString(),
                        "R$" + value.comissao.ToString(),
                        "R$" + value.icms.ToString(),
                        "R$" + value.desconto.ToString()
                        );                        
                }

                dgvRelatorio.DataSource = dtVendas;

                dgvRelatorio.Columns[0].Width = 150;
                dgvRelatorio.Columns[1].Width = 300;
                dgvRelatorio.Columns[2].Width = 110;
                dgvRelatorio.Columns[3].Width = 110;
                dgvRelatorio.Columns[4].Width = 110;
                dgvRelatorio.Columns[5].Width = 110;
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "VendaUser.cs, na instrução \"preencheDataGrindView\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em " + erro + ", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtUsuario.Visible = true;
            txtBuscaUser.Text = "";
            txtUsuario.Visible = false;
            txtUsuario.Text = "";
            btnOk.Visible = false;
            btnCancel.Visible = false;
            btnPesquisa.Visible = true;
            btnImprime.Enabled = false;
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            impresso = new printDGV();
            impresso.Print_DataGridView(dgvRelatorio, "Relatório de Vendas do User " + cmbUser.SelectedValue.ToString());
            btnLimpar.PerformClick();
        }
    }
}
