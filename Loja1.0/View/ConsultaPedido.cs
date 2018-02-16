using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Loja1._0.Model;
using Loja1._0.Control;
using System.IO;
using Loja1._0.View;

namespace Loja1._0
{
    public partial class ConsultaPedido : Form
    {
        public Model.Usuarios user;
        Controle controle = new Controle();
        public Model.Produtos produto = new Model.Produtos();
        static List<Model.Produtos> listaCompra = new List<Model.Produtos>();
        static List<int> listaCompraQnt = new List<int>();
        static Vendas venda = new Vendas();
        static Vendas_Produtos produtosPedido = new Vendas_Produtos();
        public List<Model.Clientes> listaClientes = new List<Model.Clientes>();
        public static List<Vendas_Produtos> produtosPedidos = new List<Vendas_Produtos>();
        static Model.Clientes cliente = new Model.Clientes();
        DataTable dtProdutos = new DataTable();
        Gerenciamento gerencia = new Gerenciamento();
        static decimal valorSub = 0;
        static bool desconto = false;

        public ConsultaPedido(Model.Usuarios user)
        {
            try
            {
                this.user = user;
                gerencia = controle.PesquisaGerenciamento(1);
                InitializeComponent();
                AcceptButton = btnPesquisar;
                lblUser.Text = user.login;
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
                dtProdutos.Columns.Add("produto", typeof(string));
                dtProdutos.Columns.Add("qnt", typeof(string));
                dtProdutos.Columns.Add("preço", typeof(string));
                valorSub = 0;
                for (int i = 0; i < listaCompra.Count; i++)
                {
                    Compras compra = controle.PesquisaCompraAnterior(listaCompra[i].id);
                    dtProdutos.Rows.Add(listaCompra[i].desc_produto + " ...............................................................................................................",
                       listaCompraQnt[i].ToString() + "x ...................",
                       "R$" + (Convert.ToDecimal(listaCompraQnt[i]) * compra.preco_venda).ToString());

                    valorSub = valorSub + (Convert.ToDecimal(listaCompraQnt[i]) * compra.preco_venda);
                    txtSub.Text = valorSub.ToString();
                }
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    dgvPedido.Rows.RemoveAt(i);
                }

                dgvPedido.DataSource = dtProdutos;

                dgvPedido.Columns[0].Width = 503;
                dgvPedido.Columns[1].Width = 50;
                dgvPedido.Columns[2].Width = 110;

                if (desconto)
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSub.Text) - (Convert.ToDecimal(txtSub.Text) * Convert.ToDecimal(gerencia.autoDescPerc))).ToString("0,00");
                }
                else
                {
                    txtTotal.Text = txtSub.Text;
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
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
                    venda = new Vendas();
                    venda = controle.PesquisaVendaID(Convert.ToInt32(txtCodigo.Text));
                    txtPedido.Text = txtCodigo.Text;
                    txtCodigo.Text = "";
                    if (venda == null)
                    {
                        MessageBox.Show("O número do pedido não corresponde a uma venda válida", "Ação Inválida");
                    }

                    else
                    {
                        btnCancelar.Enabled = true;
                        btnDescontar.Enabled = true;
                        btnImprimir.Enabled = true;
                        btnAjuste.Enabled = true;

                        if (venda.Clientes != null)
                        {
                            if (venda.Clientes.id != 0)
                            {
                                txtCliente.Text = venda.Clientes.nome;
                            }
                        }
                        if (venda.desconto != 0)
                        {
                            desconto = true;
                        }
                        else
                        {
                            desconto = false;
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
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaAdquirido(Model.Produtos produto)
        {
            try
            {
                if (produto.imagem == null)
                {
                    pnlImagem.BackgroundImage = null;
                }
                else
                {
                    pnlImagem.BackgroundImage = Image.FromStream(new MemoryStream(produto.imagem));
                }

                Compras compra = controle.PesquisaCompraAnterior(produto.id);

                txtDescricao.Text = produto.desc_produto;
                txtCodAdquirido.Text = produto.cod_produto;
                txtPreco.Text = compra.preco_venda.ToString();
                txtLocalNum.Text = produto.Estoque.num_local.ToString();
                txtLocalSigla.Text = produto.Estoque.letra_local.ToString();
                txtLocalRef.Text = produto.Estoque.ref_local.ToString();
                txtFornecedor.Text = compra.Fornecedores.nome;
                txtUnidade.Text = produto.UnidMedidas.medida;
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gdvProduto_MouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                produto = controle.PesquisaProdutoCod(listaCompra[e.RowIndex].cod_produto);
                carregaAdquirido(produto);
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDescontar_Click(object sender, EventArgs e)
        {
            try
            {
                if (desconto)
                {
                    txtTotal.Text = txtSub.Text;
                    desconto = false;
                }
                else if (Convert.ToDecimal(txtTotal.Text) > gerencia.autoDescValor)
                {
                    MessageBox.Show("Por favor, informe ao cliente que o desconto somente será válido para pagamento realizado à vista.", "Informação ao Cliente");
                    txtTotal.Text = (Convert.ToDecimal(txtSub.Text) - (Convert.ToDecimal(txtSub.Text) * Convert.ToDecimal(gerencia.autoDescPerc / 100))).ToString("0.00");
                    desconto = true;
                    venda = controle.PesquisaVendaID(Convert.ToInt32(txtPedido.Text));
                    venda.desconto = gerencia.autoDescPerc;
                }
                else
                {
                    MessageBox.Show("Não é possível conceder descontos para compras abaixo de R$" + gerencia.autoDescValor.ToString("0.00"), "Ação Inválida");
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }    

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            listaCompra = new List<Model.Produtos>();
            listaCompraQnt = new List<int>();
            listaClientes = new List<Model.Clientes>();
            cliente = new Model.Clientes();
            dtProdutos = new DataTable();
            gerencia = new Gerenciamento();
            valorSub = 0;
            desconto = false;

            btnDescontar.Enabled = false;
            btnCancelar.Enabled = false;
            btnImprimir.Enabled = false;
            btnAjuste.Enabled = false;

            pnlImagem.BackgroundImage = null;
            txtDescricao.Text = "";
            txtCodAdquirido.Text = "";
            txtPreco.Text = "";
            txtLocalNum.Text = "";
            txtLocalSigla.Text = "";
            txtLocalRef.Text = "";
            txtFornecedor.Text = "";
            txtUnidade.Text = "";

            ConsultaPedido form = new ConsultaPedido(user);
            form.Show();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                venda = new Vendas();
                venda = controle.PesquisaVendaID(Convert.ToInt32(txtPedido.Text));

                if (desconto)
                {
                    venda.desconto = Convert.ToInt32(gerencia.autoDescPerc * 100);
                }

                venda.id_Usuario = user.id;

                venda.data_Venda = DateTime.Now;
                venda.comissao = (Convert.ToDecimal(txtTotal.Text) * gerencia.comissao);

                controle.SalvaAtualiza();

                IniciaImpressao(sender, e);
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IniciaImpressao(object sender, EventArgs e)
        {
            Pedido pedido = new Pedido(this, venda);
            pedido.Show();
            btnCancelar_Click(sender, e);
        }

        //SCRIPT FECHAMENTO
        #region botão fechamento
        private void btnAjuste_Click(object sender, EventArgs e)
        {
            try
            {
                venda = new Vendas();
                venda = controle.PesquisaVendaID(Convert.ToInt32(txtPedido.Text));

                if (venda == null)
                {
                    MessageBox.Show("O número do pedido não corresponde a uma venda válida", "Ação Inválida");
                }

                else if (pedidoPago(txtPedido.Text))
                {
                    MessageBox.Show("Já existe um pagamento associado ao pedido nº" + txtPedido.Text + ", por favor, verifique e tente novamente", "Ação Inválida");
                }

                else if (vendaFechada(txtPedido.Text))
                {
                    MessageBox.Show("O pedido nº" + txtPedido.Text + " já foi fechado anteriormente, por favor, verifique e tente novamente", "Ação Inválida");
                }

                else
                {
                    Fechamento pedido = new Fechamento();
                    controle.SalvarFechamento(pedido);
                    pedido.id_venda = venda.id;
                    pedido.data_fechamento = DateTime.Now;
                    controle.SalvaAtualiza();

                    produtosPedidos = new List<Vendas_Produtos>();
                    produtosPedidos = controle.PesquisaProdutosPedido(venda.id);
                    foreach (Vendas_Produtos value in produtosPedidos)
                    {
                        produto = controle.PesquisaProdutoId(Convert.ToInt32(value.id_produto));
                        produto.Estoque.qnt_atual = produto.Estoque.qnt_atual - value.quantidade;

                        Compras compra = controle.PesquisaCompraAnterior(produto.id);

                        List<Movimentos> listaMov = controle.PesquisaMovimentoTipo(12);
                        decimal valor = value.quantidade * compra.preco_compra;
                        for (int i = 0; i < listaMov.Count; i++)
                        {
                            if (valor != 0)
                            {
                                if (valor > listaMov[i].valor)
                                {
                                    valor = valor - Convert.ToDecimal(listaMov[i].valor);
                                    controle.RemoveMovimento(listaMov[i]);
                                    controle.SalvaAtualiza();
                                }
                                else
                                {
                                    listaMov[i].valor = listaMov[i].valor - valor;
                                    valor = 0;
                                    controle.SalvaAtualiza();
                                }
                            }
                        }
                        controle.SalvaAtualiza();
                    }
                    btnCancelar_Click(sender, e);
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private bool vendaFechada(string numPedido)
        {
            //verifica se existe um pagamento referente ao numero de pedido incluido, e havendo retorna false, sendo um novo pagamento devolve true
            if (controle.PesquisaFechamentoIdVenda(numPedido))
            {
                return true;
            }
            return false;
        }
        #endregion

        private bool pedidoPago(string numPedido)
        {
            //verifica se existe um pagamento referente ao numero de pedido incluido, e havendo retorna false, sendo um novo pagamento devolve true
            if (controle.PesquisaPagamentoIdVenda(numPedido))
            {
                return true;
            }
            return false;
        }

    }
}
