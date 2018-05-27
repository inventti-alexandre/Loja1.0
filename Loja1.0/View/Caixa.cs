using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Loja1._0.Model;
using Loja1._0.Control;
using Bematech.Fiscal.ECF;
using Loja1._0.View;
using FiscalPrinterBematech;

namespace Loja1._0
{
    public partial class Caixa : Form
    {
        //função para chamada do objeto bematech flash builder, atualmente fora de uso, sugerido para TEF
        //static ImpressoraFiscal BematechFiscal = ImpressoraFiscal.Construir();

        //declaração das variaveis locais referentes ao Control
        private Controle controle = new Controle();
        public Model.Usuarios user;        
        //static List<Vendas> listaVendas = new List<Vendas>();
        static List<Vendas_Produtos> listaProdutosVenda = new List<Vendas_Produtos>();
        public Gerenciamento gerencia = new Gerenciamento();
        public static Vendas venda = new Vendas();
        static Model.Clientes cliente = new Model.Clientes();
        private Email email = new Email();
        public Valida validaCpf = new Valida();
        static CupomFiscal PagDinheiro = new CupomFiscal();
        static CupomFiscal PagDebito = new CupomFiscal();
        static CupomFiscal PagPrePago = new CupomFiscal();
        static CupomFiscal PagCredito = new CupomFiscal();
        static CupomFiscal PagCheque = new CupomFiscal();

        //declaração das variaáveis locais referentes ao Form
        public TextBox txtChequePrimeiro = new TextBox();
        public TextBox txtChequeUltimo = new TextBox();

        //declaração das variaveis locais estaticas    
        public static string erro;
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
        public static string AcrescimoDesconto = "";
        public static string ValorAcrescimoDesconto = "";        
        static double clienteCreditos = 0;
        //public static List<int> listaNumPedidos = new List<int>();
        public static List<int> listaIdPagamentos = new List<int>();
        public static bool pedidosInclusos = false;        
        public static int ent = 0;

        //Variaveis SAT
        string chaveAcesso = "";
        string numeroCupom = "";
        string NumeroSAT = "";
        string message = "";
        string code = "";
        string errorMessage = "";
        string errorCode = "";
        public string cnpj = "";
        public string assinaturaSoftHouse = "";

        //inicilização do form com parametro do usuário vindo de Inicial   
        public Caixa(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();

            //configuração visual do form
            lblUser.Text = user.login;
            txtPedidoNum1.Focus();
            AcceptButton = btnAdicionar1;
            txtRecebido.Text = "0.00";

            //tentativa de instanciar objeto Gerenciamento
            try
            {
                gerencia = controle.PesquisaGerenciamento(1);
            }
            catch
            {
                //caso não seja possível a pesquisa atribui a localização a string erro, envia email ao desenvolvedor e exibe mensagem ao usuário
                erro = "Caixa.cs, linhas 71 a 74";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro na consulta aos dados gerenciais, linhas 71 a 74, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //função do botão sair
        private void btnExit_Click(object sender, EventArgs e)
        {
            //limpa o form atual, chama o formulário inicial, exibe ele e dispensa o formulário atual
            btnLimpar_Click(sender, e);
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        //chamada da função do botão troca user
        private void btnTrocaUser_Click(object sender, EventArgs e)
        {
            //exibe um form de login a frente dos demais forms visiveis
            TrocaUserCx form = new TrocaUserCx(user, this);
            form.Show();
        }

        //função do botão "+" do primeiro campo
        private void btnAdicionar1_Click(object sender, EventArgs e)
        {
            //realiza a tentativa de todas as ações e funções abaixo, havendo qualquer erro sai para a função catch
            try
            {                
                //verifica se houve o preenchimento do campo referente ao pedido número 1 e informa ao usuário caso este esteja em branco
                if (txtPedidoNum1.Text.Equals(""))
                {
                    MessageBox.Show("É necessário preencher o número do pedido a ser adicionado para faturamento", "Ação Inválida");
                }

                //verifica se existe uma venda correspondente aos dados do preenchimento do campo número de pedido 1 e informa o erro ao usuário caso este ocorra
                else if (controle.PesquisaVendaID(Convert.ToInt32(txtPedidoNum1.Text)) == null)
                {
                    MessageBox.Show("Os dados inseridos não correspondem a nenhum pedido de compra, por favor, verifique e tente novamente.", "Ação Inválida");
                }

                //SCRIPT FECHAMENTO e pagamento
                //validação da existencia de pagamento para o pedido inserido / ou se o mesmo recebeu o fechamento
                else if (pedidoPago(txtPedidoNum1.Text) || vendaFechada(txtPedidoNum1.Text))
                {
                    MessageBox.Show("Já existe um pagamento associado ao pedido nº" + txtPedidoNum1.Text + ", por favor, verifique e tente novamente", "Ação Inválida");
                }

                //caso nenhum dos erros anteriore ocorra entra no trecho abaixo
                else
                {
                    //atribui após pesquisa valor ao objeto venda do tipo dbo.Vendas
                    venda = controle.PesquisaVendaID(Convert.ToInt32(txtPedidoNum1.Text));

                    //efetua a verificação de cliente associado a venda e da ausência de cliente associado ao cupom fiscal
                    if (venda.Clientes != null && cliente.cpf == null)
                    {
                        //realiza ajustes de visibilidade e preenchimento do form, panel cliente, e atribui o cliente da venda ao cupom fiscal                        
                        cliente = venda.Clientes;
                        btnAtribuirCliente.Enabled = false;
                        txtCliente.Text = cliente.nome;
                        txtCpf.Text = cliente.cpf;
                        txtCreditosCliente.Text = Convert.ToDecimal(cliente.creditos).ToString("0.00");
                        if (txtCreditosCliente.Text.Equals(""))
                        {
                            txtCreditosCliente.Text = "0.00";
                        }
                    }

                    /* efetua a verificação de cliente associado a venda e da ausência de cliente associado ao cupom fiscal 
                     * e ainda da condição de preenchimento do radio button referente a nota paulista */
                    if (venda.Clientes != null && cliente.cpf != null && rdbNPsim.Checked)
                    {
                        //função de impressão de Cupom Fiscal
                        
                        //realiza a abertura do cupom na ECF com cnpj ou cpf do cliente
                        BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_AbreCupomMFD(txtCpf.Text, "", ""));
                        
                        //chamada da função para adição de venda a lista de vendas
                        adicionaPrimeiro();
                    }

                    else if (rdbNPsim.Checked && validaCpf.validaTipoCpfCnpj(txtCpf.Text))
                    {
                        //função de impressão de Cupom Fiscal
                        //realiza a abertura do cupom na ECF com cnpj ou cpf do cliente
                        BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_AbreCupomMFD(txtCpf.Text, "", ""));

                        //chamada da função para adição de venda a lista de vendas
                        adicionaPrimeiro();
                    }

                    else if (confirmaAssociacao(sender, e))
                    {
                        //função de impressão de Cupom Fiscal
                        //realiza a abertura do cupom na ECF sem associação a cliente
                        BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_AbreCupomMFD("", "", ""));

                        adicionaPrimeiro();
                    }
                }
            }
            catch
            {
                //Envio de email como parametro string do método da classe Email
                erro = "Caixa.cs, linha 107 a 175";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linha 107 a 175, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //chamada da função para adição de venda a lista de vendas a serem faturadas 
        private void adicionaPrimeiro()
        {
            //altera a visibilidade do painel de clientes 
            pnlCliente.Enabled = false;
            rdbDinheiro.Select();

            //adiciona a venda ao List<Vendas> listaVendas
            //*/listaVendas.Add(venda);

            //verifica se existe incidencia de desconto fornecido pelo PDV para esta venda
            if (venda.desconto != 0)
            {
                //havendo desconto na venda, aplica o desconto, teremina o valor total dele e atribui a variavel local decimal valorDesc
                valorDesc = valorDesc + Convert.ToDecimal(venda.valor_Venda) * (Convert.ToDecimal(venda.desconto) / 100);
            }
            //altera a visibilidade os controles do form
            btnDesconto.Enabled = true;
            btnLimpar.Enabled = true;
            pnlPagamento.Enabled = true;
            btnAdicionar1.Enabled = false;
            txtPedidoNum1.Enabled = false;

            //realiza a chamada da função que altera os valores das variaveis e campos do form referentes a venda e seu valor
            acrescentaPedido();

            //adiciona a lista de inteiros o numero do pedido registrado
            //listaNumPedidos.Add(Convert.ToInt32(txtPedidoNum1.Text));  
        }        

        //SCRIPT FECHAMENTO
        private bool vendaFechada(string numPedido)
        {
            //verifica se existe um pagamento referente ao numero de pedido incluido, e havendo retorna false, sendo um novo pagamento devolve true
            if (controle.PesquisaFechamentoIdVenda(numPedido))
            {
                return true;
            }
            return false;
        }
        //Fim FECHAMENTO

        private bool pedidoPago(string numPedido)
        {
            //verifica se existe um pagamento referente ao numero de pedido incluido, e havendo retorna false, sendo um novo pagamento devolve true
            if (controle.PesquisaPagamentoIdVenda(numPedido) != true)
            {
                return true;
            }
            return false;
        }

        private void acrescentaPedido()
        {
            /* realiza a tentativa de alterar o conteudo dos campos exibidos no form, 
             * atribuindo a eles os valores referente as vendas armazenadas na lista de vendas
             * e alterando cada uma das formas de pagamento exibidas neste mesmo form */
            try
            {
                //zera os campos utilizados como referencia para os demais
                txtDinheiro.Text = "0.00";
                txtPrePago.Text = "0.00";
                txtDebito.Text = "0.00";
                txtCredito.Text = "0.00";
                txtCheque.Text = "0.00";
                txtValorVenda.Text = "0.00";
                txtValorTotal.Text = "0.00";
                txtCustoTotal.Text = "0.00";
                //zera a variavel auxiliar de calculo de custo
                custoAux = 0;

                
                //*/foreach (Vendas value in listaVendas)
                //{
                    //para cada venda da lista preenche os valores de referencia de cada um dos campos
                    txtDinheiro.Text = (Convert.ToDecimal(txtDinheiro.Text) + Convert.ToDecimal(venda.valor_Venda)).ToString("0.00");
                    txtSaldoDinheiro.Text = txtDinheiro.Text;
                    txtPrePago.Text = (Convert.ToDecimal(txtPrePago.Text) + Convert.ToDecimal(venda.valor_Venda)).ToString("0.00");
                    txtSaldoPrePago.Text = txtPrePago.Text;
                    txtDebito.Text = (Convert.ToDecimal(txtDebito.Text) + Convert.ToDecimal(venda.valor_Venda)).ToString("0.00");
                    txtSaldoDebito.Text = txtDebito.Text;
                    txtCredito.Text = (Convert.ToDecimal(txtCredito.Text) + Convert.ToDecimal(venda.valor_Venda)).ToString("0.00");
                    txtParcCred.Text = txtCredito.Text;
                    txtCheque.Text = (Convert.ToDecimal(txtCheque.Text) + Convert.ToDecimal(venda.valor_Venda) + (Convert.ToDecimal(venda.valor_Venda) * (gerencia.jurosCheque1x / 100))).ToString("0.00");
                    txtParcCheq.Text = txtCheque.Text;

                    //Com base nos valor da venda e com o acumulado preenche os campos de total
                    txtValorVenda.Text = (Convert.ToDecimal(txtValorVenda.Text) + Convert.ToDecimal(venda.valor_Venda)).ToString("0.00");
                    txtValorTotal.Text = (Convert.ToDecimal(txtValorTotal.Text) + Convert.ToDecimal(venda.valor_Venda)).ToString("0.00");
                    
                    //pesquisa a relação de produtos referentes a venda da lista
                    listaProdutosVenda = controle.PesquisaProdutosVenda(venda.id);

                    foreach (Vendas_Produtos result in listaProdutosVenda)
                    {
                        Compras compra = controle.PesquisaCompraAnterior(Convert.ToInt32(result.id_produto));
                        //para cada produto incrementa o custo auxiliar com o custo multiplicado pela quantidade
                        custoAux = custoAux + (compra.preco_compra * result.quantidade);
                    }

                    //apresenta como custo o valor do custo auxiliar adicionado ao valor atual de tributação
                    txtCustoTotal.Text = ((Convert.ToDecimal(txtCustoTotal.Text) + custoAux) + ((Convert.ToDecimal(txtCustoTotal.Text) + custoAux)) * (gerencia.tributacao / 100)).ToString("0.00");
                    
                    //apresenta nos campos abaixo o valor de total herdado do respectivo campo do form e atribui a variavel decimal valorTotal esse valor
                    txtTotal.Text = txtValorVenda.Text;
                    valorTotal = Convert.ToDecimal(txtTotal.Text);

                //}
                //incrementa os campos de texto de cada forma de pagamento
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
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 531 a 590";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 531 a 590, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //função atribuida ao botão de inclusão de cliente pelo CPF/CNPJ
        private void btnAtribuir_Click(object sender, EventArgs e)
        {
            //realiza a tentativa de associar cliente as vendas que compõem o cupom fiscal
            try
            {
                //verifica o preenchimnto do campo cpf, caso este não tenha sido preenchido informa o usuário quanto a obrigatoriedade 
                if (txtCpf.Text.Equals(""))
                {
                    MessageBox.Show("Para associar Cnpj / Cpf a um pagamento é necessário preencher o campo correspondente.", "Ação Inválida");
                }

                //testa a validade dos dados utilizados no preenchimento do campo
                else if (validaCpf.validaTipoCpfCnpj(txtCpf.Text))
                {
                    //verifica se o CPF/CNPJ utilizado pertence a um usuário préviamente cadastrado no sistema, em casos positivo altera as visibilidades do form de acordo com os dados deste
                    if (controle.PesquisaClienteCpf(txtCpf.Text) != null)
                    {
                        btnAtribuirCliente.Enabled = false;
                        btnAtribuirOutro.Enabled = false;
                        txtCpf.Enabled = false;
                        cliente = controle.PesquisaClienteCpf(txtCpf.Text);
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
                        btnAdicionar1.PerformClick();
                        controle.SalvaAtualiza();
                    }

                    //caso o CPF/CNPJ inserido seja válido mas não pertença a um usuário préviamente cadastrado o sistema informa ao usuário e solicita a correção 
                    else
                    {
                        MessageBox.Show("Não existe cliente com o Cnpj / Cpf digitado, para associar cliente não cadastrado utilize o botão \"Associar Outro\"", "Ação Inválida");
                    }
                }

                //caso os dados inseridos no campo não correspondam a um cpf/cnpj valido limpa o campo, o usuário já terá sido informado pelo método da classe que realiza a validação
                else
                {
                    txtCpf.Text = "";
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 735 a 780";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 735 a 780, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //função atribuida ao botão de inclusão de não cliente pelo CPF/CNPJ para fins de nota paulista e associação no BD
        private void btnAtribuirOutro_Click(object sender, EventArgs e)
        {
            //realiza a tentativa de associar cpf/cnpj as vendas que compõem o cupom fiscal
            try
            {
                //verifica o preenchimnto do campo cpf, caso este não tenha sido preenchido informa o usuário quanto a obrigatoriedade 
                if (txtCpf.Text.Equals(""))
                {
                    MessageBox.Show("Para associar Cnpj / Cpf a um pagamento é necessário preencher o campo correspondente.", "Ação Inválida");
                }

                //testa a validade dos dados utilizados no preenchimento do campo
                else if (validaCpf.validaTipoCpfCnpj(txtCpf.Text))
                {
                    //verifica se o CPF/CNPJ utilizado pertence a um usuário préviamente cadastrado no sistema, em casos positivo informa ao usuário a maneira correta de proceder
                    if (controle.PesquisaClienteCpf(txtCpf.Text) != null)
                    {
                        MessageBox.Show("Cnpj / Cpf informado pertence a um cliente previamente cadastrado, pressione \"Associar Cliente\"", "Ação Inválida");
                        btnAtribuirCliente.Enabled = true;
                    }

                    //Associa o cpf/cnpj a venda e altera as opções de visibilidade do form
                    else
                    {
                        txtCliente.Text = "Não Cadastrado";
                        txtCreditosCliente.Text = "0.00";
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
                        
                        controle.SalvaAtualiza();
                        btnAdicionar1.PerformClick();
                    }
                }

                //caso os dados inseridos no campo não correspondam a um cpf/cnpj valido limpa o campo, o usuário já terá sido informado pelo método da classe que realiza a validação
                else
                {
                    btnAtribuirCliente.Enabled = true;
                    txtCpf.Text = "";
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 794 a 840";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 794 a 840, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ação realizada ao efetuar qualquer alteração no traking desconto
        private void trkDesconto_ValueChanged(object sender, EventArgs e)
        {
            //altera os valores visualizados no form
            txtValorTotal.Text = (Convert.ToDecimal(txtValorVenda.Text) - (Convert.ToDecimal(txtValorVenda.Text) * (Convert.ToDecimal(trkDesconto.Value) / 100))).ToString("0.00");
            txtDescValor.Text = (Convert.ToDecimal(txtValorVenda.Text) * (Convert.ToDecimal(trkDesconto.Value) / 100)).ToString("0.00");
            txtPercDesc.Text = trkDesconto.Value.ToString();
            txtSaldoDinheiro.Text = txtDinheiro.Text = txtSaldoPrePago.Text = txtPrePago.Text = txtSaldoDebito.Text = txtDebito.Text = txtValorTotal.Text;
            
            //atribui a variavel desconto, que será salva nas vendas referente ao cupm fiscal, o valor final do traking desconto
            desconto = trkDesconto.Value;
        }

        //ação realizada ao clicar o botão realizar pagamento
        private void btnPagamento_Click(object sender, EventArgs e)
        {
            //tenta realizar a redução dos itens que compõem a venda no estoque e inclusão destes no cupom fiscal, alterar a visibilidade do traking desconto
            try
            {
                //atribui o valor das variáveis locais booleanas 
                vista = true;

                //altera o valor da váriavel de verificação de pagamento efetuado
                ultimoExcluido = false;

                //altera a visibilidade de itens do form
                btnPagamento.Enabled = false;

                //atribui o valor do desconto préviamente cadastrado nas vendas ao traking desconto caso este não tenha sofrido alteração
                if (trkDesconto.Value == 0)
                {
                    trkDesconto.Value = Convert.ToInt32(valorDesc / (valorTotal / 100));
                }

                //verifica se a variável booleana que identifica se já houve alteração devida aos pedidos inclusos no pagamento ainda possuí o valor falso atribuido
                if (!pedidosInclusos)
                {
                    //altera o valor desta variavel booleana indicando que os pedidos estão inclusos no cupom fiscal
                    pedidosInclusos = true;
                    //*/foreach (int value in listaNumPedidos)
                    //{
                        foreach (Vendas_Produtos result in controle.PesquisaProdutosVenda(venda.id))
                        {
                            /* para cada produto em cada venda existente na lista de vendas referente a este cupom 
                             * pesquisa o produto pelo id*/
                            Model.Produtos produto = controle.PesquisaProdutoId(Convert.ToInt32(result.id_produto));

                            Compras compra = controle.PesquisaCompraAnterior(produto.id);                                                                                                                                                                                                                                                  

                            //adiciona o produto ao cupom fiscal no item SAT*/
                            BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_VendeItemCompleto(produto.cod_produto.ToString(), produto.cod_produto.ToString(), produto.desc_produto                , "00", "F1", produto.UnidMedidas.abrev, "I", "3", result.quantidade.ToString(), "2", compra.preco_venda.ToString(), "%", "0,00", "0,00", "A", produto.ncm.ToString(), "5102", "INFORMAÇÔES", "40", "0", "1234", "", "", "", "", "102", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "", "04", "", "", "", "", "", "04", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""));

                            //altera a quantidade deste no estoque e salva a alteração
                            produto.Estoque.qnt_atual = produto.Estoque.qnt_atual - result.quantidade;
                            controle.SalvaAtualiza();
                        }
                    //}
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 867 a 904";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 794 a 840, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //tenta identificar e registrar a forma de pagamento empregada
            try
            { 
                //verifica se a forma de pagamento selecionada foi dinheiro
                if (rdbDinheiro.Checked)
                {
                    //atribui o valor da variavel boleana entrada como verdadeiro
                    entrada = true;

                    //verifica e informa o usuário caso o valor do pagamento não tenha sido preenchido
                    if (txtPagDinheiro.Text.Equals(""))
                    {
                        MessageBox.Show("O valor do pagamento em dinheiro deve ser preenchido, por favor, corrija e tente novamente", "Ação Invalida");
                    }

                    //verifica se o valor de entrada é decimal
                    else if (!validaValorEntrada(txtPagDinheiro.Text))
                    {
                        MessageBox.Show("O valor do pagamento não corresponde a um valor valido, por favor, corrija e tente novamente", "Ação Invalida");
                    }

                    //verifica se o valor registrado como pagamento excede o valor total
                    else if (Convert.ToDecimal(txtPagDinheiro.Text) > Convert.ToDecimal(txtSaldoDinheiro.Text))
                    {
                        //informa o valor do troco
                        MessageBox.Show("O valor do pagamento em dinheiro excedeu o saldo, TROCO = R$" + (Convert.ToDecimal(txtPagDinheiro.Text) - Convert.ToDecimal(txtSaldoDinheiro.Text)).ToString("0.00"), "Ação Necessária");

                        //altera a visibilidade do form
                        btnDesconto.Enabled = false;
                        pnlDesconto.Enabled = false;
                        rdbDinheiro.Enabled = false;
                        pnlDinheiro.Enabled = false;

                        //chamada da função responsável por cadatrar na base de dados e no cupom fical o pagamento realizado
                        salvaPagamentoDinheiro(Convert.ToDecimal(txtPagDinheiro.Text), sender, e);

                        //caso haja algum valor no campo referente ao pagamento em dinheiro atuaiza os valores visualizados no form
                        if (!txtPagDinheiro.Text.Equals(""))
                        {
                            //chamada da função que realiza essas alterações
                            atualizaValores(Convert.ToDecimal(txtPagDinheiro.Text));
                        }

                        //limpa o campo referente ao valor do pagamento
                        txtPagDinheiro.Text = "";
                    }

                    //se o valor inserido for inferior ao total da compra, registra o pagamento parcial
                    else
                    {
                        //altera a visibilidade de itens do form
                        btnDesconto.Enabled = false;
                        pnlDesconto.Enabled = false;
                        rdbDinheiro.Enabled = false;
                        pnlDinheiro.Enabled = false;

                        //chamada da função responsável por cadatrar na base de dados e no cupom fical o pagamento realizado
                        salvaPagamentoDinheiro(Convert.ToDecimal(txtPagDinheiro.Text), sender, e);

                        //caso haja algum valor no campo referente ao pagamento em dinheiro atuaiza os valores visualizados no form
                        if (!txtPagDinheiro.Text.Equals(""))
                        {
                            //chamada da função que realiza essas alterações
                            atualizaValores(Convert.ToDecimal(txtPagDinheiro.Text));
                        }

                        //limpa o campo referente ao valor do pagamento
                        txtPagDinheiro.Text = "";
                    }
                }

                //verifica se a forma de pagamento selecionada foi cartão de débito
                else if (rdbDebito.Checked)
                {
                    //atribui o valor da variavel boleana entrada como verdadeiro
                    entrada = true;

                    //verifica e informa o usuário caso o valor do pagamento não tenha sido preenchido
                    if (txtPagDebito.Text.Equals(""))
                    {
                        MessageBox.Show("O campo valor deve ser preenchido", "Ação Inválida");
                    }

                    //verifica se o valor de entrada é decimal
                    else if (!validaValorEntrada(txtPagDebito.Text))
                    {
                        MessageBox.Show("O valor do pagamento não corresponde a um valor valido, por favor, corrija e tente novamente", "Ação Invalida");
                    }

                    //verifica se o valor registrado como pagamento excede o valor total
                    else if (Convert.ToDecimal(txtPagDebito.Text) > Convert.ToDecimal(txtSaldoDebito.Text))
                    {
                        //informa e restringe esse tipo de ação
                        MessageBox.Show("O valor do pagamento não deve exceder o valor da compra para pagamentos no débito", "Ação Inválida");
                    }

                    //se o valor inserido for inferior ao total da compra, registra o pagamento parcial
                    else
                    {
                        //realiza a chamada da função para o usuário indicar que foi realizado o TEF, ou mesmo para realizar o TEF
                        if (realizaTEF())
                        {
                            //altera a visibilidade de itens do form
                            btnDesconto.Enabled = false;
                            pnlDesconto.Enabled = false;
                            rdbDebito.Enabled = false;
                            pnlDebito.Enabled = false;

                            //chamada da função responsável por cadatrar na base de dados e no cupom fical o pagamento realizado
                            salvaPagamentoDebito(Convert.ToDecimal(txtPagDebito.Text), sender, e);

                            //caso haja algum valor no campo referente ao pagamento em dinheiro atuaiza os valores visualizados no form
                            if (!txtPagDebito.Text.Equals(""))
                            {
                                //chamada da função que realiza essas alterações
                                atualizaValores(Convert.ToDecimal(txtPagDebito.Text));
                            }

                            //limpa o campo referente ao valor do pagamento
                            txtPagDebito.Text = "";
                        }
                    }
                }

                //verifica se a forma de pagamento selecionada foi compra pré-paga
                else if (rdbPrePag.Checked)
                {
                    //atribui o valor da variavel boleana entrada como verdadeiro
                    entrada = true;

                    //verifica e informa o usuário caso o valor dos créditos do cliente esteja em branco, identificando que o cliente não foi registrado ou cadastrado
                    if (txtCreditosCliente.Text.Equals(""))
                    {
                        MessageBox.Show("Pagamento pré-pago somente está habilitado para clientes cadastrados e com créditos", "Ação Inválida");
                    }

                    //verifica se o valor de entrada é decimal
                    else if (!validaValorEntrada(txtCreditosCliente.Text))
                    {
                        MessageBox.Show("O valor do pagamento não corresponde a um valor valido, por favor, corrija e tente novamente", "Ação Invalida");
                    }

                    //caso o cliente possua créditos
                    else
                    {
                        //verifica e informa o usuário caso o valor do pagamento não tenha sido preenchido
                        if (txtPagPrePago.Text.Equals(""))
                        {
                            MessageBox.Show("O campo valor deve ser preenchido", "Ação Inválida");
                            btnPagamento.Enabled = true;
                        }

                        //verifica se o valor registrado como pagamento excede o valor total
                        else if (Convert.ToDecimal(txtPagPrePago.Text) > Convert.ToDecimal(txtSaldoPrePago.Text))
                        {
                            MessageBox.Show("O valor do pagamento não deve exceder o valor da compra para os pagamentos pré-pagos", "Ação Inválida");
                            btnPagamento.Enabled = true;
                        }

                        /*/verifica se o valor registrado como paramento excede o valor dos créditos do cliente
                        else if (Convert.ToDecimal(txtPagPrePago.Text) > Convert.ToDecimal(txtCreditosCliente.Text))
                        {
                            MessageBox.Show("O valor do pagamento não deve exceder o valor dos créditos que o cliente possuí", "Ação Inválida");
                        }/*/

                        //caso todos os critérios para pagamentos válidos estejam corretos
                        else
                        {
                            //verifica se o valor registrado como paramento excede o valor dos créditos do cliente
                            if (Convert.ToDecimal(txtPagPrePago.Text) > Convert.ToDecimal(txtCreditosCliente.Text))
                            {
                                MessageBox.Show("O valor do pagamento excede o valor dos créditos que o cliente possuí, a venda será realizada para pagamento posterior.", "Ação Inválida");
                            }

                            //altera a visibilidade de controles do form
                            btnDesconto.Enabled = false;
                            pnlDesconto.Enabled = false;
                            rdbPrePag.Enabled = false;
                            pnlPrePag.Enabled = false;

                            //chamada da função responsável por cadatrar na base de dados e no cupom fical o pagamento realizado
                            salvaPagamentoPrePago(Convert.ToDecimal(txtPagPrePago.Text), sender, e);

                            //caso haja algum valor no campo referente ao pagamento atualiza os valores visualizados no form
                            if (!txtPagPrePago.Text.Equals(""))
                            {
                                //chamada da função que realiza essas alterações
                                atualizaValores(Convert.ToDecimal(txtPagPrePago.Text));

                                //altera o valor total dos créditos do cliente no panel cliente
                                txtCreditosCliente.Text = (Convert.ToDecimal(txtCreditosCliente.Text) - Convert.ToDecimal(txtPrePago.Text)).ToString("0.00");
                            }                            

                            //limpa o campo referente ao valor do pagamento
                            txtPagPrePago.Text = "";
                        }
                    }
                }

                //verifica se a forma de pagamento selecionada foi cartão de crédito
                else if (rdbCredito.Checked)
                {
                    //verifica se o combobox com o numero de parcelas foi preenchido
                    if (Convert.ToInt32(cmbNumParcCredito.SelectedItem) >= 1)
                    {
                        //realiza a chamada da função para o usuário indicar que foi realizado o TEF, ou mesmo para realizar o TEF
                        if (realizaTEF())
                        {
                            //altera a visibilidade de itens do form
                            btnDesconto.Enabled = false;
                            pnlDesconto.Enabled = false;                            
                            rdbDebito.Enabled = false;
                            rdbDinheiro.Enabled = false;
                            rdbPrePag.Enabled = false;
                            rdbCredito.Enabled = false;
                            pnlCredito.Enabled = false;

                            //altera o valor da variavel local booleana para falso
                            vista = false;

                            //chamada da função responsável por cadatrar na base de dados e no cupom fical o pagamento realizado
                            salvaPagamentoCredito((Convert.ToDecimal(txtParcCred.Text) * Convert.ToInt32(cmbNumParcCredito.SelectedItem)), sender, e);

                            //caso haja algum valor no campo referente ao pagamento em dinheiro atuaiza os valores visualizados no form
                            if (!txtParcCred.Text.Equals("0.00"))
                            {
                                //chamada da função que realiza essas alterações
                                atualizaValores(Convert.ToDecimal(txtParcCred.Text) * Convert.ToInt32(cmbNumParcCredito.SelectedItem));
                            }
                        }
                    }
                    //informa o usuário da obrigatoriedade do preenchimento do número de parcelas e altera a visibilidade de elementos do form
                    else
                    {
                        btnPagamento.Enabled = true;
                        MessageBox.Show("É obrigatório o preenchimento da quantidade de parcelas", "Ação Inválida");
                    }
                }

                //verifica se a forma de pagamento selecionada foi cheque
                else if (rdbCheque.Checked)
                {
                    //verifica se houve o preenchimento do número de parcelas
                    if (Convert.ToInt32(cmbNumParcCheque.SelectedItem) >= 1)
                    {
                        //altera a visibilidade de itens do form
                        btnDesconto.Enabled = false;
                        pnlDesconto.Enabled = false;                        
                        rdbDebito.Enabled = false;
                        rdbDinheiro.Enabled = false;
                        rdbPrePag.Enabled = false;
                        rdbCheque.Enabled = false;
                        pnlCheque.Enabled = false;

                        //altera o valor da variavel local booleana para falso
                        vista = false;

                        //chamada da função responsável por cadatrar na base de dados e no cupom fical o pagamento realizado
                        salvaPagamentoCheque((Convert.ToDecimal(txtParcCheq.Text) * Convert.ToInt32(cmbNumParcCheque.SelectedItem)), sender, e);

                        //caso haja algum valor no campo referente ao pagamento em dinheiro atuaiza os valores visualizados no form
                        if (!txtParcCred.Text.Equals("0.00"))
                        {
                            //chamada da função que realiza essas alterações
                            atualizaValores(Convert.ToDecimal(txtParcCheq.Text) * Convert.ToInt32(cmbNumParcCheque.SelectedItem));
                        }
                    }

                    //informa o usuário da obrigatoriedade do preenchimento do número de parcelas e altera a visibilidade de elementos do form
                    else
                    {
                        btnPagamento.Enabled = true;
                        MessageBox.Show("É obrigatório o preenchimento da quantidade de parcelas", "Ação Inválida");
                    }
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 915 a 1159";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 915 a 1159, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //função que alteras a visibilidade dos elementos do form
        private void atualizaValores(decimal valorRecebido)
        {
            //tenta alterar os valores dos elementos exibidos no form
            try
            {
                //se a variavel local booleana vista possuir valor verdadeiro, indicando pagamento em dinheiro, débito, ou pré-pago
                if (vista)
                {
                    txtSaldoDinheiro.Text = (Convert.ToDecimal(txtSaldoDinheiro.Text) - valorRecebido).ToString("0.00");
                    txtSaldoPrePago.Text = (Convert.ToDecimal(txtSaldoPrePago.Text) - valorRecebido).ToString("0.00");
                    txtSaldoDebito.Text = (Convert.ToDecimal(txtSaldoDebito.Text) - valorRecebido).ToString("0.00");
                    valorRecebido = valorRecebido + ((Convert.ToDecimal(txtTotal.Text) / ((Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtTotal.Text) * (Convert.ToDecimal(desconto) / 100))) / valorRecebido)) * (Convert.ToDecimal(desconto) / 100));
                    txtParcCred.Text = ((Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtRecebido.Text) + valorRecebido))).ToString("0.00");
                }

                //no caso de pagamento com crédito ou cheque
                else
                {
                    txtParcCred.Text = "0.00";
                    txtSaldoDinheiro.Text = "0.00";
                    txtSaldoPrePago.Text = "0.00";
                    txtSaldoDebito.Text = "0.00";
                }

                //altera os valores informados nos elementos do form
                txtParcCheq.Text = txtParcCred.Text;
                txtRecebido.Text = (Convert.ToDecimal(txtRecebido.Text) + valorRecebido).ToString("0.00");
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 1173 a 1197";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 1173 a 1197, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ação do botão que habilita o painel desconto
        private void btnDesconto_Click(object sender, EventArgs e)
        {
            
            //tenta habilitar o painel desconto e atribuir a variavel local desconto o valor definido
            try
            {
                //altera a visibiidade do painel pedidos no form
                pnlPedidos.Enabled = false;
                pnlDesconto.Cursor = Cursors.Arrow;

                //se o valor monetário do desconto for maior que 0
                if (valorDesc > 0)
                {
                    //atribui a variavel desconto o valor percentual referente ao total de desconto concedido
                    desconto = Convert.ToInt32(valorDesc / (valorTotal / 100));

                    //atribui a variavel max, que corresponde ao valor maximo de desconto a ser concedido o valor percentual referido de acordo com a carga de tributação e a definição para desconto maximo do modulo de gerenciamento
                    int max = Convert.ToInt32(100 * (valorTotal - (Convert.ToDecimal(txtCustoTotal.Text) + (Convert.ToDecimal(txtCustoTotal.Text) * Convert.ToDecimal(gerencia.lucroMinimo / 100)))) / valorTotal) - 1;

                    //se o valor do desconto maximo for inferior ao valor determinado no módulo gerencial
                    if (max < gerencia.maxDescPerc)
                    {
                        //define o range do traking desconto como estando entre o desconto pré-existente vindo do PDV e o maximo atribuido em max
                        trkDesconto.SetRange(desconto, max);
                    }

                    //caso o deconto maximo ermitido pelo m´dulo gerencial for inferior ao valor atribuido a max
                    else
                    {
                        //define o range do traking desconto como estando entre o desconto pré-existente vindo do PDV e o maximo atribuido no módulo gerencial
                        trkDesconto.SetRange(desconto, gerencia.maxDescPerc);
                    }

                }

                //altera a visibilidade do painel de desconto pra o inverso da atual
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
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 1211 a 1250";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 1211 a 1250, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ação do botão cancelar pagamento e cupom fiscal
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //tenta cancelar o cupom fiscal aberto, ou último emitido, e limpar os registros do banco de dados do referido cupom
            try
            {
                //abre dialogo confirmando ação do usuário
                DialogResult = MessageBox.Show("Você tem certeza que deseja cancelar o último cupom emitido e excluir o registro de pagamento ?", "Confirmação Necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //caso a resposta seja de confirmação
                if (DialogResult.Equals(DialogResult.Yes))
                {
                    //verifica se a variavel local booleana que indica que indica que o último pagamento realizado se encontra válido no BD
                    if (!ultimoExcluido)
                    {
                        //altera o valor desta para indicar que o último pagamento efetuado
                        ultimoExcluido = true;                        

                        //pesquisa a lista de pagamentos referente ao ultimo pagamento e atribui a variavel listaPagamentos
                        List<Pagamentos> listaPagamentos = controle.PesquisaUltimoPagamento();

                        //cria uma lista de inteiros vazia
                        List<int> vendasRealizadas = new List<int>();

                        foreach (Pagamentos value in listaPagamentos)
                        {
                            //para cada pagamento identificado na variavel listaPagamentos atribui valor ao inteiros
                            int idPag = value.id;
                            int idMov = Convert.ToInt32(value.id_movimento);
                            int vendaID = 0;

                            //pesquisa e atribui a listaPagVenda os elementos de associação entre pagamento e venda
                            List<Pagamentos_Vendas> listaPagVenda = controle.PesquisaPagVendaIdPagamento(value.id);

                            foreach (Pagamentos_Vendas pagVend in listaPagVenda)
                            {
                                //para cada elemento associativo encontrado remove o mesmo do BD e salva esta atualização
                                controle.RemovePagamentoVenda(pagVend);
                                controle.SalvaAtualiza();

                                //atribui o id da venda a variavel
                                vendaID = pagVend.id_Venda;
                            }

                            //remove do BD o registro do pagamento e salva esta alteração
                            controle.RemovePagamento(value);
                            controle.SalvaAtualiza();

                            //remove o movimento financeiro referente ao pagamento
                            Movimentos movimento = controle.PesquisaMovimentoId(idMov);
                            controle.RemoveMovimento(movimento);
                            controle.SalvaAtualiza();

                            //cria uma lista com os movimentos relacionados ao pagamento, desde tributação, a dedução do preço do estoque
                            List<Movimentos> ListaMovimento = controle.PesquisaMovimentoReferIdPagamento(idPag);

                            foreach (Movimentos mov in ListaMovimento)
                            {
                                //remove cada movimento desta lista do BD e salva esta atualização
                                controle.RemoveMovimento(mov);
                                controle.SalvaAtualiza();
                            }

                            //cria variavel local booleana para identificar a pré-existencia de cada venda na variavel lista de inteiros
                            bool novoElemento = true;
                            for (int i = 0; i < vendasRealizadas.Count; i++)
                            {
                                //verifica se o id da venda já foi adicionado a lista de inteiros
                                if (vendasRealizadas[i] == vendaID)
                                {
                                    //altera o valor da variavel que responde essa verificação
                                    novoElemento = false;
                                }
                            }

                            //não havendo alteração nessa variavel
                            if (novoElemento)
                            {
                                //adiciona o id da venda a lista de inteiros vendasRealizadas
                                vendasRealizadas.Add(vendaID);
                            }
                        }

                        foreach (int value in vendasRealizadas)
                        {
                            //para cada id de venda dessa lista cria uma lista de produtos da referida venda
                            List<Vendas_Produtos> listaProdVendido = controle.PesquisaProdutosVenda(value);
                            foreach (Vendas_Produtos prodVend in listaProdVendido)
                            {
                                //para cada propriedade associativa entre produto e venda atribui o produto por pesquisa a variavel prod e adiciona a quantidade vendida ao estoque, movimento de retorno
                                Model.Produtos prod = controle.PesquisaProdutoId(Convert.ToInt32(prodVend.id_produto));
                                prod.Estoque.qnt_atual = prod.Estoque.qnt_atual + prodVend.quantidade;
                                //remove a associação entre produtos e venda, não testado
                                controle.removeProdutoVenda(prodVend);
                                //salva as alterações
                                controle.SalvaAtualiza();
                            }
                        }

                        //altera a propriedade da entidade cliente adicionando os créditos utilizados no caso de pagamento pré pago
                        cliente.creditos = cliente.creditos + clienteCreditos;
                        controle.SalvaAtualiza();
                        MessageBox.Show("Exclusão do pagamento realizada com sucesso", "Ação Bem Sucedida");

                        //função de impressão de Cupom Fiscal
                        //cancela o cupom em aberto ou último emitido na ausência deste
                        BemaFI32.Bematech_FI_CancelaCupom();

                        //BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_AbreCupomMFD("", "", ""));
                        //BemaFI32.Bematech_FI_FechaComprovanteNaoFiscalVinculado();                        

                        //limpa o formulário, retorma a codição inicial
                        btnLimpar_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("O último pagamento já foi excluído, para exclusão do cupom aberto utilize a opção \"Cancela Último Cupom\"", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                    
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 1264 a 1375";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 1264 a 1375, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //alteração dos controle de visibilidade do form ao selecionar forma de pagamento em dinheiro
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
            btnCancelaAberto.Enabled = true;
        }

        //alteração dos controle de visibilidade do form ao selecionar forma de pagamento em cartão de débito
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
            btnCancelaAberto.Enabled = true;
        }

        //alteração dos controle de visibilidade do form ao selecionar forma de pagamento para pré pago
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
            btnCancelaAberto.Enabled = true;
        }

        //alteração dos controle de visibilidade do form ao selecionar forma de pagamento em cartão de crédito
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
            btnCancelaAberto.Enabled = true;
        }

        //alteração dos controle de visibilidade do form ao selecionar forma de pagamento em cheque
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
            btnCancelaAberto.Enabled = true;
        }

        //ação realizada ao realizar alterações no combobox com o número de parcelas do cartão de crédito
        private void cmbNumParcCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tenta adicionar a variavel juros o valor do percentual devido a quantidade de parcelas selecionada, em acordo com a defiição do módulo gerencia
            try
            {
                //zera e instancia o valor de juros
                decimal juros = 0;

                //caso tenha sido selecionado algum item na combobox referente a quantidade de parcelas
                if (cmbNumParcCredito.SelectedItem != null)
                {
                    //altera o valor do juros de acordo com a seleção
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
                    //altera o valor da parcela de acordo com os juros e a quantidade
                    txtParcCred.Text = Convert.ToDecimal(((Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text)) * juros) + (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text)) / Convert.ToInt32(cmbNumParcCredito.SelectedItem)).ToString("0.00");
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 1473 a 1522";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 1473 a 1522, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ação realizada ao realizar alterações no combobox com o número de parcelas do cheque
        private void cmbNumParcCheque_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tenta adicionar a variavel juros o valor percentual devido a quantidade de parcelas selecionada, em acordo com a defiição do módulo gerencia
            try
            {
                //zera e instancia o valor de juros
                decimal juros = 0;

                //caso tenha sido selecionado algum item na combobox referente a quantidade de parcelas
                if (cmbNumParcCheque.SelectedItem != null)
                {
                    //altera o valor do juros de acordo com a seleção
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
                    //altera o valor da parcela de acordo com os juros e a quantidade
                    txtParcCheq.Text = Convert.ToDecimal(((Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text)) * juros) + (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text)) / Convert.ToInt32(cmbNumParcCheque.SelectedItem)).ToString("0.00");
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 1538 a 1589";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 1538 a 1589, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //rotina para salvar pagamentos em cheque e os respectivos movimentos
        private void salvaPagamentoCheque(decimal valor, object sender, EventArgs e)
        {
            //tenta definir o valor de acréscimo e o valor pago
            try
            {
                //define a variavel o valor referente aos juros devido da forma de pagamento selecionada
                valorAcres = valor - (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text));

                //incrementa a variavel com o valor do pagamento
                valorPago = valorPago + valor;

                //verifica se o valor pago é inferior ao total, devido a necessidade de arredondamento para 2 casas decimais pode faltar 1 centavo por parcela para a conta fechar em alguns casos
                if (Convert.ToDecimal(txtParcCheq.Text) * Convert.ToInt32(cmbNumParcCheque.SelectedItem) < valor)
                {
                    //acrescenta 1 centavo por parcela no valor pago, não cobra do cliente, considera como pago
                    valorPago = valorPago + (0.01M * Convert.ToInt32(cmbNumParcCheque.SelectedItem));
                }                

                //verifica se a variavel booleana que representa entrada possuí valor verdadeiro
                if (entrada)
                {
                    //em caso positivo acrescenta o valor 1 a ent
                    ent = 1;
                }
                //realiza a chamada da DialogBox que salva o número do primeiro e do último cheque ultilizados para o pagamento
                ShowMyDialogBox();

                //Atribuição de valores ao objeto da classe CupomFiscal PagCheque
                PagCheque.formaPagamento = "Cheque";
                PagCheque.acrescimo = valorAcres.ToString("0.00");
                PagCheque.desconto = "0.00";
                PagCheque.valorPagamento = valor.ToString("0.00");
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 1604 a 1633";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 1604 a 1633, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //tenta instanciar e salvar os movimentos e pagamentos referentes 
            try
            {
                //laço de repetição para cada parcela
                for (int i = 0; i < Convert.ToInt32(cmbNumParcCheque.SelectedItem); i++)
                {
                    //instancia e salva o movimento referente a respectiva parcela
                    Movimentos movimento = new Movimentos();
                    controle.SalvarMovimento(movimento);

                    //verifica se houve entrada
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
                    controle.SalvaAtualiza();

                    //instancia e salva o pagamento referente a parcela
                    Pagamentos pagamento = new Pagamentos();
                    controle.SalvarPagamento(pagamento);
                    pagamento.id_movimento = movimento.id;
                    pagamento.dataPagamento = DateTime.Today.AddMonths(i + ent);
                    pagamento.formaPag = "Cheque";
                    pagamento.valorTotal = Convert.ToDecimal(txtTotal.Text);
                    pagamento.valorParcela = valor / Convert.ToInt32(cmbNumParcCheque.SelectedItem);

                    //atribui para o pagamento os valores digitados no DialogBox
                    pagamento.numChequePrimeiro = chequePrim;
                    pagamento.numChequeUltimo = chequeUlt;

                    //caso não haja entrada e o pagamento foi a vista
                    if (!entrada && Convert.ToInt32(cmbNumParcCheque.SelectedItem) == 1)
                    {
                        pagamento.tipoPag = "Total";
                        pagamento.numParcela = 1;
                        pagamento.qntParcelas = 1;
                    }

                    //caso não haja entrada, haja mais de uma parcela selecinada e não seja a primeira iteração
                    else if (!entrada && Convert.ToInt32(cmbNumParcCheque.SelectedItem) != 1 && i >= 1)
                    {
                        pagamento.tipoPag = "Parcelado";
                        pagamento.numParcela = i;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCheque.SelectedItem);
                    }

                    //caso haja entrada e o restante tenha sido selecionado em parcela única
                    else if (entrada && Convert.ToInt32(cmbNumParcCheque.SelectedItem) == 1)
                    {
                        pagamento.tipoPag = "Parcial";
                        pagamento.numParcela = 0;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCheque.SelectedItem);
                    }

                    //caso haja entrada e mais de uma parcela selecionada como forma de pagamento
                    else if (entrada && Convert.ToInt32(cmbNumParcCheque.SelectedItem) >= 2)
                    {
                        pagamento.tipoPag = "Entrada + Parcelas";
                        pagamento.numParcela = i + 1;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCheque.SelectedItem);
                    }

                    controle.SalvaAtualiza();

                    listaIdPagamentos.Add(pagamento.id);

                    //salva o movimento referente a tributação
                    Movimentos movimentoImposto = new Movimentos();
                    controle.SalvarMovimento(movimentoImposto);
                    movimentoImposto.data = DateTime.Today;
                    movimentoImposto.desc = "Trib Pag:" + pagamento.id;
                    movimentoImposto.id_tipo = 11;
                    movimentoImposto.valor = Convert.ToDecimal(pagamento.valorTotal * 0.1039M / Convert.ToInt32(cmbNumParcCheque.SelectedItem));
                    controle.SalvaAtualiza();

                    //salva o movimento referente a saída de mercadoria do estoque
                    Movimentos movimentoMercadoria = new Movimentos();
                    controle.SalvarMovimento(movimentoMercadoria);
                    movimentoMercadoria.data = DateTime.Today;
                    movimentoMercadoria.desc = "Estoque Pag:" + pagamento.id;
                    movimentoMercadoria.id_tipo = 65;
                    movimentoMercadoria.valor = custoAux / Convert.ToInt32(cmbNumParcCheque.SelectedItem);
                    controle.SalvaAtualiza();

                    //para cada venda do cupom fical instancia e salva uma associação com o pagamento
                    //*/foreach (Vendas value in listaVendas)
                    //{
                        Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                        controle.SalvaPagamentoPedido(pagamentoPedido);
                        pagamentoPedido.id_Venda = venda.id;
                        pagamentoPedido.id_Pagamento = pagamento.id;

                        controle.SalvaAtualiza();
                    //}
                }

                //informa ao usuário a conclusão bem sucedida
                MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");

                //chama a função que realiza o fechamento do cupom fiscal
                imprimeCupomFiscal();

                //limpa o formulário
                btnLimpar_Click(sender, e);

            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 1642 a 1752";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 1642 a 1752, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //rotina para salvar pagamentos em cartão de crédito e os respectivos movimentos
        private void salvaPagamentoCredito(decimal valor, object sender, EventArgs e)
        {
            //tenta definir o valor de acréscimo e o valor pago
            try
            {
                //laço de repetição para cada parcela
                valorAcres = valor - (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtRecebido.Text));

                //incrementa a variavel com o valor do pagamento
                valorPago = valorPago + valor;

                //verifica se o valor pago é inferior ao total, devido a necessidade de arredondamento para 2 casas decimais pode faltar 1 centavo por parcela para a conta fechar em alguns casos
                if (Convert.ToDecimal(txtParcCred.Text) * Convert.ToInt32(cmbNumParcCredito.SelectedItem) < valor)
                {
                    //acrescenta ao valor pago em cada parcela 1 centavo
                    valorPago = valorPago + (0.01M * Convert.ToInt32(cmbNumParcCredito.SelectedItem));
                }

                //atribuí zero a variável inteira que representa entrada
                ent = 0;

                //verifica se a variavel booleana que representa entrada possuí valor verdadeiro
                if (entrada)
                {
                    //em caso positivo atribuí o valor 1 a ent
                    ent = 1;
                }

                //Atribuição de valores ao objeto CupomFiscal PagCredito
                PagCredito.formaPagamento = "Crédito";
                PagCredito.acrescimo = valorAcres.ToString("0.00");
                PagCredito.desconto = "0.00";
                PagCredito.valorPagamento = valor.ToString("0.00");

            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 1766 a 1900";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 1766 a 1900, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //tenta instanciar e salvar os movimentos e pagamentos referentes
            try
            {
                //laço de repetição para cada parcela
                for (int i = 0; i < Convert.ToInt32(cmbNumParcCredito.SelectedItem); i++)
                {
                    //instancia e salva o movimento referente o pagamento
                    Movimentos movimento = new Movimentos();
                    controle.SalvarMovimento(movimento);
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
                    controle.SalvaAtualiza();

                    //instancia e salva o pagamento 
                    Pagamentos pagamento = new Pagamentos();
                    controle.SalvarPagamento(pagamento);
                    pagamento.id_movimento = movimento.id;
                    pagamento.dataPagamento = DateTime.Today.AddMonths(i + ent);
                    pagamento.formaPag = "C.Crédito";
                    pagamento.valorTotal = Convert.ToDecimal(txtTotal.Text);
                    pagamento.valorParcela = valor / Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    pagamento.numChequePrimeiro = "";
                    pagamento.numChequeUltimo = "";

                    //caso o pagamento seja sem entrada em parcela única
                    if (!entrada && Convert.ToInt32(cmbNumParcCredito.SelectedItem) == 1)
                    {
                        pagamento.tipoPag = "Total";
                        pagamento.numParcela = 1;
                        pagamento.qntParcelas = 1;
                    }

                    //caso o pagamento seja sem entrada, em mais de uma parcela e não seja a primeira iteração
                    else if (!entrada && Convert.ToInt32(cmbNumParcCredito.SelectedItem) != 1 && i >= 1)
                    {
                        pagamento.tipoPag = "Parcelado";
                        pagamento.numParcela = i;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    }

                    //caso haja entrada e o saldo restante seja pago em parcela única
                    else if (entrada && Convert.ToInt32(cmbNumParcCredito.SelectedItem) == 1)
                    {
                        pagamento.tipoPag = "Parcial";
                        pagamento.numParcela = 0;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    }

                    //caso haja entrada e o restante seja quitado em mais de uma parcela
                    else if (entrada && Convert.ToInt32(cmbNumParcCredito.SelectedItem) >= 2)
                    {
                        pagamento.tipoPag = "Entrada + Parcelas";
                        pagamento.numParcela = i + 1;
                        pagamento.qntParcelas = Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    }
                    controle.SalvaAtualiza();

                    listaIdPagamentos.Add(pagamento.id);

                    //instancia e salva o movimento referente ao imposto
                    Movimentos movimentoImposto = new Movimentos();
                    controle.SalvarMovimento(movimentoImposto);
                    movimentoImposto.data = DateTime.Today;
                    movimentoImposto.desc = "Trib Pag:" + pagamento.id;
                    movimentoImposto.id_tipo = 11;
                    movimentoImposto.valor = Convert.ToDecimal(pagamento.valorTotal * 0.1039M / Convert.ToInt32(cmbNumParcCredito.SelectedItem));
                    controle.SalvaAtualiza();

                    //instancia e salva o movimento referente a saída de mercadoria do estoque
                    Movimentos movimentoMercadoria = new Movimentos();
                    controle.SalvarMovimento(movimentoMercadoria);
                    movimentoMercadoria.data = DateTime.Today;
                    movimentoMercadoria.desc = "Estoque Pag:" + pagamento.id;
                    movimentoMercadoria.id_tipo = 65;
                    movimentoMercadoria.valor = custoAux / Convert.ToInt32(cmbNumParcCredito.SelectedItem);
                    controle.SalvaAtualiza();

                    //para cada venda do cupom fical instancia e salva uma associação com o pagamento
                    //*/foreach (Vendas value in listaVendas)
                    //{
                        Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                        controle.SalvaPagamentoPedido(pagamentoPedido);
                        pagamentoPedido.id_Venda = venda.id;
                        pagamentoPedido.id_Pagamento = pagamento.id;

                        controle.SalvaAtualiza();
                    //}
                }

                //informa ao usuário a conclusão bem sucedida
                MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");

                //chama a função que realiza o fechamento do cupom fiscal
                imprimeCupomFiscal();

                //limpa o formulário
                btnLimpar_Click(sender, e);

            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 1766 a 1900";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 1766 a 1900, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //rotina para salvar pagamentos pré-pagos e os respectivos movimentos
        private void salvaPagamentoPrePago(decimal valor, object sender, EventArgs e)
        {
            //tenta atribuir a variavel o valor pago e instanciar e salvar os movimentos e o pagamento
            try
            {
                //incrementa o valor pago com o valor do pagamento
                valorPago = valorPago + valor;                

                //Atribuição de valores ao objeto CupomFiscal PagPrePago
                PagPrePago.formaPagamento = "Pré-Pago";
                PagPrePago.acrescimo = valorAcres.ToString("0.00");
                PagPrePago.desconto = "0.00";
                PagPrePago.valorPagamento = valor.ToString("0.00");

                //instancia o movimento
                Movimentos movimento = new Movimentos();
                controle.SalvarMovimento(movimento);
                movimento.id_tipo = 30;
                movimento.valor = valor;
                movimento.data = DateTime.Now;

                //instancia o pagamento
                Pagamentos pagamento = new Pagamentos();
                controle.SalvarPagamento(pagamento);
                pagamento.dataPagamento = DateTime.Now;
                pagamento.formaPag = "Pré-Pago";
                pagamento.valorTotal = Convert.ToDecimal(txtTotal.Text);
                pagamento.valorParcela = valor;
                pagamento.numChequePrimeiro = "";
                pagamento.numChequeUltimo = "";
                
                //se o pagamento for superior ou igual ao valor pago
                if (Convert.ToDecimal(txtSaldoPrePago.Text) <= valor)
                {
                    movimento.desc = "Total Pré-Pago";
                    pagamento.tipoPag = "Total";
                    pagamento.numParcela = 1;
                    pagamento.qntParcelas = 1;
                }

                //caso contrario
                else
                {
                    movimento.desc = "Entrada pré-pago";
                    pagamento.tipoPag = "Entrada";
                    pagamento.numParcela = 0;
                    pagamento.qntParcelas = 0;
                }

                //salva as alterações
                controle.SalvaAtualiza();

                listaIdPagamentos.Add(pagamento.id);
                pagamento.id_movimento = movimento.id;

                //instancia e salva o movimento referente a tributação
                Movimentos movimentoImposto = new Movimentos();
                controle.SalvarMovimento(movimentoImposto);
                movimentoImposto.data = DateTime.Today;
                movimentoImposto.desc = "Trib Pag:" + pagamento.id;
                movimentoImposto.id_tipo = 11;
                movimentoImposto.valor = Convert.ToDecimal(pagamento.valorTotal * 0.1039M);
                controle.SalvaAtualiza();

                //instancia e salva o movimento de saída das mercadorias do estoque
                Movimentos movimentoMercadoria = new Movimentos();
                controle.SalvarMovimento(movimentoMercadoria);
                movimentoMercadoria.data = DateTime.Today;
                movimentoMercadoria.desc = "Estoque Pag:" + pagamento.id;
                movimentoMercadoria.id_tipo = 65;
                movimentoMercadoria.valor = custoAux;
                controle.SalvaAtualiza();

                //altera o desconto atribuido ao objeto da classe CupomFiscal
                PagPrePago.desconto = Convert.ToDecimal(PagPrePago.desconto) + Convert.ToDecimal((Convert.ToDecimal(txtTotal.Text) * trkDesconto.Value / 100) / (Convert.ToDecimal(txtValorTotal.Text) / valor)).ToString("0.00");

                //para cada venda do cupom fical instancia e salva uma associação com o pagamento
                //*/foreach (Vendas value in listaVendas)
                //{
                    venda = controle.PesquisaVendaID(venda.id);
                    venda.desconto = trkDesconto.Value;                    

                    Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                    controle.SalvaPagamentoPedido(pagamentoPedido);
                    pagamentoPedido.id_Venda = venda.id;
                    pagamentoPedido.id_Pagamento = pagamento.id;

                    controle.SalvaAtualiza();
                // }

                //altera a instancia de cliente atribuida, corrige a quantidade de créditos que este possuí e salva a alteração
                clienteCreditos = Convert.ToDouble(valor);
                cliente.creditos = cliente.creditos - clienteCreditos;
                controle.SalvaAtualiza();

                //novamente verifica se o pagamento corresponde ao total devido em caso positivo concluí a venda
                if (Convert.ToDecimal(txtSaldoPrePago.Text) <= valor)
                {
                    //informa ao usuário a conclusão bem sucedida
                    MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");

                    //chama a função que realiza o fechamento do cupom fiscal
                    imprimeCupomFiscal();

                    //limpa o formulário
                    btnLimpar_Click(sender, e);
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 1927 a 2028";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 1927 a 2028, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //rotina para salvar pagamentos em cartão de débito e os respectivos movimentos
        private void salvaPagamentoDebito(decimal valor, object sender, EventArgs e)
        {
            //tenta atribuir a variavel o valor pago e instanciar e salvar os movimentos e o pagamento
            try
            {
                //incrementa o valor pago com o valor do pagamento
                valorPago = valorPago + valor;

                //Atribuição de valores ao objeto CupomFiscal PagDebito
                PagDebito.formaPagamento = "Débito";
                PagDebito.acrescimo = valorAcres.ToString("0.00");
                PagDebito.desconto = "0.00";
                PagDebito.valorPagamento = valor.ToString("0.00");

                //instancia o movimento
                Movimentos movimento = new Movimentos();
                controle.SalvarMovimento(movimento);
                movimento.id_tipo = 33;
                movimento.valor = valor;
                movimento.data = DateTime.Now;

                //instancia o pagamento
                Pagamentos pagamento = new Pagamentos();
                controle.SalvarPagamento(pagamento);
                pagamento.dataPagamento = DateTime.Now;
                pagamento.formaPag = "Débito";
                pagamento.valorTotal = Convert.ToDecimal(txtTotal.Text);
                pagamento.valorParcela = valor;
                pagamento.numChequePrimeiro = "";
                pagamento.numChequeUltimo = "";

                //se o pagamento for superior ou igual ao valor pago
                if (Convert.ToDecimal(txtSaldoDebito.Text) <= valor)
                {
                    movimento.desc = "Total TEF Débito";
                    pagamento.tipoPag = "Total";
                    pagamento.numParcela = 1;
                    pagamento.qntParcelas = 1;
                }

                //caso contrario
                else
                {
                    movimento.desc = "Entrada TEF Débito";
                    pagamento.tipoPag = "Entrada";
                    pagamento.numParcela = 0;
                    pagamento.qntParcelas = 0;
                }

                //salva as alterações
                controle.SalvaAtualiza();

                listaIdPagamentos.Add(pagamento.id);
                pagamento.id_movimento = movimento.id;

                //instancia e salva o movimento referente a tributação
                Movimentos movimentoImposto = new Movimentos();
                controle.SalvarMovimento(movimentoImposto);
                movimentoImposto.data = DateTime.Today;
                movimentoImposto.desc = "Trib Pag:" + pagamento.id;
                movimentoImposto.id_tipo = 11;
                movimentoImposto.valor = Convert.ToDecimal(pagamento.valorTotal * 0.1039M);
                controle.SalvaAtualiza();

                //instancia e salva o movimento de saída das mercadorias do estoque
                Movimentos movimentoMercadoria = new Movimentos();
                controle.SalvarMovimento(movimentoMercadoria);
                movimentoMercadoria.data = DateTime.Today;
                movimentoMercadoria.desc = "Estoque Pag:" + pagamento.id;
                movimentoMercadoria.id_tipo = 65;
                movimentoMercadoria.valor = custoAux;
                controle.SalvaAtualiza();

                //altera o desconto atribuido ao objeto da classe CupomFiscal
                PagDebito.desconto = Convert.ToDecimal(PagDebito.desconto) + Convert.ToDecimal((Convert.ToDecimal(txtTotal.Text) * trkDesconto.Value / 100 ) / (Convert.ToDecimal(txtValorTotal.Text) / valor)).ToString("0.00");

                //para cada venda do cupom fical instancia e salva uma associação com o pagamento
                //*/foreach (Vendas value in listaVendas)
                //{
                    venda = controle.PesquisaVendaID(venda.id);
                    venda.desconto = trkDesconto.Value;                    

                    Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                    controle.SalvaPagamentoPedido(pagamentoPedido);
                    pagamentoPedido.id_Venda = venda.id;
                    pagamentoPedido.id_Pagamento = pagamento.id;

                    controle.SalvaAtualiza();
                //}

                //novamente verifica se o pagamento corresponde ao total devido em caso positivo concluí a venda
                if (Convert.ToDecimal(txtSaldoDebito.Text) <= valor)
                {
                    //informa ao usuário a conclusão bem sucedida
                    MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");

                    //chama a função que realiza o fechamento do cupom fiscal
                    imprimeCupomFiscal();

                    //limpa o formulário
                    btnLimpar_Click(sender, e);
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 2042 a 2139";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 2042 a 2139, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //rotina para salvar pagamentos em dinheiro e os respectivos movimentos
        private void salvaPagamentoDinheiro(decimal valor, object sender, EventArgs e)
        {
            //tenta atribuir a variavel o valor pago e instanciar e salvar os movimentos e o pagamento
            try
            {
                //incrementa o valor pago com o valor do pagamento
                valorPago = valorPago + valor;

                //Atribuição de valores ao objeto CupomFiscal PagDinheiro
                PagDinheiro.formaPagamento = "Dinheiro";
                PagDinheiro.acrescimo = valorAcres.ToString("0.00");
                PagDinheiro.desconto = "0.00";
                PagDinheiro.valorPagamento = valor.ToString("0.00");                

                //instancia o movimento
                Movimentos movimento = new Movimentos();
                controle.SalvarMovimento(movimento);
                movimento.id_tipo = 19;
                movimento.valor = valor;
                movimento.data = DateTime.Now;

                //instancia o pagamento
                Pagamentos pagamento = new Pagamentos();
                controle.SalvarPagamento(pagamento);
                pagamento.dataPagamento = DateTime.Now;
                pagamento.formaPag = "Dinheiro";
                pagamento.valorTotal = Convert.ToDecimal(txtTotal.Text);
                pagamento.valorParcela = valor;
                pagamento.numChequePrimeiro = "";
                pagamento.numChequeUltimo = "";

                //caso o pagamento seja superior ou equivalente ao total devido
                if (Convert.ToDecimal(txtSaldoDinheiro.Text) <= valor)
                {
                    movimento.desc = "Total a dinheiro";
                    pagamento.tipoPag = "Total";
                    pagamento.numParcela = 1;
                    pagamento.qntParcelas = 1;
                }

                //caso contrario
                else
                {
                    movimento.desc = "Entrada a dinheiro";
                    pagamento.tipoPag = "Entrada";
                    pagamento.numParcela = 0;
                    pagamento.qntParcelas = 0;
                }
              
                controle.SalvaAtualiza();

                listaIdPagamentos.Add(pagamento.id);
                pagamento.id_movimento = movimento.id;

                //instancia e salva o movimento referente a tributação
                Movimentos movimentoImposto = new Movimentos();
                controle.SalvarMovimento(movimentoImposto);
                movimentoImposto.data = DateTime.Today;
                movimentoImposto.desc = "Trib Pag:" + pagamento.id;
                movimentoImposto.id_tipo = 11;
                movimentoImposto.valor = Convert.ToDecimal(pagamento.valorTotal * 0.1039M);
                controle.SalvaAtualiza();

                //instancia e salva o movimento referente a saída de mercadoria
                Movimentos movimentoMercadoria = new Movimentos();
                controle.SalvarMovimento(movimentoMercadoria);
                movimentoMercadoria.data = DateTime.Today;
                movimentoMercadoria.desc = "Estoque Pag:" + pagamento.id;
                movimentoMercadoria.id_tipo = 65;
                movimentoMercadoria.valor = custoAux;
                controle.SalvaAtualiza();

                //altera o desconto atribuido ao objeto da classe CupomFiscal
                PagDinheiro.desconto = Convert.ToDecimal(PagDinheiro.desconto) + Convert.ToDecimal((Convert.ToDecimal(txtTotal.Text) * trkDesconto.Value / 100) / (Convert.ToDecimal(txtValorTotal.Text) / valor)).ToString("0.00");

                //para cada venda do cupom fical instancia e salva uma associação com o pagamento
                //foreach (Vendas value in listaVendas)
                //{
                    venda = controle.PesquisaVendaID(venda.id);
                    venda.desconto = trkDesconto.Value;                    

                    Pagamentos_Vendas pagamentoPedido = new Pagamentos_Vendas();
                    controle.SalvaPagamentoPedido(pagamentoPedido);
                    pagamentoPedido.id_Venda = venda.id;
                    pagamentoPedido.id_Pagamento = pagamento.id;

                    controle.SalvaAtualiza();
                //}

                //verifica se o valor pago corresponde ao valor devido, em caso positivo concluí a venda
                if (Convert.ToDecimal(txtSaldoDinheiro.Text) <= valor)
                {
                    //informa ao usuário a conclusão bem sucedida
                    MessageBox.Show("Venda concluída com sucesso, obrigado", "Ação Bem Sucedida");

                    //chama a função que realiza o fechamento do cupom fiscal
                    imprimeCupomFiscal();

                    //limpa o formulário
                    btnLimpar_Click(sender, e);
                }
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 2153 a 2248";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 2153 a 2248, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //DialogBox para confirmação de TEF, futuramente incluirá o TEF esta função
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

        //DialogBox para cadastro dos números de primeiro e último cheque
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
        
        //função responsável pelo impressão do cupom fiscal
        private void imprimeCupomFiscal()
        {
            //tenta atribuir os totais de descontos e acréscimos, iniciar o fechamento
            try
            {
                //atribui as variaveis o total de descontos e acréscimos
                valorDesc = Convert.ToDecimal(PagDinheiro.desconto) + Convert.ToDecimal(PagDebito.desconto) + Convert.ToDecimal(PagPrePago.desconto);
                valorAcres = (Convert.ToDecimal(PagCredito.acrescimo) + Convert.ToDecimal(PagCheque.acrescimo));

                //inicializa as variaveis com valor vazio
                string AcrescimoDesconto = "";
                string ValorAcrescimoDesconto = "";

                //verifica qual o maior valor entre descontos e acréscimos e atribui as variaveis a diferença total
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

                //função de impressão de Cupom Fiscal
                //Abre o cupom fiscal e imprime o total mais valor acrescido/desconto
                //ABERTURA DO FECHAMENTO
                //Bematech_FI_IniciaFechamentoCupom(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
                BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_IniciaFechamentoCupom(AcrescimoDesconto, "$", ValorAcrescimoDesconto));

                //imprime cada valor e sua respectiva forma de pagamento
                //PAGAMENTOS
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


                MessageBox.Show( "Implementação SAT pendente de teste, linha 2410 Caixa.cs", "Aviso");
                /*
                //informação da software house, especificação SAT
                BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_DadosSoftwareHouseSAT(cnpj, assinaturaSoftHouse));
                //informações e retorno SAT
                BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_UltimasInformacoesSAT(ref chaveAcesso, ref numeroCupom, ref NumeroSAT));
                BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_RetornaMensagemSeFazSAT(ref message, ref code, ref errorMessage, ref errorCode));
                //*/

                //encerra o cupom fiscal com mensagem personalizada, inserindo valor aproximado de tributação
                //CONCLUI O FECHAMENTO
                if (AcrescimoDesconto.Equals("D"))
                {
                    BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_TerminaFechamentoCupom("O Alemao da Construcao agradece sua preferencia\n\nTrib Aprox R$: " + ((valorTotal - Convert.ToDecimal(ValorAcrescimoDesconto)) * 0.0552M).ToString("0.00") + " Federal e " + ((valorTotal - Convert.ToDecimal(ValorAcrescimoDesconto)) * 0.0486M).ToString("0.00") + " Estadual\nFonte: SEBRAE\n\nAtendido por: " + user.nome));
                }
                else
                {
                    BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_TerminaFechamentoCupom("O Alemao da Construcao agradece sua preferencia\n\nTrib Aprox R$: " + ((valorTotal + Convert.ToDecimal(ValorAcrescimoDesconto)) * 0.0552M).ToString("0.00") + " Federal e " + ((valorTotal + Convert.ToDecimal(ValorAcrescimoDesconto)) * 0.0486M).ToString("0.00") + " Estadual\nFonte: SEBRAE\n\nAtendido por: " + user.nome));
                }

                try
                {
                    foreach (int value in listaIdPagamentos)
                    {
                        controle.PesquisaPagamentoId(value).status = 1;
                        controle.SalvaAtualiza();
                    }
                }
                catch
                {

                }
                btnLimpar.PerformClick();
                
            }
            catch
            {
                //havendo erro na execução das instruções envia email ao desenvolvedor e mensagem de erro desconhecido ao usuário
                erro = "Caixa.cs, linhas 2294 a 2355";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Caixa.cs, linhas 2294 a 2355, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //função de limpeza do formulário, reestabelecendo as condições iniciais 
        private void btnLimpar_Click(object sender, EventArgs e)
        {

            //reinicializa as variaveis
            //*/listaVendas = new List<Vendas>();
            listaIdPagamentos = new List<int>();
            //*/listaNumPedidos = new List<int>();
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

            //altera as visibilidades do form
            pnlDesconto.Cursor = Cursors.Hand;
            pedidosInclusos = false;
            pnlPedidos.Enabled = true;
            btnPagamento.Enabled = false;
            btnDesconto.Enabled = false;
            btnLimpar.Enabled = true;
            btnCancelaAberto.Enabled = true;
            btnCancelar.Enabled = true;
            txtPedidoNum1.Text = "";
            pnlCheque.Enabled = false;
            pnlCredito.Enabled = false;
            pnlDebito.Enabled = false;
            pnlDinheiro.Enabled = false;
            pnlPrePag.Enabled = false;
            txtPedidoNum1.Enabled = true;            

            //zera o valor referente aos descontos
            trkDesconto.Minimum = 0;
            trkDesconto.Value = 0;

            //altera as visibilidades do panel de pedidos
            AcceptButton = btnAdicionar1;
            txtPedidoNum1.Text = "";
            txtPedidoNum1.Enabled = true;
            btnAdicionar1.Enabled = true;

            //altera os valores e visibilidades do panel clientes
            pnlCliente.Enabled = true;
            txtCreditosCliente.Text = "";
            txtCpf.Text = "";
            rdbNPnao.Checked = true;
            txtCliente.Text = "";

            //altera os valores exibidos no form
            txtDescValor.Text = "0.00";
            txtCustoTotal.Text = "0.00";
            txtValorVenda.Text = "0.00";
            txtValorTotal.Text = "0.00";

            //altera as visibilidades no panel pagamentos
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

            //altera os valores dos itens visiveis no form
            txtDinheiro.Text = "0.00";
            txtSaldoDinheiro.Text = "0.00";
            txtPagDinheiro.Text = "";
            txtDebito.Text = "0.00";
            txtSaldoDebito.Text = "0.00";
            txtPagDebito.Text = "";
            txtPrePago.Text = "0.00";
            txtSaldoPrePago.Text = "0.00";
            txtPagPrePago.Text = "";
            txtCredito.Text = "0.00";
            cmbNumParcCredito.SelectedItem = null;
            cmbNumParcCheque.SelectedItem = null;
            txtParcCred.Text = "0.00";
            txtCheque.Text = "0.00";
            txtParcCheq.Text = "0.00";
            txtRecebido.Text = "0.00";
            txtTotal.Text = "R$";
            AcrescimoDesconto = "";
            ValorAcrescimoDesconto = "";

        }

        //função responsável por confirmar a associação de cliente ou cpf/cnpj ao cupom fiscal
        private bool confirmaAssociacao(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("O cliente não deseja a identificação por CPF/CNPJ no Cupom Fiscal ? Caso cliente já identificado ou CPF/CNPJ informado clique em \"Não\" para associar outro CPF ou em \"Sim\" para associa-lo ao cupom fiscal", "Ação Necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (DialogResult.Equals(DialogResult.Yes))
            {
                if (!txtCpf.Text.Equals(""))
                {
                    rdbNPsim.Checked = true;
                    btnAdicionar1.PerformClick();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        /* 
         * Ação atribuida ao botão cancela aberto
         */
        private void btnCancelaAberto_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Você tem certeza que deseja cancelar o cupom em aberto e limpar o formulário ?", "Ação Necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Equals(DialogResult.Yes))
            {
                //função de impressão de Cupom Fiscal
                BemaFI32.Bematech_FI_CancelaCupom();

                MessageBox.Show("Cupom Fiscal cancelado com sucesso", "Ação Bem Sucedida");
                btnLimpar_Click(sender, e);
            }
        }

        private bool validaValorEntrada(string valor)
        {
            Decimal result = 0.00M;
            if(Decimal.TryParse(valor,out result))
            {
                return true;
            }
            return false;
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            DialogoConsulta consultaPreco = new DialogoConsulta(this);
            consultaPreco.Show();
            this.Hide();
        }

    }
}
