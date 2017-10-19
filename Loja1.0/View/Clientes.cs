using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Loja1._0.Model;
using Loja1._0.Control;
using System.Linq;
using Loja1._0.View;

namespace Loja1._0
{
    public partial class Clientes : Form
    {
        //declaração de variaves de uso local, tipos definidos no Control
        private Model.Usuarios user;
        Controle controle = new Controle();
        Valida validaDoc = new Valida();
        List<Estados> listaEstado = new List<Estados>();
        List<Cidades> listaCidade = new List<Cidades>();
        List<Model.Clientes> listaClientes = new List<Model.Clientes>();
        Email email = new Email();
        public static Model.Clientes cliente;

        //declaração de variaveis locais de uso exclusivo
        bool flagNovo = true;
        static double credito = 0.00;
        string erro = "";
        static decimal valor = 0;

        public Clientes(Model.Usuarios user)
        {
            //tenta iniciar o componente pesquisar a relação de clientes preencher o gridview
            try
            {
                //inicializa o componente
                this.user = user;
                InitializeComponent();

                //pesquisa a relação de clientes ativos
                listaClientes = controle.PesquisaClientesCompleta("");

                //preenche o gridviewlist com os clientes ativos
                preencheRelacao(listaClientes);
            }
            catch
            {
                //caso não seja possível a pesquisa atribui a localização a string erro, envia email ao desenvolvedor e exibe mensagem ao usuário
                erro = "Clientes.cs, linhas 32 a 42";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Clientes.cs, linhas 32 a 42, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //chamada do método de preenchimento do gridviewlist com lista de clientes passada como parametro
        private void preencheRelacao(List<Model.Clientes> listaClientes)
        {
            //tenta alterar a visibilidade, preencher e configurar o gridviewlist
            try
            {
                //configura as colunas do DataTable
                DataTable dtClientes = new DataTable();
                dtClientes.Columns.Add("cliente", typeof(string));
                dtClientes.Columns.Add("CNPJ / CPF", typeof(string));
                dtClientes.Columns.Add("Contato", typeof(string));
                dtClientes.Columns.Add("Telefone 1", typeof(string));
                dtClientes.Columns.Add("Telefone 2", typeof(string));
                dtClientes.Columns.Add("Celular", typeof(string));

                //preencher o DataTable com o conteudo da lista de clientes passada por parametro
                for (int i = 0; i < listaClientes.Count; i++)
                {
                    dtClientes.Rows.Add(listaClientes[i].nome, listaClientes[i].cpf, listaClientes[i].contato, listaClientes[i].telefone, listaClientes[i].recado, listaClientes[i].celular);
                }

                //define o DataTable como source do gridviewlist
                dgvClientes.DataSource = dtClientes;

                //configura as larguras do gridviewlist
                dgvClientes.Columns[0].Width = 252;
                dgvClientes.Columns[1].Width = 150;
                dgvClientes.Columns[2].Width = 152;
                dgvClientes.Columns[3].Width = 90;
                dgvClientes.Columns[4].Width = 90;
                dgvClientes.Columns[5].Width = 90;

                //define as cores do back e do fore quando há a seleção de um elemento do gridviewlist
                dgvClientes.RowsDefaultCellStyle.SelectionBackColor = Color.White;
                dgvClientes.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            }
            catch
            {
                //caso não seja possível a pesquisa atribui a localização a string erro, envia email ao desenvolvedor e exibe mensagem ao usuário
                erro = "Clientes.cs, linhas 57 a 87";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Clientes.cs, linhas 57 a 87, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //função do botão que realiza o encerramento do form atual
        private void btnExit_Click(object sender, EventArgs e)
        {
            //chamada do form inicial e encerramento do form atual
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        //chamada da função do botão de pesquisa de clientes 
        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            //tenta localizar clientes que respeitem parametros validos de pesquisa e exibi-los no gridviewlist
            try
            {
                //verifica se o campo cpf está vazio e o campo cliente possuí mais de 3 caracteres, informa o usuário na ocorrencia de erro
                if (txtCliente.Text.Trim().Length < 3 && txtCpf.Text.Trim().Equals(""))
                {
                    MessageBox.Show("É necessário ao menos 3 caracteres para realizar uma busca por nome", "Ação inválida");
                    btnLimpar_Click(sender, e);
                }

                //verifica se o campo cliente está vazio, e o campo cpf respeita o critério, caso contrario informa o usuário
                else if ((txtCpf.Text.Trim().Length != 11 || txtCpf.Text.Trim().Length != 14) && txtCliente.Text.Trim().Equals("") || !txtCpf.Text.All(char.IsNumber))
                {
                    MessageBox.Show("Para pesquisa por CNPJ/CPF, por favor, utilize a numeração sem pontuação", "Ação inválida");
                    btnLimpar_Click(sender, e);
                }

                //caso passe pela primeira validação
                else
                {
                    //verifica se o campo cliente está em branco e o campo cpf preenchido
                    if (txtCliente.Text.ToUpper().Trim().Equals("") && !txtCpf.Text.ToUpper().Trim().Equals(""))
                    {
                        //verifica se existem clientes segundo os critérios de pesquisa
                        if (controle.PesquisaClienteCpf(txtCpf.Text.ToUpper().Trim()) != null)
                        {
                            //altera a visibilidade do form
                            txtCliente.Enabled = false;
                            txtCpf.Enabled = false;
                            lblMensagem.Text = "cliente com o CPF/CNPJ: " + txtCpf.Text.ToUpper().Trim();

                            //busca na base o cliente que atende o parametro de pesquisa e chama o método para apresentar este no gridviewlist
                            cliente = controle.PesquisaClienteCpf(txtCpf.Text.ToUpper().Trim());
                            preencheDados(cliente);
                        }

                        //caso não haja cliente segundo os critérios de pesquisa informa o usuário
                        else
                        {
                            MessageBox.Show("Não existem clientes com o CNPJ/CPF " + txtCpf.Text.ToUpper().Trim() + ", Por favor verifique e refaça sua pesquisa", "Pesquisa Inválida");
                            btnLimpar_Click(sender, e);
                        }
                    }

                    //verifica se o campo cliente esta preenchido e o campo cpf em branco
                    else if (!txtCliente.Text.ToUpper().Trim().Equals("") && txtCpf.Text.ToUpper().Trim().Equals(""))
                    {
                        //verifica se existem clientes segundo os critérios de pesquisa
                        if (controle.PesquisaClientesCompleta(txtCliente.Text.ToUpper().Trim()).Count > 0)
                        {
                            //altera a visibilidade do form
                            txtCliente.Enabled = false;
                            txtCpf.Enabled = false;
                            lblMensagem.Text = "Clientes que contém a expressão: " + txtCliente.Text.ToUpper().Trim() + " no nome";

                            //busca na base a relação dos clientes que atendem o parametro de pesquisa e chama o método para apresentar estes no gridviewlist
                            listaClientes = controle.PesquisaClientesCompleta(txtCliente.Text.ToUpper().Trim());
                            preencheRelacao(listaClientes);
                        }

                        //caso não haja cliente segundo os critérios de pesquisa informa o usuário
                        else
                        {
                            MessageBox.Show("Não existem clientes ativos com a expressão \"" + txtCliente.Text.ToUpper().Trim() + "\", Por favor verifique e refaça sua pesquisa", "Pesquisa Inválida");
                            btnLimpar_Click(sender, e);
                        }
                    }

                    //verifica se ambos campos estão preenchidos, informa ação inválida
                    else if (!txtCliente.Text.ToUpper().Trim().Equals("") && !txtCpf.Text.ToUpper().Trim().Equals(""))
                    {
                        MessageBox.Show("Para pesquisa de cliente preencha somente um campo de busca, ou CNPJ/CPF, ou Nome", "Ação Inválida");
                        btnLimpar_Click(sender, e);
                    }

                    //caso não se enquadre em nenhuma das condições anteriores ambos os campos devem estar vazios, informa ação inválida
                    else
                    {
                        MessageBox.Show("Para pesquisa de cliente preencha um campo de busca, ou CNPJ/CPF, ou Nome", "Ação Inválida");
                        btnLimpar_Click(sender, e);
                    }
                }
            }
            catch
            {
                //caso não seja possível a pesquisa atribui a localização a string erro, envia email ao desenvolvedor e exibe mensagem ao usuário
                erro = "Clientes.cs, linhas 111 a 192";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Clientes.cs, linhas 111 a 192, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //função do botão novo
        private void btnNovo_Click(object sender, EventArgs e)
        {
            //tenta pesquisa a lista de estados e alterar a visibilidade do form
            try
            {
                //chamada do método de limpeza do formulário
                limpaForm();
                
                //pesquisa da relação de estados cadastrados
                listaEstado = controle.PesquisaGeralEstados();

                //carrega o combobox estado com a lista 
                cmbUf.DataSource = listaEstado;
                cmbUf.ValueMember = "id";
                cmbUf.DisplayMember = "sigla";
                cmbUf.SelectedText = "";

                //altera a variavel local booleana para verdadeiro
                flagNovo = true;

                //altera a visibilidade do form
                btnNovo.Enabled = false;
                btnSalvar.Enabled = true;
                btnPesquisa.Enabled = false;
                pnlTelefone.Enabled = true;
                cmbUf.Enabled = true;
                cmbCidade.Enabled = true;
                rdbDinheiro.Enabled = true;
                rdbTef.Enabled = true;
                txtCreditos.Enabled = true;
                txtContato.Enabled = true;
                txtEmail.Enabled = true;
                txtBairro.Enabled = true;
                txtCpf.Enabled = true;
                txtEndereco.Enabled = true;
                txtCliente.Enabled = true;
                txtNum.Enabled = true;
                txtTel1.Enabled = true;
                txtTel2.Enabled = true;
                txtRG.Enabled = true;
                dgvClientes.Enabled = false;
            }
            catch
            {
                //caso não seja possível a pesquisa atribui a localização a string erro, envia email ao desenvolvedor e exibe mensagem ao usuário
                erro = "Clientes.cs, linhas 206 a 242";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Clientes.cs, linhas 206 a 242, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //função do botão alterar
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //altera o valor da variavel local booleana para falso
            flagNovo = false;

            //altera a visibilidade do form
            btnAlterar.Enabled = false;            
            btnSalvar.Enabled = true;
            txtCreditos.Enabled = true;
            rdbDinheiro.Enabled = true;
            rdbTef.Enabled = true;
            txtContato.Enabled = true;
            txtEmail.Enabled = true;
            pnlTelefone.Enabled = true;
            cmbUf.Enabled = true;
            cmbCidade.Enabled = true;
            txtBairro.Enabled = true;
            txtCpf.Enabled = true;
            txtEndereco.Enabled = true;
            txtCliente.Enabled = true;
            txtNum.Enabled = true;
            txtTel1.Enabled = true;
            txtTel2.Enabled = true;
            txtRG.Enabled = true;
            dgvClientes.Enabled = false;

            //preenche a variavel credito com o valor dos créditos do cliente cadastrado
            credito = Convert.ToDouble(cliente.creditos);
        }

        //função do botão salvar
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //tenta salvar cliente novo ou alteração em cliente existente
            try
            {
                //Condicionais com método para validação do preenchimento do form, em caso de não preenchimento informa o usuário
                if (!validaCampos())
                {
                    MessageBox.Show("Todos os campos devem ser preenchidos com valores válidos.");
                }

                //caso preenchimento válido
                else
                {
                    //se flag novo = false, novo elemento
                    if (flagNovo)
                    {
                        //verifica se os dados de cpf/cnpj são válidos
                        if (validaDoc.validaTipoCpfCnpj(txtCpf.Text.Trim()))
                        {
                            //altera a visibilidade do form
                            btnPesquisa.Enabled = true;
                            btnSalvar.Enabled = false;
                            dgvClientes.Enabled = true;
                            flagNovo = false;

                            //Verifica o preenchimento do campo créditos
                            if (txtCreditos.Text.Equals(""))
                            {
                                txtCreditos.Text = "0,00";
                            }

                            //instancia e salva novo cliente
                            cliente = new Model.Clientes();
                            controle.SalvarCliente(cliente);
                            cliente.cpf = txtCpf.Text.Trim();
                            cliente.nome = txtCliente.Text.ToUpper().Trim();
                            cliente.rg = txtRG.Text.ToUpper().Trim();
                            cliente.status = 1;
                            cliente.creditos = Convert.ToDouble(txtCreditos.Text.Trim());
                            cliente.contato = txtContato.Text.ToUpper().Trim();
                            cliente.email = txtEmail.Text.Trim();
                            cliente.endereço = txtEndereco.Text.ToUpper().Trim();
                            cliente.numeral = txtNum.Text.ToUpper().Trim();
                            cliente.bairro = txtBairro.Text.ToUpper().Trim();
                            cliente.id_Cidade = controle.PesquisaCidade(cmbCidade.Text).id;
                            cliente.telefone = txtTel1.Text;
                            cliente.recado = txtTel2.Text;
                            cliente.celular = txtCelular.Text;
                            if (rdbClaro.Checked)
                            {
                                cliente.operadora = "Claro";
                            }
                            else if (rdbTim.Checked)
                            {
                                cliente.operadora = "Tim";
                            }
                            else if (rdbOi.Checked)
                            {
                                cliente.operadora = "Oi";
                            }
                            else if (rdbVivo.Checked)
                            {
                                cliente.operadora = "Vivo";
                            }
                            controle.SalvaAtualiza();

                            //verifica se há a inserção de créditos no cadastramento
                            if (Convert.ToDouble(txtCreditos.Text) > 0)
                            {
                                //instancia e salva o movimento de inserção de créditos
                                Movimentos movimento = new Movimentos();
                                controle.SalvarMovimento(movimento);
                                movimento.data = DateTime.Now;
                                movimento.desc = "Adição de crédito (" + txtCliente.Text.ToUpper().Trim() + ")";
                                movimento.valor = Convert.ToDecimal(txtCreditos.Text);
                                if (rdbDinheiro.Checked)
                                {
                                    movimento.id_tipo = 29;
                                }
                                else
                                {
                                    movimento.id_tipo = 53;
                                }
                                controle.SalvaAtualiza();
                                valor = Convert.ToDecimal(txtCreditos.Text);
                                ImprimeRecibo(sender, e);
                            }

                            //limpa o form
                            btnLimpar_Click(sender, e);

                            //informa o usuário do cadastro bem sucedido
                            MessageBox.Show("Inclusão realizada com sucesso", "Ação bem sucedida");

                            //carrega a lista atualizada de clientes e atuliza o gridviewlist
                            listaClientes = controle.PesquisaClientesCompleta("");
                            preencheRelacao(listaClientes);
                        }
                    }

                    //alteração de elemento existente na base de dados
                    else if (!flagNovo)
                    {
                        //verifica se os dados de cpf/cnpj são válidos
                        if (validaDoc.validaTipoCpfCnpj(txtCpf.Text))
                        {
                            //altera a visibilidade do form
                            btnPesquisa.Enabled = true;
                            btnSalvar.Enabled = false;
                            dgvClientes.Enabled = true;

                            //Verifica o preenchimento do campo créditos
                            if (txtCreditos.Text.Equals(""))
                            {
                                txtCreditos.Text = "0,00";
                            }

                            //altera dados de instancia pré-existente
                            cliente.cpf = txtCpf.Text;
                            cliente.nome = txtCliente.Text;
                            cliente.rg = txtRG.Text;
                            cliente.status = 1;                            
                            cliente.creditos = Convert.ToInt32(txtCreditos.Text);
                            cliente.contato = txtContato.Text;
                            cliente.email = txtEmail.Text;
                            cliente.endereço = txtEndereco.Text;
                            cliente.numeral = txtNum.Text;
                            cliente.bairro = txtBairro.Text;
                            cliente.id_Cidade = controle.PesquisaCidade(cmbCidade.Text).id;
                            cliente.telefone = txtTel1.Text;
                            cliente.recado = txtTel2.Text;
                            cliente.celular = txtCelular.Text;
                            if (rdbClaro.Checked)
                            {
                                cliente.operadora = "Claro";
                            }
                            else if (rdbTim.Checked)
                            {
                                cliente.operadora = "Tim";
                            }
                            else if (rdbOi.Checked)
                            {
                                cliente.operadora = "Oi";
                            }
                            else if (rdbVivo.Checked)
                            {
                                cliente.operadora = "Vivo";
                            }

                            //verifica se há a inclusão de créditos no update
                            if (Convert.ToDouble(txtCreditos.Text) > credito)
                            {
                                Movimentos movimento = new Movimentos();
                                controle.SalvarMovimento(movimento);
                                movimento.data = DateTime.Now;
                                movimento.desc = "Adição de crédito (" + txtCliente.Text + ")";
                                movimento.valor = Convert.ToDecimal(txtCreditos.Text) - Convert.ToDecimal(credito);
                                if (rdbDinheiro.Checked)
                                {
                                    movimento.id_tipo = 29;
                                }
                                else
                                {
                                    movimento.id_tipo = 53;
                                }
                                controle.SalvaAtualiza();
                                valor = Convert.ToDecimal(txtCreditos.Text) - Convert.ToDecimal(credito);
                                ImprimeRecibo(sender, e);
                                limpaForm();
                                btnLimpar_Click(sender, e);
                                MessageBox.Show("Alteração realizada com sucesso", "Ação bem sucedida");
                            }

                            //verifica se as alterações não incluem modificação dos créditos
                            else if (Convert.ToDouble(txtCreditos.Text) == credito)
                            {
                                controle.SalvaAtualiza();
                                limpaForm();
                                btnLimpar_Click(sender, e);
                                MessageBox.Show("Alteração realizada com sucesso", "Ação bem sucedida");
                            }

                            //verifica se houve a tentiva inválida de redução dos créditos e informa ao usuário
                            else
                            {
                                MessageBox.Show("Não é permitida a redução dos direta dos créditos de clientes, corrija e tente novamente", "Ação Inválida");
                            }

                            //atualiza a lista de clientes com as alterações realizadas
                            listaClientes = controle.PesquisaClientesCompleta("");

                            //preenche o gridviewlist com essa lista
                            preencheRelacao(listaClientes);
                        }
                    }
                }
            }
            catch
            {
                //caso não seja possível a pesquisa atribui a localização a string erro, envia email ao desenvolvedor e exibe mensagem ao usuário
                erro = "Clientes.cs, linhas 287 a 462";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Clientes.cs, linhas 287 a 462, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validaCampos()
        {
            if (!txtCpf.Text.Equals("") && !txtCliente.Text.Equals("") && (!txtCelular.Text.Equals("") || !txtTel1.Text.Equals("") || !txtTel2.Text.Equals("")))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            //tenta alterar o status de variavel booleana, limpar os campos, alterar a visibilidade do form e preencher o gridviewlist com a relação completa dos clientes
            try
            {
                //altera a variavel booleana que indica um novo elemento
                flagNovo = true;

                //chama a função que limpa o formulário
                limpaForm();

                //pesquisa a relação completa de clientes
                listaClientes = controle.PesquisaClientesCompleta("");

                //preenche o gridviewlist com essa relação
                preencheRelacao(listaClientes);

                //altera o conteudo de elemento de texto do form
                lblMensagem.Text = "Relação de Clientes";
            }
            catch
            {
                //caso não seja possível a pesquisa atribui a localização a string erro, envia email ao desenvolvedor e exibe mensagem ao usuário
                erro = "Clientes.cs, linhas 489 a 503";
                email.EnviaEmail(erro);
                MessageBox.Show("Erro não identificado em Clientes.cs, linhas 489 a 503, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //chamada da função responsável por reiniciar a visualização 
        private void limpaForm()
        {
            //altera a visibilidade de elementos do form
            dgvClientes.Enabled = true;            
            btnSalvar.Enabled = false;
            btnNovo.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnPesquisa.Enabled = true;
            pnlTelefone.Enabled = false;
            cmbCidade.Enabled = false;
            cmbUf.Enabled = false;
            txtCreditos.Enabled = false;
            txtContato.Enabled = false;
            rdbDinheiro.Enabled = false;
            rdbTef.Enabled = false;
            txtBairro.Enabled = false;
            txtCpf.Enabled = true;
            txtEmail.Enabled = false;
            txtRG.Enabled = false;
            txtEndereco.Enabled = false;
            txtCliente.Enabled = true;
            txtNum.Enabled = false;
            rdbOi.Checked = false;
            rdbTim.Checked = false;
            rdbVivo.Checked = false;
            rdbClaro.Checked = false;
            pnlTelefone.Text = "";
            txtBairro.Text = "";
            txtCpf.Text = "";
            txtRG.Text = "";
            txtCreditos.Text = "";
            txtContato.Text = "";
            txtEmail.Text = "";
            txtEndereco.Text = "";
            txtCliente.Text = "";
            txtNum.Text = "";
            txtTel1.Text = "";
            txtTel2.Text = "";
            txtCelular.Text = "";
            cmbUf.Text = "";
            cmbCidade.Text = "";
        }

        private void cmbUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbCidade.Enabled = true;
                listaCidade = controle.PesquisaCidadesPorEstado(cmbUf.SelectedIndex + 1);
                cmbCidade.DisplayMember = "cidade";
                cmbCidade.ValueMember = "id";
                cmbCidade.DataSource = listaCidade;
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void preencheDados(Model.Clientes cliente)
        {
            try
            {
                txtCpf.Enabled = false;
                txtCliente.Enabled = false;
                if (cliente != null)
                {
                    btnNovo.Enabled = false;
                    btnPesquisa.Enabled = false;
                    btnAlterar.Enabled = true;
                    btnExcluir.Enabled = true;
                    txtCliente.Text = cliente.nome;
                    txtCpf.Text = cliente.cpf;
                    txtRG.Text = cliente.rg;
                    txtContato.Text = cliente.contato;
                    txtEmail.Text = cliente.email;
                    txtEndereco.Text = cliente.endereço;
                    txtNum.Text = cliente.numeral;
                    txtBairro.Text = cliente.bairro;
                    txtCelular.Text = cliente.celular;
                    txtTel1.Text = cliente.telefone;
                    txtTel2.Text = cliente.recado;
                    cmbUf.SelectedText = cliente.Cidades.Estados.sigla;
                    cmbCidade.SelectedText = cliente.Cidades.cidade;
                    txtCreditos.Text = (cliente.creditos).ToString();

                    if (cliente.operadora == null)
                    {
                        rdbOi.Checked = false;
                        rdbTim.Checked = false;
                        rdbVivo.Checked = false;
                        rdbClaro.Checked = false;
                    }
                    else if (cliente.operadora.Equals("Oi"))
                    {
                        rdbOi.Checked = true;
                    }
                    else if (cliente.operadora.Equals("Tim"))
                    {
                        rdbTim.Checked = true;
                    }
                    else if (cliente.operadora.Equals("Vivo"))
                    {
                        rdbVivo.Checked = true;
                    }
                    else if (cliente.operadora.Equals("Claro"))
                    {
                        rdbClaro.Checked = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvClientes_Click(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                cliente = controle.PesquisaClienteCpf(dgvClientes.Rows[e.RowIndex].Cells[1].Value.ToString());
                btnLimpar_Click(sender, e);
                btnPesquisa.Enabled = false;
                preencheDados(cliente);
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                cliente.status = 0;
                controle.SalvaAtualiza();
                btnLimpar_Click(sender, e);
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimeRecibo(object sender, EventArgs e)
        {
            Recibo recibo = new Recibo(cliente, user, valor);
            recibo.Show();
        }
    }
}
