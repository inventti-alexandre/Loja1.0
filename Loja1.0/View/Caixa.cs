using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Loja1._0.Model;
using Loja1._0.Control;
using Bematech.Fiscal.ECF;
using FiscalPrinterBematech;
using Loja1._0.View;

namespace Loja1._0
{
    public partial class Caixa : Form
    {
        //função para chamada do objeto bematech flash builder
        //static ImpressoraFiscal BematechFiscal = ImpressoraFiscal.Construir();
        private Controle controle = new Controle();
        public Model.Usuarios user;
        public Valida validaCpf = new Valida();
        static List<Vendas> listaVendas = new List<Vendas>();
        static List<Vendas_Produtos> listaProdutosVenda = new List<Vendas_Produtos>();
        public Gerenciamento gerencia = new Gerenciamento();
        static Vendas venda = new Vendas();
        static Model.Clientes cliente = new Model.Clientes();
        public TextBox txtChequePrimeiro = new TextBox();
        public TextBox txtChequeUltimo = new TextBox();
        public static string chequePrim;
        public static string chequeUlt;
        static decimal valorTotal = 0;
        static decimal custoAux = 0;
        static decimal valorParcelaCred = 0;
        static decimal valorParcelaCheq = 0;
        static int desconto = 0;
        public static bool vista = true;
        public static bool entrada = false;
        public static decimal valorDesc = 0.00M;
        public static decimal valorAcres = 0.00M;
        public static decimal valorPago = 0.00M;
        public static bool ultimoExcluido = false;
        static bool houvePag = false;
        public static string AcrescimoDesconto = "";
        public static string ValorAcrescimoDesconto = "";
        static CupomFiscal PagDinheiro = new CupomFiscal();
        static CupomFiscal PagDebito = new CupomFiscal();
        static CupomFiscal PagPrePago = new CupomFiscal();
        static CupomFiscal PagCredito = new CupomFiscal();
        static CupomFiscal PagCheque = new CupomFiscal();
        static double clienteCreditos = 0;
        public static List<int> listaNumPedidos = new List<int>();
        public static bool pedidosInclusos = false;

        public Caixa(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();

            lblUser.Text = user.nome;
            txtPedidoNum1.Focus();
            AcceptButton = btnAdicionar1;
            txtRecebido.Text = "0,00";

            try
            {
                gerencia = controle.pesquisaGerenciamento(1);
            }
            catch
            {
                MessageBox.Show("Erro na consulta aos dados gerenciais, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            btnLimpar_Click(sender, e);
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        private void btnTrocaUser_Click(object sender, EventArgs e)
        {
            TrocaUserCx form = new TrocaUserCx(user, this);
            form.Show();
        }

        private void btnAdicionar1_Click(object sender, EventArgs e)
        {
            try
            {
                houvePag = false;
                venda = controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum1.Text));

                if (txtPedidoNum1.Text.Equals(""))
                {
                    MessageBox.Show("É necessário preencher o número do pedido a ser adicionado para faturamento", "Ação Inválida");
                }
                else if (controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum1.Text)) == null)
                {
                    MessageBox.Show("Os dados inseridos não correspondem a nenhum pedido de compra, por favor, verifique e tente novamente.", "Ação Inválida");
                }
                //SCRIPT FECHAMENTO parcial
                //validação da existencia de pagamento para o pedido inserido
                else if (pedidoPago(txtPedidoNum1.Text) || vendaFechada(txtPedidoNum1.Text))
                {
                    MessageBox.Show("Já existe um pagamento associado ao pedido nº" + txtPedidoNum1.Text + ", por favor, verifique e tente novamente", "Ação Inválida");
                }
                else
                {

                    if (venda.Clientes != null && cliente.cpf == null)
                    {
                        btnAtribuirCliente.Enabled = false;
                        cliente = venda.Clientes;
                        txtCliente.Text = cliente.nome;
                        txtCpf.Text = cliente.cpf;
                        txtCreditosCliente.Text = Convert.ToDecimal(cliente.creditos).ToString("0.00");
                        if (txtCreditosCliente.Text.Equals(""))
                        {
                            txtCreditosCliente.Text = "0,00";
                        }
                    }

                    if (venda.Clientes != null && cliente.cpf != null && rdbNPsim.Checked)
                    {
                        //cnpj ou cpf do cliente
                        BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_AbreCupom(txtCpf.Text));
                        adicionaPrimeiro();
                    }

                    else if (rdbNPsim.Checked && validaCpf.validaTipoCpfCnpj(txtCpf.Text))
                    {
                        //cnpj ou cpf do cliente
                        BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_AbreCupom(txtCpf.Text));
                        adicionaPrimeiro();
                    }

                    else if (confirmaAssociacao(sender, e))
                    {
                        BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_AbreCupom(""));
                        adicionaPrimeiro();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void adicionaPrimeiro()
        {
            pnlCliente.Enabled = false;            

            listaVendas.Add(venda);

            if (venda.desconto != 0)
            {
                valorDesc = valorDesc + Convert.ToDecimal(venda.valor_Venda) * (Convert.ToDecimal(venda.desconto) / 100);
            }
            btnDesconto.Enabled = true;
            btnLimpar.Enabled = true;

            pnlPagamento.Enabled = true;
            btnAdicionar2.Enabled = true;
            txtPedidoNum2.Enabled = true;
            btnAdicionar1.Enabled = false;
            txtPedidoNum1.Enabled = false;

            acrescentaPedido();
            AcceptButton = btnAdicionar2;
            txtPedidoNum2.Focus();

            listaNumPedidos.Add(Convert.ToInt32(txtPedidoNum1.Text));
  
        }

        private void btnAdicionar2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPedidoNum2.Text.Equals(""))
                {
                    MessageBox.Show("É necessário preencher o número do pedido a ser adicionado para faturamento", "Ação Inválida");
                }
                else if (controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum2.Text)) == null)
                {
                    MessageBox.Show("O número inserido não corresponde a nenhum pedido de compra, por favor, verifique e tente novamente.", "Ação Inválida");
                }
                //SCRIPT FECHAMENTO parcial
                else if (pedidoPago(txtPedidoNum2.Text) || vendaFechada(txtPedidoNum2.Text))
                {
                    MessageBox.Show("Já existe um pagamento associado ao pedido nº" + txtPedidoNum2.Text + ", por favor, verifique e tente novamente", "Ação Inválida");
                }
                else
                {
                    venda = controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum2.Text));
                    bool existe = false;

                    foreach (Model.Vendas value in listaVendas)
                    {
                        if (venda.id == value.id)
                        {
                            existe = true;
                        }
                    }

                    if (!existe)
                    {
                        if (venda.desconto != 0)
                        {
                            valorDesc = valorDesc + Convert.ToDecimal(venda.valor_Venda) * (Convert.ToDecimal(venda.desconto) / 100);
                        }
                        listaVendas.Add(venda);
                        btnAdicionar3.Enabled = true;
                        txtPedidoNum3.Enabled = true;
                        btnAdicionar2.Enabled = false;
                        txtPedidoNum2.Enabled = false;
                        if (venda.Clientes != null && cliente.cpf == null)
                        {
                            btnAtribuirCliente.Enabled = false;
                            cliente = venda.Clientes;
                            txtCliente.Text = cliente.nome;
                            txtCpf.Text = cliente.cpf;
                            txtCreditosCliente.Text = Convert.ToDecimal(cliente.creditos).ToString("0.00");
                            if (txtCreditosCliente.Text.Equals(""))
                            {
                                txtCreditosCliente.Text = "0,00";
                            }
                        }
                        acrescentaPedido();
                        AcceptButton = btnAdicionar3;
                        txtPedidoNum3.Focus();

                        listaNumPedidos.Add(Convert.ToInt32(txtPedidoNum2.Text));

                    }
                    else
                    {
                        MessageBox.Show("O número inserido já havia sido inserido, por favor, verifique e tente novamente.", "Ação Inválida");
                    }

                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdicionar3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPedidoNum3.Text.Equals(""))
                {
                    MessageBox.Show("É necessário preencher o número do pedido a ser adicionado para faturamento", "Ação Inválida");
                }
                else if (controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum3.Text)) == null)
                {
                    MessageBox.Show("O inserido não corresponde a nenhum pedido de compra, por favor, verifique e tente novamente.", "Ação Inválida");
                }
                //SCRIPT FECHAMENTO parcial
                else if (pedidoPago(txtPedidoNum3.Text) || vendaFechada(txtPedidoNum3.Text))
                {
                    MessageBox.Show("Já existe um pagamento associado ao pedido nº" + txtPedidoNum3.Text + ", por favor, verifique e tente novamente", "Ação Inválida");
                }
                else
                {
                    venda = controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum3.Text));
                    bool existe = false;

                    foreach (Model.Vendas value in listaVendas)
                    {
                        if (venda.id == value.id)
                        {
                            existe = true;
                        }
                    }

                    if (!existe)
                    {
                        if (venda.desconto != 0)
                        {
                            valorDesc = valorDesc + Convert.ToDecimal(venda.valor_Venda) * (Convert.ToDecimal(venda.desconto) / 100);
                        }
                        listaVendas.Add(venda);
                        btnAdicionar4.Enabled = true;
                        txtPedidoNum4.Enabled = true;
                        btnAdicionar3.Enabled = false;
                        txtPedidoNum3.Enabled = false;
                        if (venda.Clientes != null && cliente.cpf == null)
                        {
                            btnAtribuirCliente.Enabled = false;
                            cliente = venda.Clientes;
                            txtCliente.Text = cliente.nome;
                            txtCpf.Text = cliente.cpf;
                            txtCreditosCliente.Text = Convert.ToDecimal(cliente.creditos).ToString("0.00");
                            if (txtCreditosCliente.Text.Equals(""))
                            {
                                txtCreditosCliente.Text = "0,00";
                            }
                        }
                        acrescentaPedido();
                        AcceptButton = btnAdicionar4;
                        txtPedidoNum4.Focus();

                        listaNumPedidos.Add(Convert.ToInt32(txtPedidoNum3.Text));

                    }
                    else
                    {
                        MessageBox.Show("O número inserido já havia sido inserido, por favor, verifique e tente novamente.", "Ação Inválida");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdicionar4_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPedidoNum4.Text.Equals(""))
                {
                    MessageBox.Show("É necessário preencher o número do pedido a ser adicionado para faturamento", "Ação Inválida");
                }
                else if (controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum4.Text)) == null)
                {
                    MessageBox.Show("O inserido não corresponde a nenhum pedido de compra, por favor, verifique e tente novamente.", "Ação Inválida");
                }
                //SCRIPT FECHAMENTO parcial
                else if (pedidoPago(txtPedidoNum4.Text) || vendaFechada(txtPedidoNum4.Text))
                {
                    MessageBox.Show("Já existe um pagamento associado ao pedido nº" + txtPedidoNum4.Text + ", por favor, verifique e tente novamente", "Ação Inválida");
                }
                else
                {
                    venda = controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum4.Text));
                    bool existe = false;

                    foreach (Model.Vendas value in listaVendas)
                    {
                        if (venda.id == value.id)
                        {
                            existe = true;
                        }
                    }

                    if (!existe)
                    {
                        if (venda.desconto != 0)
                        {
                            valorDesc = valorDesc + Convert.ToDecimal(venda.valor_Venda) * (Convert.ToDecimal(venda.desconto) / 100);
                        }
                        listaVendas.Add(venda);
                        btnAdicionar5.Enabled = true;
                        txtPedidoNum5.Enabled = true;
                        btnAdicionar4.Enabled = false;
                        txtPedidoNum4.Enabled = false;
                        if (venda.Clientes != null && cliente.cpf == null)
                        {
                            btnAtribuirCliente.Enabled = false;
                            cliente = venda.Clientes;
                            txtCliente.Text = cliente.nome;
                            txtCpf.Text = cliente.cpf;
                            txtCredito.Text = cliente.creditos.ToString();
                            txtCreditosCliente.Text = Convert.ToDecimal(cliente.creditos).ToString("0.00");
                            if (txtCreditosCliente.Text.Equals(""))
                            {
                                txtCreditosCliente.Text = "0,00";
                            }
                        }
                        acrescentaPedido();
                        AcceptButton = btnAdicionar5;
                        txtPedidoNum5.Focus();

                        listaNumPedidos.Add(Convert.ToInt32(txtPedidoNum4.Text));

                    }
                    else
                    {
                        MessageBox.Show("O número inserido já havia sido inserido, por favor, verifique e tente novamente.", "Ação Inválida");
                    }
                }                            
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void btnAdicionar5_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPedidoNum5.Text.Equals(""))
                {
                    MessageBox.Show("É necessário preencher o número do pedido a ser adicionado para faturamento", "Ação Inválida");
                }
                else if (controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum5.Text)) == null)
                {
                    MessageBox.Show("O inserido não corresponde a nenhum pedido de compra, por favor, verifique e tente novamente.", "Ação Inválida");
                }
                //SCRIPT FECHAMENTO parcial
                else if (pedidoPago(txtPedidoNum5.Text) || vendaFechada(txtPedidoNum5.Text))
                {
                    MessageBox.Show("Já existe um pagamento associado ao pedido nº" + txtPedidoNum5.Text + ", por favor, verifique e tente novamente", "Ação Inválida");
                }
                else
                {
                    venda = controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum5.Text));
                    bool existe = false;

                    foreach (Model.Vendas value in listaVendas)
                    {
                        if (venda.id == value.id)
                        {
                            existe = true;
                        }
                    }

                    if (!existe)
                    {
                        if (venda.desconto != 0)
                        {
                            valorDesc = valorDesc + Convert.ToDecimal(venda.valor_Venda) * (Convert.ToDecimal(venda.desconto) / 100);
                        }
                        listaVendas.Add(venda);
                        btnAdicionar5.Enabled = false;
                        txtPedidoNum5.Enabled = false;
                        if (venda.Clientes != null && cliente.cpf == null)
                        {
                            btnAtribuirCliente.Enabled = false;
                            cliente = venda.Clientes;
                            txtCliente.Text = cliente.nome;
                            txtCpf.Text = cliente.cpf;
                            txtCreditosCliente.Text = Convert.ToDecimal(cliente.creditos).ToString("0.00");
                            if (txtCreditosCliente.Text.Equals(""))
                            {
                                txtCreditosCliente.Text = "0,00";
                            }
                        }
                        acrescentaPedido();

                        listaNumPedidos.Add(Convert.ToInt32(txtPedidoNum5.Text));

                        MessageBox.Show("Pedido Adicionado com sucesso. Para a cobrança de mais de 5 pedidos é necessário realizar mais de um faturamento, conclua este faturamento e inicie um novo.", "Atenção");
                    }
                    else
                    {
                        MessageBox.Show("O número inserido já havia sido inserido, por favor, verifique e tente novamente.", "Ação Inválida");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //SCRIPT FECHAMENTO
        private bool vendaFechada(string numPedido)
        {
            //verifica se existe um pagamento referente ao numero de pedido incluido, e havendo retorna false, sendo um novo pagamento devolve true
            if (controle.pesquisaFechamentoIdVenda(numPedido))
            {
                return true;
            }
            return false;
        }

        private bool pedidoPago(string numPedido)
        {
            //verifica se existe um pagamento referente ao numero de pedido incluido, e havendo retorna false, sendo um novo pagamento devolve true
            if (controle.pesquisaPagamentoIdVenda(numPedido))
            {
                return true;
            }
            return false;
        }

        private void acrescentaPedido()
        {
            try
            {
                txtDinheiro.Text = "0,00";
                txtPrePago.Text = "0,00";
                txtDebito.Text = "0,00";
                txtCredito.Text = "0,00";
                txtCheque.Text = "0,00";
                txtValorVenda.Text = "0,00";
                txtValorTotal.Text = "0,00";
                txtCustoTotal.Text = "0,00";
                custoAux = 0;

                foreach (Vendas value in listaVendas)
                {
                    txtDinheiro.Text = (Convert.ToDecimal(txtDinheiro.Text) + Convert.ToDecimal(value.valor_Venda)).ToString("0.00");
                    txtSaldoDinheiro.Text = txtDinheiro.Text;
                    txtPrePago.Text = (Convert.ToDecimal(txtPrePago.Text) + Convert.ToDecimal(value.valor_Venda)).ToString("0.00");
                    txtSaldoPrePago.Text = txtPrePago.Text;
                    txtDebito.Text = (Convert.ToDecimal(txtDebito.Text) + Convert.ToDecimal(value.valor_Venda)).ToString("0.00");
                    txtSaldoDebito.Text = txtDebito.Text;
                    txtCredito.Text = (Convert.ToDecimal(txtCredito.Text) + Convert.ToDecimal(value.valor_Venda)).ToString("0.00");
                    txtParcCred.Text = txtCredito.Text;
                    txtCheque.Text = (Convert.ToDecimal(txtCheque.Text) + Convert.ToDecimal(value.valor_Venda) + (Convert.ToDecimal(value.valor_Venda) * (gerencia.jurosCheque1x / 100))).ToString("0.00");
                    txtParcCheq.Text = txtCheque.Text;

                    txtValorVenda.Text = (Convert.ToDecimal(txtValorVenda.Text) + Convert.ToDecimal(value.valor_Venda)).ToString("0.00");
                    txtValorTotal.Text = (Convert.ToDecimal(txtValorTotal.Text) + Convert.ToDecimal(value.valor_Venda)).ToString("0.00");
                    listaProdutosVenda = controle.pesquisaProdutosVenda(value.id);

                    foreach (Vendas_Produtos result in listaProdutosVenda)
                    {
                        custoAux = custoAux + (result.Produtos.preco_compra * result.quantidade);
                    }

                    txtCustoTotal.Text = ((Convert.ToDecimal(txtCustoTotal.Text) + custoAux) + ((Convert.ToDecimal(txtCustoTotal.Text) + custoAux)) * (gerencia.tributacao / 100)).ToString("0.00");
                    txtTotal.Text = txtValorVenda.Text;
                    valorTotal = Convert.ToDecimal(txtTotal.Text);

                }
                txtDinheiro.Text = (Convert.ToDecimal(txtDinheiro.Text) - valorDesc).ToString("0.00");
                txtSaldoDinheiro.Text = txtDinheiro.Text;
                txtPrePago.Text = (Convert.ToDecimal(txtPrePago.Text) - valorDesc).ToString("0.00");
                txtSaldoPrePago.Text = txtPrePago.Text;
                txtDebito.Text = (Convert.ToDecimal(txtDebito.Text) - valorDesc).ToString("0.00");
                txtSaldoDebito.Text = txtDebito.Text;
                txtValorTotal.Text = (Convert.ToDecimal(txtValorTotal.Text) - valorDesc).ToString("0.00");
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtribuir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCpf.Text.Equals(""))
                {
                    MessageBox.Show("Para associar Cnpj / Cpf a um pagamento é necessário preencher o campo correspondente.", "Ação Inválida");
                }
                else if (validaCpf.validaTipoCpfCnpj(txtCpf.Text))
                {
                    if (controle.pesquisaClienteCpf(txtCpf.Text) != null)
                    {
                        btnAtribuirCliente.Enabled = false;
                        btnAtribuirOutro.Enabled = false;
                        txtCpf.Enabled = false;
                        cliente = controle.pesquisaClienteCpf(txtCpf.Text);
                        txtCliente.Text = cliente.nome;
                        txtCpf.Text = cliente.cpf;
                        txtCreditosCliente.Text = Convert.ToDecimal(cliente.creditos).ToString("0.00");
                        if (txtCpf.Text.Length == 11)
                        {
                            venda.cpf = txtCpf.Text;
                            venda.cnpj = "";
                        }
                        else
                        {
                            venda.cpf = "";
                            venda.cnpj = txtCpf.Text;
                        }
                        controle.salvaAtualiza();
                    }
                    else
                    {
                        MessageBox.Show("Não existe cliente com o Cnpj / Cpf digitado, para associar cliente não cadastrado utilize o botão \"Associar Outro\"", "Ação Inválida");
                    }
                }
                else
                {
                    txtCpf.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtribuirOutro_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCpf.Text.Equals(""))
                {
                    MessageBox.Show("Para associar Cnpj / Cpf a um pagamento é necessário preencher o campo correspondente.", "Ação Inválida");
                }
                else if (validaCpf.validaTipoCpfCnpj(txtCpf.Text))
                {
                    if (controle.pesquisaClienteCpf(txtCpf.Text) != null)
                    {
                        MessageBox.Show("Cnpj / Cpf informado pertence a um cliente previamente cadastrado, pressione \"Associar Cliente\"", "Ação Inválida");
                        btnAtribuirCliente.Enabled = true;
                    }
                    else
                    {
                        txtCliente.Text = "Não Cadastrado";
                        txtCreditosCliente.Text = "0,00";
                        btnAtribuirCliente.Enabled = false;
                        btnAtribuirOutro.Enabled = false;
                        txtCpf.Enabled = false;
                        if (txtCpf.Text.Length == 11)
                        {
                            venda.cpf = txtCpf.Text;
                            venda.cnpj = "";
                        }
                        else
                        {
                            venda.cpf = "";
                            venda.cnpj = txtCpf.Text;
                        }
                        controle.salvaAtualiza();
                    }
                }
                else
                {
                    btnAtribuirCliente.Enabled = true;
                    txtCpf.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trkDesconto_ValueChanged(object sender, EventArgs e)
        {
            txtValorTotal.Text = (Convert.ToDecimal(txtValorVenda.Text) - (Convert.ToDecimal(txtValorVenda.Text) * (Convert.ToDecimal(trkDesconto.Value) / 100))).ToString("0.00");
            txtDescValor.Text = (Convert.ToDecimal(txtValorVenda.Text) * (Convert.ToDecimal(trkDesconto.Value) / 100)).ToString("0.00");
            txtPercDesc.Text = trkDesconto.Value.ToString();
            txtSaldoDinheiro.Text = txtDinheiro.Text = txtSaldoPrePago.Text = txtPrePago.Text = txtSaldoDebito.Text = txtDebito.Text = txtValorTotal.Text;

            desconto = trkDesconto.Value;
        }

        private void btnPagamento_Click(object sender, EventArgs e)
        {
            try
            {
                vista = true;
                houvePag = true;
                btnPagamento.Enabled = false;
                if (trkDesconto.Value == 0)
                {
                    trkDesconto.Value = Convert.ToInt32(valorDesc / (valorTotal / 100)); ;
                }

                if (!pedidosInclusos)
                {
                    pedidosInclusos = true;
                    foreach (int value in listaNumPedidos)
                    {
                        foreach (Vendas_Produtos result in controle.pesquisaProdutosVenda(value))
                        {
                            Model.Produtos produto = controle.pesquisaProdutoId(Convert.ToInt32(result.id_produto));
                            //adiciona ao cupom o item vendido                        
                            BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_VendeItem(produto.cod_produto, produto.desc_produto, gerencia.tributacao.ToString(), TipoQuantidade.Inteira.ToString(), result.quantidade.ToString(), 2, produto.preco_venda.ToString(), "%", "0"));

                            produto.Estoque.qnt_atual = produto.Estoque.qnt_atual - result.quantidade;
                            controle.salvaAtualiza();
                        }
                    }
                }

                if (rdbDinheiro.Checked)
                {
                    entrada = true;
                    if (txtPagDinheiro.Text.Equals(""))
                    {
                        MessageBox.Show("O valor do pagamento em dinheiro deve ser preenchido, por favor, corrija e tente novamente", "Ação Invalida");
                    }
                    else if (Convert.ToDecimal(txtPagDinheiro.Text) > Convert.ToDecimal(txtSaldoDinheiro.Text) && rdbDinheiro.Checked)
                    {
                        MessageBox.Show("O valor do pagamento em dinheiro excedeu o saldo, TROCO = R$" + (Convert.ToDecimal(txtPagDinheiro.Text) - Convert.ToDecimal(txtSaldoDinheiro.Text)).ToString("0.00"), "Ação Necessária");

                        btnDesconto.Enabled = false;
                        pnlDesconto.Enabled = false;
                        rdbDinheiro.Enabled = false;
                        pnlDinheiro.Enabled = false;
                        salvaPagamentoDinheiro(Convert.ToDecimal(txtPagDinheiro.Text), sender, e);
                        if (!txtPagDinheiro.Text.Equals(""))
                        {
                            atualizaValores(Convert.ToDecimal(txtPagDinheiro.Text));
                        }
                        txtPagDinheiro.Text = "";
                    }
                    else
                    {
                        btnDesconto.Enabled = false;
                        pnlDesconto.Enabled = false;
                        rdbDinheiro.Enabled = false;
                        pnlDinheiro.Enabled = false;
                        salvaPagamentoDinheiro(Convert.ToDecimal(txtPagDinheiro.Text), sender, e);
                        if (!txtPagDinheiro.Text.Equals(""))
                        {
                            atualizaValores(Convert.ToDecimal(txtPagDinheiro.Text));
                        }
                        txtPagDinheiro.Text = "";
                    }
                }
                else if (rdbDebito.Checked)
                {
                    entrada = true;
                    if (txtPagDebito.Text.Equals(""))
                    {
                        MessageBox.Show("O campo valor deve ser preenchido", "Ação Inválida");
                    }
                    else if (Convert.ToDecimal(txtPagDebito.Text) > Convert.ToDecimal(txtSaldoDebito.Text) && rdbDebito.Checked)
                    {
                        MessageBox.Show("O valor do pagamento não deve exceder o valor da compra para pagamentos no débito", "Ação Inválida");
                    }
                    else
                    {
                        if (realizaTEF())
                        {
                            btnDesconto.Enabled = false;
                            pnlDesconto.Enabled = false;
                            rdbDebito.Enabled = false;
                            pnlDebito.Enabled = false;
                            salvaPagamentoDebito(Convert.ToDecimal(txtPagDebito.Text), sender, e);
                            if (!txtPagDebito.Text.Equals(""))
                            {
                                atualizaValores(Convert.ToDecimal(txtPagDebito.Text));
                            }
                            txtPagDebito.Text = "";
                        }
                    }
                }
                else if (rdbPrePag.Checked)
                {
                    entrada = true;
                    if (txtCreditosCliente.Text.Equals(""))
                    {
                        MessageBox.Show("Pagamento pré-pago somente está habilitado para clientes cadastrados e com créditos", "Ação Inválida");
                    }
                    else
                    {
                        if (txtPagPrePago.Text.Equals(""))
                        {
                            MessageBox.Show("O campo valor deve ser preenchido", "Ação Inválida");
                        }
                        else if (Convert.ToDecimal(txtPagPrePago.Text) > Convert.ToDecimal(txtSaldoPrePago.Text) && rdbDinheiro.Checked)
                        {
                            MessageBox.Show("O valor do pagamento não deve exceder o valor da compra para os pagamentos pré-pagos", "Ação Inválida");
                        }
                        else if (Convert.ToDecimal(txtPagPrePago.Text) > Convert.ToDecimal(txtCreditosCliente.Text))
                        {
                            MessageBox.Show("O valor do pagamento não deve exceder o valor dos créditos que o cliente possuí", "Ação Inválida");
                        }
                        else
                        {
                            btnDesconto.Enabled = false;
                            pnlDesconto.Enabled = false;
                            rdbPrePag.Enabled = false;
                            pnlPrePag.Enabled = false;

                            salvaPagamentoPrePago(Convert.ToDecimal(txtPagPrePago.Text), sender, e);

                            if (!txtPagPrePago.Text.Equals(""))
                            {
                                atualizaValores(Convert.ToDecimal(txtPagPrePago.Text));
                            }
                            txtPagPrePago.Text = "";
                        }
                    }
                }
                else if (rdbCredito.Checked)
                {
                    if (Convert.ToInt32(cmbNumParcCredito.SelectedItem) >= 1)
                    {
                        if (realizaTEF())
                        {
                            btnDesconto.Enabled = false;
                            pnlDesconto.Enabled = false;
                            vista = false;
                            rdbDebito.Enabled = false;
                            rdbDinheiro.Enabled = false;
                            rdbPrePag.Enabled = false;
                            rdbCredito.Enabled = false;
                            pnlCredito.Enabled = false;
                            salvaPagamentoCredito((Convert.ToDecimal(txtParcCred.Text) * Convert.ToInt32(cmbNumParcCredito.SelectedItem)), sender, e);
                            if (!txtParcCred.Text.Equals("0,00"))
                            {
                                atualizaValores(Convert.ToDecimal(txtParcCred.Text) * Convert.ToInt32(cmbNumParcCredito.SelectedItem));
                            }
                        }
                    }
                    else
                    {
                        btnPagamento.Enabled = true;
                        MessageBox.Show("É obrigatório o preenchimento da quantidade de parcelas", "Ação Inválida");
                    }
                }
                else if (rdbCheque.Checked)
                {
                    if (Convert.ToInt32(cmbNumParcCheque.SelectedItem) >= 1)
                    {
                        btnDesconto.Enabled = false;
                        pnlDesconto.Enabled = false;
                        vista = false;
                        rdbDebito.Enabled = false;
                        rdbDinheiro.Enabled = false;
                        rdbPrePag.Enabled = false;
                        rdbCheque.Enabled = false;
                        pnlCheque.Enabled = false;
                        salvaPagamentoCheque((Convert.ToDecimal(txtParcCheq.Text) * Convert.ToInt32(cmbNumParcCheque.SelectedItem)), sender, e);
                        if (!txtParcCred.Text.Equals("0,00"))
                        {
                            atualizaValores(Convert.ToDecimal(txtParcCheq.Text) * Convert.ToInt32(cmbNumParcCheque.SelectedItem));
                        }
                    }
                    else
                    {
                        btnPagamento.Enabled = true;
                        MessageBox.Show("É obrigatório o preenchimento da quantidade de parcelas", "Ação Inválida");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atualizaValores(decimal valorRecebido)
        {
            try
            {
                if (vista)
                {
                    txtSaldoDinheiro.Text = (Convert.ToDecimal(txtSaldoDinheiro.Text) - valorRecebido).ToString("0.00");
                    txtSaldoPrePago.Text = (Convert.ToDecimal(txtSaldoPrePago.Text) - valorRecebido).ToString("0.00");
                    txtSaldoDebito.Text = (Convert.ToDecimal(txtSaldoDebito.Text) - valorRecebido).ToString("0.00");
                    valorRecebido = valorRecebido + ((Convert.ToDecimal(txtTotal.Text) / ((Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtTotal.Text) * (Convert.ToDecimal(desconto) / 100))) / valorRecebido)) * (Convert.ToDecimal(desconto) / 100));
                    txtParcCred.Text = ((Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtRecebido.Text) + valorRecebido))).ToString("0.00");
                }
                else
                {
                    txtParcCred.Text = "0.00";
                    txtSaldoDinheiro.Text = "0.00";
                    txtSaldoPrePago.Text = "0.00";
                    txtSaldoDebito.Text = "0.00";
                }
                txtParcCheq.Text = txtParcCred.Text;
                txtRecebido.Text = (Convert.ToDecimal(txtRecebido.Text) + valorRecebido).ToString("0.00");
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesconto_Click(object sender, EventArgs e)
        {
            try
            {
                pnlPedidos.Enabled = false;

                if (valorDesc > 0)
                {
                    desconto = Convert.ToInt32(valorDesc / (valorTotal / 100));
                    int max = Convert.ToInt32(100 * (valorTotal - (Convert.ToDecimal(txtCustoTotal.Text) + (Convert.ToDecimal(txtCustoTotal.Text) * Convert.ToDecimal(gerencia.lucroMinimo / 100)))) / valorTotal) - 1;
                    if (max < gerencia.maxDescPerc)
                    {
                        trkDesconto.SetRange(desconto, max);
                    }
                    else
                    {
                        trkDesconto.SetRange(desconto, gerencia.maxDescPerc);
                    }

                }
                if (pnlDesconto.Enabled)
                {
                    pnlDesconto.Enabled = false;
                }
                else
                {
                    pnlDesconto.Enabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = MessageBox.Show("Você tem certeza que deseja cancelar o último cupom emitido e excluir o registro de pagamento ?", "Confirmação Necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Equals(DialogResult.Yes))
                {
                    if (!ultimoExcluido)
                    {
                        ultimoExcluido = true;
                        if (houvePag)
                        {
                            houvePag = false;
                            List<Pagamentos> listaPagamentos = controle.pesquisaUltimoPagamento();
                            List<int> vendasRealizadas = new List<int>();
                            foreach (Pagamentos value in listaPagamentos)
                            {
                                int idPag = value.id;
                                int idMov = Convert.ToInt32(value.id_movimento);
                                int vendaID = 0;

                                List<Pagamentos_Vendas> listaPagVenda = controle.pesquisaPagVendaIdPagamento(value.id);
                                foreach (Pagamentos_Vendas pagVend in listaPagVenda)
                                {
                                    controle.removePagamentoVenda(pagVend);
                                    controle.salvaAtualiza();

                                    vendaID = pagVend.id_Venda;
                                }
                                controle.removePagamento(value);
                                controle.salvaAtualiza();

                                Movimentos movimento = controle.pesquisaMovimentoId(idMov);
                                controle.removeMovimento(movimento);
                                controle.salvaAtualiza();

                                List<Movimentos> ListaMovimento = controle.pesquisaMovimentoReferIdPagamento(idPag);
                                foreach (Movimentos mov in ListaMovimento)
                                {
                                    controle.removeMovimento(mov);
                                    controle.salvaAtualiza();
                                }

                                bool novoElemento = true;
                                for (int i = 0; i < vendasRealizadas.Count; i++)
                                {
                                    if (vendasRealizadas[i] == vendaID)
                                    {
                                        novoElemento = false;
                                    }
                                }
                                if (novoElemento)
                                {
                                    vendasRealizadas.Add(vendaID);
                                }
                            }
                            foreach (int value in vendasRealizadas)
                            {
                                List<Vendas_Produtos> listaProdVendido = controle.pesquisaProdutosVenda(value);
                                foreach (Vendas_Produtos prodVend in listaProdVendido)
                                {
                                    Model.Produtos prod = controle.pesquisaProdutoId(Convert.ToInt32(prodVend.id_produto));
                                    prod.Estoque.qnt_atual = prod.Estoque.qnt_atual + prodVend.quantidade;
                                    controle.salvaAtualiza();
                                }
                            }
                            cliente.creditos = cliente.creditos + clienteCreditos;
                            controle.salvaAtualiza();
                            MessageBox.Show("Exclusão do pagamento realizada com sucesso", "Ação Bem Sucedida");
                        }
                    }
                    BemaFI32.Bematech_FI_CancelaCupom();

                    btnLimpar_Click(sender, e);
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdbDinheiro_CheckedChanged(object sender, EventArgs e)
        {
            AcceptButton = btnPagamento;
            txtPagDinheiro.Focus();
            pnlPedidos.Enabled = false;
            btnPagamento.Enabled = true;
            pnlDinheiro.Enabled = true;
            pnlDebito.Enabled = false;
            pnlPrePag.Enabled = false;
            pnlCredito.Enabled = false;
            pnlCheque.Enabled = false;           
        }

        private void rdbDebito_CheckedChanged(object sender, EventArgs e)
        {
            AcceptButton = btnPagamento;
            txtPagDebito.Focus();
            pnlPedidos.Enabled = false;
            btnPagamento.Enabled = true;
            pnlDinheiro.Enabled = false;
            pnlDebito.Enabled = true;
            pnlPrePag.Enabled = false;
            pnlCredito.Enabled = false;
            pnlCheque.Enabled = false;
        }

        private void rdbPrePag_CheckedChanged(object sender, EventArgs e)
        {
            AcceptButton = btnPagamento;
            txtPagPrePago.Focus();
            pnlPedidos.Enabled = false;
            btnPagamento.Enabled = true;
            pnlDinheiro.Enabled = false;
            pnlDebito.Enabled = false;
            pnlPrePag.Enabled = true;
            pnlCredito.Enabled = false;
            pnlCheque.Enabled = false;
        }

        private void rdbCredito_CheckedChanged(object sender, EventArgs e)
        {
            AcceptButton = btnPagamento;
            cmbNumParcCredito.Focus();

            if (cmbNumParcCredito.SelectedItem == null)
            {
                valorParcelaCred = Convert.ToDecimal(txtParcCred.Text);
            }
            pnlPedidos.Enabled = false;
            btnPagamento.Enabled = true;
            pnlDinheiro.Enabled = false;
            pnlDebito.Enabled = false;
            pnlPrePag.Enabled = false;
            pnlCredito.Enabled = true;
            pnlCheque.Enabled = false;
        }

        private void rdbCheque_CheckedChanged(object sender, EventArgs e)
        {
            AcceptButton = btnPagamento;
            cmbNumParcCheque.Focus();

            if (cmbNumParcCheque.SelectedItem == null)
            {
                valorParcelaCheq = Convert.ToDecimal(txtParcCheq.Text);
            }
            pnlPedidos.Enabled = false;
            btnPagamento.Enabled = true;
            pnlDinheiro.Enabled = false;
            pnlDebito.Enabled = false;
            pnlPrePag.Enabled = false;
            pnlCredito.Enabled = false;
            pnlCheque.Enabled = true;
        }

        private void cmbNumParcCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                decimal juros = 0;
                if (cmbNumParcCredito.SelectedItem != null)
                {
                    switch (cmbNumParcCredito.SelectedItem)
                    {
                        case "1":
                            juros = 0;
                            break;
                        case "2":
                            juros = gerencia.jurosPrazo2x / 100;
                            break;
                        case "3":
                            juros = gerencia.jurosPrazo3x / 100;
                            break;
                        case "4":
                            juros = gerencia.jurosPrazo4x / 100;
                            break;
                        case "5":
                            juros = gerencia.jurosPrazo5x / 100;
                            break;
                        case "6":
                            juros = gerencia.jurosPrazo6x / 100;
                            break;
                        case "7":
                            juros = gerencia.jurosPrazo7x / 100;
                            break;
                        case "8":
                            juros = gerencia.jurosPrazo8x / 100;
                            break;
                        case "9":
                            juros = gerencia.jurosPrazo9x / 100;
                            break;
                        case "10":
                            juros = gerencia.jurosPrazo10x / 100;
                            break;
                        case "11":
                            juros = gerencia.jurosPrazo11x / 100;
                            break;
                        case "12":
                            juros = gerencia.jurosPrazo12x / 100;
                            break;
                    }
                    //txtParcCred.Text = ((valorParcelaCred + (valorParcelaCred * juros))/ Convert.ToInt32(cmbNumParcCredito.SelectedItem)).ToString("0.00");
                    txtParcCred.Text = Convert.ToDecimal(((Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text)) * juros) + (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text)) / Convert.ToInt32(cmbNumParcCredito.SelectedItem)).ToString("0.00");
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbNumParcCheque_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                decimal juros = 0;
                if (cmbNumParcCheque.SelectedItem != null)
                {
                    switch (cmbNumParcCheque.SelectedItem)
                    {
                        case "1":
                            juros = 0;
                            break;
                        case "2":
                            juros = gerencia.jurosCheque2x / 100;
                            break;
                        case "3":
                            juros = gerencia.jurosCheque3x / 100;
                            break;
                        case "4":
                            juros = gerencia.jurosCheque4x / 100;
                            break;
                        case "5":
                            juros = gerencia.jurosCheque5x / 100;
                            break;
                        case "6":
                            juros = gerencia.jurosCheque6x / 100;
                            break;
                        case "7":
                            juros = gerencia.jurosCheque7x / 100;
                            break;
                        case "8":
                            juros = gerencia.jurosCheque8x / 100;
                            break;
                        case "9":
                            juros = gerencia.jurosCheque9x / 100;
                            break;
                        case "10":
                            juros = gerencia.jurosCheque10x / 100;
                            break;
                        case "11":
                            juros = gerencia.jurosCheque11x / 100;
                            break;
                        case "12":
                            juros = gerencia.jurosCheque12x / 100;
                            break;
                    }
                    txtParcCheq.Text = Convert.ToDecimal(((Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text)) * juros) + (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text)) / Convert.ToInt32(cmbNumParcCheque.SelectedItem)).ToString("0.00");
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salvaPagamentoCheque(decimal valor, object sender, EventArgs e)
        {
            try
            {
                valorAcres = valor - (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text));
                valorPago = valorPago + valor;

                if (Convert.ToDecimal(txtParcCheq.Text) * Convert.ToInt32(cmbNumParcCheque.SelectedItem) < valor)
                {
                    valorPago = valorPago + (0.01M * Convert.ToInt32(cmbNumParcCheque.SelectedItem));
                }

                int ent = 0;
                if (entrada)
                {
                    ent = 1;
                }
                ShowMyDialogBox();

                //Atribuição de valores ao objeto CupomFiscal PagCheque
                PagCheque.formaPagamento = "Cheque";
                PagCheque.acrescimo = valorAcres.ToString("0.00");
                PagCheque.desconto = "0.00";
                PagCheque.valorPagamento = valor.ToString("0.00");

                for (int i = 0; i < Convert.ToInt32(cmbNumParcCheque.SelectedItem); i++)
                {
                    Movimentos movimento = new Movimentos();
                    controle.salvarMovimento(movimento);
                    if (!entrada && i == 0)
                    {
                        movimento.id_tipo = 33;
                        movimento.desc = "Pag. Cheque";
                    }
                    else
                    {
                        movimento.id_tipo = 18;
                        movimento.desc = "Entrada + Parcelas, Cheque";
                    }
                    movimento.valor = valor / Convert.ToInt32(cmbNumParcCheque.SelectedItem);
                    movimento.data = DateTime.Today.AddMonths(i + ent);
                    controle.salvaAtualiza();

                    Pagamentos pagamento = new Pagamentos();
                    controle.salvarPagamento(pagamento);
                    pagamento.id_movimento = movimento.id;
                    pagamento.dataPagamento = DateTime.Today.AddMonths(i + ent);
                    pagamento.formaPag = "Cheque";                    
                    pagamento.valorTotal = Convert.ToDecimal(txtTotal.Text);
                    pagamento.valorParcela = valor / Convert.ToInt32(cmbNumParcCheque.SelectedItem);

                    pagamento.numChequePrimeiro = chequePrim;
                    pagamento.numChequeUltimo = chequeUlt;                    

                    if (!entrada && Convert.ToInt32(cmbNumParcCheque.SelectedItem) == 1)
                    {
                        pagamento.tipoPag = "Total";
                        pagamento.numParcela = 1;
                        pagamento.qntParcelas = 1;
                    }
                    else if (!entrada && Convert.ToInt32(cmbNumParcCheque.SelectedItem) != 1 && i >= 1)
                    {
                        pagamento.tipoPag = "Parcelado";
                        pagamento.numParcela = i;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCheque.SelectedItem);
                    }
                    else if (entrada && Convert.ToInt32(cmbNumParcCheque.SelectedItem) == 1)
                    {
                        pagamento.tipoPag = "Parcial";
                        pagamento.numParcela = 0;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCheque.SelectedItem);
                    }
                    else if (entrada && Convert.ToInt32(cmbNumParcCheque.SelectedItem) >= 2)
                    {
                        pagamento.tipoPag = "Entrada + Parcelas";
                        pagamento.numParcela = i + 1;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCheque.SelectedItem);
                    }

                    controle.salvaAtualiza();
                    Movimentos movimentoImposto = new Movimentos();
                    controle.salvarMovimento(movimentoImposto);
                    movimentoImposto.data = DateTime.Today;
                    movimentoImposto.desc = "Trib Pag:" + pagamento.id;
                    movimentoImposto.id_tipo = 11;
                    movimentoImposto.valor = pagamento.valorTotal * 0.1039M / Convert.ToInt32(cmbNumParcCheque.SelectedItem);
                    controle.salvaAtualiza();

                    Movimentos movimentoMercadoria = new Movimentos();
                    controle.salvarMovimento(movimentoMercadoria);
                    movimentoMercadoria.data = DateTime.Today;
                    movimentoMercadoria.desc = "Estoque Pag:" + pagamento.id;
                    movimentoMercadoria.id_tipo = 65;
                    movimentoMercadoria.valor = custoAux / Convert.ToInt32(cmbNumParcCheque.SelectedItem);
                    controle.salvaAtualiza();

                    foreach (Vendas value in listaVendas)
                    {
                        Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                        controle.salvaPagamentoPedido(pagamentoPedido);
                        pagamentoPedido.id_Venda = value.id;
                        pagamentoPedido.id_Pagamento = pagamento.id;

                        controle.salvaAtualiza();
                    }
                }

                MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");
                imprimeCupomFiscal();
                btnLimpar_Click(sender, e);

            }
            catch
            {
                MessageBox.Show("Erro não identificado ao registrar o pagamento, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salvaPagamentoCredito(decimal valor, object sender, EventArgs e)
        {
            try
            {
                //valorAcres = (Convert.ToDecimal(txtParcCred.Text) * Convert.ToDecimal(cmbNumParcCredito.SelectedItem)) - (Convert.ToDecimal(txtTotal.Text) - valorPago);
                valorAcres = valor - (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text));
                valorPago = valorPago + valor;                                

                if(Convert.ToDecimal(txtParcCred.Text) * Convert.ToInt32(cmbNumParcCredito.SelectedItem) < valor)
                {
                    valorPago = valorPago + (0.01M * Convert.ToInt32(cmbNumParcCredito.SelectedItem));
                }

                int ent = 0;
                if (entrada)
                {
                    ent = 1;
                }

                //Atribuição de valores ao objeto CupomFiscal PagCredito
                PagCredito.formaPagamento = "Crédito";
                PagCredito.acrescimo = valorAcres.ToString("0.00");
                PagCredito.desconto = "0.00";
                PagCredito.valorPagamento = valor.ToString("0.00");

                for (int i = 0; i < Convert.ToInt32(cmbNumParcCredito.SelectedItem); i++)
                {
                    Movimentos movimento = new Movimentos();
                    controle.salvarMovimento(movimento);
                    if (!entrada && i == 0)
                    {
                        movimento.id_tipo = 33;
                        movimento.desc = "TEF Crédito";
                    }
                    else
                    {
                        movimento.id_tipo = 18;
                        movimento.desc = "Entrada + Parcelas, Crédito";
                    }
                    movimento.valor = valor / Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    movimento.data = DateTime.Today.AddMonths(i + ent);
                    controle.salvaAtualiza();

                    Pagamentos pagamento = new Pagamentos();
                    controle.salvarPagamento(pagamento);
                    pagamento.id_movimento = movimento.id;
                    pagamento.dataPagamento = DateTime.Today.AddMonths(i + ent);
                    pagamento.formaPag = "C.Crédito";
                    pagamento.valorTotal = Convert.ToDecimal(txtTotal.Text);
                    pagamento.valorParcela = valor / Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    pagamento.numChequePrimeiro = "";
                    pagamento.numChequeUltimo = "";
                    

                    if (!entrada && Convert.ToInt32(cmbNumParcCredito.SelectedItem) == 1)
                    {
                        pagamento.tipoPag = "Total";
                        pagamento.numParcela = 1;
                        pagamento.qntParcelas = 1;
                    }
                    else if (!entrada && Convert.ToInt32(cmbNumParcCredito.SelectedItem) != 1 && i >= 1)
                    {
                        pagamento.tipoPag = "Parcelado";
                        pagamento.numParcela = i;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    }
                    else if (entrada && Convert.ToInt32(cmbNumParcCredito.SelectedItem) == 1)
                    {
                        pagamento.tipoPag = "Parcial";
                        pagamento.numParcela = 0;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    }
                    else if (entrada && Convert.ToInt32(cmbNumParcCredito.SelectedItem) >= 2)
                    {
                        pagamento.tipoPag = "Entrada + Parcelas";
                        pagamento.numParcela = i + 1;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    }
                    controle.salvaAtualiza();
                    Movimentos movimentoImposto = new Movimentos();
                    controle.salvarMovimento(movimentoImposto);
                    movimentoImposto.data = DateTime.Today;
                    movimentoImposto.desc = "Trib Pag:" + pagamento.id;
                    movimentoImposto.id_tipo = 11;
                    movimentoImposto.valor = pagamento.valorTotal * 0.1039M / Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    controle.salvaAtualiza();

                    Movimentos movimentoMercadoria = new Movimentos();
                    controle.salvarMovimento(movimentoMercadoria);
                    movimentoMercadoria.data = DateTime.Today;
                    movimentoMercadoria.desc = "Estoque Pag:" + pagamento.id;
                    movimentoMercadoria.id_tipo = 65;
                    movimentoMercadoria.valor = custoAux / Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    controle.salvaAtualiza();

                    foreach (Vendas value in listaVendas)
                    {                       
                        Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                        controle.salvaPagamentoPedido(pagamentoPedido);
                        pagamentoPedido.id_Venda = value.id;
                        pagamentoPedido.id_Pagamento = pagamento.id;

                        controle.salvaAtualiza();
                    }
                }

                MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");
                imprimeCupomFiscal();
                btnLimpar_Click(sender, e);

            }
            catch
            {
                MessageBox.Show("Erro ao registrar o pagamento, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salvaPagamentoPrePago(decimal valor, object sender, EventArgs e)
        {
            try
            {                
                valorPago = valorPago + valor;                

                //Atribuição de valores ao objeto CupomFiscal PagPrePago
                PagPrePago.formaPagamento = "Pré-Pago";
                PagPrePago.acrescimo = valorAcres.ToString("0.00");
                PagPrePago.desconto = "0.00";
                PagPrePago.valorPagamento = valor.ToString("0.00");

                Movimentos movimento = new Movimentos();
                controle.salvarMovimento(movimento);
                movimento.id_tipo = 30;
                movimento.valor = valor;
                movimento.data = DateTime.Now;

                Pagamentos pagamento = new Pagamentos();
                controle.salvarPagamento(pagamento);
                pagamento.dataPagamento = DateTime.Now;
                pagamento.formaPag = "Pré-Pago";
                pagamento.valorTotal = Convert.ToDecimal(txtTotal.Text);
                pagamento.valorParcela = valor;
                pagamento.numChequePrimeiro = "";
                pagamento.numChequeUltimo = "";

                //Bematech_FI_EfetuaFormaPagamento(string FormaPagamento, string ValorFormaPagamento);
                //BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_EfetuaFormaPagamento(pagamento.formaPag, valor.ToString("0.00")));

                if (Convert.ToDecimal(txtSaldoPrePago.Text) <= valorPago)
                {
                    movimento.desc = "Total Pré-Pago";
                    pagamento.tipoPag = "Total";
                    pagamento.numParcela = 1;
                    pagamento.qntParcelas = 1;
                }
                else
                {
                    movimento.desc = "Entrada pré-pago";
                    pagamento.tipoPag = "Entrada";
                    pagamento.numParcela = 0;
                    pagamento.qntParcelas = 0;
                }
                controle.salvaAtualiza();
                pagamento.id_movimento = movimento.id;

                Movimentos movimentoImposto = new Movimentos();
                controle.salvarMovimento(movimentoImposto);
                movimentoImposto.data = DateTime.Today;
                movimentoImposto.desc = "Trib Pag:" + pagamento.id;
                movimentoImposto.id_tipo = 11;
                movimentoImposto.valor = pagamento.valorTotal * 0.1039M;
                controle.salvaAtualiza();

                Movimentos movimentoMercadoria = new Movimentos();
                controle.salvarMovimento(movimentoMercadoria);
                movimentoMercadoria.data = DateTime.Today;
                movimentoMercadoria.desc = "Estoque Pag:" + pagamento.id;
                movimentoMercadoria.id_tipo = 65;
                movimentoMercadoria.valor = custoAux;
                controle.salvaAtualiza();

                PagPrePago.desconto = Convert.ToDecimal(PagPrePago.desconto) + Convert.ToDecimal((Convert.ToDecimal(txtTotal.Text) * trkDesconto.Value / 100) / (Convert.ToDecimal(txtValorTotal.Text) / valor)).ToString("0.00");

                foreach (Vendas value in listaVendas)
                {
                    venda = controle.pesquisaVendaID(value.id);
                    venda.desconto = trkDesconto.Value;                    

                    Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                    controle.salvaPagamentoPedido(pagamentoPedido);
                    pagamentoPedido.id_Venda = venda.id;
                    pagamentoPedido.id_Pagamento = pagamento.id;

                    controle.salvaAtualiza();
                }

                clienteCreditos = Convert.ToDouble(valor);
                cliente.creditos = cliente.creditos - Convert.ToDouble(valor);
                controle.salvaAtualiza();

                if (Convert.ToDecimal(txtSaldoPrePago.Text) <= valorPago)
                {
                    MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");
                    imprimeCupomFiscal();
                    btnLimpar_Click(sender, e);
                }
            }
            catch
            {
                MessageBox.Show("Erro ao registrar o pagamento, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salvaPagamentoDebito(decimal valor, object sender, EventArgs e)
        {
            try
            {
                valorPago = valorPago + valor;

                //Atribuição de valores ao objeto CupomFiscal PagDebito
                PagDebito.formaPagamento = "Débito";
                PagDebito.acrescimo = valorAcres.ToString("0.00");
                PagDebito.desconto = "0.00";
                PagDebito.valorPagamento = valor.ToString("0.00");

                Movimentos movimento = new Movimentos();
                controle.salvarMovimento(movimento);
                movimento.id_tipo = 33;
                movimento.valor = valor;
                movimento.data = DateTime.Now;

                Pagamentos pagamento = new Pagamentos();
                controle.salvarPagamento(pagamento);
                pagamento.dataPagamento = DateTime.Now;
                pagamento.formaPag = "Débito";
                pagamento.valorTotal = Convert.ToDecimal(txtTotal.Text);
                pagamento.valorParcela = valor;
                pagamento.numChequePrimeiro = "";
                pagamento.numChequeUltimo = "";

                if (Convert.ToDecimal(txtSaldoDebito.Text) <= valorPago)
                {
                    movimento.desc = "Total TEF Débito";
                    pagamento.tipoPag = "Total";
                    pagamento.numParcela = 1;
                    pagamento.qntParcelas = 1;
                }
                else
                {
                    movimento.desc = "Entrada TEF Débito";
                    pagamento.tipoPag = "Entrada";
                    pagamento.numParcela = 0;
                    pagamento.qntParcelas = 0;
                }
                controle.salvaAtualiza();
                pagamento.id_movimento = movimento.id;

                Movimentos movimentoImposto = new Movimentos();
                controle.salvarMovimento(movimentoImposto);
                movimentoImposto.data = DateTime.Today;
                movimentoImposto.desc = "Trib Pag:" + pagamento.id;
                movimentoImposto.id_tipo = 11;
                movimentoImposto.valor = pagamento.valorTotal * 0.1039M;
                controle.salvaAtualiza();

                Movimentos movimentoMercadoria = new Movimentos();
                controle.salvarMovimento(movimentoMercadoria);
                movimentoMercadoria.data = DateTime.Today;
                movimentoMercadoria.desc = "Estoque Pag:" + pagamento.id;
                movimentoMercadoria.id_tipo = 65;
                movimentoMercadoria.valor = custoAux;
                controle.salvaAtualiza();

                PagDebito.desconto = Convert.ToDecimal(PagDebito.desconto) + Convert.ToDecimal((Convert.ToDecimal(txtTotal.Text) * trkDesconto.Value / 100 ) / (Convert.ToDecimal(txtValorTotal.Text) / valor)).ToString("0.00");

                foreach (Vendas value in listaVendas)
                {
                    venda = controle.pesquisaVendaID(value.id);
                    venda.desconto = trkDesconto.Value;                    

                    Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                    controle.salvaPagamentoPedido(pagamentoPedido);
                    pagamentoPedido.id_Venda = venda.id;
                    pagamentoPedido.id_Pagamento = pagamento.id;

                    controle.salvaAtualiza();
                }

                if (Convert.ToDecimal(txtSaldoDebito.Text) <= valorPago)
                {
                    MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");
                    imprimeCupomFiscal();
                    btnLimpar_Click(sender, e);
                }
            }
            catch
            {
                MessageBox.Show("Erro ao registrar o pagamento, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salvaPagamentoDinheiro(decimal valor, object sender, EventArgs e)
        {
            try
            {
                valorPago = valorPago + valor;

                //Atribuição de valores ao objeto CupomFiscal PagDinheiro
                PagDinheiro.formaPagamento = "Dinheiro";
                PagDinheiro.acrescimo = valorAcres.ToString("0.00");
                PagDinheiro.desconto = "0.00";
                PagDinheiro.valorPagamento = valor.ToString("0.00");

                Movimentos movimento = new Movimentos();
                controle.salvarMovimento(movimento);
                movimento.id_tipo = 19;
                movimento.valor = valor;
                movimento.data = DateTime.Now;

                Pagamentos pagamento = new Pagamentos();
                controle.salvarPagamento(pagamento);
                pagamento.dataPagamento = DateTime.Now;
                pagamento.formaPag = "Dinheiro";
                pagamento.valorTotal = Convert.ToDecimal(txtTotal.Text);
                pagamento.valorParcela = valor;
                pagamento.numChequePrimeiro = "";
                pagamento.numChequeUltimo = "";
                
                if (Convert.ToDecimal(txtSaldoDinheiro.Text) <= valorPago)
                {
                    movimento.desc = "Total a dinheiro";
                    pagamento.tipoPag = "Total";
                    pagamento.numParcela = 1;
                    pagamento.qntParcelas = 1;
                }
                else
                {
                    movimento.desc = "Entrada a dinheiro";
                    pagamento.tipoPag = "Entrada";
                    pagamento.numParcela = 0;
                    pagamento.qntParcelas = 0;
                }
                controle.salvaAtualiza();
                pagamento.id_movimento = movimento.id;

                Movimentos movimentoImposto = new Movimentos();
                controle.salvarMovimento(movimentoImposto);
                movimentoImposto.data = DateTime.Today;
                movimentoImposto.desc = "Trib Pag:" + pagamento.id;
                movimentoImposto.id_tipo = 11;
                movimentoImposto.valor = pagamento.valorTotal * 0.1039M;
                controle.salvaAtualiza();

                Movimentos movimentoMercadoria = new Movimentos();
                controle.salvarMovimento(movimentoMercadoria);
                movimentoMercadoria.data = DateTime.Today;
                movimentoMercadoria.desc = "Estoque Pag:" + pagamento.id;
                movimentoMercadoria.id_tipo = 65;
                movimentoMercadoria.valor = custoAux;
                controle.salvaAtualiza();

                PagDinheiro.desconto = Convert.ToDecimal(PagDinheiro.desconto) + Convert.ToDecimal((Convert.ToDecimal(txtTotal.Text) * trkDesconto.Value / 100) / (Convert.ToDecimal(txtValorTotal.Text) / valor)).ToString("0.00");

                foreach (Vendas value in listaVendas)
                {
                    venda = controle.pesquisaVendaID(value.id);
                    venda.desconto = trkDesconto.Value;                    

                    Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                    controle.salvaPagamentoPedido(pagamentoPedido);
                    pagamentoPedido.id_Venda = venda.id;
                    pagamentoPedido.id_Pagamento = pagamento.id;

                    controle.salvaAtualiza();
                }

                if (Convert.ToDecimal(txtSaldoDinheiro.Text) <= valorPago)
                {
                    imprimeCupomFiscal();
                    MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");
                    btnLimpar_Click(sender, e);
                }
            }
            catch
            {
                MessageBox.Show("Erro ao registrar o pagamento, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool realizaTEF()
        {
            //chamada e resposta enviada pelo gerenciador padrão de transação TEF
            DialogResult = MessageBox.Show("Já foi realizada a transação de transferência de fundos ?", "Ação Necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (DialogResult.Equals(DialogResult.Yes))
            {
                return true;
            }
            else
            {
                btnPagamento.Enabled = true;
                return false;
            }
        }

        public void ShowMyDialogBox()
        {
            DialogoCheque testDialog = new DialogoCheque();
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {

            }
            else
            {
                this.txtChequePrimeiro.Text = "Cancelled";
                this.txtChequeUltimo.Text = "Cancelled";
            }
            testDialog.Dispose();
        }

        private void imprimeCupomFiscal()
        {
            try
            {
                valorDesc = Convert.ToDecimal(PagDinheiro.desconto) + Convert.ToDecimal(PagDebito.desconto) + Convert.ToDecimal(PagPrePago.desconto);
                valorAcres = (Convert.ToDecimal(PagCredito.acrescimo) + Convert.ToDecimal(PagCheque.acrescimo));

                string AcrescimoDesconto = "";
                string ValorAcrescimoDesconto = "";

                if (valorDesc > valorAcres)
                {
                    AcrescimoDesconto = "D";
                    ValorAcrescimoDesconto = (valorDesc - valorAcres).ToString("0.00");
                }
                else
                {
                    AcrescimoDesconto = "A";
                    ValorAcrescimoDesconto = (valorAcres - valorDesc).ToString("0.00");
                }

                //ABERTURA DO FECHAMENTO
                //Bematech_FI_IniciaFechamentoCupom(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
                BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_IniciaFechamentoCupom(AcrescimoDesconto, "$", ValorAcrescimoDesconto));


                //PAGAMENTOS
                //Bematech_FI_EfetuaFormaPagamento(string FormaPagamento, string ValorFormaPagamento);
                if (Convert.ToDecimal(PagDinheiro.valorPagamento) > 0)
                {
                    BemaFI32.Bematech_FI_EfetuaFormaPagamento("Dinheiro", PagDinheiro.valorPagamento);
                }
                if (Convert.ToDecimal(PagDebito.valorPagamento) > 0)
                {
                    BemaFI32.Bematech_FI_EfetuaFormaPagamento("Débito", PagDebito.valorPagamento);
                }
                if (Convert.ToDecimal(PagPrePago.valorPagamento) > 0)
                {
                    BemaFI32.Bematech_FI_EfetuaFormaPagamento("Pré-Pago", PagPrePago.valorPagamento);
                }
                if (Convert.ToDecimal(PagCredito.valorPagamento) > 0)
                {
                    BemaFI32.Bematech_FI_EfetuaFormaPagamento("C.Crédito", PagCredito.valorPagamento);
                }
                if (Convert.ToDecimal(PagCheque.valorPagamento) > 0)
                {
                    BemaFI32.Bematech_FI_EfetuaFormaPagamento("Cheque", PagCheque.valorPagamento);
                }


                //CONCLUI O FECHAMENTO
                if (AcrescimoDesconto.Equals("D"))
                {
                    BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_TerminaFechamentoCupom("O Alemao da Construcao agradece sua preferencia\n\nTrib Aprox R$: " + ((valorTotal - Convert.ToDecimal(ValorAcrescimoDesconto)) * 0.0552M).ToString("0.00") + " Federal e " + ((valorTotal - Convert.ToDecimal(ValorAcrescimoDesconto)) * 0.0486M).ToString("0.00") + " Estadual\nFonte: SEBRAE\n\nAtendido por: " + user.nome));
                }
                else
                {
                    BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_TerminaFechamentoCupom("O Alemao da Construcao agradece sua preferencia\n\nTrib Aprox R$: " + ((valorTotal + Convert.ToDecimal(ValorAcrescimoDesconto)) * 0.0552M).ToString("0.00") + " Federal e " + ((valorTotal + Convert.ToDecimal(ValorAcrescimoDesconto)) * 0.0486M).ToString("0.00") + " Estadual\nFonte: SEBRAE\n\nAtendido por: " + user.nome));
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            try
            {
                pedidosInclusos = false;
                pnlPedidos.Enabled = true;

                txtPedidoNum1.Text = "";
                txtPedidoNum1.Enabled = true;
                btnAdicionar1.Enabled = true;
                btnPagamento.Enabled = false;
                btnDesconto.Enabled = false;
                btnLimpar.Enabled = false;
                trkDesconto.Minimum = 0;
                trkDesconto.Value = 0;

                txtPedidoNum2.Text = "";
                txtPedidoNum2.Enabled = false;
                btnAdicionar2.Enabled = false;
                txtPedidoNum3.Text = "";
                txtPedidoNum3.Enabled = false;
                btnAdicionar3.Enabled = false;
                txtPedidoNum4.Text = "";
                txtPedidoNum4.Enabled = false;
                btnAdicionar4.Enabled = false;
                txtPedidoNum5.Text = "";
                txtPedidoNum5.Enabled = false;
                btnAdicionar5.Enabled = false;

                pnlCliente.Enabled = true;
                txtCreditosCliente.Text = "";
                txtCpf.Text = "";
                rdbNPnao.Checked = true;
                txtCliente.Text = "";

                txtDescValor.Text = "0,00";
                txtCustoTotal.Text = "0,00";
                txtValorVenda.Text = "0,00";
                txtValorTotal.Text = "0,00";

                pnlPagamento.Enabled = false;
                rdbDinheiro.Checked = false;
                rdbDebito.Checked = false;
                rdbCredito.Checked = false;
                rdbPrePag.Checked = false;
                rdbCheque.Checked = false;
                rdbDinheiro.Enabled = true;
                rdbDebito.Enabled = true;
                rdbCredito.Enabled = true;
                rdbPrePag.Enabled = true;
                rdbCheque.Enabled = true;

                txtDinheiro.Text = "0,00";
                txtSaldoDinheiro.Text = "0,00";
                txtPagDinheiro.Text = "";
                txtDebito.Text = "0,00";
                txtSaldoDebito.Text = "0,00";
                txtPagDebito.Text = "";
                txtPrePago.Text = "0,00";
                txtSaldoPrePago.Text = "0,00";
                txtPagPrePago.Text = "";
                txtCredito.Text = "0,00";
                cmbNumParcCredito.SelectedItem = null;
                cmbNumParcCheque.SelectedItem = null;
                txtParcCred.Text = "0,00";
                txtCheque.Text = "0,00";
                txtParcCheq.Text = "0,00";

                txtRecebido.Text = "0,00";
                txtTotal.Text = "R$";

                AcrescimoDesconto = "";
                ValorAcrescimoDesconto = "";

                listaVendas = new List<Vendas>();
                listaNumPedidos = new List<int>();
                listaProdutosVenda = new List<Vendas_Produtos>();
                venda = new Vendas();
                cliente = new Model.Clientes();
                PagDinheiro = new CupomFiscal();
                PagDebito = new CupomFiscal();
                PagPrePago = new CupomFiscal();
                PagCredito = new CupomFiscal();
                PagCheque = new CupomFiscal();
                chequePrim = "";
                chequeUlt = "";
                valorParcelaCred = 0.00M;
                valorParcelaCheq = 0.00M;
                valorAcres = 0.00M;
                valorDesc = 0.00M;
                valorPago = 0.00M;
                valorTotal = 0.00M;
                custoAux = 0.00M;
                desconto = 0;
                entrada = false;
                vista = true;
                ultimoExcluido = false;
                txtPedidoNum1.Text = "";
                pnlCheque.Enabled = false;
                pnlCredito.Enabled = false;
                pnlDebito.Enabled = false;
                pnlDinheiro.Enabled = false;
                pnlPrePag.Enabled = false;
                txtPedidoNum1.Enabled = true;
                btnAdicionar1.Enabled = true;
                pnlPedidos.Enabled = true;
                btnPagamento.Enabled = false;
                AcceptButton = btnAdicionar1;
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool confirmaAssociacao(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("O cliente não deseja a identificação por CPF/CNPJ no Cupom Fiscal ? Caso cliente já identificado ou CPF/CNPJ informado clique em \"Não\" para associar outro CPF ou em \"Sim\" para associa-lo ao cupom fiscal", "Ação Necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (DialogResult.Equals(DialogResult.Yes))
            {
                if (!txtCpf.Text.Equals(""))
                {
                    rdbNPsim.Checked = true;
                    btnAdicionar1_Click(sender, e);
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCancelaAberto_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Você tem certeza que deseja cancelar o cupom em aberto e limpar o formulário ?", "Ação Necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Equals(DialogResult.Yes))
            {
                BemaFI32.Bematech_FI_CancelaCupom();
                MessageBox.Show("Cupom Fiscal cancelado com sucesso", "Ação Bem Sucedida");
                btnLimpar_Click(sender, e);
            }
        }
    }
}
