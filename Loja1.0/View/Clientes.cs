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

namespace Loja1._0
{
    public partial class Clientes : Form
    {
        private Model.Usuarios user;
        Controle controle = new Controle();
        Valida validaDoc = new Valida();
        static public Model.Clientes cliente = new Model.Clientes();
        List<Estados> listaEstado = new List<Estados>();
        List<Cidades> listaCidade = new List<Cidades>();
        List<Model.Clientes> listaClientes = new List<Model.Clientes>();
        bool flagNovo = true;
        static double credito = 0.00;

        public Clientes(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();

            listaClientes = controle.pesquisaClientesCompleta("");
            preencheRelacao(listaClientes);

        }

        private void preencheRelacao(List<Model.Clientes> listaClientes)
        {
            DataTable dtClientes = new DataTable();
            dtClientes.Columns.Add("cliente", typeof(string));
            dtClientes.Columns.Add("CNPJ / CPF", typeof(string));
            dtClientes.Columns.Add("Contato", typeof(string));
            dtClientes.Columns.Add("Telefone 1", typeof(string));
            dtClientes.Columns.Add("Telefone 2", typeof(string));
            dtClientes.Columns.Add("Celular", typeof(string));

            for (int i = 0; i < listaClientes.Count; i++)
            {
                dtClientes.Rows.Add(listaClientes[i].nome, listaClientes[i].cpf, listaClientes[i].telefone, listaClientes[i].recado, listaClientes[i].celular);
            }
            dgvClientes.DataSource = dtClientes;

            dgvClientes.Columns[0].Width = 250;
            dgvClientes.Columns[1].Width = 150;
            dgvClientes.Columns[2].Width = 150;
            dgvClientes.Columns[3].Width = 90;
            dgvClientes.Columns[4].Width = 90;
            dgvClientes.Columns[5].Width = 95;

            dgvClientes.RowsDefaultCellStyle.SelectionBackColor = Color.White;
            dgvClientes.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (txtCliente.Text.Length < 3 && txtCpf.Text.Equals(""))
            {
                MessageBox.Show("É necessário ao menos 3 caracteres para realizar uma busca por nome", "Ação inválida");
                btnLimpar_Click(sender, e);
            }

            else if ((txtCpf.Text.Length < 11 || txtCpf.Text.Length > 14) && txtCliente.Text.Equals(""))
            {
                MessageBox.Show("É necessário 13 caracteres para realizar uma busca por CNPJ/CPF", "Ação inválida");
                btnLimpar_Click(sender, e);
            }
            else
            {
                if (txtCliente.Text.Equals("") && !txtCpf.Text.Equals(""))
                {
                    if (controle.pesquisaClienteCpf(txtCpf.Text) != null)
                    {
                        txtCliente.Enabled = false;
                        txtCpf.Enabled = false;
                        lblMensagem.Text = "cliente com o CNPJ: " + txtCpf.Text;
                        cliente = controle.pesquisaClienteCpf(txtCpf.Text);
                        preencheDados(cliente);
                    }
                    else
                    {
                        MessageBox.Show("Não existem clientes com o CNPJ/CPF " + txtCpf.Text + ", Por favor verifique e refaça sua pesquisa", "Pesquisa Inválida");
                        btnLimpar_Click(sender, e);
                    }
                }
                else if (!txtCliente.Text.Equals("") && txtCpf.Text.Equals(""))
                {
                    if (controle.pesquisaClientesCompleta(txtCliente.Text).Count > 0)
                    {
                        txtCliente.Enabled = false;
                        txtCpf.Enabled = false;
                        lblMensagem.Text = "Clientes que contém a expressão: " + txtCliente.Text + " no nome";
                        listaClientes = controle.pesquisaClientesCompleta(txtCliente.Text);
                        preencheRelacao(listaClientes);
                    }
                    else
                    {
                        MessageBox.Show("Não existem clientes ativos com a expressão \"" + txtCliente.Text + "\", Por favor verifique e refaça sua pesquisa", "Pesquisa Inválida");
                        btnLimpar_Click(sender, e);
                    }
                }
                else if (!txtCliente.Text.Equals("") && !txtCpf.Text.Equals(""))
                {
                    MessageBox.Show("Para pesquisa de cliente preencha somente um campo de busca, ou CNPJ/CPF, ou Nome", "Ação Inválida");
                    btnLimpar_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Para pesquisa de cliente preencha um campo de busca, ou CNPJ/CPF, ou Nome", "Ação Inválida");
                    btnLimpar_Click(sender, e);
                }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limpaForm();
            listaEstado = controle.pesquisaGeralEstados();
            cmbUf.DataSource = listaEstado;
            cmbUf.ValueMember = "id";
            cmbUf.DisplayMember = "sigla";

            flagNovo = true;

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

            dgvClientes.Enabled = false;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            btnAlterar.Enabled = false;
            flagNovo = false;
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
            dgvClientes.Enabled = false;
            credito = Convert.ToDouble(cliente.creditos);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //Condicionais para validação do preenchimento
            //campos preenchidos
            if (!validaCampos())
            {
                MessageBox.Show("Todos os campos devem ser preenchidos com valores válidos.");
            }
            else if (validaCampos())
            {
                //se flag novo = false, novo elemento
                if (flagNovo)
                {
                    if (validaDoc.validaTipoCpfCnpj(txtCpf.Text))
                    {
                        btnPesquisa.Enabled = true;
                        btnSalvar.Enabled = false;
                        dgvClientes.Enabled = true;
                        flagNovo = false;

                        Model.Clientes cliente = new Model.Clientes();
                        controle.salvarCliente(cliente);

                        cliente.cpf = txtCpf.Text;
                        cliente.nome = txtCliente.Text;
                        cliente.status = 1;
                        cliente.creditos = Convert.ToDouble(txtCreditos.Text);
                        cliente.contato = txtContato.Text;
                        cliente.email = txtEmail.Text;
                        cliente.endereço = txtEndereco.Text;
                        cliente.numeral = txtNum.Text;
                        cliente.bairro = txtBairro.Text;
                        cliente.id_Cidade = controle.pesquisaCidade(cmbCidade.Text).id;
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
                        controle.salvaAtualiza();

                        if (Convert.ToDouble(txtCreditos.Text) > 0)
                        {
                            Movimentos movimento = new Movimentos();
                            controle.salvarMovimento(movimento);
                            movimento.data = DateTime.Now;
                            movimento.desc = "Adição de crédito (" + txtCliente.Text + ")";
                            movimento.valor = Convert.ToDecimal(txtCreditos.Text);                            
                            if (rdbDinheiro.Checked)
                            {
                                movimento.id_tipo = 29;
                            }
                            else
                            {
                                movimento.id_tipo = 53;
                            }                            
                            controle.salvaAtualiza();
                        }                        
                        btnLimpar_Click(sender, e);
                        MessageBox.Show("Inclusão realizada com sucesso", "Ação bem sucedida");
                        listaClientes = controle.pesquisaClientesCompleta("");
                        preencheRelacao(listaClientes);
                    }
                }

                //alteração de elemento existente na base de dados
                else if (!flagNovo)
                {
                    if (validaDoc.validaTipoCpfCnpj(txtCpf.Text))
                    {
                        btnPesquisa.Enabled = true;
                        btnSalvar.Enabled = false;
                        dgvClientes.Enabled = true;

                        cliente.cpf = txtCpf.Text;
                        cliente.nome = txtCliente.Text;
                        cliente.status = 1;
                        cliente.creditos = Convert.ToInt32(txtCreditos.Text);
                        cliente.contato = txtContato.Text;
                        cliente.email = txtEmail.Text;
                        cliente.endereço = txtEndereco.Text;
                        cliente.numeral = txtNum.Text;
                        cliente.bairro = txtBairro.Text;
                        cliente.id_Cidade = controle.pesquisaCidade(cmbCidade.Text).id;
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

                        if (Convert.ToDouble(txtCreditos.Text) > credito)
                        {
                            Movimentos movimento = new Movimentos();
                            controle.salvarMovimento(movimento);
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
                            controle.salvaAtualiza();
                            limpaForm();
                            btnLimpar_Click(sender, e);
                            MessageBox.Show("Alteração realizada com sucesso", "Ação bem sucedida");
                        }
                        else if (Convert.ToDouble(txtCreditos.Text) == credito)
                        {
                            controle.salvaAtualiza();
                            limpaForm();
                            btnLimpar_Click(sender, e);
                            MessageBox.Show("Alteração realizada com sucesso", "Ação bem sucedida");
                        }
                        else
                        {
                            MessageBox.Show("Não é permitida a redução dos direta dos créditos de clientes", "Ação Inválida");
                        }
                                            
                        listaClientes = controle.pesquisaClientesCompleta("");
                        preencheRelacao(listaClientes);
                    }
                }
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
            flagNovo = true;
            limpaForm();
            listaClientes = controle.pesquisaClientesCompleta("");
            preencheRelacao(listaClientes);
            lblMensagem.Text = "Relação de Clientes";
        }

        private void limpaForm()
        {
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

            cmbCidade.Enabled = true;
            listaCidade = controle.pesquisaCidadesPorEstado(cmbUf.SelectedIndex + 1);
            cmbCidade.DataSource = listaCidade;
            cmbCidade.DisplayMember = "cidade";
            cmbCidade.ValueMember = "id";

        }

        private void preencheDados(Model.Clientes cliente)
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

        private void dgvClientes_Click(object sender, DataGridViewCellMouseEventArgs e)
        {
            cliente = controle.pesquisaClienteCpf(dgvClientes.Rows[e.RowIndex].Cells[1].Value.ToString());
            btnLimpar_Click(sender, e);
            btnPesquisa.Enabled = false;
            preencheDados(cliente);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            cliente.status = 0;
            controle.salvaAtualiza();
            btnLimpar_Click(sender, e);
        }
    }
}
