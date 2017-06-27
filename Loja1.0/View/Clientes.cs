﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
            try
            {
                this.user = user;
                InitializeComponent();

                listaClientes = controle.PesquisaClientesCompleta("");
                preencheRelacao(listaClientes);
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void preencheRelacao(List<Model.Clientes> listaClientes)
        {
            try
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
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCliente.Text.Trim().Length < 3 && txtCpf.Text.Trim().Equals(""))
                {
                    MessageBox.Show("É necessário ao menos 3 caracteres para realizar uma busca por nome", "Ação inválida");
                    btnLimpar_Click(sender, e);
                }

                else if ((txtCpf.Text.Trim().Length < 11 || txtCpf.Text.Trim().Length > 14) && txtCliente.Text.Trim().Equals(""))
                {
                    MessageBox.Show("É necessário 13 caracteres para realizar uma busca por CNPJ/CPF", "Ação inválida");
                    btnLimpar_Click(sender, e);
                }
                else
                {
                    if (txtCliente.Text.ToUpper().Trim().Equals("") && !txtCpf.Text.ToUpper().Trim().Equals(""))
                    {
                        if (controle.PesquisaClienteCpf(txtCpf.Text.ToUpper().Trim()) != null)
                        {
                            txtCliente.Enabled = false;
                            txtCpf.Enabled = false;
                            lblMensagem.Text = "cliente com o CPF/CNPJ: " + txtCpf.Text.ToUpper().Trim();
                            cliente = controle.PesquisaClienteCpf(txtCpf.Text.ToUpper().Trim());
                            preencheDados(cliente);
                        }
                        else
                        {
                            MessageBox.Show("Não existem clientes com o CNPJ/CPF " + txtCpf.Text.ToUpper().Trim() + ", Por favor verifique e refaça sua pesquisa", "Pesquisa Inválida");
                            btnLimpar_Click(sender, e);
                        }
                    }
                    else if (!txtCliente.Text.ToUpper().Trim().Equals("") && txtCpf.Text.ToUpper().Trim().Equals(""))
                    {
                        if (controle.PesquisaClientesCompleta(txtCliente.Text.ToUpper().Trim()).Count > 0)
                        {
                            txtCliente.Enabled = false;
                            txtCpf.Enabled = false;
                            lblMensagem.Text = "Clientes que contém a expressão: " + txtCliente.Text.ToUpper().Trim() + " no nome";
                            listaClientes = controle.PesquisaClientesCompleta(txtCliente.Text.ToUpper().Trim());
                            preencheRelacao(listaClientes);
                        }
                        else
                        {
                            MessageBox.Show("Não existem clientes ativos com a expressão \"" + txtCliente.Text.ToUpper().Trim() + "\", Por favor verifique e refaça sua pesquisa", "Pesquisa Inválida");
                            btnLimpar_Click(sender, e);
                        }
                    }
                    else if (!txtCliente.Text.ToUpper().Trim().Equals("") && !txtCpf.Text.ToUpper().Trim().Equals(""))
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
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                limpaForm();
                listaEstado = controle.PesquisaGeralEstados();
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
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            try
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
                        if (validaDoc.validaTipoCpfCnpj(txtCpf.Text.Trim()))
                        {
                            btnPesquisa.Enabled = true;
                            btnSalvar.Enabled = false;
                            dgvClientes.Enabled = true;
                            flagNovo = false;

                            Model.Clientes cliente = new Model.Clientes();
                            controle.SalvarCliente(cliente);

                            cliente.cpf = txtCpf.Text.Trim();
                            cliente.nome = txtCliente.Text.ToUpper().Trim();
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

                            if (Convert.ToDouble(txtCreditos.Text) > 0)
                            {
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
                            }
                            btnLimpar_Click(sender, e);
                            MessageBox.Show("Inclusão realizada com sucesso", "Ação bem sucedida");
                            listaClientes = controle.PesquisaClientesCompleta("");
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
                                limpaForm();
                                btnLimpar_Click(sender, e);
                                MessageBox.Show("Alteração realizada com sucesso", "Ação bem sucedida");
                            }
                            else if (Convert.ToDouble(txtCreditos.Text) == credito)
                            {
                                controle.SalvaAtualiza();
                                limpaForm();
                                btnLimpar_Click(sender, e);
                                MessageBox.Show("Alteração realizada com sucesso", "Ação bem sucedida");
                            }
                            else
                            {
                                MessageBox.Show("Não é permitida a redução dos direta dos créditos de clientes", "Ação Inválida");
                            }

                            listaClientes = controle.PesquisaClientesCompleta("");
                            preencheRelacao(listaClientes);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            listaClientes = controle.PesquisaClientesCompleta("");
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
            try
            {
                cmbCidade.Enabled = true;
                listaCidade = controle.PesquisaCidadesPorEstado(cmbUf.SelectedIndex + 1);
                cmbCidade.DataSource = listaCidade;
                cmbCidade.DisplayMember = "cidade";
                cmbCidade.ValueMember = "id";
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
    }
}
