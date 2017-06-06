using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Loja1._0.Model;
using Loja1._0.Control;
using Bematech.Fiscal.ECF;
using FiscalPrinterBematech;
using Loja1._0.View;
using System.Runtime.InteropServices;

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
        static decimal valorParcela = 0;
        static int desconto = 0;
        public bool vista = true;
        public bool entrada = false;
        public string Aliquota = "10,38";
        public static decimal valorDesc = 0.00M;
        public static decimal valorPago = 0.00M;
        public bool ultimoExcluido = false;

        public Caixa(Model.Usuarios user)
        {            
            this.user = user;
            InitializeComponent();
            lblUser.Text = user.nome;
            txtPedidoNum1.Focus();
            AcceptButton = btnAdicionar1;
            txtRecebido.Text = "0,00";
            gerencia = controle.pesquisaGerenciamento(1);
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
            if (txtPedidoNum1.Text.Equals(""))
            {
                MessageBox.Show("É necessário preencher o número do pedido a ser adicionado para faturamento", "Ação Inválida");
            }
            else if(controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum1.Text)) == null)
            {
                MessageBox.Show("Os dados inseridos não correspondem a nenhum pedido de compra, por favor, verifique e tente novamente.","Ação Inválida");
            }
            //validação da existencia de pagamento para o pedido inserido
            else if (pedidoPago(txtPedidoNum1.Text))
            {
                MessageBox.Show("Já existe um pagamento associado ao pedido nº" + txtPedidoNum1.Text + ", por favor, verifique e tente novamente", "Ação Inválida");
            }
            else
            {               
                venda = controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum1.Text));
                listaVendas.Add(venda);

                if (venda.desconto != 0)
                {
                    valorDesc = valorDesc + Convert.ToDecimal(venda.valor_Venda) / Convert.ToDecimal(venda.desconto);
                }           
                btnDesconto.Enabled = true;
                btnLimpar.Enabled = true;                

                pnlPagamento.Enabled = true;
                pnlCliente.Enabled = true;
                btnAdicionar2.Enabled = true;
                txtPedidoNum2.Enabled = true;
                btnAdicionar1.Enabled = false;
                txtPedidoNum1.Enabled = false;

                if(venda.Clientes != null && cliente.cpf == null)
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
                AcceptButton = btnAdicionar2;
                txtPedidoNum2.Focus();
            }            
        }

        private void btnAdicionar2_Click(object sender, EventArgs e)
        {
            if (txtPedidoNum2.Text.Equals(""))
            {
                MessageBox.Show("É necessário preencher o número do pedido a ser adicionado para faturamento", "Ação Inválida");
            }
            else if (controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum2.Text)) == null)
            {
                MessageBox.Show("O número inserido não corresponde a nenhum pedido de compra, por favor, verifique e tente novamente.", "Ação Inválida");
            }
            else if (pedidoPago(txtPedidoNum2.Text))
            {
                MessageBox.Show("Já existe um pagamento associado ao pedido nº" + txtPedidoNum2.Text + ", por favor, verifique e tente novamente", "Ação Inválida");
            }
            else
            {
                venda = controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum2.Text));
                bool existe = false;

                foreach(Model.Vendas value in listaVendas)
                {
                    if(venda.id == value.id)
                    {
                        existe = true;
                    }
                }

                if (!existe)
                {
                    if (venda.desconto != 0)
                    {
                        valorDesc = valorDesc + Convert.ToDecimal(venda.valor_Venda) / Convert.ToDecimal(venda.desconto);
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
                }
                else
                {
                    MessageBox.Show("O número inserido já havia sido inserido, por favor, verifique e tente novamente.", "Ação Inválida");
                }
                
            }
        }

        private void btnAdicionar3_Click(object sender, EventArgs e)
        {
            if (txtPedidoNum3.Text.Equals(""))
            {
                MessageBox.Show("É necessário preencher o número do pedido a ser adicionado para faturamento", "Ação Inválida");
            }
            else if (controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum3.Text)) == null)
            {
                MessageBox.Show("O inserido não corresponde a nenhum pedido de compra, por favor, verifique e tente novamente.", "Ação Inválida");
            }
            else if (pedidoPago(txtPedidoNum3.Text))
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
                    if (venda.desconto != 0 && venda.desconto != null)
                    {
                        valorDesc = valorDesc + Convert.ToDecimal(venda.valor_Venda) / Convert.ToDecimal(venda.desconto);
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
                }
                else
                {
                    MessageBox.Show("O número inserido já havia sido inserido, por favor, verifique e tente novamente.", "Ação Inválida");
                }
            }
        }

        private void btnAdicionar4_Click(object sender, EventArgs e)
        {
            if (txtPedidoNum4.Text.Equals(""))
            {
                MessageBox.Show("É necessário preencher o número do pedido a ser adicionado para faturamento", "Ação Inválida");
            }
            else if (controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum4.Text)) == null)
            {
                MessageBox.Show("O inserido não corresponde a nenhum pedido de compra, por favor, verifique e tente novamente.", "Ação Inválida");
            }
            else if (pedidoPago(txtPedidoNum4.Text))
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
                        valorDesc = valorDesc + Convert.ToDecimal(venda.valor_Venda) / Convert.ToDecimal(venda.desconto);
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
                }
                else
                {
                    MessageBox.Show("O número inserido já havia sido inserido, por favor, verifique e tente novamente.", "Ação Inválida");
                }
            }
        }

        private void btnAdicionar5_Click(object sender, EventArgs e)
        {
            if (txtPedidoNum5.Text.Equals(""))
            {
                MessageBox.Show("É necessário preencher o número do pedido a ser adicionado para faturamento", "Ação Inválida");
            }
            else if (controle.pesquisaVendaID(Convert.ToInt32(txtPedidoNum5.Text)) == null)
            {
                MessageBox.Show("O inserido não corresponde a nenhum pedido de compra, por favor, verifique e tente novamente.", "Ação Inválida");
            }
            else if (pedidoPago(txtPedidoNum5.Text))
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
                        valorDesc = valorDesc + Convert.ToDecimal(venda.valor_Venda) / Convert.ToDecimal(venda.desconto);
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
                    MessageBox.Show("Pedido Adicionado com sucesso. Para a cobrança de mais de 5 pedidos é necessário realizar mais de um faturamento, conclua este faturamento e inicie um novo.", "Atenção");
                    acrescentaPedido();
                }
                else
                {
                    MessageBox.Show("O número inserido já havia sido inserido, por favor, verifique e tente novamente.", "Ação Inválida");
                }
            }
        }

        private bool pedidoPago(string numPedido)
        {
            //comentario exclusivo para debug
            /*/verifica se existe um pagamento referente ao numero de pedido incluido, e havendo retorna false, sendo um novo pagamentoe devolve true
            if (controle.pesquisaPagamentoIdVenda(numPedido))
            {
                return true;
            }*/
            return false;
        }

        private void acrescentaPedido()
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
                cmbNumParcCredito.SelectedItem = 1;
                txtCredito.Text = (Convert.ToDecimal(txtCredito.Text) + Convert.ToDecimal(value.valor_Venda)).ToString("0.00");
                txtParcCred.Text = txtCredito.Text;
                cmbNumParcCheque.SelectedItem = 1;
                txtCheque.Text = (Convert.ToDecimal(txtCheque.Text) + Convert.ToDecimal(value.valor_Venda)).ToString("0.00");
                txtParcCheq.Text = txtCheque.Text;                

                txtValorVenda.Text = (Convert.ToDecimal(txtValorVenda.Text) + Convert.ToDecimal(value.valor_Venda)).ToString("0.00");                
                txtValorTotal.Text = (Convert.ToDecimal(txtValorTotal.Text) + Convert.ToDecimal(value.valor_Venda)).ToString("0.00");
                listaProdutosVenda = controle.pesquisaProdutosVenda(value.id);
                
                foreach (Vendas_Produtos result in listaProdutosVenda)
                {
                    custoAux = custoAux + (result.Produtos.preco_compra * result.quantidade);
                }

                txtCustoTotal.Text = ((Convert.ToDecimal(txtCustoTotal.Text) + custoAux) + ((Convert.ToDecimal(txtCustoTotal.Text) + custoAux)) * 0.1039M).ToString("0.00");
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

        private void btnAtribuir_Click(object sender, EventArgs e)
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
                    if(txtCpf.Text.Length == 11)
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

        private void btnAtribuirOutro_Click(object sender, EventArgs e)
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
            vista = true;                                                
            btnPagamento.Enabled = false;

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
                    //valorDesc = valorDesc + (Convert.ToDecimal(txtTotal.Text)/(Convert.ToDecimal(txtDinheiro.Text)/Convert.ToDecimal(txtDinheiro.Text))) * (Convert.ToDecimal(desconto) / 100);
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
                    //valorDesc = valorDesc + (Convert.ToDecimal(txtTotal.Text) / (Convert.ToDecimal(txtDinheiro.Text) / Convert.ToDecimal(txtPagDinheiro.Text))) * (Convert.ToDecimal(desconto) / 100);
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
                        //valorDesc = valorDesc + (Convert.ToDecimal(txtTotal.Text) / (Convert.ToDecimal(txtDebito.Text) / Convert.ToDecimal(txtPagDebito.Text))) * (Convert.ToDecimal(desconto) / 100);
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
                if (txtPagPrePago.Text.Equals(""))
                {
                    MessageBox.Show("O campo valor deve ser preenchido", "Ação Inválida");
                }
                else if (Convert.ToDecimal(txtPagPrePago.Text) > Convert.ToDecimal(txtSaldoPrePago.Text) && rdbDinheiro.Checked)
                {
                    MessageBox.Show("O valor do pagamento não deve exceder o valor da compra para os pagamentos pré-pagos", "Ação Inválida");
                }
                else
                {
                    btnDesconto.Enabled = false;
                    pnlDesconto.Enabled = false;
                    rdbPrePag.Enabled = false;
                    pnlPrePag.Enabled = false;
                    //valorDesc = valorDesc + (Convert.ToDecimal(txtTotal.Text) / (Convert.ToDecimal(txtPrePago.Text) / Convert.ToDecimal(txtPagPrePago.Text))) * (Convert.ToDecimal(desconto) / 100);
                    salvaPagamentoPrePago(Convert.ToDecimal(txtPagPrePago.Text), sender, e);
                    if (!txtPagPrePago.Text.Equals(""))
                    {
                        atualizaValores(Convert.ToDecimal(txtPagPrePago.Text));
                    }
                    txtPagPrePago.Text = "";
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
                        if (!txtParcCred.Text.Equals(""))
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
                    if (!txtParcCred.Equals(""))
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

        private void atualizaValores(decimal valorRecebido)
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

        private void btnDesconto_Click(object sender, EventArgs e)
        {
            pnlPedidos.Enabled = false;

            if (valorDesc > 0)
            {
                desconto = Convert.ToInt32( valorDesc/ (valorTotal / 100));
                trkDesconto.SetRange(desconto, Convert.ToInt32((valorTotal - (Convert.ToDecimal(txtCustoTotal.Text) + (Convert.ToDecimal(txtCustoTotal.Text) * Convert.ToDecimal(gerencia.lucroMinimo)))) / (valorTotal/100) ));
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
            DialogResult = MessageBox.Show("Você tem certeza que deseja cancelar o último cupom emitido e excluir o registro de pagamento ?","Confirmação Necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Equals(DialogResult.Yes))
            {
                if (!ultimoExcluido)
                {
                    ultimoExcluido = true;                    
                    List<Pagamentos> listaPagamentos = controle.pesquisaUltimoPagamento();

                    foreach(Pagamentos value in listaPagamentos)
                    {
                        int idPag = value.id;
                        int idMov = Convert.ToInt32(value.id_movimento);

                        List<Pagamentos_Vendas> listaPagVenda = controle.pesquisaPagVendaIdPagamento(value.id);
                        foreach (Pagamentos_Vendas pagVend in listaPagVenda)
                        {
                            controle.removePagamentoVenda(pagVend);
                            controle.salvaAtualiza();
                        }
                        controle.removePagamento(value);
                        controle.salvaAtualiza();

                        Movimentos movimento = controle.pesquisaMovimentoId(idMov);
                        controle.removeMovimento(movimento);
                        controle.salvaAtualiza();

                        List<Movimentos> ListaMovimento = controle.pesquisaMovimentoReferIdPagamento(idPag);
                        foreach(Movimentos mov in ListaMovimento)
                        {
                            controle.removeMovimento(mov);
                            controle.salvaAtualiza();
                        }                        
                        
                    }
                    BemaFI32.Bematech_FI_CancelaCupom();
                    MessageBox.Show("Exclusão realizada com sucesso","Ação Bem Sucedida");
                    btnLimpar_Click(sender, e);
                }
                
            } 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            BemaFI32.Bematech_FI_LeituraX();
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
                valorParcela = Convert.ToDecimal(txtParcCheq.Text);
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
            if(cmbNumParcCredito.SelectedItem != null)
            {
                txtParcCred.Text = (valorParcela / Convert.ToInt32(cmbNumParcCredito.SelectedItem)).ToString("0.00");
            }      
            valorParcela = Convert.ToDecimal(txtParcCred.Text) * Convert.ToDecimal(cmbNumParcCredito.SelectedItem);
        }

        private void cmbNumParcCheque_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNumParcCheque.SelectedItem != null)
            {                
                txtParcCheq.Text = (valorParcela / Convert.ToInt32(cmbNumParcCheque.SelectedItem)).ToString("0.00");
            }
            valorParcela = Convert.ToDecimal(txtParcCheq.Text) * Convert.ToDecimal(cmbNumParcCheque.SelectedItem);
        }
        private void salvaPagamentoCheque(decimal valor, object sender, EventArgs e)
        {
            try
            {
                valorPago = valorPago + valor;

                int ent = 0;
                if (entrada)
                {
                    ent = 1;
                }
                ShowMyDialogBox();
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
                        pagamento.numParcela = i+1;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCheque.SelectedItem);
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
                        venda = controle.pesquisaVendaID(value.id);
                        venda.desconto = trkDesconto.Value;

                        Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                        controle.salvaPagamentoPedido(pagamentoPedido);
                        pagamentoPedido.id_Venda = venda.id;
                        pagamentoPedido.id_Pagamento = pagamento.id;

                        controle.salvaAtualiza();
                    }
                }
                foreach (Vendas value in listaVendas)
                {
                    venda = controle.pesquisaVendaID(value.id);
                    venda.desconto = trkDesconto.Value;
                    controle.salvaAtualiza();
                }

                MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");
                imprimeCupomFiscal();
                btnLimpar_Click(sender, e);

            }
            catch
            {
                MessageBox.Show("Não foi possivel registrar o pagamento, por favor, tente novamente", "Erro !");
            }
        }

        private void salvaPagamentoCredito(decimal valor, object sender, EventArgs e)
        {
            try
            {
                valorPago = valorPago + valor;

                int ent = 0;
                if (entrada)
                {
                    ent = 1;
                }
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
                    pagamento.formaPag = "C. Crédito";
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
                    else if(!entrada && Convert.ToInt32(cmbNumParcCredito.SelectedItem) != 1 && i >= 1)
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
                        pagamento.numParcela = i+1;
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
                        venda = controle.pesquisaVendaID(value.id);
                        venda.desconto = trkDesconto.Value;

                        Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                        controle.salvaPagamentoPedido(pagamentoPedido);
                        pagamentoPedido.id_Venda = venda.id;
                        pagamentoPedido.id_Pagamento = pagamento.id;

                        controle.salvaAtualiza();
                    }
                }
                foreach (Vendas value in listaVendas)
                {
                    venda = controle.pesquisaVendaID(value.id);
                    venda.desconto = trkDesconto.Value;
                    controle.salvaAtualiza();
                }

                MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");
                imprimeCupomFiscal();
                btnLimpar_Click(sender, e);

            }
            catch
            {
                MessageBox.Show("Não foi possivel registrar o pagamento, por favor, tente novamente", "Erro !");
            }
        }

        private void salvaPagamentoPrePago(decimal valor, object sender, EventArgs e)
        {
            try
            {
                valorPago = valorPago + valor;

                Movimentos movimento = new Movimentos();
                controle.salvarMovimento(movimento);
                movimento.id_tipo = 30;
                movimento.valor = valor;
                movimento.data = DateTime.Now;

                Pagamentos pagamento = new Pagamentos();
                controle.salvarPagamento(pagamento);
                pagamento.dataPagamento = DateTime.Now;
                pagamento.formaPag = "Pré-Paga";
                pagamento.valorTotal = Convert.ToDecimal(txtTotal.Text);
                pagamento.valorParcela = valor;
                pagamento.numChequePrimeiro = "";
                pagamento.numChequeUltimo = "";
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

                if (Convert.ToDecimal(txtSaldoPrePago.Text) <= valorPago)
                {
                    MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");
                    imprimeCupomFiscal();
                    btnLimpar_Click(sender, e);
                }
            }
            catch
            {
                MessageBox.Show("Não foi possivel registrar o pagamento, por favor, tente novamente", "Erro !");
            }
        }

        private void salvaPagamentoDebito(decimal valor, object sender, EventArgs e)
        {
            try
            {
                valorPago = valorPago + valor;

                Movimentos movimento = new Movimentos();
                controle.salvarMovimento(movimento);
                movimento.id_tipo = 33;
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
                MessageBox.Show("Não foi possivel registrar o pagamento, por favor, tente novamente", "Erro !");
            }
        }

        private void salvaPagamentoDinheiro(decimal valor, object sender, EventArgs e)
        {
            try
            {
                valorPago = valorPago + valor;

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
                MessageBox.Show("Não foi possivel registrar o pagamento, por favor, tente novamente","Erro !");
            }
        }

        private bool realizaTEF()
        {
            //chamada e resposta enviada pelo gerenciador padrão de transação TEF
            DialogResult = MessageBox.Show("Já foi realizada a transação de transferência de fundos ?","Ação Necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (DialogResult.Equals(DialogResult.Yes))
            {
                return true;
            }
            else
            {
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

            /*if (não existir configuração de aliquota)
            {
                //Configuração inicial Impressora fiscal
                BemaFI32.Bematech_FI_AlteraSimboloMoeda("R");
                BemaFI32.Bematech_FI_ProgramaAliquota("18,00", 0);
                BemaFI32.Bematech_FI_LinhasEntreCupons(1);
                BemaFI32.Bematech_FI_EspacoEntreLinhas(5);
                BemaFI32.Bematech_FI_ProgramaArredondamento();
            }*/

            if (rdbNPsim.Checked)
            {
                //cnpj ou cpf do cliente
                BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_AbreCupom(txtCpf.Text));
            }
            else
            {
                BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_AbreCupom(""));
            }

            //Relação das ordens de serviço que compõem o pagamento efetuado
            foreach (Vendas value in listaVendas)
            {
                listaProdutosVenda = controle.pesquisaProdutosVenda(value.id);

                //Relação dos produtos que compõem cada uma das vendas                
                foreach (Vendas_Produtos item in listaProdutosVenda)
                {
                    Model.Produtos produto = controle.pesquisaProdutoId(Convert.ToInt32(item.id_produto));
                    //adiciona ao cupom o item vendido
                    //public static extern int Bematech_FI_VendeItem(string Codigo, string Descricao, string Aliquota, string TipoQuantidade, string Quantidade, int CasasDecimais, string ValorUnitario, string TipoDesconto, string Desconto);
                    BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_VendeItem(produto.cod_produto, produto.desc_produto, Aliquota, TipoQuantidade.Inteira.ToString(), item.quantidade.ToString(), 2, produto.preco_venda.ToString(), "%", "0"));
                }

            }

            //Bematech_FI_FechaCupom(string FormaPagamento, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string ValorPago, string Mensagem);
            BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_FechaCupom("Dinheiro", "D", "$", valorDesc.ToString("0.00"), valorPago.ToString("0.00"), "O Alemao da Construcao agradece sua preferencia\n\nTrib Aprox R$: "+ ((valorTotal - valorDesc) * 0.0552M).ToString("0.00") + " Federal e " + ((valorTotal - valorDesc) * 0.0486M).ToString("0.00") +" Estadual\nFonte: SEBRAE\n\nAtendido por: " + user.nome));
            
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            pnlPedidos.Enabled = true;
            AcceptButton = btnAdicionar1;
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

            pnlCliente.Enabled = false;
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
            cmbNumParcCredito.SelectedItem = 1;
            txtParcCred.Text = "0,00";
            txtCheque.Text = "0,00";
            txtParcCheq.Text = "0,00";

            txtRecebido.Text = "0,00";
            txtTotal.Text = "R$";

            listaVendas = new List<Vendas>();
            listaProdutosVenda = new List<Vendas_Produtos>();
            venda = new Vendas();
            cliente = new Model.Clientes();
            valorDesc = 0.00M;
            valorPago = 0.00M;
            valorTotal = 0;
            custoAux = 0;
            desconto = 0;
            entrada = false;
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
        }
    }
}
