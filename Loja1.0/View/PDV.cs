using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Loja1._0.Model;
using Loja1._0.Control;
using System.IO;
using System.Drawing.Printing;
using Loja1._0.View;

namespace Loja1._0
{
    public partial class PDV : Form
    {
        public Model.Usuarios user;
        Controle controle = new Controle();
        public List<Model.Produtos> listaProdutos = new List<Model.Produtos>();
        static List<Model.Produtos> listaCompra = new List<Model.Produtos>();
        static List<int> listaCompraQnt = new List<int>();
        static Vendas venda = new Vendas();
        static Vendas_Produtos produtosPedido = new Vendas_Produtos();
        public List<Model.Clientes> listaClientes = new List<Model.Clientes>();
        static Model.Clientes cliente = new Model.Clientes();
        DataTable dtProdutos = new DataTable();
        Gerenciamento gerencia = new Gerenciamento();
        static decimal valorSub = 0;
        static bool desconto = false;
        bool repetido = false;

        public PDV(Model.Usuarios user)
        {
            this.user = user;
            gerencia = controle.pesquisaGerenciamento(1);
            InitializeComponent();            
            lblUser.Text = user.nome;
        }

        private void carregaLista(List<Model.Produtos> listaCompra, List<int> listaCompraQnt)
        {
            DataTable dtProdutos = new DataTable();
            dtProdutos.Columns.Add("produto", typeof(string));
            dtProdutos.Columns.Add("qnt", typeof(string));
            dtProdutos.Columns.Add("preço", typeof(string));
            valorSub = 0;
            for (int i = 0; i < listaCompra.Count; i++)
            {
                 dtProdutos.Rows.Add(listaCompra[i].desc_produto + "...............................................................................................................",
                    listaCompraQnt[i].ToString() + "x...................",
                    "R$" + (Convert.ToDecimal(listaCompraQnt[i]) * listaCompra[i].preco_venda).ToString());

                valorSub = valorSub + (Convert.ToDecimal(listaCompraQnt[i]) * listaCompra[i].preco_venda);
                txtSub.Text = valorSub.ToString();
            }
            for (int i = 0; i < dgvClientes.Rows.Count; i++)
            {
                dgvClientes.Rows.RemoveAt(i);
            }

            dgvClientes.DataSource = dtProdutos;

            dgvClientes.Columns[0].Width = 503;
            dgvClientes.Columns[1].Width = 50;
            dgvClientes.Columns[2].Width = 110;

            if (desconto)
            {
                txtTotal.Text = (Convert.ToDecimal(txtSub.Text) - (Convert.ToDecimal(txtSub.Text) * Convert.ToDecimal(gerencia.autoDescPerc))).ToString("0,00");
            }
            else
            {
                txtTotal.Text = txtSub.Text;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
            Inicial form = new Inicial(user);
            form.Show();
        }

        private void btnTrocaUser_Click(object sender, EventArgs e)
        {
            TrocaUser form = new TrocaUser(user, this);
            form.Show();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Equals(""))
            {
                MessageBox.Show("Para pesquisa de produto insira parte do nome/descrição", "Ação Inválida");
            }
            else
            {
                listaProdutos = controle.pesquisaProdutosValidoNome(txtCodigo.Text);
                cmbListaProdutos.DataSource = listaProdutos;
                cmbListaProdutos.ValueMember = "cod_produto";
                cmbListaProdutos.DisplayMember = "desc_produto";
                if (listaProdutos.Count == 0)
                {
                    MessageBox.Show("Não foram encontrados produtos com o termo \"" + txtCodigo.Text + "\" em sua descrição, por favor, altere o termo e tente novamente.", "Pesquisa Inválida");
                    txtCodigo.Text = "";
                }
                else if (listaProdutos.Count == 1)
                {
                    txtCodigo.Text = listaProdutos[0].cod_produto;
                    btnAdicionar_Click(sender, e);
                    txtCodigo.Text = "";
                }
                else
                {
                    lblCodigo.Text = "Resultado Busca : ";
                    txtCodigo.Visible = false;
                    btnPesquisar.Visible = false;
                    cmbListaProdutos.Visible = true;
                    btnCancelPesquisa.Visible = true;
                    btnOkPesquisa.Visible = true;
                    txtCodigo.Text = "";
                }
                cmbListaProdutos.Focus();
            }
        }

        private void btnCancelPesquisa_Click(object sender, EventArgs e)
        {
            lblCodigo.Text = "Código Produto : ";
            txtCodigo.Visible = true;
            btnPesquisar.Visible = true;
            cmbListaProdutos.Visible = false;
            btnCancelPesquisa.Visible = false;
            btnOkPesquisa.Visible = false;

        }

        private void btnOkPesquisa_Click(object sender, EventArgs e)
        {
            AcceptButton = btnAdicionar;
            txtCodigo.Focus();
            lblCodigo.Text = "Código Produto : ";
            txtCodigo.Visible = true;
            btnPesquisar.Visible = true;
            cmbListaProdutos.Visible = false;
            btnCancelPesquisa.Visible = false;
            btnOkPesquisa.Visible = false;
            txtCodigo.Text = cmbListaProdutos.SelectedValue.ToString();
            btnAdicionar_Click(sender, e);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Equals(""))
            {
                txtCodigo.Focus();
            }
            else if (controle.pesquisaProdutoCod(txtCodigo.Text) == null)
            {
                btnPesquisar_Click(sender, e);
            }
            else
            { 
                btnImprimir.Enabled = true;
                btnRemover.Enabled = true;
                btnCancelar.Enabled = true;
                btnDescontar.Enabled = true;
                repetido = false;

                for (int i = 0; i < listaCompra.Count; i++)
                {
                    if (listaCompra[i].cod_produto.Equals(txtCodigo.Text))
                    {
                        repetido = true;
                        if (txtQnt.Text.Equals(""))
                        {
                            listaCompraQnt[i]++;
                        }
                        else
                        {
                            listaCompraQnt[i] = listaCompraQnt[i] + Convert.ToInt32(txtQnt.Text);
                        }
                    }
                }
                if (!repetido)
                {
                    listaCompra.Add(controle.pesquisaProdutoCod(txtCodigo.Text));
                    if (txtQnt.Text.Equals(""))
                    {
                        listaCompraQnt.Add(1);
                    }
                    else
                    {
                        listaCompraQnt.Add(Convert.ToInt32(txtQnt.Text));
                    }
                }
                carregaLista(listaCompra, listaCompraQnt);
                carregaAdquirido(controle.pesquisaProdutoCod(txtCodigo.Text));
                txtCodigo.Text = "";
                txtQnt.Text = "";
                txtCodigo.Focus();
            }
        }

        private void carregaAdquirido(Model.Produtos produto)
        {
            if (produto.imagem == null)
            {
                pnlImagem.BackgroundImage = null;
            }
            else
            {
                pnlImagem.BackgroundImage = Image.FromStream(new MemoryStream(produto.imagem));
            }

            txtDescricao.Text = produto.desc_produto;
            txtCodAdquirido.Text = produto.cod_produto;
            txtPreco.Text = produto.preco_venda.ToString();
            txtLocalNum.Text = produto.Estoque.num_local.ToString();
            txtLocalSigla.Text = produto.Estoque.letra_local.ToString();
            txtLocalRef.Text = produto.Estoque.ref_local.ToString();
            txtFornecedor.Text = produto.Fornecedores.nome;
            txtUnidade.Text = produto.UnidMedidas.medida;
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Equals(""))
            {
                MessageBox.Show("Para a remoção de itens é necessário definir a quantidade e o código do produto por meio dos respectivos campos, por favor, tente novamente", "Ação Inválida");
            }
            else
            {
                for (int i = 0; i < listaCompra.Count; i++)
                {
                    if (listaCompra[i].cod_produto.Equals(txtCodigo.Text))
                    {
                        if (txtQnt.Text.Equals(""))
                        {
                            if (listaCompraQnt[i] == 1)
                            {
                                listaCompra.RemoveAt(i);
                                listaCompraQnt.RemoveAt(i);
                            }
                            else
                            {
                                listaCompraQnt[i] = listaCompraQnt[i] - 1;
                            }
                        }
                        else
                        {
                            if (listaCompraQnt[i] <= Convert.ToInt32(txtQnt.Text))
                            {
                                listaCompra.RemoveAt(i);
                                listaCompraQnt.RemoveAt(i);
                            }
                            else
                            {
                                listaCompraQnt[i] = listaCompraQnt[i] - Convert.ToInt32(txtQnt.Text);
                            }
                        }
                    }
                }
                carregaLista(listaCompra, listaCompraQnt);
                txtCodigo.Text = "";
                txtQnt.Text = "";
            }
        }

        private void gdvProduto_MouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtCodigo.Text = listaCompra[e.RowIndex].cod_produto;
            txtQnt.Focus();
        }

        private void btnDescontar_Click(object sender, EventArgs e)
        {
            if (desconto)
            {
                txtTotal.Text = txtSub.Text;
                desconto = false;
            }
            else
            {
                MessageBox.Show("Por favor, informe ao cliente que o desconto somente será válido para pagamento realizado à vista.", "Informação ao Cliente");
                txtTotal.Text = (Convert.ToDecimal(txtSub.Text) - (Convert.ToDecimal(txtSub.Text) * Convert.ToDecimal(gerencia.autoDescPerc))).ToString("0.00");
                desconto = true;
            }
        }

        private void txtQnt_TextChanged(object sender, EventArgs e)
        {
            txtCodigo.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            listaProdutos = new List<Model.Produtos>();
            listaCompra = new List<Model.Produtos>();
            listaCompraQnt = new List<int>();
            listaClientes = new List<Model.Clientes>();
            cliente = new Model.Clientes();
            dtProdutos = new DataTable();
            gerencia = new Gerenciamento();
            valorSub = 0;
            desconto = false;
            repetido = false;

            PDV form = new PDV(user);
            form.Show();
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            cmbCliente.Focus();
            AcceptButton = btnOkCliente;
            CancelButton = btnCancelCliente;
            if (txtBuscaCliente.Text.Equals(""))
            {
                MessageBox.Show("Para pesquisa de cliente insira parte do nome, ou cpf completo", "Ação Inválida");
            }
            else
            {
                listaClientes = controle.pesquisaClientesCompleta(txtBuscaCliente.Text);
                cmbCliente.DataSource = listaClientes;
                cmbCliente.ValueMember = "cpf";
                cmbCliente.DisplayMember = "nome";
                if (listaClientes.Count == 0)
                {
                    MessageBox.Show("Não foram encontrados clientes com o termo \"" + txtBuscaCliente.Text + "\" em sua descrição, por favor, altere o termo e tente novamente.", "Pesquisa Inválida");
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
                    lblCliente.Text = "Resultado : ";
                    txtBuscaCliente.Visible = false;
                    btnPesquisaCliente.Visible = false;
                    cmbCliente.Visible = true;
                    btnCancelCliente.Visible = true;
                    btnOkCliente.Visible = true;
                    txtBuscaCliente.Text = "";
                }
            }
        }
        private void btnOkCliente_Click(object sender, EventArgs e)
        {
            this.AcceptButton = btnAdicionar;
            this.CancelButton = null;
            cliente = controle.pesquisaClienteCpf(cmbListaProdutos.SelectedValue.ToString());
            lblCodigo.Text = "Cliente : ";
            txtCliente.Visible = true;
            btnPesquisaCliente.Visible = false;
            cmbCliente.Visible = false;
            btnCancelCliente.Visible = false;
            btnOkCliente.Visible = false;
            txtCliente.Text = cliente.nome;
        }

        private void btnCancelCliente_Click(object sender, EventArgs e)
        {
            this.AcceptButton = btnAdicionar;
            this.CancelButton = null;
            lblCodigo.Text = "Cliente : ";
            txtCliente.Visible = true;
            btnPesquisaCliente.Visible = false;
            cmbCliente.Visible = false;
            btnCancelCliente.Visible = false;
            btnOkCliente.Visible = false;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            venda = new Vendas();
            controle.salvarVenda(venda);
            venda.cnpj = "";
            venda.cpf = "";
            if (cliente.id != 0)
            {
                venda.id_Cliente = cliente.id;

                if (cliente.cpf.Length == 11)
                {
                    venda.cpf = cliente.cpf;
                    venda.cnpj = "";
                }
                else
                {
                    venda.cpf = "";
                    venda.cnpj = cliente.cpf;
                }
            }
            if (desconto)
            {
                venda.desconto = Convert.ToInt32(gerencia.autoDescPerc * 100);
            }
            venda.ICMS = 0;
            venda.id_Usuario = user.id;
            venda.valor_Venda = 0;
            venda.data_Venda = DateTime.Now;
            venda.comissao = Convert.ToDecimal(txtTotal.Text) + ((Convert.ToDecimal(txtTotal.Text) * gerencia.comissao));
            controle.salvaAtualiza();

            for (int i = 0; i < listaCompra.Count; i++)
            {
                produtosPedido = new Vendas_Produtos();
                controle.salvaProdutosVendidos(produtosPedido);
                produtosPedido.id_venda = venda.id;
                produtosPedido.id_produto = listaCompra[i].id;
                produtosPedido.num_item = i;
                produtosPedido.quantidade = listaCompraQnt[i];                
                controle.salvaAtualiza();

                venda.ICMS = venda.ICMS + Convert.ToDouble(produtosPedido.Produtos.ICMS_pago * produtosPedido.quantidade);
                venda.valor_Venda = venda.valor_Venda + Convert.ToDouble(produtosPedido.Produtos.preco_venda * produtosPedido.quantidade);
                controle.salvaAtualiza();
            }
            IniciaImpressao(sender, e);
        }

        private void IniciaImpressao(object sender, EventArgs e)
        {
            Pedido pedido = new Pedido(this, venda);
            pedido.Show();
            btnCancelar_Click(sender, e);
        }

        private void cmbListaProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AcceptButton = btnOkPesquisa;
            this.CancelButton = btnCancelPesquisa;
        }

        private void txtCliente_Click(object sender, EventArgs e)
        {
            AcceptButton = btnPesquisaCliente;
        }

        private void txtCodigo_OnFocus(object sender, EventArgs e)
        {
            AcceptButton = btnAdicionar;
        }
    }
}
