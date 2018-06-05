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
    public partial class Entrega : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();

        public List<Model.Clientes> listaClientes = new List<Model.Clientes>();
        public static Model.Clientes cliente = new Model.Clientes();

        public static Vendas venda = new Vendas();
        static Vendas_Produtos produtosPedido = new Vendas_Produtos();
        public static List<Vendas_Produtos> produtosPedidos = new List<Vendas_Produtos>();
        public Model.Produtos produto = new Model.Produtos();
        static List<Model.Produtos> listaCompra = new List<Model.Produtos>();
        static List<int> listaCompraQnt = new List<int>();

        Email email = new Email();
        public string erro;


        public Entrega(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();
           
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inicial form = new Inicial(user);
            form.Show();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigo.Text.Equals(""))
                {
                    MessageBox.Show("Para pesquisa de pedido insira o número deste", "Ação Inválida");
                }
                else
                {
                    txtBuscaCliente.Enabled = true;
                    btnPesquisaCliente.Enabled = true;

                    venda = new Vendas();
                    venda = controle.PesquisaVendaID(Convert.ToInt32(txtCodigo.Text));
                    
                    if (venda == null)
                    {
                        MessageBox.Show("O número do pedido não corresponde a uma venda válida", "Ação Inválida");
                    }

                    else
                    {                        
                        btnImprimir.Enabled = true;

                        if (venda.Clientes != null)
                        {
                            if (venda.Clientes.id != 0)
                            {
                                txtCliente.Text = venda.Clientes.nome;
                                btnPesquisaCliente.PerformClick();
                                btnOkCliente.PerformClick();
                            }
                        }

                        listaCompra = new List<Model.Produtos>();
                        listaCompraQnt = new List<int>();
                        produtosPedidos = new List<Vendas_Produtos>();

                        produtosPedidos = controle.PesquisaProdutosPedido(venda.id);
                        foreach (Vendas_Produtos value in produtosPedidos)
                        {
                            listaCompra.Add(controle.PesquisaProdutoId(Convert.ToInt32(value.id_produto)));
                            listaCompraQnt.Add(value.quantidade);
                        }
                                               
                        carregaLista(listaCompra, listaCompraQnt);

                        List<CtrlEntrega> listaEntrega = new List<CtrlEntrega>();
                        listaEntrega = controle.PesquisaEntregaIdVenda(Convert.ToInt32(txtCodigo.Text));

                        if (listaEntrega.Count > 0)
                        {
                            foreach (DataGridViewRow row in dgvPedido.Rows)
                            {
                                string[] prodDesc = new string[2];
                                prodDesc = row.Cells[0].Value.ToString().Split('.');

                                foreach (CtrlEntrega value in listaEntrega)
                                {
                                    string desc = controle.PesquisaProdutoId(value.id_Produto).desc_produto;
                                    if (prodDesc[0].Trim().Equals(desc))
                                    {
                                        row.Selected = true;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaLista(List<Model.Produtos> listaCompra, List<int> listaCompraQnt)
        {
            try
            {
                DataTable dtProdutos = new DataTable();
                //dtProdutos.Columns.Add("Entregar", typeof(CheckBox));
                dtProdutos.Columns.Add("produto", typeof(string));
                dtProdutos.Columns.Add("qnt", typeof(string));
                dtProdutos.Columns.Add("preço", typeof(string));

                for (int i = 0; i < listaCompra.Count; i++)
                {
                    //CheckBox entrega = new CheckBox();
                    Compras compra = controle.PesquisaCompraAnterior(listaCompra[i].id);
                    dtProdutos.Rows.Add(/*entrega,*/ listaCompra[i].desc_produto + " ...............................................................................................................",
                       listaCompraQnt[i].ToString() + "x ...................",
                       "R$" + (Convert.ToDecimal(listaCompraQnt[i]) * compra.preco_venda).ToString());
                }

                dgvPedido.DataSource = dtProdutos;
                
                dgvPedido.Columns[0].Width = 503;
                dgvPedido.Columns[1].Width = 50;
                dgvPedido.Columns[2].Width = 110;                

            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            try
            {
                cmbCliente.Focus();
                AcceptButton = btnOkCliente;
                CancelButton = btnCancelCliente;
                if (txtBuscaCliente.Text.ToUpper().Trim().Equals(""))
                {
                    MessageBox.Show("Para pesquisa de cliente insira parte do nome, ou cpf completo", "Ação Inválida");
                }
                else
                {
                    listaClientes = controle.PesquisaClientesCompleta(txtBuscaCliente.Text.ToUpper().Trim());
                    cmbCliente.DataSource = listaClientes;
                    cmbCliente.ValueMember = "cpf";
                    cmbCliente.DisplayMember = "nome";
                    if (listaClientes.Count == 0)
                    {
                        MessageBox.Show("Não foram encontrados clientes com o termo \"" + txtBuscaCliente.Text.ToUpper().Trim() + "\" em sua descrição, por favor, altere o termo e tente novamente.", "Pesquisa Inválida");
                        txtBuscaCliente.Text = "";
                    }
                    else if (listaClientes.Count == 1)
                    {
                        cliente = listaClientes[0];
                        txtCliente.Text = cliente.nome;
                        txtCliente.Visible = true;
                        txtBuscaCliente.Visible = false;
                        btnCancelCliente.Visible = false;
                        btnOkCliente.Visible = false;
                        btnPesquisaCliente.Visible = false;
                        cmbCliente.Visible = false;
                    }
                    else
                    {
                        //lblCliente.Text = "Resultado : ";
                        txtBuscaCliente.Visible = false;
                        btnPesquisaCliente.Visible = false;
                        cmbCliente.Visible = true;
                        btnCancelCliente.Visible = true;
                        btnOkCliente.Visible = true;
                        txtBuscaCliente.Text = "";
                    }
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "PDV.cs, instrução \"btnPesquisaCliente_Click\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em PDV.cs, instrução \"btnPesquisaCliente_Click\", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPedido.Rows)
            {
                if (row.Selected)
                {
                    string[] prodDesc = row.Cells[0].Value.ToString().Split('.');

                    CtrlEntrega entrega = new CtrlEntrega();
                    controle.SalvaEntrega(entrega);
                    if(cliente.id == 0)
                    {
                        entrega.id_Cliente = null;

                    }
                    else
                    {
                        entrega.id_Cliente = cliente.id;
                    }
                    entrega.id_Venda = venda.id;
                    entrega.id_Produto = controle.PesquisaProdutoNome(prodDesc[0].Trim()).id;
                    entrega.DataVenda = DateTime.Now;
                    entrega.EndEntrega = txtEndereco.Text + "," + txtNum.Text + "," + txtBairro.Text;
                    controle.SalvaAtualiza();
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {

        }

        private void btnOkCliente_Click(object sender, EventArgs e)
        {
            try
            {
                cliente = controle.PesquisaClienteCpf(cmbCliente.SelectedValue.ToString());
                lblCodigo.Text = "Cliente : ";
                txtCliente.Visible = true;
                btnPesquisaCliente.Visible = false;
                cmbCliente.Visible = false;
                btnCancelCliente.Visible = false;
                btnOkCliente.Visible = false;
                txtCliente.Text = cliente.nome;
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "PDV.cs, instrução \"btnOkCliente_Click\"";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em PDV.cs, instrução \"btnOkCliente_Click\", por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelCliente_Click(object sender, EventArgs e)
        {
            lblCodigo.Text = "Cliente : ";
            txtCliente.Visible = true;
            btnPesquisaCliente.Visible = false;
            cmbCliente.Visible = false;
            btnCancelCliente.Visible = false;
            btnOkCliente.Visible = false;
        }
    }
}